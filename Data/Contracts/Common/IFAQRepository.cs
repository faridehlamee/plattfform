using Data.DTO.Common;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Common
{
    public interface IFAQRepository : IRepository<FAQ>
    {
        Task<List<FAQDTO>> GetAll();



    }
}