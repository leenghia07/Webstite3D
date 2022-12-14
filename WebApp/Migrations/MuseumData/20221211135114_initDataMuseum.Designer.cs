// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data;

#nullable disable

namespace WebApp.Migrations.MuseumData
{
    [DbContext(typeof(MuseumDataContext))]
    [Migration("20221211135114_initDataMuseum")]
    partial class initDataMuseum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApp.Models.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ArtifactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ExhibitionRoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<Guid?>("TypeOfArticleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ArtifactId");

                    b.HasIndex("ExhibitionRoomId");

                    b.HasIndex("TypeOfArticleId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("WebApp.Models.Artifact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DiscoveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("File3D")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MuseumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameArtifact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TypeOfArtifactId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MuseumId");

                    b.HasIndex("TypeOfArtifactId");

                    b.ToTable("Aritifact");
                });

            modelBuilder.Entity("WebApp.Models.ExhibitionRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("File3D")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MuseumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MuseumId");

                    b.ToTable("ExhibitionRoom");
                });

            modelBuilder.Entity("WebApp.Models.Museum", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MuseumName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Museum");
                });

            modelBuilder.Entity("WebApp.Models.TypeOfArticle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfArticle");
                });

            modelBuilder.Entity("WebApp.Models.TypeOfArtifact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameTypeArtifact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfArtifact");
                });

            modelBuilder.Entity("WebApp.Models.Article", b =>
                {
                    b.HasOne("WebApp.Models.Artifact", "Artifact")
                        .WithMany()
                        .HasForeignKey("ArtifactId");

                    b.HasOne("WebApp.Models.ExhibitionRoom", "ExhibitionRoom")
                        .WithMany()
                        .HasForeignKey("ExhibitionRoomId");

                    b.HasOne("WebApp.Models.TypeOfArticle", "TypeOfArticle")
                        .WithMany()
                        .HasForeignKey("TypeOfArticleId");

                    b.Navigation("Artifact");

                    b.Navigation("ExhibitionRoom");

                    b.Navigation("TypeOfArticle");
                });

            modelBuilder.Entity("WebApp.Models.Artifact", b =>
                {
                    b.HasOne("WebApp.Models.Museum", "Museum")
                        .WithMany()
                        .HasForeignKey("MuseumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Models.TypeOfArtifact", "TypeOfArtifact")
                        .WithMany()
                        .HasForeignKey("TypeOfArtifactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Museum");

                    b.Navigation("TypeOfArtifact");
                });

            modelBuilder.Entity("WebApp.Models.ExhibitionRoom", b =>
                {
                    b.HasOne("WebApp.Models.Museum", "Museum")
                        .WithMany()
                        .HasForeignKey("MuseumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Museum");
                });
#pragma warning restore 612, 618
        }
    }
}
