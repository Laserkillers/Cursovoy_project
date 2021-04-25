using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Cursovoy_project
{
    public partial class AutoServiceContext : DbContext
    {
        public AutoServiceContext()
        {
        }

        public AutoServiceContext(DbContextOptions<AutoServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AutoService> AutoServices { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientsCarDatum> ClientsCarData { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersDatum> UsersData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Username=user_from_app;Password=12345;Database=AutoService;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1252");

            modelBuilder.Entity<AutoService>(entity =>
            {
                entity.ToTable("auto_service");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .HasColumnName("cost");

                entity.Property(e => e.Fault).HasColumnName("fault");

                entity.Property(e => e.GosNumber)
                    .IsRequired()
                    .HasColumnName("gos_number");

                entity.Property(e => e.IssureTime).HasColumnName("issure_time");

                entity.Property(e => e.ReceptionTime).HasColumnName("reception_time");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");
            });

            modelBuilder.Entity<ClientsCarDatum>(entity =>
            {
                entity.ToTable("clients_car_data");

                entity.HasIndex(e => e.GosNumber, "Gos")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarBrend)
                    .IsRequired()
                    .HasColumnName("car_brend");

                entity.Property(e => e.CarModel)
                    .IsRequired()
                    .HasColumnName("car_model");

                entity.Property(e => e.GosNumber)
                    .IsRequired()
                    .HasColumnName("gos_number");

                entity.Property(e => e.Odometr).HasColumnName("odometr");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.TypeOfAccount).HasColumnName("type_of_account");
            });

            modelBuilder.Entity<UsersDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users_data");

                entity.Property(e => e.DateBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_birth");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.MiddleName).HasColumnName("middle_name");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.Property(e => e.Telephone).HasColumnName("telephone");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
