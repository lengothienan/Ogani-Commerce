namespace Library
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AddressOrder> AddressOrders { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<FeeShip> FeeShips { get; set; }
        public virtual DbSet<InforOrder> InforOrders { get; set; }
        public virtual DbSet<LoveProduct> LoveProducts { get; set; }
        public virtual DbSet<PayOrder> PayOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOfEvent> ProductOfEvents { get; set; }
        public virtual DbSet<ProductOfType> ProductOfTypes { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.ProductOfEvents)
                .WithRequired(e => e.Event)
                .HasForeignKey(e => e.IDE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FeeShip>()
                .HasMany(e => e.AddressOrders)
                .WithOptional(e => e.FeeShip1)
                .HasForeignKey(e => e.FeeShip);

            modelBuilder.Entity<PayOrder>()
                .HasMany(e => e.InforOrders)
                .WithRequired(e => e.PayOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.InforOrders)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.LoveProducts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductOfEvents)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductOfTypes)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.ProductOfTypes)
                .WithRequired(e => e.ProductType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.LoveProducts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PayOrders)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UID);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.UID);
        }
    }
}
