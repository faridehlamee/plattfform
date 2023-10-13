using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class EmailDTO : BaseDto<EmailDTO, Email, int>
    {

        public EmailDTO()
        {
            IsCheck = false;
        }

        public string Fullname { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public string Subject { get; set; }
        public string message { get; set; }
        public string Replay { get; set; }
        public bool IsCheck { get; set; }

    }
}
