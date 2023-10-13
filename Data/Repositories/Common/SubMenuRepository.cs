using Common;
using Data.Contracts.Common;
using Entites.Entities.Menu;
using Microsoft.AspNetCore.Http;

namespace Data.Repositories.Public
{
    public class SubMenuRepository : Repository<SubMenu>, ISubMenuRepository, IScopedDependency
    {
        public SubMenuRepository(KiatechDbContext dbContext, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
        }
     

    }
}
