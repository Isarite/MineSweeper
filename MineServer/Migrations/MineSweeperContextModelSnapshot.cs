﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MineServer.Models;

namespace MineServer.Migrations
{
    [DbContext(typeof(MineSweeperContext))]
    partial class MineSweeperContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MineServer.Models.Cell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("bombs");

                    b.Property<int?>("mapId")
                        .IsRequired();

                    b.Property<bool>("marked");

                    b.Property<int>("number");

                    b.HasKey("Id");

                    b.HasIndex("mapId");

                    b.ToTable("Cells");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Cell");
                });

            modelBuilder.Entity("MineServer.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameMapId");

                    b.HasKey("Id");

                    b.HasIndex("GameMapId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MineServer.Models.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("MineServer.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("TurnsLeft");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int?>("currentGameId");

                    b.Property<int>("role");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("currentGameId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MineServer.Models.PlayerStrategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("playerId");

                    b.HasKey("Id");

                    b.HasIndex("playerId");

                    b.ToTable("Strategies");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PlayerStrategy");
                });

            modelBuilder.Entity("MineServer.Models.ExplodedTnt", b =>
                {
                    b.HasBaseType("MineServer.Models.Cell");


                    b.ToTable("ExplodedTnt");

                    b.HasDiscriminator().HasValue("ExplodedTnt");
                });

            modelBuilder.Entity("MineServer.Models.Revealed", b =>
                {
                    b.HasBaseType("MineServer.Models.Cell");


                    b.ToTable("Revealed");

                    b.HasDiscriminator().HasValue("Revealed");
                });

            modelBuilder.Entity("MineServer.Models.Tnt", b =>
                {
                    b.HasBaseType("MineServer.Models.Cell");


                    b.ToTable("Tnt");

                    b.HasDiscriminator().HasValue("Tnt");
                });

            modelBuilder.Entity("MineServer.Models.Unknown", b =>
                {
                    b.HasBaseType("MineServer.Models.Cell");


                    b.ToTable("Unknown");

                    b.HasDiscriminator().HasValue("Unknown");
                });

            modelBuilder.Entity("MineServer.Models.WrongTnt", b =>
                {
                    b.HasBaseType("MineServer.Models.Cell");


                    b.ToTable("WrongTnt");

                    b.HasDiscriminator().HasValue("WrongTnt");
                });

            modelBuilder.Entity("MineServer.Models.MarkCell", b =>
                {
                    b.HasBaseType("MineServer.Models.PlayerStrategy");


                    b.ToTable("MarkCell");

                    b.HasDiscriminator().HasValue("MarkCell");
                });

            modelBuilder.Entity("MineServer.Models.RevealCell", b =>
                {
                    b.HasBaseType("MineServer.Models.PlayerStrategy");


                    b.ToTable("RevealCell");

                    b.HasDiscriminator().HasValue("RevealCell");
                });

            modelBuilder.Entity("MineServer.Models.SetMine", b =>
                {
                    b.HasBaseType("MineServer.Models.PlayerStrategy");


                    b.ToTable("SetMine");

                    b.HasDiscriminator().HasValue("SetMine");
                });

            modelBuilder.Entity("MineServer.Models.UnsetMine", b =>
                {
                    b.HasBaseType("MineServer.Models.PlayerStrategy");


                    b.ToTable("UnsetMine");

                    b.HasDiscriminator().HasValue("UnsetMine");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MineServer.Models.Player")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MineServer.Models.Player")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MineServer.Models.Player")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MineServer.Models.Player")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MineServer.Models.Cell", b =>
                {
                    b.HasOne("MineServer.Models.Map", "map")
                        .WithMany("_cells")
                        .HasForeignKey("mapId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MineServer.Models.Game", b =>
                {
                    b.HasOne("MineServer.Models.Map", "GameMap")
                        .WithMany()
                        .HasForeignKey("GameMapId");
                });

            modelBuilder.Entity("MineServer.Models.Player", b =>
                {
                    b.HasOne("MineServer.Models.Game", "currentGame")
                        .WithMany("players")
                        .HasForeignKey("currentGameId");
                });

            modelBuilder.Entity("MineServer.Models.PlayerStrategy", b =>
                {
                    b.HasOne("MineServer.Models.Player", "player")
                        .WithMany("strategies")
                        .HasForeignKey("playerId");
                });
#pragma warning restore 612, 618
        }
    }
}
