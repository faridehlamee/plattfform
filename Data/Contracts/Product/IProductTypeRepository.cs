using Data.DTO.BaseProduct;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Data.Contracts.Product
{
    public interface IProductTypeRepository : IRepository<Entites.Entities.ProductType>
    {
        Task<List<SelectListItem>> GetByStoreTypeId(int id);
    }
}
