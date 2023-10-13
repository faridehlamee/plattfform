using AutoMapper;
using Common;
using Data.Contracts;
using Data.Contracts.Blog;
using Data.Contracts.Common;
using Data.DTO.Blog;
using Data.DTO.Common;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UIIndex
{
    public class IndexService : IIndexService, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly ISliderRepository _sliderRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Pricing> _priceRepository;
        private readonly IRepository<Portfolio> _portfolioRepository;

        public IndexService(IMapper Mapper,
            ISliderRepository sliderRepository,
             IBlogRepository blogRepository,
             IRepository<Service> serviceRepository,
             IRepository<Team> teamRepository,
             IRepository<Pricing> priceRepository,
             IRepository<Portfolio> portfolioRepository


            )
        {


            _mapper = Mapper;
            _sliderRepository = sliderRepository;
            this._blogRepository = blogRepository;
            this._serviceRepository = serviceRepository;
            this._teamRepository = teamRepository;
            this._priceRepository = priceRepository;
            this._portfolioRepository = portfolioRepository;
        }
        public async Task<IndexViewModel> GetData()
        {
            var data = new IndexViewModel();
            data.Sliders = await _sliderRepository.GetAllAsync();

            data.Services = await _serviceRepository.TableNoTracking.Where(c => c.IsActive).Select(c => new ServiceDTO
            {

                Id = c.Id,
                Name = c.Name,
                Icone = c.Icone,
                Decription = c.Decription
            }).ToListAsync();




            //data.Pricings = await _priceRepository.TableNoTracking.Where(c => c.IsActive).Select(c => new PricingDTO
            //{

            //    Id = c.Id,
            //    Name = c.Name,
            //    Unit = c.Unit,
            //    Price = c.Price,
            //    Options = c.Options
            //}).ToListAsync();

            //data.Portfolios = await _portfolioRepository.TableNoTracking.Where(c => c.IsActive).Select(c => new PortfolioDTO
            //{

            //    Id = c.Id,
            //    Name = c.Name,
            //    Url = c.Url,
            //    Category = c.Category,
            //    CompletionTime = c.CompletionTime,
            //    ProjectDate = c.ProjectDate,

            //}).Take(4).ToListAsync();

            data.LastBlog = await _blogRepository.TableNoTracking.Where(c => c.IsActive && c.BlogCategoryId == 2).Select(c => new BlogDTO
            {
                Id = c.Id,
                Title = c.Title,
                Image = c.Image,
                DateInsert = c.DateInsert,
                BlogCategoryId=c.BlogCategoryId


            }).OrderByDescending(c => c.DateInsert).Take(5).ToListAsync();


            data.LastArticles = await _blogRepository.TableNoTracking.Where(c => c.IsActive && c.BlogCategoryId == 1).Select(c => new BlogDTO
            {
                Id = c.Id,
                Title = c.Title,
                Image = c.Image,
                DateInsert = c.DateInsert,
                BlogCategoryId = c.BlogCategoryId

            }).OrderByDescending(c => c.DateInsert).Take(5).ToListAsync();

            return data;


        }



    }
}
