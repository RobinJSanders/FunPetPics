using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Models
{
    public class PetPhotoModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PetID { get; set; }

        public int UserID { get; set; }
        public string PetName { get; set; }

        //todo: enable users to upload actual photos but for now we just use string for the file path
        public string PhotoPath { get; set; }

        public Single AverageCutenessRating
        {
            get
            {
                var total = Ratings.Sum(r => r.CutenessRating);
                return total / Ratings.Count;
            }
        }
        public Single AverageFunnynessRating
        {
            get
            {
                var total = Ratings.Sum(r => r.FunynessRating);
                return total / Ratings.Count;
            }
        }
        public Single AverageAwsomnessRating
        {
            get
            {
                var total = Ratings.Sum(r => r.AwsomenessRating);
                return total / Ratings.Count;
            }
        }
        public virtual ICollection<RatingModel> Ratings { get; set; }
    }
}
