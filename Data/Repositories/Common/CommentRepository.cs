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
using Data.DTO.Common;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Data.Repositories.Public
{
    public class CommentRepository : Repository<Comment>, ICommentRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public CommentRepository(IMapper mapper , KiatechDbContext dbContext, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _mapper = mapper;
        }


        public async Task<List<CommentDTO>> GetListByProductId(int ProductId , CancellationToken cancellationToken)
        {
            var data = await TableNoTracking.Where(c => c.ProductId == ProductId && c.IsShow)
                .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return data;
        }




    }
}
