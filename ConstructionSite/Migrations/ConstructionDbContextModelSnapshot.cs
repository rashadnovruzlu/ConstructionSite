﻿// <auto-generated />
using System;
using ConstructionSite.Entity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConstructionSite.Migrations
{
    [DbContext(typeof(ConstructionDbContext))]
    partial class ConstructionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConstructionSite.Entity.Models.About", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleRu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abouts");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.AboutImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AboutId")
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AboutId");

                    b.HasIndex("ImageId");

                    b.ToTable("AboutImages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lng")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.CustomerFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CustomerFeedbacks");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Galery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TitleAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleRu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Galeries");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.GaleryFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GaleryId")
                        .HasColumnType("int");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GaleryId");

                    b.HasIndex("ImageId");

                    b.ToTable("GaleryFiles");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.GaleryVido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GaleryId")
                        .HasColumnType("int");

                    b.Property<string>("VidoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GaleryId");

                    b.ToTable("GaleryVidos");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.HomePage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("HomePages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAnswerd")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TittleAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TittleRu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.NewsImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsImages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.PortfolioImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("PortfolioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PortfolioImages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PortfolioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.ProjectImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectImages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleRu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.ServiceImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceImages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Slide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleRu")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Slides");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.StaticField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("StaticFields");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.SubService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameAz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("SubServices");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.SubServiceImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<int>("SubServiceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("SubServiceId");

                    b.ToTable("SubServiceImages");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.AboutImage", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.About", "About")
                        .WithMany("AboutImages")
                        .HasForeignKey("AboutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("AboutImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.GaleryFile", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Galery", "Galery")
                        .WithMany("GaleryFiles")
                        .HasForeignKey("GaleryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("GaleryFiles")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.GaleryVido", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Galery", "Galery")
                        .WithMany("GaleryVidos")
                        .HasForeignKey("GaleryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.HomePage", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("HomePages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Image", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.NewsImage", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("NewsImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstructionSite.Entity.Models.News", "News")
                        .WithMany("NewsImages")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.PortfolioImage", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("PortfolioImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstructionSite.Entity.Models.Portfolio", "Portfolio")
                        .WithMany("PortfolioImages")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.Project", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Portfolio", "Portfolio")
                        .WithMany("Projects")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.ProjectImage", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("ProjectImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstructionSite.Entity.Models.Project", "Project")
                        .WithMany("ProjectImages")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.ServiceImage", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("ServiceImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstructionSite.Entity.Models.Service", "Service")
                        .WithMany("ServiceImages")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.SubService", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Service", "Service")
                        .WithMany("SubServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConstructionSite.Entity.Models.SubServiceImage", b =>
                {
                    b.HasOne("ConstructionSite.Entity.Models.Image", "Image")
                        .WithMany("SubServiceImages")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConstructionSite.Entity.Models.SubService", "SubService")
                        .WithMany("SubServiceImages")
                        .HasForeignKey("SubServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
