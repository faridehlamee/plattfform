using Common;
using Data.Contracts;
using Data.Contracts.Common;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentrepository;

        public CommentController(ICommentRepository commentrepository)
        {
            _commentrepository = commentrepository;
        }
       
        public async Task<ActionResult> AddComment(string commentText, int productId , CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.Identity.GetUserId<int>();
            if (userId==0)
            {
                return Json(false);
            }
            var data = new Comment()
                {
                    ProductId=productId,
                    CommentText= commentText,
                    IsShow=false,
                    UserId= userId
            };

           await _commentrepository.AddAsync(data, cancellationToken);
           return Json(true);
        }

    }
}
