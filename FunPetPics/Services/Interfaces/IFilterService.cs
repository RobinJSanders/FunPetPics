using FunPetPics.Controllers;
using FunPetPics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Services.Interfaces
{
    public interface IFilterService
    {
        void SetupFilters(ref List<PetPhotoModel> model, string sortOrder, ControllerBase controler);
    }
}