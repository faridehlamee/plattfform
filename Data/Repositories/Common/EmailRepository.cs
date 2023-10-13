using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Data.DTO.Common;
using System.Threading; 
using Data.DTO.Product;
using Data.DTO.BaseDTO;
using System;
using static Common.AllEnum.Commons;
using Data.Contracts.Common;
using Entites.Entities;

namespace Data.Repositories.Public
{
    public class EmailRepository : Repository<Email>, IEmailRepository, IScopedDependency
    {
        private readonly IMapper _mapper; 

        public EmailRepository(KiatechDbContext dbContext,
            IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _mapper = Mapper; 
        }


        public async Task<Pagedata<EmailDTO>> GetPaging(SearchDTO model, EmailDTO Search)
        {
            var data = new Pagedata<EmailDTO>();
            var query = TableNoTracking.Where(c => c.IsActive);
            //query = Filter(query, EmailDTO);
            data.Resualt = await query.Select(c => new EmailDTO
            {
                Id = c.Id,
                Fullname= c.Fullname,
                EmailAddress =c.EmailAddress,
                Subject=c.Subject,
                DateInsert = c.DateInsert
            }).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();
            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total / model.take); //
            data.TotalProduct = (int)total;
            return data;
        }

        //public IQueryable<Entites.Entities.Exams.Examer> Filter(IQueryable<Email> query, EmailDTO Search)
        //{

        //    if (Search.UserId != 0)
        //    {
        //        query = query.Where(c => c.UserId == Search.UserId);
        //    }
        //    if (Search.IsDone != null)
        //    {
        //        query = query.Where(c => c.IsDone == Search.IsDone);
        //    }
        //    return query;
        //}

 

    }
}
