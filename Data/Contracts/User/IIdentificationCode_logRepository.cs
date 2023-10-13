using Data.DTO.BaseDTO;
using Entites.Entities.User;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.User
{
    public interface IIdentificationCode_logRepository : IRepository<IdentificationCode_log>
    {
        Task<ResultDTO<int>> CheckUserUsedInRule(int ruleId, int userId);
        Task Create(int ruleId, int userId, CancellationToken cancellationToken);
    }
}