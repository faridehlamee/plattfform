using Data.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.Email
{
    public interface IEmailService
    {
        Task Create(EmailDTO email, CancellationToken cancellationToken);
        Task SendEmail(EmailDTO email, CancellationToken cancellationToken);
    }
}
