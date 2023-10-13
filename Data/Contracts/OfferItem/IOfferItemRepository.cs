using Data.DTO.BaseProduct;
using Data.DTO.Offer;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.OfferItem
{
    public interface IOfferItemRepository : IRepository<Entites.Entities.OfferItem>
    {

        Task<List<int?>> GetUsedProductId(int offerId);
        Task<List<OfferItemDTO>> GetByOfferId(int Id);
        Task<Entites.Entities.OfferItem> GetByProductAndOfferId(int ProductId, int offerId);
        Task<bool> IsExist(int offerid, int productid);


    
    }
}