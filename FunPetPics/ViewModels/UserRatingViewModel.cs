using FunPetPics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.ViewModels
{
    public class UserRatingViewModel
    {
        public PetPhotoModel PetPhotoModel { get; set; }
        public RatingModel RatingModel { get; set; }
        public UserModel UserModel { get; set; }
    }
}