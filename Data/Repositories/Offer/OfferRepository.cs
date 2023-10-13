using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts.Offer;
using Data.DTO.Offer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Offer
{
    public class OfferRepository : Repository<Entites.Entities.Offer.Offer>, IOfferRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public OfferRepository(IMapper Mapper , KiatechDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }

        public async Task<List<OfferDTO>> GetAllAsync(OfferDTO offerDTO)
        {
            var data = TableNoTracking.ProjectTo<OfferDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive).OrderByDescending(x => x.DateInsert);

            return await data.ToListAsync(); 
        }

        public async Task<List<OfferDTO>> GetByZoneAsync(int zoneId)
        {
            var data = TableNoTracking.ProjectTo<OfferDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive && c.OfferZoneId== zoneId).OrderByDescending(x => x.DateInsert);

            return await data.ToListAsync();
        }

    }
}
