using FunPetPics.Data;
using FunPetPics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("Details/Rate/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate(int id, RatingModel model)
        {
            var petPhotoModel = _context.PetPhotos.Include(e => e.Ratings)
                .FirstOrDefault(e => e.Id == model.Id);

            var ratingModels = petPhotoModel.Ratings;

            var existingRating = ratingModels.FirstOrDefault(r => r.Id == model.Id);

            if (existingRating != null)
            {
                existingRating = model;

                CalculateAverageRatings(ratingModels, petPhotoModel);
            }
            else
            {
                ratingModels.Add(model);
                CalculateAverageRatings(ratingModels, petPhotoModel);
            }

            await _context.SaveChangesAsync();

            return View(model.PetPhotoModel.Id);
        }

        private void CalculateAverageRatings(ICollection<RatingModel> ratingModels, PetPhotoModel petPhotoModel)
        {
            var ratingsTable = new List<List<int?>>
                {
                    ratingModels.Select(  r => r.AwsomenessRating )
                    .Where(d => d != null).ToList(),

                    ratingModels.Select(  r => r.CutenessRating )
                    .Where(d => d != null).ToList(),

                    ratingModels.Select(  r => r.FunynessRating )
                    .Where(d => d != null).ToList(),
                };

            petPhotoModel.AverageAwsomnessRating = Math.Round((double)ratingsTable[0].Average(), 1);
            petPhotoModel.AverageCutenessRating = Math.Round((double)ratingsTable[1].Average(), 1);
            petPhotoModel.AverageFunnynessRating = Math.Round((double)ratingsTable[2].Average(), 1);
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