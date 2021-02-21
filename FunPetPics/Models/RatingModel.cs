namespace FunPetPics.Models
{
    public class RatingModel
    {
        public int Id { get; set; }

        public UserModel User { get; set; }
        public PetPhotoModel PetPhotoModel { get; set; }

        public int? CutenessRating { get; set; }

        public int? FunynessRating { get; set; }
        public int? AwsomenessRating { get; set; }
    }
}