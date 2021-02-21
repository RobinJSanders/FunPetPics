using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FunPetPics.Models
{
    public class PetPhotoModel
    {
        public int Id { get; set; }

        [Required]
        public string PetName { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string UploadedBy { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public DateTime DateUploaded
        { get; set; }

        public double AverageCutenessRating { get; set; }

        public double AverageFunnynessRating { get; set; }

        public double AverageAwsomnessRating { get; set; }

        public virtual ICollection<RatingModel> Ratings { get; set; }
    }
}