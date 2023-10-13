using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.User;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.Contracts.User
{
    public interface IUserRepository : IRepository<Entites.Entities.User.User>
    {
        Task<Entites.Entities.User.User> GetByUserAndPass(string email, string password, CancellationToken CancellationToken);
        Task AddAsync(Entites.Entities.User.User User, string password, CancellationToken CancellationToken);
        Task UpdateSecurityStampAsync(Entites.Entities.User.User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(Entites.Entities.User.User user, CancellationToken cancellationToken);
        Task<bool> CheckUniqePhoneNumber(string phoneNumber);
        Task<Entites.Entities.User.User> CheckUserBeforLogin(string phoneNumber);
        Task<bool> UpdatePassWord(UserDTO user, CancellationToken cancellationToken);
        Task<bool> UpdateAccountForPayment(UserDTO model, CancellationToken cancellationToken);
        Task<Pagedata<UserDTO>> GetListUser(SearchDTO model, UserDTO Search);
        Task<string> SendForgetCode(string PhoneNumber, CancellationToken cancellationToken);
        Task<ResultDTO> CheckForgetCode(string PhoneNumber, string Code);
        ResultDTO<TOTPDTO> CheckTOTPResend();
        ResultDTO<TOTPDTO> CheckTOTPVerify();
        ResultDTO<TOTPDTO> CheckValidity(string phoneNumber);
        void SaveTOTPCache(string value);
        Task<UserDTO> GetByUserName(string phoneNumber);
        Task<int> GetOwnerIdByRepresentativeCode(string RepresentativeCode);
        Task<ResultDTO<string>> UdateRepresentative(int userId, string representativeCode, CancellationToken cancellationToken);

        Task<string> GenerateIdentifactionCode();

    }
}