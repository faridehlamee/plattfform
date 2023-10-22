using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.AllEnum;
using Common.Exceptions;
using Common.Utilities;
using Data.Cashe;
using Data.Contracts.User;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.User;
using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Common.AllEnum.Commons;

namespace Data.Repositories.User
{
    public class UserRepository : Repository<Entites.Entities.User.User>, IUserRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(RoyalCanyonDBContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
            _contextAccessor = contextAccessor;
        }


        public async Task<Pagedata<UserDTO>> GetListUser(SearchDTO model , UserDTO Search)
        {
            var data = new Pagedata<UserDTO>();
            var query = TableNoTracking
               .Where(c => c.IsActive && (int)c.State>1);
            query =  Filter( query, Search);
            data.Resualt =await query.Select(c=> new UserDTO
            {
                Id=c.Id,
                FirstName=c.FirstName,
                LastName=c.LastName,
                DateInsert=c.DateInsert,
                PhoneNumber=c.PhoneNumber

            })
                .OrderByDescending(c=> c.DateInsert)
                .Skip(model.take * (model.page - 1))
                .Take(model.take).ToListAsync();
            data.TotalPages = await query.CountAsync();

            return data;
        }

        public  IQueryable<Entites.Entities.User.User> Filter(IQueryable<Entites.Entities.User.User> query, UserDTO Search)
        {
            if (Search.FirstName != null)
            {
                query = query.Where(c => c.FirstName .Contains( Search.FirstName));
            }
            if (Search.LastName != null)
            {
                query = query.Where(c => c.LastName.Contains( Search.LastName));
            }
            if (Search.PhoneNumber != null)
            {
                query = query.Where(c => c.PhoneNumber .Contains( Search.PhoneNumber));
            }
            if (Search.Email != null)
            {
                query = query.Where(c => c.Email == Search.Email);
            }

            return query;
        }


        public Task<Entites.Entities.User.User> GetByUserAndPass(string email, string password, CancellationToken CancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return Table.Where(p => p.Email == email && p.PasswordHash == passwordHash).SingleOrDefaultAsync(CancellationToken);

        }

