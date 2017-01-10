using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HamburgerUI.Services.RepositoryServices;

namespace HamburgerUI.Migrations
{
    [DbContext(typeof(EFRepositoryContext))]
    [Migration("20170109014305_FileChangeConstructor")]
    partial class FileChangeConstructor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("HamburgerUI.Models.Archive", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Archives");
                });

            modelBuilder.Entity("HamburgerUI.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArchiveId");

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<string>("Extension");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<ulong>("Size");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("HamburgerUI.Models.File", b =>
                {
                    b.HasOne("HamburgerUI.Models.Archive", "Archive")
                        .WithMany("Files")
                        .HasForeignKey("ArchiveId");
                });
        }
    }
}
