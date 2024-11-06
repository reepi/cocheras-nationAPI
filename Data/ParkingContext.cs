using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ParkingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Parking> Parkings { get; set; }

        public ParkingContext(DbContextOptions<ParkingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                    .IsRequired();

                entity.Property(u => u.Name)
                    .IsRequired();

                entity.Property(u => u.Password)
                    .IsRequired();

            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Description)
                    .IsRequired();

                entity.Property(s => s.IsAvailable)
                    .IsRequired();

            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(f => f.Type);

                entity.Property(f => f.Value)
                    .IsRequired();

            });

            modelBuilder.Entity<Parking>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.SlotId)
                    .IsRequired();

                entity.Property(p => p.Plate)
                    .IsRequired();

                entity.Property(p => p.EntryTime)
                    .IsRequired();

                entity.Property(p => p.ExitTime);

                entity.Property(p => p.Fee);

            });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Name = "Administrador",
                    Password = "admin",
                }
            );

            modelBuilder.Entity<Fee>().HasData(
                new Fee
                {
                    Type = Common.Models.FeeTypeEnum.HalfHourly,
                    Value = 1000
                },
                new Fee
                {
                    Type = Common.Models.FeeTypeEnum.Hourly,
                    Value = 1500
                },
                new Fee
                {
                    Type = Common.Models.FeeTypeEnum.Daily,
                    Value = 30000
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
