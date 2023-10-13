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
    public class CounselingRequestRepository : Repository<CounselingRequest>, ICounselingRequestRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public CounselingRequestRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }


        public async Task<CounselingRequestDTO> GetById(int id)
        {
            return await Table.Where(c => c.Id == id).ProjectTo<CounselingRequestDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

    }
}
