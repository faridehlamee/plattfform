using AutoMapper;
using Common;
using Data.Contracts.Request;
using Data.Contracts.WorkFlow;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Request;
using Entites.Entities.Request;
using Entites.Entities.WorkFlow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.Request
{

    public class CounselingRequestService : ICounselingRequestService, IScopedDependency
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWorkFlowRepository _workFlowRepository;
        private readonly ICounselingRequestRepository   _counselingRequestRepository;
        private readonly IMapper _mapper;
        private readonly IWorkFlowHistoryRepository _workFlowHistoryRepository;

        public CounselingRequestService(
            IHttpContextAccessor contextAccessor,
            IWorkFlowRepository workFlowRepository,
            IWorkFlowHistoryRepository workFlowHistoryRepository,
            ICounselingRequestRepository  counselingRequestRepository,

            IMapper _mapper
            )
        {
            this._contextAccessor = contextAccessor;
            this._workFlowRepository = workFlowRepository;;
            this._counselingRequestRepository = counselingRequestRepository;
            this._mapper = _mapper;
            this._workFlowHistoryRepository = workFlowHistoryRepository;
        }

        public async Task<ResultDTO> CreateCounselingRequest(CounselingRequestDTO requestHeader, CancellationToken cancellationToken)
        {
            var result = new ResultDTO();

            var Request = requestHeader.ToEntity(_mapper);
            Request.Index = 1;
            Request.Title = "Consultation request form";
            Request.Description = $"Service = {requestHeader.Service } {System.Environment.NewLine} Budget = {requestHeader.Budget } {System.Environment.NewLine} {requestHeader.Description}";

            await _counselingRequestRepository.AddAsync(Request, cancellationToken);


            var workflow = await _workFlowRepository.TableNoTracking
                .Where(C => C.EntityType == "CounselingRequest" && C.IsActive)
                .OrderBy(c => c.Index).Select(
                x => new WorkFlowHistory
                {
                    ApproveId = 1,
                    EntityId = Request.Id,
                    EntityType = x.EntityType,
                    Index = x.Index,
                    Status = x.Status

                }).ToListAsync();

            await _workFlowHistoryRepository.AddRangeAsync(workflow, cancellationToken);
            result.Status = true;
            result.Messages = "Success!";
            return result;
            //}
            //result.Status = false;
            //result.Messages = "این دوره قبلا ثبت شده";
            //return result;
        }
  


        public async Task<Pagedata<CounselingRequestDTO>> GetPaging(SearchDTO model, CounselingRequestDTO Search)
        {
            var data = new Pagedata<CounselingRequestDTO>();

            var query = _workFlowHistoryRepository
                .TableNoTracking.Include(x => x.Approve)
                .Where(c => c.IsActive && c.EntityType == Search.EntityType)
                .Join(_counselingRequestRepository.TableNoTracking, we => we.EntityId, re => re.Id, (we, re) => new CounselingRequestDTO
                {

                    Id = re.Id,
                    Title = re.Title,
                    FirstName=re.FirstName,
                    LastName=re.LastName,
                    Mobile=re.Mobile,
                    EntityType=we.EntityType,
                    Index = re.Index,
                    WoIndex = we.Index,
                    IsConfirm = we.IsConfirm,
                    Statuse = we.Status,
                    DateInsert = re.DateInsert,
                    File = re.File,
                    

                }).Where(c => c.Index == c.WoIndex);

            query = await Filter(query, Search);

            data.Resualt = await query.Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();

            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total / model.take);
            data.TotalProduct = (int)total;
            return data;
        }

        public async Task<IQueryable<CounselingRequestDTO>> Filter(IQueryable<CounselingRequestDTO> query, CounselingRequestDTO Search)
        {
           
            if (Search.Index != 0)
            {
                query = query.Where(c => c.Index == Search.Index);
            }
            return query;
        }

        public async Task<CounselingRequestDTO> GetById(int Id, string entityType)
        {
            var data = await _counselingRequestRepository.GetById(Id);
            data.workFlowHistories = await _workFlowHistoryRepository.GetByEntityIdAndEntityType(Id, entityType);
            return data;
        }

        public async Task UpdateStatuse(int id, int status, string message, string entityType)
        {
            var request = await _counselingRequestRepository.Table.Where(c => c.Id == id).FirstOrDefaultAsync();

            var listworkFlow = await _workFlowHistoryRepository.Table.Where(c => c.EntityId == request.Id && c.EntityType == entityType).OrderBy(x => x.Index).ToListAsync();
            var currentFlow = listworkFlow.Where(c => c.Index == request.Index).FirstOrDefault();
            if (status == 1)
            {
                currentFlow.IsConfirm = true;
                currentFlow.Comment = message;
                await _workFlowHistoryRepository.UpdateAsync(currentFlow, CancellationToken.None);

                var nextIndex = listworkFlow.Where((c) => c.Index > request.Index).OrderBy(i => i.Index).Select(y => y.Index).FirstOrDefault();

                request.Index = nextIndex;
                await _counselingRequestRepository.UpdateAsync(request, CancellationToken.None);
            }
            else
            {
                listworkFlow.ForEach(c => c.IsConfirm = true);
                await _workFlowHistoryRepository.UpdateRangeAsync(listworkFlow, CancellationToken.None);

                var rejectWorkFlow = listworkFlow.Where(c => c.Index == -1).FirstOrDefault();
                rejectWorkFlow.Index = -1;
                rejectWorkFlow.ApproveId = currentFlow.ApproveId;
                rejectWorkFlow.Comment = message;
                rejectWorkFlow.CreatorId = currentFlow.CreatorId;
                rejectWorkFlow.EntityId = currentFlow.EntityId;
                rejectWorkFlow.EntityType = currentFlow.EntityType;
                rejectWorkFlow.IsConfirm = true;



                await _workFlowHistoryRepository.UpdateAsync(rejectWorkFlow, CancellationToken.None);
                //await _workFlowHistoryRepository.AddAsync(rejectWorkFlow, CancellationToken.None);
                request.Index = -1;
                await _counselingRequestRepository.UpdateAsync(request, CancellationToken.None);
            }
        }

 


    }
}
