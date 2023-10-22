using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Data.Contracts.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Data.Contracts.Product;

namespace Data.Repositories.Public
{
    public class ImageRepository : Repository<Entites.Entities.Image>, IImageRepository, IScopedDependency
    {
        public ImageRepository(RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
           
        }
        public async Task DeleteIsActive(int Id, CancellationToken CancellationToken)
        {
            var data = await GetByIdAsync(CancellationToken, Id);
            data.IsActive = false;
            await UpdateAsync(data, CancellationToken);
        }

        public async Task DeleteImage(int id, string Location)
        {

            var data = await GetByIdAsync(CancellationToken.None, id);
            var folderName = Path.Combine("wwwroot/images" + Location);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullname = Path.Combine(pathToSave, data.ImageFile);
            if (System.IO.File.Exists(fullname))
            {
                System.IO.File.Delete(fullname);
            }
            await DeleteAsync(data, CancellationToken.None);

        }

        public async Task<bool> SaveImageAsync(string Location, string entityType, int entityId, int[] ListPriority, IFormCollection Files, CancellationToken CancellationToken)
        {
            //Location like "~/Content/images/blog/"
            try
            {
                for (int i = 0; i < Files.Files.Count; i++)
                {
                    if (Files.Files[i].Length < 512000)
                    {
                        if (Files.Files[i].ContentType.ToLower() == "image/png" || Files.Files[i].ContentType.ToLower() == "image/jpg" || Files.Files[i].ContentType.ToLower() == "image/jpeg")
                        {
                            var filename = CreateRefCode() + Files.Files[i].FileName;

                            var folderName = Path.Combine("wwwroot/images" + Location);
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                            var fullname = Path.Combine(pathToSave, filename);

                            using (var stream = new FileStream(fullname, FileMode.Create))
                            {
                                Files.Files[i].CopyTo(stream);
                                var imageFile = new Entites.Entities.Image
                                {
                                    EntityId = entityId,
                                    EntityType = entityType,
                                    ImageFile = filename,
                                    CreatorId = 1,
                                    Priority = ListPriority == null ? 1 : ListPriority[i]
                                };
                                await AddAsync(imageFile, CancellationToken);
                            }
                            Resize(fullname, 250, 250);

                        }

                    }
                }


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public string SaveStaticFile(string Location, IFormCollection Files)
        {
            try
            {
                for (int i = 0; i < Files.Files.Count; i++)
                {
                    if (Files.Files[i].Length < 1500000)
                    {
                        if (Files.Files[i].ContentType.ToLower() == "image/png" || Files.Files[i].ContentType.ToLower() == "image/jpg" || Files.Files[i].ContentType.ToLower() == "image/jpeg")
                        {
                            var filename = CreateRefCode() + Files.Files[i].FileName;

                            var folderName = Path.Combine("wwwroot/images" + Location);
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                            var fullname = Path.Combine(pathToSave, filename);

                            using (var stream = new FileStream(fullname, FileMode.Create))
                            {
                                Files.Files[i].CopyTo(stream);
                                return filename;
                            }

                        }

                    }
                }


                return "faild.jpg";
            }
            catch (Exception ex)
            {

                return "faild.jpg";
            }
        }

        public void DeleteStaticImage(string OldFile, string Location)
        {
            var folderName = Path.Combine("wwwroot/images" + Location);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullname = Path.Combine(pathToSave, OldFile);
            if (System.IO.File.Exists(fullname))
            {
                System.IO.File.Delete(fullname);
            }

        }

        public async Task<string> GetbyCurrentImage(int EntityId, string EntityType, CancellationToken CancellationToken)
        {
            var data = await TableNoTracking.Where(c => c.EntityId == EntityId && c.EntityType == EntityType).OrderBy(c => c.Priority).FirstOrDefaultAsync();
            if (data == null)
            {
                return "NotFount.jpg";
            }
            else
            {
                return data.ImageFile;
            }


        }
        public async Task<List<string>> GetListImageByEntityId(int EntityId, string EntityType)
        {
            var data = await TableNoTracking.Where(c => c.EntityId == EntityId && c.EntityType == EntityType).OrderBy(c => c.Priority).Select(w => w.ImageFile).ToListAsync();

            return data;


        }
        public async Task<Entites.Entities.Image> GetImagebyEntityId(int EntityId, string EntityType, CancellationToken CancellationToken)
        {
            var data = await TableNoTracking.Where(c => c.EntityId == EntityId && c.EntityType == EntityType).OrderBy(c => c.Priority).FirstOrDefaultAsync();
            return data;

        }

        public string CreateRefCode()
        {
            Random rnd = new Random();
            var num = rnd.Next(1000, 9999);
            var num2 = rnd.Next(1000, 9999);
            var num3 = rnd.Next(1000, 9999);
            return num.ToString() + num2.ToString() + num3.ToString();
        }

        private static void Resize(string filePath, int width, int height)
        {
            var file = filePath;
            using (var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var image = new Bitmap(imageStream))
            {
                var resizedImage = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.DrawImage(image, 0, 0, width, height);
                    var newFilePath = $"wwwroot/images/thumbnailproduct/{Path.GetFileNameWithoutExtension(file)}.jpg";
                    resizedImage.Save(
                        newFilePath,
                        ImageFormat.Png);
                }
            }
        }


        public async Task TempResize(int width, int height, List<string> lstname)
        {
            foreach (var item in lstname)
            {

                if (item != null)
                {
                    var folderName = Path.Combine("wwwroot/images/product/");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    var filePath = Path.Combine(pathToSave, item);
                    var file = filePath;
                    using (var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    using (var image = new Bitmap(imageStream))
                    {
                        var resizedImage = new Bitmap(width, height);
                        using (var graphics = Graphics.FromImage(resizedImage))
                        {
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.DrawImage(image, 0, 0, width, height);
                            var newFilePath = $"wwwroot/images/thumbnailproduct/{Path.GetFileNameWithoutExtension(file)}.jpg";
                            resizedImage.Save(
                                newFilePath,
                                ImageFormat.Png);
                        }
                    }
                }


            }
        }



    }
}
