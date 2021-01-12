using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FunPetPics.Data;
using FunPetPics.Models;

namespace FunPetPics.Controllers
{
    public class RatingModelsController : Controller
    {
        private readonly FunPetPicsContext _context;

        public RatingModelsController(FunPetPicsContext context)
        {
            _context = context;
        }

        // GET: RatingModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ratings.ToListAsync());
        }

        // GET: RatingModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingModel = await _context.Ratings
                .FirstOrDefaultAsync(m => m.RaitngId == id);
            if (ratingModel == null)
            {
                return NotFound();
            }

            return View(ratingModel);
        }

        // GET: RatingModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RatingModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RaitngId,UserId,PetID,CutenessRating,FunynessRating,AwsomenessRating")] RatingModel ratingModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ratingModel);
        }

        // GET: RatingModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingModel = await _context.Ratings.FindAsync(id);
            if (ratingModel == null)
            {
                return NotFound();
            }
            return View(ratingModel);
        }

        // POST: RatingModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RaitngId,UserId,PetID,CutenessRating,FunynessRating,AwsomenessRating")] RatingModel ratingModel)
        {
            if (id != ratingModel.RaitngId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingModelExists(ratingModel.RaitngId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ratingModel);
        }

        // GET: RatingModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratingModel = await _context.Ratings
                .FirstOrDefaultAsync(m => m.RaitngId == id);
            if (ratingModel == null)
            {
                return NotFound();
            }

            return View(ratingModel);
        }

        // POST: RatingModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratingModel = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(ratingModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingModelExists(int id)
        {
            return _context.Ratings.Any(e => e.RaitngId == id);
        }
    }
}
