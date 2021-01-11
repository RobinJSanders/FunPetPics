using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Models
{
    public class PetPhotoModel
    {
        public string PetName { get; set; }
        public string PhotoPath{ get; set; }
        public Single AverageChonkienessRating{ get; set; }
        public Single AverageCutenessRating{ get; set; }
        public Single AverageFunynessRating { get; set; }
    }
}
