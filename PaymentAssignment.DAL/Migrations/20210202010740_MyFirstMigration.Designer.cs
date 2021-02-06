﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentAssignment.DAL.DBContext;

namespace PaymentAssignment.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210202010740_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PaymentAssignment.DAL.Entity.Payment", b =>
                {
                    b.Property<long>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CardHolder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityCode")
                        .HasColumnName("SecurityCode")
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("PaymentId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("PaymentAssignment.DAL.Entity.PaymentState", b =>
                {
                    b.Property<long>("PaymentStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<long>("PaymentId")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentStateId");

                    b.HasIndex("PaymentId");

                    b.ToTable("PaymentState");
                });

            modelBuilder.Entity("PaymentAssignment.DAL.Entity.PaymentState", b =>
                {
                    b.HasOne("PaymentAssignment.DAL.Entity.Payment", "Payment")
                        .WithMany("PaymentStates")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
