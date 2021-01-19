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