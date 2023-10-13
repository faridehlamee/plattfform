using Data.DTO.BaseProduct;
using Data.DTO.Offer;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Offer
{
    public interface IOfferRepository : IRepository<Entites.Entities.Offer.Offer>
    {
        Task<List<OfferDTO>> GetAllAsync(OfferDTO offerDTO);
        Task<List<OfferDTO>> GetByZoneAsync(int zoneId);
    }
}