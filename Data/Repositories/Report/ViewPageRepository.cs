using AutoMapper;
using Common;
using Data.Contracts.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories.Report
{
    public class ViewPageRepository : Repository<Entites.Entities.ViewPage>, IViewPageRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public ViewPageRepository(RoyalCanyonDBContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        public async Task Add(CancellationToken cancellationToken)
        {
            var ip = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var isOkey = await TableNoTracking.Where(c => c.DateInsert.Date == DateTime.Now.Date && c.SystemIp == ip).AnyAsync();
            if (!isOkey)
            {
                var data = new Entites.Entities.ViewPage()
                {
                    SystemIp = ip,
                    Date=DateTime.Now
                    
                };
                await AddAsync(data, cancellationToken);
            }
        }

      
    }
}
