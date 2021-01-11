using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Models
{
    public class RatingModel
    {
        public string PetName { get; set; }
        public Single ChonkienessRating { get; set; }
        public Single CutenessRating { get; set; }
        public Single FunynessRating { get; set; }
    }
}
