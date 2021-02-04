using FunPetPics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FunPetPics.Data
{
    public class FunPetPicsContext : DbContext
    {
        public FunPetPicsContext(DbContextOptions<FunPetPicsContext> options) : base(options)
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