using Common;
using Data.Contracts.Product;
using Entites.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories.Product
{
    public class GuideRepository : Repository<Guide>, IGuideRepository, IScopedDependency
    {
        public GuideRepository(KiatechDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
        }

        public async Task DeleteIsActive(int Id, CancellationToken cancellationToken)
        {
            var data = await GetByIdAsync(cancellationToken, Id);
            data.IsActive = false;
            await UpdateAsync(data, cancellationToken);
        }
    }
}
