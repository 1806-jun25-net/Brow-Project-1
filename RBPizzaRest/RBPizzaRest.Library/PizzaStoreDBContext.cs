using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RBPizzaRest.Library
{
    public partial class PizzaStoreDBContext : DbContext
    {
        public PizzaStoreDBContext()
        {
        }

        public PizzaStoreDBContext(DbContextOptions<PizzaStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Toppings> Toppings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Store");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(100);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(12);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "Store");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders", "Store");

                entity.Property(e => e.CustomerLastname)
                    .IsRequired()
                    .HasColumnName("customerLastname")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("customerName")
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerPhoneNumber)
                    .IsRequired()
                    .HasColumnName("customerPhoneNumber")
                    .HasMaxLength(100);

                entity.Property(e => e.OrderDate)
                    .HasColumnName("orderDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderLocaton)
                    .IsRequired()
                    .HasColumnName("orderLocaton")
                    .HasMaxLength(100);

                entity.Property(e => e.OrderNumber).HasColumnName("orderNumber");

                entity.Property(e => e.PizzaFprice).HasColumnName("pizzaFPrice");

                entity.Property(e => e.PizzaPrice).HasColumnName("pizzaPrice");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("storeName")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza", "Store");

                entity.Property(e => e.GarlicCrust).HasColumnName("garlicCrust");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasColumnName("size")
                    .HasMaxLength(50);

                entity.Property(e => e.Topping)
                    .IsRequired()
                    .HasColumnName("topping")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.ToTable("Stores", "Store");

                entity.Property(e => e.Cheese).HasColumnName("cheese");

                entity.Property(e => e.Dough).HasColumnName("dough");

                entity.Property(e => e.Sauce).HasColumnName("sauce");

                entity.Property(e => e.StoreLocation)
                    .IsRequired()
                    .HasColumnName("storeLocation")
                    .HasMaxLength(100);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasColumnName("storeName")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Toppings>(entity =>
            {
                entity.ToTable("Toppings", "Store");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.Topping)
                    .IsRequired()
                    .HasColumnName("topping")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Toppings)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Topping_Store");
            });
        }
    }
}
