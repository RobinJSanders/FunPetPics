using FunPetPics.Data;
using FunPetPics.Models;
using FunPetPics.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FunPetPics.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFilterService _filterService;

        public HomeController(FunPetPicsContext context, ILogger<HomeController> logger, IFilterService filterService) : base(context)
        {
            _logger = logger;
            _filterService = filterService;
        }

        public IActionResult Index(string sortOrder)
        {
            var uploads = _context.PetPhotos;

            if (uploads == null || !uploads.Any())
            {
                return View();
            }

            var model = uploads.ToList();

            _filterService.SetupFilters(ref model, sortOrder, this);

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UploadPhoto()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}