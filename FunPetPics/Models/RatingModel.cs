namespace FunPetPics.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        public int? UserModelId { get; set; }

        public int? PetPhotoModelId { get; set; }

        public int? CutenessRating { get; set; }

        public int? FunynessRating { get; set; }
        public int? AwsomenessRating { get; set; }
    }
}