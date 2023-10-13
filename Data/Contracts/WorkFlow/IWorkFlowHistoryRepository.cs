using Data.DTO.Common;
using Data.Repositories;
using Entites.Entities;
using Entites.Entities.Menu;
using Entites.Entities.Request;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.WorkFlow
{
    public interface IWorkFlowHistoryRepository : IRepository<Entites.Entities.WorkFlow.WorkFlowHistory>
    {
        Task<List<Entites.Entities.WorkFlow.WorkFlowHistory>> GetByEntityIdAndEntityType(int entityId, string entityType);
    }
}