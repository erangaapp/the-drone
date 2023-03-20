using DroneAPI.Data.Context.FluentAPI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;

namespace DroneAPI.Data
{
    public class DroneAPIDbContext : DbContext
    {
        public DroneAPIDbContext(DbContextOptions<DroneAPIDbContext> _context) :
            base(_context)
        {
            Database.EnsureCreated();
        }

        public DbSet<DroneEntity> Drones { get; set; }
        public DbSet<MedicationEntity> Medications { get; set; }
        public DbSet<DroneMedicationEntity> DroneMedications { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DroneEntityBuilder.Build(modelBuilder.Entity<DroneEntity>());
            MedicationEntityBuilder.Build(modelBuilder.Entity<MedicationEntity>());
            DroneMedicationEntityBuilder.Build(modelBuilder.Entity<DroneMedicationEntity>());

            #region Seed
            modelBuilder.Entity<MedicationEntity>().
                HasData(new MedicationEntity
                {
                    Id = 1,
                    Code = "MD-1",
                    Name = "Medication 1",
                    Weight= 100,
                    CreatedDateTime = DateTime.Now,
                    CreatedBy = "System Seed"
                }, new MedicationEntity
                {
                    Id = 2,
                    Code = "MD-2",
                    Name = "Medication 2",
                    Weight = 50,
                    CreatedDateTime = DateTime.Now,
                    CreatedBy = "System Seed"
                }, new MedicationEntity
                {
                    Id = 3,
                    Code = "MD-3",
                    Name = "Medication 3",
                    Weight = 200,
                    CreatedDateTime = DateTime.Now,
                    CreatedBy = "System Seed"
                });
            #endregion
        }
    }
}
