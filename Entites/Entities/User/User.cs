using Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Entites.Entities.User
{
    [Table("User", Schema = "ACC")]
    public class User : IdentityUser<int>,IEntity<int>
    {
        public User()
        {
            IsActive = true;
            RegisterDate = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string PlaceService { get; set; }
        public DegreeOfEducation? DegreeOfEducation { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DateTimeOffset LastLoginDate { get; set; }
        public string Profile { get; set; }
        public bool IsConfirmed { get; set; }
        public UserState State { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDate { get; set; }
        public long CreatorId { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string ForgetCode { get; set; }
        public DateTime SendForgetCodeDate { get; set; }
        public string IdentificationCode { get; set; }
        public string Representative { get; set; }
    }
}
