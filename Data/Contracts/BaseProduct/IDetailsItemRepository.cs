using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.BaseProduct
{
    public interface IDetailsItemRepository : IRepository<DetailsItem>
    {

        List<SelectListItem> GetbyDetaiId(int detailid);


    }
}