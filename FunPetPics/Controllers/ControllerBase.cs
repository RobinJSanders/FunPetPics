using FunPetPics.Data;
using FunPetPics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FunPetPics.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly FunPetPicsContext _context;

        public ControllerBase(FunPetPicsContext context)
        {
            _context = context;
        }

        protected UserModel GetLoggedInUser()
        {
            var userId = HttpContext.Session.GetInt32("Id");

            return userId == null ? null :

            _context.Users
                .Include(e => e.Uploads)
                .Include(e => e.Ratings)
                .FirstOrDefault(u => u.Id == userId);
        }

        protected void CreateFolderIfNotExist(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                return;
            }
            DirectoryInfo folder = Directory.CreateDirectory(folderPath);
        }

        protected ICollection<T> SetDefaultsAndAdd<T>(ICollection<T> collection, T model)
        {
            if (collection == null)
                collection = new List<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var info in properties)
            {
                // if a string and null, set to String.Empty
                if (info.PropertyType == typeof(string) &&
                   info.GetValue(model) == null)
                {
                    info.SetValue(model, String.Empty);
                }
                if (info.PropertyType == typeof(double) &&
                info.GetValue(model) == null)
                {
                    info.SetValue(model, 0);
                }
            }

            collection.Add(model);
            return collection;
        }

        protected void SetupFilters(ref List<PetPhotoModel> model, string sortOrder)
        {
            ViewBag.SortOrder = new List<string>
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