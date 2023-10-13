﻿using Data.DTO.Common;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Common
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<CommentDTO>> GetListByProductId(int ProductId, CancellationToken cancellationToken);



    }
}