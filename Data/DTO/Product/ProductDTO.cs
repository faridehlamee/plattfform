using Data.DTO.BaseDTO;
using Data.DTO.BaseProduct;
using Data.DTO.Common;
using Data.DTO.Discount;
using Data.DTO.WareHouse;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.DTO.Product
{

    public class BaseProductDTO : BaseDto<ProductDTO, Entites.Entities.Product.Product, int>
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
        public double Amount { get; set; }
        [Display(Name = "مقدار تخفیف")]
        public double DisCountAmount { get {
                try
                {
                    return Discount.Where(c => c.IsActive && c.ExpireDate>DateTime.Now ).Select(c=> c.Value.Value).FirstOrDefault();
                }
                catch (Exception)
                {

                    return 0;
                }
             
            
            } }
        public TypeOffPrice DisCountType
        {
            get
            {
                try
                {
                    return Discount.Where(c => c.IsActive && c.ExpireDate > DateTime.Now ).Select(c => c.TypeOffPrice.Value).FirstOrDefault();
                }
                catch (Exception)
                {

                    return 0;
                }


            }
        }
        [Display(Name = " قیمت نهایی")]
        public double FinalAmount
        {
            get
            {
                try
                {
                    if (DisCountAmount != 0)
                    {
                        if (DisCountType == TypeOffPrice.Percent)
                        {
                            return Amount - (int)(Amount * (DisCountAmount / 100));
                        }
                        else
                        {
                            return Amount - DisCountAmount;
                        }
                    }
                    else
                    {
                        return Amount;
                    }

                }
                catch (Exception)
                {
                    return Amount;
                }

            }
        }
        //end
        public string CurrentImage { get; set; }
        public bool IsShow { get; set; }
        public ICollection<DiscountDTO> Discount { get; set; }
        public int? DiscountId { get; set; }
        public ICollection<ProductDetailDTO> details { get; set; }
        public ICollection<WareHouse.ProductWareHouseDTO> ProductWareHouses { get; set; }
        public int SumWareHouses { get; set; }
    }
    public class ProductDTO : BaseProductDTO
    {
        public ProductDTO()
        {
            ListImage = new List<ImageDTO>();
            listComment = new List<CommentDTO>();
        }
       
        public string Description { get; set; }
        public string Tags { get; set; }
        public int? GuideId { get; set; }
        public string GuideImageFile { get; set; }
        public List<SelectListItem> GuideList { get; set; }
        public virtual GuideDTO Guide { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public List<SelectListItem> ProductTypeList { get; set; }
        public int StoreTypeId { get; set; }
        public string StoreTypeStoreName { get; set; }
        //لیست برای ثبت ادمین
        public List<ImageDTO> ListImage { get; set; }
        //لیست برای نمایش ادمین
        [Display(Name = "تصاویر")]
        public List<ImageDTO> Gallery { get; set; }
        public List<string> ProductGallery { get; set; }
        //for show
        public List<ProductDetailDTO> ListParameter { get; set; }
        public List<WareHouse.ProductWareHouseDTO> ListWareHouse { get; set; }
        public List<int?> listNotExist { get; set; }

        public bool HasEmptySize { get; set; }
        public List<CommentDTO> listComment { get; set; }

        public int? TypeSizeId { get; set; }
        public List<SelectListItem> TypeSizeList { get; set; }
        // public  TypeSizeDTO TypeSize { get; set; }
        public int OfferId { get; set; }

         
        public List<BaseProductDTO> ListSameProduct { get; set; }

    }

    public class SearchProduct
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public int Group { get; set; }
    }
}
