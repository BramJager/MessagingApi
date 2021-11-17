using MessagingApi.Business.Objects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MessagingApi.Business.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData
               (
                new ApplicationRole { Id = 1, Name = "User" },
                new ApplicationRole { Id = 2, Name = "Groupmoderator"},
                new ApplicationRole { Id = 3, Name = "Administrator" }
               );

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.UserName)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}
