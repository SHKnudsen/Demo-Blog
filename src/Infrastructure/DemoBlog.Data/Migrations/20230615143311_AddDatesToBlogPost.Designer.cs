﻿// <auto-generated />
using System;
using DemoBlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoBlog.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230615143311_AddDatesToBlogPost")]
    partial class AddDatesToBlogPost
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DemoBlog.Domain.Entities.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Published")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasMaxLength(240)
                        .HasColumnType("nvarchar(240)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("nvarchar(160)");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
