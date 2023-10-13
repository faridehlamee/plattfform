using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.Contracts.Common;
using Data.DTO.Common;
using Data.DTO.Product;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Services.Email;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.SiteSetting
{
    public class EmailController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmailRepository _emailRepository;
        private readonly IEmailService _emailService;

        public EmailController(IMapper mapper, IEmailRepository emailRepository, IEmailService emailService)
        {
            this._mapper = mapper;
            this._emailRepository = emailRepository;
            this._emailService = emailService;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(SearchDTO model, EmailDTO Search, CancellationToken cancellationToken)
        {

            var dto = await _emailRepository.GetPaging(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }

        public async Task<IActionResult> Replay(int? id)
        {
            var data = new EmailDTO();

            if (id != null)
            {
                data = await _emailRepository.TableNoTracking.Where(c => c.Id == id).ProjectTo<EmailDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            }
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmailDTO model)
        {
            //var data = model.ToEntity(_mapper);
            await _emailService.SendEmail(model, CancellationToken.None);
            return RedirectToAction("Index", "Email");

        }

    }
}
