
using Entites.Entities.User;
using System.Threading.Tasks;

namespace Services
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(User user);
    }
}