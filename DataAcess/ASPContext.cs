using Domain;
using Domain.Join_Tables;
using Domain.LookupTables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class ASPContext:DbContext
    {
        private readonly string connection_string;
        public ASPContext()
        {
            this.connection_string = "Data Source=DESKTOP-INTENQ7\\SQLEXPRESS;Initial Catalog=moj_sajt_asp;Integrated Security=True;Trust Server Certificate=True";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection_string).UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Price> Product_Prices { get; set; }
        public DbSet<Product_Color> Product_Colors { get; set; }
        public DbSet<Product_Specification> Product_Specifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product_Cart> Product_Carts { get; set;}
        public DbSet<UserUseCase> UserUseCases { get; set; }
    }
}
