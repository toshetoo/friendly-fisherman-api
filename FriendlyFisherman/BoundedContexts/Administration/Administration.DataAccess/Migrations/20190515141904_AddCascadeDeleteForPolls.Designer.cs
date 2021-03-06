﻿// <auto-generated />
using System;
using Administration.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Administration.DataAccess.Migrations
{
    [DbContext(typeof(AdministrationDbContext))]
    [Migration("20190515141904_AddCascadeDeleteForPolls")]
    partial class AddCascadeDeleteForPolls
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Administration.Domain.Entities.Events.Event", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("EventStatus");

                    b.Property<string>("ImageCover");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Administration.Domain.Entities.Events.EventComment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("CreatorId");

                    b.Property<string>("EventId");

                    b.HasKey("Id");

                    b.ToTable("EventComments");
                });

            modelBuilder.Entity("Administration.Domain.Entities.Events.EventParticipant", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EventId");

                    b.Property<int>("ParticipantStatus");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("Administration.Domain.Entities.Polls.Poll", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("EndOn");

                    b.Property<string>("Question");

                    b.HasKey("Id");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("Administration.Domain.Entities.Polls.PollAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("PollId");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.ToTable("PollAnswers");
                });

            modelBuilder.Entity("Administration.Domain.Entities.Polls.UserPollAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerIndex");

                    b.Property<string>("PollId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserPollAnswers");
                });

            modelBuilder.Entity("Administration.Domain.Entities.Polls.PollAnswer", b =>
                {
                    b.HasOne("Administration.Domain.Entities.Polls.Poll", "Poll")
                        .WithMany("Answers")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
