using Data.Repositories;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Product
{
    public interface IGuideRepository : IRepository<Guide>
    {
        Task DeleteIsActive(int Id, CancellationToken cancellationToken);
    }
}
