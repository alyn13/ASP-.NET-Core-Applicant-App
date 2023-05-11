using BaseCode.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace BaseCode.Data
{
    public partial class BaseCodeEntities : IdentityDbContext<IdentityUser> 
    {
        public BaseCodeEntities(DbContextOptions<BaseCodeEntities> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Disables cascade delete for tables with foreign key relationships 
            /*var cascadeTables = modelBuilder.Model.GetEntityTypes()
                .SelectMany(foreignKeysTables => foreignKeysTables.GetForeignKeys())
                .Where(foreignKeysTables => !foreignKeysTables.IsOwnership && 
                       foreignKeysTables.DeleteBehavior == DeleteBehavior.Cascade);
            */
            modelBuilder.Entity<RefreshToken>()
               .HasAlternateKey(c => c.Username)
               .HasName("refreshToken_UserId");

            modelBuilder.Entity<RefreshToken>()
               .HasAlternateKey(c => c.Token)
               .HasName("refreshToken_Token");
            base.OnModelCreating(modelBuilder);
        }

        public void InsertNew(RefreshToken token)
        {
            var tokenModel = RefreshToken.SingleOrDefault(i => i.Username == token.Username);
            if (tokenModel != null)
            {
                RefreshToken.Remove(tokenModel);
                SaveChanges();
            }
            RefreshToken.Add(token);
            SaveChanges();
        }

        public virtual DbSet<Applicant> Applicant { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<Website> Website { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
        public virtual DbSet<CollegeEducation> College { get; set; }
        public virtual DbSet<HighSchoolEducation> HighSchool { get; set; }
        public virtual DbSet<Experience> Experience { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
    }
}
