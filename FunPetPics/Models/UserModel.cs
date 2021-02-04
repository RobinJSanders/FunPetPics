using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FunPetPics.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public virtual ICollection<PetPhotoModel> Uploads
        { get; set; }

        public virtual ICollection<RatingModel> Ratings { get; set; }
    }
}