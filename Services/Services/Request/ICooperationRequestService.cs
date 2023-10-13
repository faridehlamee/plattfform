using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Request;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.Request
{
    public interface ICooperationRequestService
    {
        Task<Pagedata<CooperationRequestDTO>> GetPaging(SearchDTO model, CooperationRequestDTO Search);
        Task<ResultDTO> CreateCooperationRequest(CooperationRequestDTO requestHeader, CancellationToken cancellationToken);
        Task<CooperationRequestDTO> GetById(int Id, string entityType);
        Task UpdateStatuse(int id, int status, string message, string entityType);
    }
}
