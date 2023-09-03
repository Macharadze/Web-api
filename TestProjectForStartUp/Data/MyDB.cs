using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace ZauriStartUp.Models
{
    public class MyDB : DbContext
    {
        public MyDB(DbContextOptions<MyDB> o) : base(o)
        {
        }
        public DbSet<Profile> profiles { get; set; }    
        public DbSet<Account> accounts { get; set; }
        public DbSet<Comment> comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Comment and Account
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Account)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict); // Set ON DELETE NO ACTION

            // Configure the relationship between Comment and Profile
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Profile)
                .WithMany(p => p.comments)
                .HasForeignKey(c => c.ProfileId)
                .OnDelete(DeleteBehavior.Restrict); // Set ON DELETE NO ACTION

            // Configure the primary key for the Profile entity
            modelBuilder.Entity<Profile>()
                .HasKey(p => p.Id);

            // Configure the relationship between Profile and Comment
            modelBuilder.Entity<Profile>()
                .HasMany(p => p.comments)
                .WithOne(c => c.Profile)
                .HasForeignKey(c => c.ProfileId)
                .OnDelete(DeleteBehavior.Restrict); // Set ON DELETE NO ACTION
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Profiles)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.userId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the primary key for the Account entity
            modelBuilder.Entity<Account>()
                .HasKey(a => a.Id);

            // Configure the relationship between Account and Comment
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict); // Set ON DELETE NO ACTION
        }



    }
}
