using FunPetPics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunPetPics.Data
{
    public class FunPetPicsContext : DbContext
    {
        public FunPetPicsContext(DbContextOptions<FunPetPicsContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<PetPhotoModel> PetPhotos { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
    }
}
