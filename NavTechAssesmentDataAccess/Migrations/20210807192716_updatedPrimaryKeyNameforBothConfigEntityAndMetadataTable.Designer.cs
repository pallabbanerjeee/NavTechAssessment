﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NavTechAssesment.DataAccess.EntityFramework;

namespace NavTechAssesment.DataAccess.Migrations
{
    [DbContext(typeof(NavTechDbContext))]
    [Migration("20210807192716_updatedPrimaryKeyNameforBothConfigEntityAndMetadataTable")]
    partial class updatedPrimaryKeyNameforBothConfigEntityAndMetadataTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NavTechAssesment.DataAccess.Entities.ConfigEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<DateTime>("CreatedDatetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(GETUTCDATE())");

                    b.Property<DateTime?>("DeletedDatetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ConfigEntities");
                });

            modelBuilder.Entity("NavTechAssesment.DataAccess.Entities.ConfigEntityMetadata", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<DateTime>("CreatedDatetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(GETUTCDATE())");

                    b.Property<DateTime?>("DeletedDatetime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Entity_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<int>("MaxLength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Entity_Id");

                    b.ToTable("ConfigEntityMetadatas");
                });

            modelBuilder.Entity("NavTechAssesment.DataAccess.Entities.ConfigEntityMetadata", b =>
                {
                    b.HasOne("NavTechAssesment.DataAccess.Entities.ConfigEntity", "ConfigEntity")
                        .WithMany("ConfigEntityMetadatas")
                        .HasForeignKey("Entity_Id")
                        .HasConstraintName("FK_ConfigEntityMetadata_ConfigEntity")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConfigEntity");
                });

            modelBuilder.Entity("NavTechAssesment.DataAccess.Entities.ConfigEntity", b =>
                {
                    b.Navigation("ConfigEntityMetadatas");
                });
#pragma warning restore 612, 618
        }
    }
}
