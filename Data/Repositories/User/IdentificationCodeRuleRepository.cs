using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts.User;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.User;
using Entites.Entities.User;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.User
{
    public class IdentificationCodeRuleRepository : Repository<IdentificationCodeRule>, IIdentificationCodeRuleRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public IdentificationCodeRuleRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<Pagedata<IdentificationCodeRuleDTO>> GetList(SearchDTO model, IdentificationCodeRuleDTO filter)
        {
            var data = new Pagedata<IdentificationCodeRuleDTO>();
            var query = TableNoTracking
               .Where(c => c.IsActive);
            //query = Filter(query, filter);

            data.Resualt = await query.ProjectTo<IdentificationCodeRuleDTO>(_mapper.ConfigurationProvider).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();
            data.TotalPages = await query.CountAsync();

            return data;
        }

        public async Task<ResultDTO<IdentificationCodeRule>> GetRule()
        {
            var res = new ResultDTO<IdentificationCodeRule>();
            var IdentificationCodeRule =await TableNoTracking.Where(c => c.IsActive && c.FromDate <= DateTime.Now && c.ToDate >= DateTime.Now).FirstOrDefaultAsync();
            if (IdentificationCodeRule != null)
            {
                res.Data = IdentificationCodeRule;
                res.Status = true;
            }
            else
            {
                res.Status = false;
            }
            return res;
        }
         
    
    }
}
