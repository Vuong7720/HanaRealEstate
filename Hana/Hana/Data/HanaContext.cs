using System;
using Hana.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hana.Data
{
    public partial class HanaContext : DbContext
    {
        public HanaContext()
        {
        }

        public HanaContext(DbContextOptions<HanaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AboutUs> AboutUs { get; set; }
        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Faq> Faq { get; set; }
        public virtual DbSet<Level> Level { get; set; }
        public virtual DbSet<Map> Map { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<RealEstate> RealEstate { get; set; }
        public virtual DbSet<RealEstateDetail> RealEstateDetail { get; set; }
        public virtual DbSet<RealEstateType> RealEstateType { get; set; }
        public virtual DbSet<SocialLogin> SocialLogin { get; set; }
        public virtual DbSet<Ward> Ward { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=.;Database=Hana;User Id=sa;Password=123;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AboutUs>(entity =>
            {
                entity.ToTable("ABOUT_US");

                entity.Property(e => e.Content).HasColumnType("ntext");
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("AGENT");

                entity.Property(e => e.ActiveKey)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.AgentName).HasMaxLength(100);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Facebook).HasMaxLength(100);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LoginName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ResetPasswordKey)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Zalo).HasMaxLength(100);

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.Agent)
                    .HasForeignKey(d => d.LevelId)
                   .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__AGENT__LevelId__3B75D760");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("CITY");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("DISTRICT");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__DISTRICT__CityId__145C0A3F");
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");

                entity.Property(e => e.Answer).HasColumnType("ntext");

                entity.Property(e => e.Question).HasColumnType("ntext");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("LEVEL");

                entity.Property(e => e.LevelName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Map>(entity =>
            {
                entity.ToTable("MAP");

                entity.HasIndex(e => e.RealEstateId)
                    .HasName("UQ__MAP__C037863418DC284F")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Latitude).HasColumnType("decimal(9,7)");

                entity.Property(e => e.Longtitude).HasColumnType("decimal(10,7)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Map)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__MAP__CityId__48CFD27E");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Map)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__MAP__DistrictId__47DBAE45");

                entity.HasOne(d => d.RealEstate)
                    .WithOne(p => p.Map)
                    .HasForeignKey<Map>(d => d.RealEstateId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__MAP__RealEstateI__49C3F6B7");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Map)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK__MAP__WardId__46E78A0C");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("PICTURE");

                entity.Property(e => e.PictureName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.RealEstate)
                    .WithMany(p => p.Picture)
                    .HasForeignKey(d => d.RealEstateId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__PICTURE__RealEst__4CA06362");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.ToTable("POLICY");

                entity.Property(e => e.PolicyContent).HasColumnType("ntext");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("RATING");

                entity.HasOne(d => d.RealEstate)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.RealEstateId)
                   .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__RATING__RealEsta__4F7CD00D");
            });

            modelBuilder.Entity<RealEstate>(entity =>
            {
                entity.ToTable("REAL_ESTATE");

                entity.Property(e => e.ExprireTime).HasColumnType("datetime");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.PostTime).HasColumnType("datetime");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.RealEstate)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__REAL_ESTA__Agent__3F466844");

                entity.HasOne(d => d.ReaEstateType)
                    .WithMany(p => p.RealEstate)
                    .HasForeignKey(d => d.RealEstateTypeId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__REAL_ESTA__ReaEs__3E52440B");

                // -----------------------------------------------------------------------------------------------change---->
                entity.HasMany(r => r.Picture)
                      .WithOne(p => p.RealEstate)
                      .HasForeignKey(p => p.RealEstateId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RealEstateDetail>(entity =>
            {
                entity.ToTable("REAL_ESTATE_DETAIL");

                entity.HasIndex(e => e.RealEstateId)
                    .HasName("UQ__REAL_EST__C0378634981FED9E")
                    .IsUnique();

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.HasPrivateWc).HasColumnName("HasPrivateWC");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Title).HasMaxLength(300);

                entity.Property(e => e.WifiPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.RealEstate)
                    .WithOne(p => p.RealEstateDetail)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasForeignKey<RealEstateDetail>(d => d.RealEstateId)
                    .HasConstraintName("FK__REAL_ESTA__RealE__5629CD9C");
            });

            modelBuilder.Entity<RealEstateType>(entity =>
            {
                entity.ToTable("REAL_ESTATE_TYPE");

                entity.Property(e => e.RealEstateTypeName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SocialLogin>(entity =>
            {
                entity.HasKey(e => e.ProviderKey)
                    .HasName("PK__SOCIAL_L__8DE43C5E9E312291");

                entity.ToTable("SOCIAL_LOGIN");

                entity.Property(e => e.ProviderKey).HasMaxLength(300);

                entity.Property(e => e.Provider)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SocialLogin)
                    .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK__SOCIAL_LO__UserI__52593CB8");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("WARD");

                entity.Property(e => e.WardName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Ward)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK__WARD__DistrictId__173876EA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
