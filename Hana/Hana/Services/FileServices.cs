using Hana.Data;
using Hana.Interfaces;
using Hana.Models.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hana.Services
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly HanaContext _context;
        public FileServices(IWebHostEnvironment environment, HanaContext context)
        {
            _enviroment = environment;
            _context = context;
        }


        public async Task<IEnumerable<Picture>> GetPicturesForRealEstate(int id)
        {
            return await _context.Picture
                .Where(p => p.RealEstateId == id && p.IsActive).ToListAsync();
        }

        public int AddPicture(int realEstateId, List<IFormFile> files)
        {
            var count = 0;
            var uploadedFiles = UploadFiles(files);
            var pictures = new List<Picture>(); 

            foreach (var file in uploadedFiles)
            {
                var picture = new Picture
                {
                    PictureName = file.Key,
                    RealEstateId = realEstateId,
                    Url = file.Value,
                    IsActive = true
                };

                pictures.Add(picture); 
                count++;
            }

            _context.Picture.AddRange(pictures); 
            _context.SaveChanges(); 

            return count;
        }


        public async Task<bool> RemovePictureFromRealEstate(int pictureId)
        {
            var picture = _context.Picture.Find(pictureId);
            if (picture != null)
            {
                _context.Picture.Remove(picture);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Dictionary<string, string> UploadFiles(List<IFormFile> files)
        {
            string wwwPath = this._enviroment.WebRootPath;

            string path = Path.Combine(wwwPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //tao dictionary de luu ten file va duong dan file
            var uploadedFiles = new Dictionary<string, string>();

            foreach (var file in files)
            {
                string fileUrl = string.Format("{0}-{1}", Guid.NewGuid(), file.FileName);

                //dung de luu trong database de nhan biet la file local
                //fileURL : GUID + FileName
                //fileName: local-picture + fileURL
                string fileName = string.Format("local-picture-{0}", fileUrl);

                var savePath = Path.Combine(path, fileUrl);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    uploadedFiles.Add(fileName, fileUrl);
                }
            }
            return uploadedFiles;
        }

        public List<Picture> GetImageFromFiles(List<IFormFile> files, int realEstateId)
        {
            string wwwPath = this._enviroment.WebRootPath;

            string path = Path.Combine(wwwPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //tao dictionary de luu ten file va duong dan file
            var pictures = new List<Picture>();

            foreach (var file in files)
            {
                int imageNativeWidth = 0;
                int imageNativeHeight = 0;
                string fileUrl = string.Format("{0}-{1}", Guid.NewGuid(), file.FileName);

                //dung de luu trong database de nhan biet la file local
                //fileURL : GUID + FileName
                //fileName: local-picture + fileURL
                string fileName = string.Format("local-picture-{0}", fileUrl);

                var savePath = Path.Combine(path, fileUrl);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    using (var img = Image.FromStream(stream))
                    {
                        imageNativeWidth = img.Width;
                        imageNativeHeight = img.Height;
                    }
                }

                var picture = new Picture()
                {
                    RealEstateId = realEstateId,
                    Url = fileUrl,
                    PictureName = fileName,
                    NativeWidth = imageNativeWidth,
                    NativeHeight = imageNativeHeight,
                    IsActive = true
                };
                pictures.Add(picture);
            }

            return pictures;
        }

        public int? GetRealEstateId(int pictureId)
        {
            var picture = _context.Picture.Find(pictureId);
            if (picture != null)
            {
                return picture.RealEstateId;
            }
            return null;
        }
    }
}

