using Data.DTO.Common;
using Data.DTO.Request;
using Data.Repositories;
using Entites.Entities;
using Entites.Entities.Menu;
using Entites.Entities.Request;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Report
{
    public interface IViewPageRepository : IRepository<ViewPage>
    {
        Task Add(CancellationToken cancellationToken);


    }
}