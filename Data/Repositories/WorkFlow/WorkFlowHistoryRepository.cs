using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entites.Entities;
using Data.Contracts.Common;
using AutoMapper;
using Data.DTO.Common;
using AutoMapper.QueryableExtensions;
using Entites.Entities.Request;
using Data.Contracts.WorkFlow;

namespace Data.Repositories.WorkFlow
{
    public class WorkFlowHistoryRepository : Repository<Entites.Entities.WorkFlow.WorkFlowHistory>, IWorkFlowHistoryRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public WorkFlowHistoryRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<List<Entites.Entities.WorkFlow.WorkFlowHistory>> GetByEntityIdAndEntityType(int entityId, string entityType)
        {
            return await TableNoTracking.Where(c => c.EntityId == entityId && c.EntityType == entityType).ToListAsync();

        }



    }
}
