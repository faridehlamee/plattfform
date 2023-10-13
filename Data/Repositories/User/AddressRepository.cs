using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts.BaseProduct;
using Data.Contracts.User;
using Data.DTO.Address;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Data.Repositories.User
{
    public class AddressRepository : Repository<Address>, IAddressRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public AddressRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
            this._contextAccessor = contextAccessor;
        }
         
        public async Task<List<AddressDTO>> GetUserAddressList(int userId ,CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(c => c.IsActive && c.UserId == userId).ProjectTo<AddressDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AddressDTO> GetAddressbyId(int Id , int userId, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(c => c.Id== Id && c.UserId == userId).ProjectTo<AddressDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
        public async Task<bool> CreateAndUpdateAddress(AddressDTO address, CancellationToken cancellationToken)
        {
          
            if (address.ProvinceId == 0)
                return false;
            if (address.CityId == 0)
                return false;
            if (address.AddressDesc == null || address.AddressDesc.Trim() == "")
                return false;
            var data = address.ToEntity(_mapper);
            data.UserId = _contextAccessor.HttpContext.User.Identity.GetUserId<int>();
            if (address.Id != 0)
            {
                await UpdateAsync(data, cancellationToken);
            }
            else
            {
                await AddAsync(data, cancellationToken);
            }

          
            return true;
        }
    }
}
