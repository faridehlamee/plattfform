using Data.DTO.Address;
using Entites.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.User
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<List<AddressDTO>> GetUserAddressList(int userId , CancellationToken cancellationToken);
        Task<AddressDTO> GetAddressbyId(int Id, int userId, CancellationToken cancellationToken);
        Task<bool> CreateAndUpdateAddress(AddressDTO address, CancellationToken cancellationToken);
    }
}