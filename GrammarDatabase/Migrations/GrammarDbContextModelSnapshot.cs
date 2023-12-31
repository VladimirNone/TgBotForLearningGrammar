﻿// <auto-generated />
using GrammarDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GrammarDatabase.Migrations
{
    [DbContext(typeof(GrammarDbContext))]
    partial class GrammarDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnswerSentence", b =>
                {
                    b.Property<int>("AnswersId")
                        .HasColumnType("integer");

                    b.Property<int>("SentencesId")
                        .HasColumnType("integer");

                    b.HasKey("AnswersId", "SentencesId");

                    b.HasIndex("SentencesId");

                    b.ToTable("AnswerSentence");
                });

            modelBuilder.Entity("GrammarDatabase.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("GrammarDatabase.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<bool>("HasPrivilege")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("NameLastCommand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("GrammarDatabase.Entities.GrammarRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CommandName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UseCases")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GrammarRules");
                });

            modelBuilder.Entity("GrammarDatabase.Entities.Sentence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GrammarRuleId")
                        .HasColumnType("integer");

                    b.Property<string>("SentenceText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GrammarRuleId");

                    b.ToTable("Sentences");
                });

            modelBuilder.Entity("AnswerSentence", b =>
                {
                    b.HasOne("GrammarDatabase.Entities.Answer", null)
                        .WithMany()
                        .HasForeignKey("AnswersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GrammarDatabase.Entities.Sentence", null)
                        .WithMany()
                        .HasForeignKey("SentencesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GrammarDatabase.Entities.Sentence", b =>
                {
                    b.HasOne("GrammarDatabase.Entities.GrammarRule", "GrammarRule")
                        .WithMany("Sentences")
                        .HasForeignKey("GrammarRuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GrammarRule");
                });

            modelBuilder.Entity("GrammarDatabase.Entities.GrammarRule", b =>
                {
                    b.Navigation("Sentences");
                });
#pragma warning restore 612, 618
        }
    }
}
