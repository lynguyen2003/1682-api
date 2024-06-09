using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataServices.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Favourites> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Followers>()
                .HasOne(f => f.Follower)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.follower_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Followers>()
                .HasOne(f => f.Following)
                .WithMany(u => u.Followings)
                .HasForeignKey(f => f.following_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Users)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.user_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Videos)
                .WithMany(v => v.Comments)
                .HasForeignKey(c => c.video_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Likes>()
                .HasOne(l => l.Users)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.user_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Likes>()
                .HasOne(l => l.Videos)
                .WithMany(v => v.Likes)
                .HasForeignKey(l => l.video_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Favourites>()
                .HasOne(f => f.Users)
                .WithMany(u => u.Favourites)
                .HasForeignKey(f => f.user_id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Favourites>()
                .HasOne(f => f.Videos)
                .WithMany(v => v.Favourites)
                .HasForeignKey(f => f.video_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
