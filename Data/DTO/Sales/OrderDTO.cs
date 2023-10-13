using Common.Utilities;
using Data.DTO.Address;
using Data.DTO.BaseDTO;
using Data.DTO.Offer;
using Data.DTO.User;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.DTO.Sales
{
    public class OrderDTO : BaseDto<OrderDTO, Order, int>
    {
        public string FacPart { get; set; }
        public FactorType FactorType { get; set; }
        public string RefId { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhoneNumber { get; set; }
        //public virtual UserDTO User { get; set; }
        public int? AddressId { get; set; }
        public virtual AddressDTO Address { get; set; }
        public List<SelectListItem> AddressList { get; set; }
        public PaymentType? PaymentType { get; set; }
        public bool IsFinaly { get; set; }


        public double TotalDiscount { get; set; }
        public double TotalExtraAmount { get; set; }
        public double TotalPayment { get; set; }
        public double FinalPayment { get; set; }


        public string Memo { get; set; }
        public int Index { get; set; }
        public int Status { get; set; }
        public string StatusDes { get; set; }

        public SaleState State { get; set; }
        public string StateDesc => State.ToDisplay(); 
        public bool? IsProductReference { get; set; }
        public float? newPrice { get; set; }
        public int? ParentId { get; set; }

        public int? OfferItemId { get; set; }
        //public virtual OfferItemDTO OfferItem { get; set; }
        public List<OrderDetailDTO> listOrderDetail { get; set; }

        public string PersianDate => DateInsert.Value.GetPrsianDate();

        public string DayName
        {
            get
            {
                try
                {
                    return DateInsert.Value.GetDayOfWeekName();
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }
        public string Hour
        {
            get
            {
                try
                {
                    return DateInsert.Value.ToString("HH:mm");
                }
                catch (Exception)
                {

                    return "";
                }
            }
        }


    }

    public class BillDTO
    {
        public BillDTO()
        {
            IsNewAddress = false;
        }
        public int Id { get; set; }
        public string Memo { get; set; }
        public string DisCountCode { get; set; }
        public double DisCountValue { get; set; }
        public PaymentType? PaymentType { get; set; }
        public int? AddressId { get; set; }
        public bool IsNewAddress { get; set; }
        public AddressDTO Address { get; set; }
        public string UserRepresentative { get; set; }


    }

}
