
using Data.DTO.WareHouse;
using Data.Repositories;
using Entites.Entities;
using Entites.Entities.WareHouse;
using Entities.Entities.WareHouse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.WareHouse
{
    public interface ITypeSizeItemRepository : IRepository<TypeSizeItem>
    {
        Task<List<SelectListItem>> GetbyProductId(int id);
    }
}