
using Data.DTO;
using Entites.Entities.User;
using System.Threading.Tasks;

namespace Services.SMS
{
    public interface ISmsService
    {
        int SendSMS(string phonenumber, SmsDTO data);
    }
}