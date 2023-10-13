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
    public interface IWorkFlowRepository : IRepository<Entites.Entities.WorkFlow.WorkFlow>
    {

    }
}