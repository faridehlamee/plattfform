using Data.DTO.Common;
using Data.DTO.Menu;
using Data.Repositories;
using Entites.Entities;
using Entites.Entities.Menu;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Common
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<List<MenuDTO>> GetAllActive();
        Task ReloadData();

    }
}