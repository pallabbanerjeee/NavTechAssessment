using Microsoft.EntityFrameworkCore;
using NavTechAssesment.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavTechAssesment.DataAccess.EntityFramework
{
    public partial class NavTechDbContext : DbContext
    {
        public NavTechDbContext(DbContextOptions<NavTechDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ConfigEntity> ConfigEntities { get; set; }
        public virtual DbSet<ConfigEntityMetadata> ConfigEntityMetadatas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigEntity>(entity =>
            {

                entity.Property(c => c.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(c => c.EntityName)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(c => c.CreatedDatetime)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE())");

                entity.Property(c => c.DeletedDatetime);

            });

            modelBuilder.Entity<ConfigEntityMetadata>(entity =>
            {

                entity.Property(c => c.Id).HasDefaultValueSql("(newsequentialid())");

                entity.Property(c => c.FieldName)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(c => c.MaxLength).IsRequired();

                entity.Property(c => c.IsRequired).IsRequired();

                entity.Property(c => c.CreatedDatetime)
                .IsRequired()
                .HasDefaultValueSql("(GETUTCDATE())");

                entity.Property(c => c.DeletedDatetime);

                entity.HasOne(c => c.ConfigEntity)
                .WithMany(c => c.ConfigEntityMetadatas)
                .HasForeignKey(c => c.Entity_Id)
                .HasConstraintName("FK_ConfigEntityMetadata_ConfigEntity");

            });
        }


    }
}
