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
    [Migration("20190511053742_AddLikesToReplies")]
    partial class AddLikesToReplies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
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

            modelBuilder.Entity("Publishing.Domain.Entities.Threads.Like", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("IsLiked");

                    b.Property<string>("ThreadReplyId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Likes");
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
