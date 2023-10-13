using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entites.Entities;
using Data.Contracts.Common;
using AutoMapper;
using Data.DTO.Common;
using AutoMapper.QueryableExtensions;
using Entites.Entities.Request;
using Data.Contracts.Request;
using Data.DTO.Request;

namespace Data.Repositories.Request
{
    public class CooperationRequestRepository : Repository<CooperationRequest>, ICooperationRequestRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public CooperationRequestRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }


        public async Task<CooperationRequestDTO> GetById(int id)
        {
            return await Table.Where(c => c.Id == id).ProjectTo<CooperationRequestDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

    }
}
