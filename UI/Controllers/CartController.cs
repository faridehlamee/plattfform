using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts.Cart;
using Data.DTO.Cart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UI.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _cartRepository.GetCart();
            if (data.Count() == 0)
                return RedirectToAction("CartEmpty", "Cart");
            return View();
        }
        public  IActionResult CartEmpty()
        {
            return View();
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<JsonResult> GetCart()
        {
            var data = await _cartRepository.GetCart();
            var total = data.Sum(c => c.TotalAmount);
            var cartTotal = data.Sum(c => c.ProductAmount*c.Value);
            var count = data.Count();
            return Json(new { data , total , cartTotal, count });
           
        }

        [HttpPost]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> AddToCart(string data)
        {
            bool resval;
            InsertCartDTO formData = JsonConvert.DeserializeObject<InsertCartDTO>(data);
            var key = _cartRepository.getKey();
            formData.key = key;
            var res = await _cartRepository.AddToCart(formData);
            if (res== "باموفقیت ثبت شد")
            {
                 resval = true;
            }
            else
            {
                 resval = false;
            }
            return Json(new { res , resval });
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _cartRepository.GetByIdAsync(CancellationToken.None, id);
            await _cartRepository.DeleteIsActiveAsync(data, CancellationToken.None);
            return Json(true);
        }



    }
}
