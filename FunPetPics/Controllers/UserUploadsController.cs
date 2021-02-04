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
using System.Collections.Generic;

namespace FunPetPics.Controllers
{
    public class UserUploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserUploadsController(FunPetPicsContext context, IWebHostEnvironment hostEnvironment) : base(context)
        {
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(string sortOrder)
        {
            //get the current logged in user and their collection of uploads from the database context
            var uploads = GetLoggedInUser().Uploads;

            if (uploads == null || !uploads.Any())
            {
                return View();
            }

            var model = uploads.ToList();

            switch (sortOrder)
            {
                case "Newest":
                    model = model.OrderByDescending(u => u.DateUploaded).ToList();
                    break;

                case "Oldest":
                    model = model.OrderBy(u => u.DateUploaded)
                        .ToList();
                    break;

                case "Cutest":
                    model = model.OrderByDescending(u => u.AverageCutenessRating)
                        .ThenByDescending(u => u.DateUploaded)
                        .ToList();
                    break;

                case "Funniest":
                    model = model.OrderByDescending(u => u.AverageFunnynessRating)
                        .ThenByDescending(u => u.DateUploaded)
                        .ToList();
                    break;

                case "Most Awsome":
                    model = model.OrderByDescending(u => u.AverageAwsomnessRating)
                         .ThenByDescending(u => u.DateUploaded)
                         .ToList();
                    break;

                default:
                    sortOrder = "Newest";
                    model = model.OrderByDescending(u => u.DateUploaded)
                        .ToList();
                    break;
            }

            ViewBag.SortOrder = new List<string>
            {
                "Newest", "Oldest", "Cutest", "Funniest", "Most Awsome"
            };

            return View(model);
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