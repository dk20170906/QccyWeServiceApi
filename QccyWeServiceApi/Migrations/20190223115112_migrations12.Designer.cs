﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QccyWeServiceApi.EF;

namespace QccyWeServiceApi.Migrations
{
    [DbContext(typeof(WebApiDbContext))]
    [Migration("20190223115112_migrations12")]
    partial class migrations12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QccyWeServiceApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Access_token");

                    b.Property<string>("Accounts");

                    b.Property<string>("Email");

                    b.Property<string>("EobilePhone");

                    b.Property<string>("Password");

                    b.Property<string>("Remark");

                    b.Property<string>("Remember");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserName");

                    b.Property<string>("Vercode");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}