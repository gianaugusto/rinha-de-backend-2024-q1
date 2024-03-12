﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using rinha_2024_q1.Data;

#nullable disable

namespace rinha_2024_q1.Migrations
{
    [DbContext(typeof(RinhaDbContext))]
    partial class RinhaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("rinha_2024_q1.Models.ClienteExtrato", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ClienteId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClienteId"));

                    b.Property<int>("Limite")
                        .HasColumnType("integer");

                    b.Property<int>("Saldo")
                        .HasColumnType("integer");

                    b.HasKey("ClienteId");

                    b.ToTable("ClientesExtrato");
                });

            modelBuilder.Entity("rinha_2024_q1.Models.Transacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RealizadaEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Valor")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("rinha_2024_q1.Models.Transacao", b =>
                {
                    b.HasOne("rinha_2024_q1.Models.ClienteExtrato", "Extrato")
                        .WithMany("Transacaos")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Extrato");
                });

            modelBuilder.Entity("rinha_2024_q1.Models.ClienteExtrato", b =>
                {
                    b.Navigation("Transacaos");
                });
#pragma warning restore 612, 618
        }
    }
}
