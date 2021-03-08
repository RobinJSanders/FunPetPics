using FunPetPics.Data;
using FunPetPics.Models;
using FunPetPics.ViewModels;
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
                return View(new UserRatingViewModel { PetPhotoModel = petPhotoModel });

            var ratingModel = _context.Ratings.FirstOrDefault(m =>
              m.UserModelId == userModel.Id &&
              m.PetPhotoModelId == petPhotoModel.Id);

            if (ratingModel == null)
            {
                ratingModel = new RatingModel { PetPhotoModelId = petPhotoModel.Id, UserModelId = userModel.Id };
            }

            if (userModel.Uploads.FirstOrDefault(u => u == petPhotoModel) != null)
                return RedirectToAction("Edit", "Details", new { id = id });

            ratingModel.PetPhotoModelId = petPhotoModel.Id;
            ratingModel.UserModelId = userModel.Id;

            return View(new UserRatingViewModel { PetPhotoModel = petPhotoModel, RatingModel = ratingModel, UserModel = userModel });
        }

        [HttpPost]
        [Route("Details/Rate/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rate(UserRatingViewModel model)
        {
            var petPhotoModel = await _context.PetPhotos.FirstOrDefaultAsync(m => m.Id == model.PetPhotoModel.Id);

            model.RatingModel.PetPhotoModelId = model.PetPhotoModel.Id;
            model.RatingModel.UserModelId = model.UserModel.Id;

            var ratingModels = _context.Ratings.Where(u => u.PetPhotoModelId == petPhotoModel.Id).ToList();

            var existingRating = await _context.Ratings.FirstOrDefaultAsync(m => m.UserModelId == model.UserModel.Id);

            if (existingRating != null)
            {
                int existingIndex = 0;
                for (int i = 0; i < ratingModels.Count; i++)
                {
                    if (ratingModels[i].Id == existingRating.Id)
                    {
                        existingIndex = i;
                        break;
                    }
                }
                ratingModels[existingIndex] = model.RatingModel;
                _context.Entry(existingRating).CurrentValues.SetValues(model.RatingModel);
                CalculateAverageRatings(ratingModels, petPhotoModel);
            }
            else
            {
                ratingModels.Add(model.RatingModel);
                _context.Ratings.Add(model.RatingModel);
                CalculateAverageRatings(ratingModels, petPhotoModel);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Rate", "Details", new { id = model.PetPhotoModel.Id });
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

            if (ratingsTable[0].Any())
                petPhotoModel.AverageAwsomnessRating = Math.Round((double)ratingsTable[0].Average(), 1);
            if (ratingsTable[1].Any())
                petPhotoModel.AverageCutenessRating = Math.Round((double)ratingsTable[1].Average(), 1);
            if (ratingsTable[2].Any())
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