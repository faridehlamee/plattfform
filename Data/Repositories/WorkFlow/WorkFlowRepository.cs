using Common;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Data.Contracts.WorkFlow;

namespace Data.Repositories.WorkFlow
{
    public class WorkFlowRepository : Repository<Entites.Entities.WorkFlow.WorkFlow>, IWorkFlowRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public WorkFlowRepository(RoyalCanyonDBContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }
      



    }
}
