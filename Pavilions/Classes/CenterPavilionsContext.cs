using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Pavilions
{
    public partial class CenterPavilionsContext : DbContext
    {
        public CenterPavilionsContext()
        {
        }

        public CenterPavilionsContext(DbContextOptions<CenterPavilionsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CityList> CityLists { get; set; }
        public virtual DbSet<Pavilion> Pavilions { get; set; }
        public virtual DbSet<PavilionsCentersList> PavilionsCentersLists { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShoppingCenter> ShoppingCenters { get; set; }
        public virtual DbSet<Staffer> Staffers { get; set; }
        public virtual DbSet<StatusList> StatusLists { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-62MFGKR; Database=CenterPavilions; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<CityList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CityList");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pavilion>(entity =>
            {
                entity.HasKey(e => e.IdPavilion);

                entity.Property(e => e.NumberPavilion)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatusPavilion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdShopCenterNavigation)
                    .WithMany(p => p.Pavilions)
                    .HasForeignKey(d => d.IdShopCenter)
                    .HasConstraintName("FK__Pavilions__IdSho__398D8EEE");
            });

            modelBuilder.Entity<PavilionsCentersList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PavilionsCentersList");

                entity.Property(e => e.NameCenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberPavilion)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StatusCenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusPavilion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rent>(entity =>
            {
                entity.HasKey(e => e.IdRent)
                    .HasName("PK__Rent__8C7628BD85B97CFC");

                entity.ToTable("Rent");

                entity.Property(e => e.EndDateRent).HasColumnType("date");

                entity.Property(e => e.StartDateRent).HasColumnType("date");

                entity.Property(e => e.StatusRent)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPavilionNavigation)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.IdPavilion)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Rent_Pavilions");

                entity.HasOne(d => d.IdRenterNavigation)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.IdRenter)
                    .HasConstraintName("FK__Rent__Statusrent__403A8C7D");

                entity.HasOne(d => d.IdStafferNavigation)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.IdStaffer)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Rent_Staffers");
            });

            modelBuilder.Entity<Renter>(entity =>
            {
                entity.HasKey(e => e.IdRenter)
                    .HasName("PK__Renters__F25CEAB5CDC87226");

                entity.Property(e => e.AddressRenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameRenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberTelephon)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Roles__B436905430F8C240");

                entity.Property(e => e.NameRole)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingCenter>(entity =>
            {
                entity.HasKey(e => e.IdShopCenter)
                    .HasName("PK__Shopping__3BC2B47C168AEB27");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImageCenter).HasColumnType("image");

                entity.Property(e => e.NameCenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusCenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Staffer>(entity =>
            {
                entity.HasKey(e => e.IdStaffer);

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameStaffer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberTelephon)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Paul)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Photo).HasColumnType("image");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StatusList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("StatusList");

                entity.Property(e => e.StatusCenter)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdStaffer)
                    .HasName("PK__Users__C9BAA3E5315FD161");

                entity.Property(e => e.IdStaffer).ValueGeneratedNever();

                entity.Property(e => e.LoginUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK__Users__IdRole__47DBAE45");

                entity.HasOne(d => d.IdStafferNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.IdStaffer)
                    .HasConstraintName("FK_Users_Staffers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
