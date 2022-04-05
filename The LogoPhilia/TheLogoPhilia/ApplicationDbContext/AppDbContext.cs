

using System;
using Microsoft.EntityFrameworkCore;
using TheLogoPhilia.Entities;
using TheLogoPhilia.Entities.JoinerTables;

namespace TheLogoPhilia.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //   modelBuilder.ApplyConfigurationsFromAssembly(TheLogoPhilia.Configurations)
        }

        public DbSet<User> Users { get; set; }

        public DbSet<AdministratorMessage> AdministratorMessages { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationAdministrator> ApplicationAdministrators { get; set; }
        public DbSet<ApplicationUserAdminMessage> ApplicationUserAdminMessages { get; set; }
        public DbSet<ApplicationUserComment> ApplicationUserComments { get; set; }
        public DbSet<ApplicationUserPost> ApplicationUserPosts { get; set; }

        public DbSet<LanguageOfOrigin> LanguageOfOrigins { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Word> Words { get; set; }
        public DbSet<Notes> Notes { get ; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PostLog> PostLogs { get; set; }

    }
}