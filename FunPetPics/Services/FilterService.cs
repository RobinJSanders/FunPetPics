using FunPetPics.Controllers;
using FunPetPics.Models;
using FunPetPics.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Services
{
    public class FilterService : IFilterService
    {
        public void SetupFilters(ref List<PetPhotoModel> model, string sortOrder, ControllerBase controler)
        {
            controler.ViewBag.SortOrder = new List<string>
            {
                "Newest", "Oldest", "Cutest", "Funniest", "Most Awsome"
            };

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
        }
    }
}