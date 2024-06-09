﻿// <auto-generated />
using System;
using DataServices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataServices.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Entities.Comments", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<int>("video_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.HasIndex("video_id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Models.Entities.Favourites", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<int>("video_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.HasIndex("video_id");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("Models.Entities.Followers", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("follower_id")
                        .HasColumnType("int");

                    b.Property<int>("following_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("follower_id");

                    b.HasIndex("following_id");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("Models.Entities.Likes", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<int>("video_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.HasIndex("video_id");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Models.Entities.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("avatar_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bio_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("display_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("follower_count")
                        .HasColumnType("int");

                    b.Property<int?>("following_count")
                        .HasColumnType("int");

                    b.Property<bool>("is_verify")
                        .HasColumnType("bit");

                    b.Property<int?>("likes_count")
                        .HasColumnType("int");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("phone_number")
                        .HasColumnType("int");

                    b.Property<int?>("video_count")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Entities.Videos", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("likes_count")
                        .HasColumnType("int");

                    b.Property<string>("thumbnail_url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("video_url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("views_count")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Models.Entities.Comments", b =>
                {
                    b.HasOne("Models.Entities.Users", "Users")
                        .WithMany("Comments")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Entities.Videos", "Videos")
                        .WithMany("Comments")
                        .HasForeignKey("video_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Users");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("Models.Entities.Favourites", b =>
                {
                    b.HasOne("Models.Entities.Users", "Users")
                        .WithMany("Favourites")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Entities.Videos", "Videos")
                        .WithMany("Favourites")
                        .HasForeignKey("video_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Users");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("Models.Entities.Followers", b =>
                {
                    b.HasOne("Models.Entities.Users", "Follower")
                        .WithMany("Followers")
                        .HasForeignKey("follower_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Entities.Users", "Following")
                        .WithMany("Followings")
                        .HasForeignKey("following_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("Models.Entities.Likes", b =>
                {
                    b.HasOne("Models.Entities.Users", "Users")
                        .WithMany("Likes")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Entities.Videos", "Videos")
                        .WithMany("Likes")
                        .HasForeignKey("video_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Users");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("Models.Entities.Videos", b =>
                {
                    b.HasOne("Models.Entities.Users", "Users")
                        .WithMany("Videos")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Models.Entities.Users", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Favourites");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Likes");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("Models.Entities.Videos", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Favourites");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
