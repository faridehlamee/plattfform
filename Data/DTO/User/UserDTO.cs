using Common.Utilities;
using Data.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.DTO.User
{
    public class UserDTO : BaseDto<UserDTO, Entites.Entities.User.User, int>
    {
        public UserDTO()
        {
            address = new Address.AddressDTO();


        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public string PlaceService { get; set; }
        public DegreeOfEducation? DegreeOfEducation { get; set; }
        public string DegreeOfEducationDesc
        {
            get
            {
                try
                {
                    return DegreeOfEducation.ToDisplay();
                }
                catch (Exception)
                {

                    return "No Value";
                }
            }
        }
        public DateTime? BirthDate { get; set; }
        public string PersianBirthDate { get {
                if (BirthDate ==null)
                {
                    return "";
                }
                else
                {
                    return BirthDate.Value.GetPrsianDate();
                }
                
            } set {
                BirthDate = value.GetGregorianDate(); 
           
            } }
         

        public string GenderDesc
        {
            get
            {
                try
                {
                    return Gender.ToDisplay();
                }
                catch (Exception)
                {

                   return "No value";
                }
            }
        }
        public DateTimeOffset LastLoginDate { get; set; }
        public string Profile { get; set; }
        public bool IsConfirmed { get; set; }
        public UserState State { get; set; }
        public string Email { get; set; }
        public string ForgetCode { get; set; }
        public DateTime SendForgetCodeDate { get; set; }
        public string IdentificationCode { get; set; }
        public string Representative { get; set; }
        public int DiscountId { get; set; }
        public double WalletBalnce { get; set; }
        public Address.AddressDTO  address { get; set; }
    }


    public class LoginDTO
    {
        [Required]
        [Display(Name = "User Name")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        public bool RememberMe { get; set; }
        public string url { get; set; }


    }
    public class TOTPDTO
    {
        public string PhoneNumber { get; set; }
        public string UserHashKey { get; set; }
        public byte[] SecretKey { get; set; }
        public DateTime ExpirtionTime { get; set; }
        public string Url { get; set; }
        public bool Status { get; set; }
        public string Messages { get; set; }

    }

    public class FirstRegisterDTO : BaseDto<FirstRegisterDTO, Entites.Entities.User.User, int>
    {
        public FirstRegisterDTO()
        {
            State = UserState.stepOne;
        }

        [Required(ErrorMessage = "Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Mobile")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Enter password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Repeat Password")]
        [Display(Name = "Repeat Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(PassWord),ErrorMessage ="Repeat password is not correct")]
        public string RePassWord { get; set; }
        public UserState State { get; set; }

    }
}
