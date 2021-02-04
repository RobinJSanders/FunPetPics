using FunPetPics.Data;
using FunPetPics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace FunPetPics.Controllers
{
    public class UserUploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserUploadsController(FunPetPicsContext context, IWebHostEnvironment hostEnvironment) : base(context)
        {
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var uploads = GetLoggedInUser().Uploads;
            if (uploads != null)
            {
                var listUploads = uploads.ToList();
                return View(uploads);
            }
            else return View();
        }

        public IActionResult UploadPhoto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPhoto([Bind("Id,PetName,Title,Description,ImageFile")] PetPhotoModel petPhotoModel)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(petPhotoModel.ImageFile.FileName);
                string extension = Path.GetExtension(petPhotoModel.ImageFile.FileName);
                petPhotoModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                petPhotoModel.DateUploaded = DateTime.Now;

                var folderPath = wwwRootPath + "\\UploadedPhotos\\";
                CreateFolderIfNotExist(folderPath);
                string path = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await petPhotoModel.ImageFile.CopyToAsync(fileStream);
                }

                //GetLoggedInUser() gets the current user by id from the db context
                var user = GetLoggedInUser();
                SetDefaultsAndAdd(user.Uploads, petPhotoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petPhotoModel);
        }
    }
}