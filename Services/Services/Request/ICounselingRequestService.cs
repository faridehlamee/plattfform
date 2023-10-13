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
    public interface ICounselingRequestService
    {
        Task<Pagedata<CounselingRequestDTO>> GetPaging(SearchDTO model, CounselingRequestDTO Search);
        Task<ResultDTO> CreateCounselingRequest(CounselingRequestDTO requestHeader, CancellationToken cancellationToken);
        Task<CounselingRequestDTO> GetById(int Id, string entityType);
        Task UpdateStatuse(int id, int status, string message, string entityType);
    }
}
