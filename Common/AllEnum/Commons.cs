 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AllEnum
{
    public class Commons
    { 
        public enum Gender
        {
            [Display(Name = "مرد")]
            Man = 1,
            [Display(Name = "زن")]
            Woman = 2,

        }

        public enum ExamStatus
        {
            [Display(Name = "انجانم نشده")]
            notDone = 1,
            [Display(Name = "درحال آزمون")]
            InProgress = 2,
            [Display(Name = "قبول")]
            Accept = 3,
            [Display(Name = "مردود")]
            Rejection = 4,
        }
        public enum CourseLevel
        {
            [Display(Name = "مبتدی")]
            Basic = 1,
            [Display(Name = "متوسط")]
            Middle = 2,
            [Display(Name = "پیشرفته")]
            Advance = 3
        }
        public enum DegreeOfEducation
        {
            [Display(Name = "زیر دیپلم")]
            UnderDiploma = 1,
            [Display(Name = "دیپلم")]
            Diploma = 2,
            [Display(Name = "فوق دیپلم")]
            Associate = 3,
            [Display(Name = "لیسانس")]
            Bachelor = 4,
            [Display(Name = "فوق لیسانس")]
            Master = 5,
            [Display(Name = "دکترا")]
            Doctoral = 6,
        }
        public enum FactorType
        {
           
            sale =1,
            ret = 2
        }

        public enum UserState
        {
            [Display(Name = "مرحه یک")]
            stepOne = 1,
            [Display(Name = "مرحه دو")]
            StepTwo = 2,
            [Display(Name = "مرحله 3")]
            StepThree = 3,

        }
        public enum PaymentType
        {
            [Description("آنلاین")]
            [Display(Name = "آنلاین")]
            olinePay = 1,
            [Description("حضوری")]
            [Display(Name = "حضوری")]
            payINcash = 2,
            [Description("کیف پول")]
            [Display(Name = "کیف پول")]
            wallet = 3,
        }



        public enum ApiResultStatusCode
        {
            [Display(Name = "عملیات با موفقیت انجام شد")]
            Success = 0,

            [Display(Name = "خطایی در سرور رخ داده است")]
            ServerError = 1,

            [Display(Name = "پارامتر های ارسالی معتبر نیستن")]
            BadRequest = 2,

            [Display(Name = "یافت نشد")]
            NotFound = 3,


            [Display(Name = "لیست خالی است")]
            ListEmpty = 4,

            [Display(Name = "خطایی در پردازش رخ داد")]
            LogicError = 5,

            [Display(Name = "خطای احراز حویت")]
            UnAuthorized = 6

        }
        public enum SaleState
        {
            [Display(Name = "در حال پردازش")]
            Pending = 1,
            [Display(Name = "ثبت درخواست")]
            Accepted = 2,
            [Display(Name = "آماده ارسال")]
            Pocket = 3,
            [Display(Name = " ارسال شده")]
            Posted = 4,
            [Display(Name = "درخواست مرجوعی")]
            ReturnedRequest = 5,
            [Display(Name = "تایید مرجوعی")]
            ReturnedConfirmation = 6,
            [Display(Name = "رد مرجوعی")]
            ReturnedRejected = 7,
        }
        public enum RequestState
        {
            [Description("در حال بررسی")]
            Pending = 1,
            [Description("ثبت درخواست")]
            Confirm = 2,
            [Description("رد شده")]
            Reject = 3
        }

        public enum RequestType
        {
            [Description("آنلاین")]
            [Display(Name = "آنلاین")]
            Online = 1,
            [Description("حضوری")]
            [Display(Name = "حضوری")]
            FaceToFace = 2
        }

        public enum ProductReferenceReason
        {
            [Display(Name = "رنگ نامناسب")]
            [Description("رنگ نامناسب")]
            Color = 1,
            [Display(Name = "سایز نامناسب")]
            [Description("سایز نامناسب")]
            Size = 2,
            [Display(Name = "ارسال اشتباه")]
            [Description("ارسال اشتباه")]
            Send = 3,
            [Display(Name = "زده دار بودن محصول")]
            [Description("زده دار بودن محصول")]
            damage = 4

        }


        public enum TypeOffPrice
        {
            [Description("درصدی")]
            [Display(Name = "درصدی")]
            Percent = 1,
            [Description("مبلغی")]
            [Display(Name = "مبلغی")]
            amount = 2
        }
        public enum DiscountType
        {
            [Description("روی محصول")]
            [Display(Name = "روی محصول")]
            onProduct = 1,
            [Description("کد تخفیف")]
            [Display(Name = "کد تخفیف")]
            discount = 2
        }
        public enum InfoSite
        {
            [Description("Aboutus")]
            [Display(Name = "Aboutus")]
            Aboutus = 1,
            [Description("Mobile")]
            [Display(Name = "Mobile")]
            Mobile = 2,
            [Description("Summary")]
            [Display(Name = "Summary")]
            Summary = 3,
            [Description("Email")]
            [Display(Name = "Email")]
            Email = 4
        }
    }
}
