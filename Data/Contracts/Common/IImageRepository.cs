using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Common
{
    public interface IImageRepository : IRepository<Entites.Entities.Image>
    {
        Task DeleteIsActive(int Id, CancellationToken CancellationToken);
        Task DeleteImage(int id, string Location);
        Task<bool> SaveImageAsync(string Location, string entityType, int entityId , int[] ListPriority , IFormCollection Files, CancellationToken CancellationToken);
        string SaveStaticFile(string Location, IFormCollection Files);
        void DeleteStaticImage(string OldFile, string Location);
        Task<string> GetbyCurrentImage(int EntityId, string EntityType, CancellationToken CancellationToken);
        Task<List<string>> GetListImageByEntityId(int EntityId, string EntityType);
        Task<Image> GetImagebyEntityId(int EntityId, string EntityType, CancellationToken CancellationToken);
        Task TempResize(int width, int height, List<string> lstname);
    }
}