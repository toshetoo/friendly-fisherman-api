﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Publishing.DataAccess;

namespace Publishing.DataAccess.Migrations
{
    [DbContext(typeof(PublishingDbContext))]
    [Migration("20190408140231_AddThreadsAndReplies")]
    partial class AddThreadsAndReplies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Publishing.Domain.Entities.Categories.ThreadCategory", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ThreadCategories");
                });

            modelBuilder.Entity("Publishing.Domain.Entities.Threads.SeenCount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ThreadId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ThreadSeenCounts");
                });

            modelBuilder.Entity("Publishing.Domain.Entities.Threads.Thread", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("CategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Publishing.Domain.Entities.Threads.ThreadReply", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ThreadId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("ThreadReplies");
                });
#pragma warning restore 612, 618
        }
    }
}
