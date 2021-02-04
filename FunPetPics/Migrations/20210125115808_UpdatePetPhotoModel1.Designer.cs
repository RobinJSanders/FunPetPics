﻿// <auto-generated />
using System;
using FunPetPics.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FunPetPics.Migrations
{
    [DbContext(typeof(FunPetPicsContext))]
    [Migration("20210125115808_UpdatePetPhotoModel1")]
    partial class UpdatePetPhotoModel1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("FunPetPics.Models.PetPhotoModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double?>("AverageAwsomnessRating")
                        .HasColumnType("float");

                    b.Property<double?>("AverageCutenessRating")
                        .HasColumnType("float");

                    b.Property<double?>("AverageFunnynessRating")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateUploaded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserModelId");

                    b.ToTable("PetPhotos");
                });

            modelBuilder.Entity("FunPetPics.Models.RatingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AwsomenessRating")
                        .HasColumnType("int");

                    b.Property<int?>("CutenessRating")
                        .HasColumnType("int");

                    b.Property<int?>("FunynessRating")
                        .HasColumnType("int");

                    b.Property<int?>("PetPhotoModelId")
                        .HasColumnType("int");

                    b.Property<int?>("UserModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PetPhotoModelId");

                    b.HasIndex("UserModelId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("FunPetPics.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FunPetPics.Models.PetPhotoModel", b =>
                {
                    b.HasOne("FunPetPics.Models.UserModel", null)
                        .WithMany("Uploads")
                        .HasForeignKey("UserModelId");
                });

            modelBuilder.Entity("FunPetPics.Models.RatingModel", b =>
                {
                    b.HasOne("FunPetPics.Models.PetPhotoModel", null)
                        .WithMany("Ratings")
                        .HasForeignKey("PetPhotoModelId");

                    b.HasOne("FunPetPics.Models.UserModel", null)
                        .WithMany("Ratings")
                        .HasForeignKey("UserModelId");
                });

            modelBuilder.Entity("FunPetPics.Models.PetPhotoModel", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("FunPetPics.Models.UserModel", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Uploads");
                });
#pragma warning restore 612, 618
        }
    }
}
