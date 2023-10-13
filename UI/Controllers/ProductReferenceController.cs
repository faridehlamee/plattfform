using Common;
using Data.Contracts.Order;
using Data.DTO.ProductReference;
using Data.DTO.Sales;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ProductReferenceController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IToastNotification _toastNotification;

        public ProductReferenceController(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IToastNotification toastNotification)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index(int Id)
        {
            var userId = HttpContext.User.Identity.GetUserId<int>();
            var data = await _orderRepository.GetDetail(Id);
            if (data == null)
            {
                _toastNotification.AddErrorToastMessage("فاکتور موجود نمی باشد");
                return RedirectToAction("Index", "User");
            }

            if (data.UserId != userId)
            {
                _toastNotification.AddErrorToastMessage("فاکتور موجود نمی باشد");
                return RedirectToAction("Index", "User");
            }
                
            //if ()
            //    return Ok("مدت مرجوع کالا به پایان رسیده است");

            data.listOrderDetail = await _orderDetailRepository.GetDetailbyOrderId(data.Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductReference(OrderDTO model, CancellationToken cancellationToken)
        {
            var data = new ProductReferenceDTO() { 
            AddressId = model.AddressId.Value,
            Memo = model.Memo,
            OrderId= model.Id
            };
            foreach (var item in model.listOrderDetail.Where(c=> c.IsSelected))
            {
                if (item.Value>0 && item.ReferenceReason!=0)
                {
                    var referenceItem = new ProductReferenceItemDTO()
                    {
                        OrderDetailId = item.Id,
                        Reason = item.ReferenceReason,
                        Value = item.Value
                    };
                    data.ListProductReferenceItem.Add(referenceItem);
                }
          
            }
            if (data.ListProductReferenceItem.Count == model.listOrderDetail.Where(c => c.IsSelected).Count() && data.ListProductReferenceItem.Count>0)
            {
                var Reference = await _orderRepository.CreateProductReference(data, cancellationToken);
                var FinalPayment = await _orderDetailRepository.CreateProductReferenceItem(data.ListProductReferenceItem, Reference.Id, cancellationToken);
                Reference.FinalPayment = FinalPayment;
                await _orderRepository.UpdateAsync(Reference, cancellationToken);

                return RedirectToAction("Index", "User");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("محصولی انتخاب نشده است");
                return RedirectToAction("Index", "ProductReference",new { Id = model.Id });
            }
        
        }
    }
}
