using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        //[Required]
        //public int UserId { get; set; }

        //[Required]
        //public int PetPhotoId { get; set; }
        public int? CutenessRating { get; set; }
        public int? FunynessRating { get; set; }
        public int? AwsomenessRating { get; set; }
    }
}
