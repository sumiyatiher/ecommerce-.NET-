using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Toko_Sumi.DATAMODELS
{
    public partial class Toko_SumiContext : DbContext
    {
        public Toko_SumiContext()
        {
        }

        public Toko_SumiContext(DbContextOptions<Toko_SumiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCategory> TblCategory { get; set; }
        public virtual DbSet<TblCustomers> TblCustomers { get; set; }
        public virtual DbSet<TblOrderDetails> TblOrderDetails { get; set; }
        public virtual DbSet<TblOrderHeaders> TblOrderHeaders { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblVariant> TblVariant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=XA-006-2021;Initial Catalog=Toko_Sumi;user id=sa;Password=Xsis123.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.ToTable("tblCategory");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(250)
                    .IsFixedLength();

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasColumnName("name_category")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<TblCustomers>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PK__tblCusto__8CC9BA4645917A4A");

                entity.ToTable("tblCustomers");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate).HasColumnName("create_date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.NameCustomer)
                    .IsRequired()
                    .HasColumnName("name_customer")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            });

            modelBuilder.Entity<TblOrderDetails>(entity =>
            {
                entity.HasKey(e => e.IdDetail)
                    .HasName("PK__tblOrder__EA833808DD12124C");

                entity.ToTable("tblOrderDetails");

                entity.Property(e => e.IdDetail).HasColumnName("id_detail");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.IdHeader).HasColumnName("id_header");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.SumPrice)
                    .HasColumnName("sum_price")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<TblOrderHeaders>(entity =>
            {
                entity.HasKey(e => e.IdHeader)
                    .HasName("PK__tblOrder__75F6DF6B9BB1BB60");

                entity.ToTable("tblOrderHeaders");

                entity.Property(e => e.IdHeader).HasColumnName("id_header");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CodeTransaction)
                    .IsRequired()
                    .HasColumnName("code_transaction")
                    .HasMaxLength(20);

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.IdCustomer).HasColumnName("id_customer");

                entity.Property(e => e.IsCheckout).HasColumnName("is_checkout");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.TotalQty).HasColumnName("total_qty");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.ToTable("tblProduct");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.IdVariant).HasColumnName("id_variant");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.NameProduct)
                    .IsRequired()
                    .HasColumnName("name_product")
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<TblVariant>(entity =>
            {
                entity.HasKey(e => e.IdVariant);

                entity.ToTable("tblVariant");

                entity.Property(e => e.IdVariant).HasColumnName("id_variant");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.NameVariant)
                    .IsRequired()
                    .HasColumnName("name_variant")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateBy).HasColumnName("update_by");

                entity.Property(e => e.UpdateDate)
                    .HasColumnName("update_date")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
