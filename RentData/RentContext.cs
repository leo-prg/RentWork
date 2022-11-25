using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace RentData
{
    public class RentContext : DbContext
    {
        //public RentContext()
        //{
        //}

        public RentContext(DbContextOptions<RentContext> options)
        : base(options)
        {
            
        }


    public virtual DbSet<RentObject> RentObjects { get; set; }
    public virtual DbSet<RentLayer> RentLayers { get; set; }
    public virtual DbSet<RentArea> RentAreas { get; set; }
    public virtual DbSet<Renter> Renters { get; set; }
    public virtual DbSet<Staff> Staff { get; set; }
    public virtual DbSet<RentRates> RentRates { get; set; }
    public virtual DbSet<RenterFiles> RenterFiles { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<AreaPoints> AreaPoints { get; set; }

    public virtual DbSet<RentLog> RentLogs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // Database.SetInitializer<RentContext>(null);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RentObject>().HasMany(c => c.RentLayers).WithOne(x => x.Object).IsRequired();
        modelBuilder.Entity<RentLayer>().HasMany(c => c.RentAreas).WithOne(x => x.Layer).IsRequired();
        modelBuilder.Entity<Renter>().HasMany(c => c.RentAreas).WithOne(x => x.Renter);
        modelBuilder.Entity<Renter>().HasMany(c => c.RenterFiles).WithOne(x => x.Owner).IsRequired();
        modelBuilder.Entity<RentArea>().HasMany(c => c.RentRates);
        modelBuilder.Entity<RentArea>().HasMany(c => c.AreaPoints);
    }
    }
}