        public async Task AddAsync(Entites.Entities.User.User User, string password, CancellationToken CancellationToken)
        {
            var exist = await TableNoTracking.AnyAsync(c => c.Email == User.Email);
            if (exist)
            {
                throw new BadRequestException("نام کاربری تکراری است");
            }

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            User.PasswordHash = passwordHash;
            await base.AddAsync(User, CancellationToken);
        }
        public Task UpdateSecurityStampAsync(Entites.Entities.User.User user, CancellationToken cancellationToken)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            return UpdateAsync(user, cancellationToken);
        }

        //public override void Update(User entity, bool saveNow = true)
        //{
        //    entity.SecurityStamp = Guid.NewGuid();
        //    base.Update(entity, saveNow);
        //}

        public Task UpdateLastLoginDateAsync(Entites.Entities.User.User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
        public async Task<bool> CheckUniqePhoneNumber(string phoneNumber)
        {
            return await TableNoTracking.AnyAsync(c => c.PhoneNumber == phoneNumber && c.UserName == phoneNumber  && c.IsActive && (int)c.State >= 2);
        }


        public async Task<Entites.Entities.User.User> CheckUserBeforLogin(string phoneNumber)
        {
            
                var data = await TableNoTracking.Where(c => c.UserName == phoneNumber && c.PhoneNumber == phoneNumber && c.State == Commons.UserState.stepOne).FirstOrDefaultAsync();
                if (data == null)
                {
                    var newUser = new Entites.Entities.User.User()
                    {
                        PhoneNumber = phoneNumber,
                        UserName = phoneNumber,
                        State=UserState.stepOne
                    };
                    await AddAsync(newUser, CancellationToken.None);
                    return newUser;
                }
                else
                {
                    return data;
                }
      


        }

        public async Task<bool> UpdatePassWord(UserDTO user ,  CancellationToken cancellationToken)
        {
            try
            {
                var data = await TableNoTracking.Where(c => c.Id == user.Id).SingleOrDefaultAsync();
                var pass = user.PassWord;

                data.PasswordHash = Common.Utilities.SecurityHelper.GetSha256Hash(pass);
                 await UpdateAsync(data, cancellationToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public async Task<bool> UpdateAccountForPayment(UserDTO model, CancellationToken cancellationToken)
        {
            try
            {
                var userData = await Table.Where(c => c.Id == model.Id).SingleOrDefaultAsync();
                if (model.FirstName != null && model.FirstName!="")
                {
                    userData.FirstName = model.FirstName;
                }
                if (model.LastName != null && model.LastName != "")
                {
                    userData.LastName = model.LastName;
                }
                if (model.FatherName != null && model.FatherName != "")
                {
                    userData.FatherName = model.FatherName;
                }
                if (model.NationalCode != null && model.NationalCode != "")
                {
                    userData.NationalCode = model.NationalCode;
                }
                if (model.PlaceService != null && model.PlaceService != "")
                {
                    userData.PlaceService = model.PlaceService;
                }
                if (model.PersianBirthDate != null )
                {
                    model.BirthDate = model.PersianBirthDate.GetGregorianDate();
                    userData.BirthDate = model.BirthDate;
                }
                if (model.Email != null && model.Email != "")
                {
                    userData.Email = model.Email;
                }
                if (model.Gender != 0)
                {
                    userData.Gender = model.Gender;
                }
                if (model.DegreeOfEducation != 0)
                {
                    userData.DegreeOfEducation = model.DegreeOfEducation;
                }

                await UpdateAsync(userData, cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> SendForgetCode(string PhoneNumber, CancellationToken cancellationToken)
        {
            try
            {
                var user = await Table.Where(c => c.UserName == PhoneNumber).SingleOrDefaultAsync();
                var code = GetRandomCode();
                user.ForgetCode = code;
                user.SendForgetCodeDate = DateTime.Now;
                await UpdateAsync(user, cancellationToken);
                return code;
            }
            catch (Exception)
            {
                return ""; 
            }
        }
        public async Task<ResultDTO> CheckForgetCode(string PhoneNumber , string Code)
        {
            var user = await Table.Where(c => c.UserName == PhoneNumber).SingleOrDefaultAsync();
            if (user.SendForgetCodeDate.AddMinutes(1) < DateTime.Now)
                return new ResultDTO() { Status=false , Messages="اعتبار کد به پایان رسیده است" };
            if (user.ForgetCode != Code)
                return new ResultDTO() { Status = false, Messages = "کد وارد شده صحیح نمی باشد" };

            return new ResultDTO() { Status = true, Messages = "" };

        }
        public string GetRandomCode()
        {
            Random rnd = new Random();
            var num = rnd.Next(1000000, 9999999);
            return num.ToString();
        }
        

        public void SaveTOTPCache(string value)
        {
            if (value.Trim() !="" || value != null)
            {
                _contextAccessor.HttpContext.Session.SetString(CacheKeys.Entry, value);
            }

        }
        public ResultDTO<TOTPDTO> CheckTOTPResend()
        {
            var res = new ResultDTO<TOTPDTO>();
            var cashData = _contextAccessor.HttpContext.Session.GetString(CacheKeys.Entry);
            if (cashData == "" || cashData == null)
            {
                res.Status = true;
                return res;
            }
            var hascash = JsonSerializer.Deserialize<TOTPDTO>(cashData);
            if (hascash.ExpirtionTime > DateTime.Now)
            {
                var timdiff = (int)(hascash.ExpirtionTime - DateTime.Now).TotalSeconds;
                res.Status = false;
                res.Messages = $"برای ارسال مجدد کد لطفا {timdiff}صبر کنید";
                return res;
            }

            res.Status = true;
            res.Data = hascash;
            return res;
        }


        public ResultDTO<TOTPDTO> CheckTOTPVerify()
        {
            var res = new ResultDTO<TOTPDTO>();
            var cashData = _contextAccessor.HttpContext.Session.GetString(CacheKeys.Entry);
            if (cashData == "" || cashData == null)
            {

                res.Messages = "خطا در دریافت کد";
                res.Status = false;
                return res;
            }
            var hascash = JsonSerializer.Deserialize<TOTPDTO>(cashData);
            if (hascash.ExpirtionTime < DateTime.Now)
            {
                var timdiff = (int)(hascash.ExpirtionTime - DateTime.Now).TotalSeconds;
                res.Status = false;
                res.Messages = $"کد وارد شده معتبر نمی باشد";
                return res;
            }

            res.Status = true;
            res.Data = hascash;
            return res;
        }


        public ResultDTO<TOTPDTO> CheckValidity(string phoneNumber)
        {
            var res = new ResultDTO<TOTPDTO>();
            var cashData = _contextAccessor.HttpContext.Session.GetString(CacheKeys.Entry);
            if (cashData == "" || cashData == null)
            {

                res.Messages = "خطا مقدار وجود ندارد";
                res.Status = false;
                return res;
            }
            var hascash = JsonSerializer.Deserialize<TOTPDTO>(cashData);

            var UserHashKey = SecurityHelper.GetSha256Hash(phoneNumber);
            if (UserHashKey != hascash.UserHashKey)
            {
                res.Messages = "خطا در صحت کاربری";
                res.Status = false;
                return res;
            }

            res.Status = true;
            return res;
        }

        public async Task<UserDTO> GetByUserName(string phoneNumber)
        {
            try
            {
                var data = await TableNoTracking.Where(c => c.UserName == phoneNumber).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
                return data;
            }
            catch (Exception)
            {

                return new UserDTO();
            }

        }

        public async Task<int> GetOwnerIdByRepresentativeCode(string RepresentativeCode)
        {
            return await TableNoTracking.Where(c => c.IdentificationCode.Equals(RepresentativeCode)).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<ResultDTO<string>> UdateRepresentative(int userId, string representativeCode, CancellationToken cancellationToken)
        {
            var res = new ResultDTO<string>();
            var user = await Table.Where(c => c.Id == userId).SingleOrDefaultAsync();
            if (user.Representative!=null)
            {
                res.Status = true;
                res.Data = user.Representative;
            }
            else
            {
                var IsValidRepresentative = await TableNoTracking.Where(c => c.Id != userId && c.IdentificationCode == representativeCode).AnyAsync();
                if (IsValidRepresentative)
                {
                    user.Representative = representativeCode;
                    await UpdateAsync(user, cancellationToken);
                    res.Status = true;
                    res.Data = representativeCode;
                }
                else
                {
                    res.Status = false;
                }
                
            }

            return res;

        }


        public async Task<string> GenerateIdentifactionCode()
        {
            var checkuniqe = true;
            string IdentifactionCode = "";
            do
            {
                IdentifactionCode = "";
                string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
                Random rand = new Random();
               
                for (int i = 0; i < 12; i++)
                {
                    int num = rand.Next(0, chars.Length);
                    IdentifactionCode = IdentifactionCode + chars[num];
                }

                 checkuniqe =await TableNoTracking.Where(c => c.IdentificationCode == IdentifactionCode).AnyAsync();


            } while (checkuniqe==true);
           
            return IdentifactionCode;

        }

    }
}
