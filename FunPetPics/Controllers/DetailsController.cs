using FunPetPics.Data;
using FunPetPics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FunPetPics.Controllers
{
    public class DetailsController : ControllerBase
    {
        public DetailsController(FunPetPicsContext context) : base(context)
        {
        }

        [HttpGet]
        [Route("Details/Rate/{id}")]
        public IActionResult Rate(int id)
        {
            var userModel = GetLoggedInUser();
            var c = _context.PetPhotos;
            var petPhotoModel = _context.PetPhotos.FirstOrDefault(p => p.Id == id);

            if (userModel == null)
                return View(new RatingModel { PetPhotoModel = petPhotoModel });

            var model = _context.Ratings.FirstOrDefault(m =>
              m.User == userModel &&
              m.PetPhotoModel.Id == id);

            if (model == null)
            {
                model = new RatingModel { PetPhotoModel = petPhotoModel, User = userModel };
            }

            if (userModel.Uploads.FirstOrDefault(u => u == petPhotoModel) != null)
                return RedirectToAction("Edit", "Details", new { id = id });

            return View(model);
        }

        [HttpGet]
        [Route("Details/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var model = _context.PetPhotos.FirstOrDefault(p => p.Id == id);
            if (GetLoggedInUser().Uploads.FirstOrDefault(u => u == model) == null)
                return RedirectToAction("Index", "Home");
            return View(model);
        }
    }
}