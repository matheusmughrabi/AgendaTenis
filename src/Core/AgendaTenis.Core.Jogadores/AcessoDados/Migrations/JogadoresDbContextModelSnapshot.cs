﻿// <auto-generated />
using System;
using AgendaTenis.Core.Jogadores.AcessoDados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgendaTenis.Core.Jogadores.AcessoDados.Migrations
{
    [DbContext(typeof(JogadoresDbContext))]
    partial class JogadoresDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AgendaTenis.Core.Jogadores.Dominio.JogadorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Backhand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstiloDeJogo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaoDominante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Nome");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PontuacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Sobrenome");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PontuacaoId")
                        .IsUnique()
                        .HasFilter("[PontuacaoId] IS NOT NULL");

                    b.ToTable("Jogador", (string)null);
                });

            modelBuilder.Entity("AgendaTenis.Core.Jogadores.Dominio.PontuacaoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("JogadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("PontuacaoAtual")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Pontuacao", (string)null);
                });

            modelBuilder.Entity("AgendaTenis.Core.Jogadores.Dominio.JogadorEntity", b =>
                {
                    b.HasOne("AgendaTenis.Core.Jogadores.Dominio.PontuacaoEntity", "Pontuacao")
                        .WithOne("Jogador")
                        .HasForeignKey("AgendaTenis.Core.Jogadores.Dominio.JogadorEntity", "PontuacaoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Pontuacao");
                });

            modelBuilder.Entity("AgendaTenis.Core.Jogadores.Dominio.PontuacaoEntity", b =>
                {
                    b.Navigation("Jogador")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
