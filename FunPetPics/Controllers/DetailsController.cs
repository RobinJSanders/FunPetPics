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
        [Route("Details/Rate/{id}")]// when I add this the controller doesn't work and I get page not found
        public IActionResult Rate(int id)
        {
            var model = new Tuple<PetPhotoModel, UserModel>
           (
                _context.PetPhotos.Include(e => e.Ratings).FirstOrDefault(p => p.Id == id),
                GetLoggedInUser()
           );

            return View(model);
        }
    }
}