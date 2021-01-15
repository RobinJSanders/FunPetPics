using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Models
{
    public class PetPhotoModel
    {
        public int Id { get; set; }

        //[Required]
        //public int UserId { get; set; }

        [Required]
        public string PetName { get; set; }

        //todo: enable users to upload actual photos but for now we just use string for the file path

        [Required]
        public string PhotoPath { get; set; }

        public double? AverageCutenessRating
        
             { get; set; }
            //get
            //{
            //    return Ratings.Select(r => (int?)r.CutenessRating).Average();
            //}

        
        public double? AverageFunnynessRating
        
             { get; set; }
            //get
            //{
            //    return Ratings.Select(r => (int?)r.FunynessRating).Average();
            //}
        
        public double? AverageAwsomnessRating
        
             { get; set; }
            //get
            //{
            //      return Ratings.Select(r => (int?)r.AwsomenessRating).Average();
            //}
        
        public virtual ICollection<RatingModel> Ratings { get; set; }


    }
}
