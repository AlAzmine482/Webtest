/*using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Webtest.Models;

public partial class CarmanufacturerContext : DbContext
{
    public CarmanufacturerContext()
    {
    }

    public CarmanufacturerContext(DbContextOptions<CarmanufacturerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=carmanufacturer;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Cars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_car_manufacturer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

*/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
//using Webtest.Models;
using System.Collections.Generic;


namespace Webtest.Models
{
    public partial class CarmanufacturerContext : IdentityDbContext<WorldCarsUser>
    {
        public CarmanufacturerContext()
        {
        }

        public CarmanufacturerContext(DbContextOptions<CarmanufacturerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.CarId).HasName("PK_car");

                entity.Property(e => e.CarId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Cars)
                    //.HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_car_manufacturer");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.ManufacturerId).HasName("PK_manufacturer");

                entity.Property(e => e.ManufacturerId).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
