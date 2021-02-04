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
    public class PetPhotoModelsController : Controller
    {
        private readonly FunPetPicsContext _context;

        public PetPhotoModelsController(FunPetPicsContext context)
        {
            _context = context;
        }

        // GET: PetPhotoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PetPhotos.ToListAsync());
        }

        // GET: PetPhotoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petPhotoModel = await _context.PetPhotos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petPhotoModel == null)
            {
                return NotFound();
            }

            return View(petPhotoModel);
        }

        // GET: PetPhotoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PetPhotoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PetName,Title,Description,ImageName,DateUploaded,AverageCutenessRating,AverageFunnynessRating,AverageAwsomnessRating")] PetPhotoModel petPhotoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(petPhotoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(petPhotoModel);
        }

        // GET: PetPhotoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petPhotoModel = await _context.PetPhotos.FindAsync(id);
            if (petPhotoModel == null)
            {
                return NotFound();
            }
            return View(petPhotoModel);
        }

        // POST: PetPhotoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PetName,Title,Description,ImageName,DateUploaded,AverageCutenessRating,AverageFunnynessRating,AverageAwsomnessRating")] PetPhotoModel petPhotoModel)
        {
            if (id != petPhotoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(petPhotoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetPhotoModelExists(petPhotoModel.Id))
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
            return View(petPhotoModel);
        }

        // GET: PetPhotoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var petPhotoModel = await _context.PetPhotos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (petPhotoModel == null)
            {
                return NotFound();
            }

            return View(petPhotoModel);
        }

        // POST: PetPhotoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var petPhotoModel = await _context.PetPhotos.FindAsync(id);
            _context.PetPhotos.Remove(petPhotoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetPhotoModelExists(int id)
        {
            return _context.PetPhotos.Any(e => e.Id == id);
        }
    }
}
