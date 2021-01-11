using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Models
{
    public class UserModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string Email{ get; set; }

        public string DisplayName { get; set; }

        public List<PetPhotoModel> Uploads { get; set; }
    }
}
