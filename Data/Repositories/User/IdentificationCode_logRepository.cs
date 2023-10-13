using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts.User;
using Data.DTO.BaseDTO;
using Entites.Entities.User;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories.User
{
    public class IdentificationCode_logRepository : Repository<IdentificationCode_log>, IIdentificationCode_logRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public IdentificationCode_logRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }

        public async Task<ResultDTO<int>> CheckUserUsedInRule(int ruleId , int userId)
        {
            var res = new ResultDTO<int>();
            var IdentificationCodeRule = await TableNoTracking.Where(c => c.IsActive && c.IdentificationCodeRuleId== ruleId && c.UserId == userId).CountAsync();
            if (IdentificationCodeRule == 0)
            {
                res.Data = IdentificationCodeRule;
                res.Status = true;
            }
            else
            {
                res.Data = IdentificationCodeRule;
                res.Status = false;
            }
            return res;
        }

        public async Task Create (int ruleId, int userId ,CancellationToken cancellationToken)
        {
            var data = new IdentificationCode_log() {IdentificationCodeRuleId= ruleId  , UserId= userId };
            await AddAsync(data, cancellationToken);
        }
    }
}
