using AutoMapper;
using Common;
using Data.Contracts;
using Data.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.Email
{
    public class EmailService : IEmailService, IScopedDependency
    {
        private readonly IRepository<Entites.Entities.Email> _emailRepository;
        private readonly IMapper _mapper;

        public EmailService(IRepository<Entites.Entities.Email> emailRepository, IMapper mapper)
        {
            this._emailRepository = emailRepository;
            this._mapper = mapper;
        }

        public async Task Create(EmailDTO email, CancellationToken cancellationToken)
        {
            var Email = email.ToEntity(_mapper);
            await _emailRepository.AddAsync(Email, cancellationToken);
        }

        public async Task SendEmail(EmailDTO email, CancellationToken cancellationToken)
        {
      
            //// Use credentials of the Mail account that you created with the steps above.
            const string FROM = "info@kiatechsoftware.com";
            const string FROM_PWD = "FHp3003976";
            const bool USE_HTML = true;

            // Get the mail server obtained in the steps described above.
            const string SMTP_SERVER = "relay-hosting.secureserver.net";
            try
            {
                MailMessage mailMsg = new MailMessage(FROM, email.EmailAddress);
                mailMsg.Subject = email.Subject;
                mailMsg.Body = email.Replay;
                mailMsg.IsBodyHtml = USE_HTML;

                SmtpClient smtp = new SmtpClient();
             //   smtp.Port = 25;
                 smtp.Port = 26;
                smtp.Host = SMTP_SERVER;
                smtp.Credentials = new System.Net.NetworkCredential(FROM, FROM_PWD);
                smtp.Send(mailMsg);

            }
            catch (System.Exception ex)
            {

            } 
        }
    }
}
