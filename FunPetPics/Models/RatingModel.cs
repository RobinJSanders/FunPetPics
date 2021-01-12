using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Models
{
    public class RatingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RaitngId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [ForeignKey("PetId")]
        public int PetID { get; set; }
        public int CutenessRating { get; set; }
        public int FunynessRating { get; set; }
        public int AwsomenessRating { get; set; }
    }
}
