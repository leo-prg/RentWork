namespace RentDB
{
    using RentDB.DataModel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class RentModel : DbContext
    {
        // Контекст настроен для использования строки подключения "RentModel" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "RentKrok.DataModel.RentModel" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "RentModel" 
        // в файле конфигурации приложения.
        public RentModel()
            // почему-то не хотело браться из конфига пока забил так
            : base("Data Source = Server; Initial Catalog = RentKrok;;Persist Security Info=True;User ID = sa; Password=empty;App=EntityFramework")
        {
            
        }
        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<RentObject> RentObjects { get; set; }
        public virtual DbSet<RentLayer> RentLayers { get; set; }
        public virtual DbSet<RentArea> RentAreas { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<RentRates> RentRates { get; set; }
        public virtual DbSet<RenterFiles> RenterFiles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<AreaPoints> AreaPoints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentObject>().HasMany(c => c.RentLayers).WithRequired(x => x.Object);
            modelBuilder.Entity<RentLayer>().HasMany(c => c.RentAreas).WithRequired(x => x.Layer);
            modelBuilder.Entity<Renter>().HasMany(c => c.RentAreas).WithOptional(x => x.Renter);
            modelBuilder.Entity<Renter>().HasMany(c => c.RenterFiles).WithRequired(x => x.Owner);
            modelBuilder.Entity<RentArea>().HasMany(c => c.RentRates);
            modelBuilder.Entity<RentArea>().HasMany(c => c.AreaPoints);
        }

    }


  

   

  


  



}