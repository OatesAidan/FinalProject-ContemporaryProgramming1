using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;


namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class ContemporaryProgramming_Final_Actual_Context : DbContext
    {
        public ContemporaryProgramming_Final_Actual_Context()
        {
        }

        public ContemporaryProgramming_Final_Actual_Context(DbContextOptions<ContemporaryProgramming_Final_Actual_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AddisonTable> AddisonTable { get; set; }
        public virtual DbSet<IsabelleTable> IsabelleTable { get; set; }
        public virtual DbSet<AidanTable> AidanTable { get; set; }
        public virtual DbSet<MattTable> MattTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='{DBContext.MdfFileLocation}';Integrated Security=True");//configuration.GetConnectionString("DefaultConnection")); local file plug
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AidanTable>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.CollegeProgram).IsUnicode(false);

                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.YearInProgram).IsUnicode(false);
            });

            modelBuilder.Entity<IsabelleTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FavoriteCeleb).IsUnicode(false);

                entity.Property(e => e.FavoriteMovie).IsUnicode(false);

                entity.Property(e => e.FavoriteAnimal).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });

            modelBuilder.Entity<AddisonTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FavoriteBreakfeast).IsUnicode(false);

                entity.Property(e => e.FavoriteLunch).IsUnicode(false);

                entity.Property(e => e.FavoriteDinner).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });

            modelBuilder.Entity<MattTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FavoriteSeason).IsUnicode(false);

                entity.Property(e => e.FavoriteGame).IsUnicode(false);

                entity.Property(e => e.FavoriteSport).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
