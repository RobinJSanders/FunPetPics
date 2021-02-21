using FunPetPics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.ViewModels
{
    public class UserRatingViewModel
    {
        public UserRatingViewModel(ICollection<RatingModel> userRatrings, ICollection<RatingModel> uploadRatings)
        {
            foreach (var userRating in userRatrings)
            {
                foreach (var uploadRating in uploadRatings)
                {
                    if (userRating == uploadRating)
                    {
                        CutenessRating = userRating.CutenessRating == null ? 0 : (int)userRating.CutenessRating;
                        FunynessRating = userRating.FunynessRating == null ? 0 : (int)userRating.FunynessRating;
                        AwsomenessRating = userRating.AwsomenessRating == null ? 0 : (int)userRating.AwsomenessRating;
                    }
                }
            }
        }

        public int CutenessRating { get; set; }

        public int FunynessRating { get; set; }
        public int AwsomenessRating { get; set; }
    }
}