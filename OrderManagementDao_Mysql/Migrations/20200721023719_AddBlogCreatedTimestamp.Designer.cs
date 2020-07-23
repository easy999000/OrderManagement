﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderManagementDao_Mysql;

namespace OrderManagementDao_Mysql.Migrations
{
    [DbContext(typeof(OrderManagementDB))]
    [Migration("20200721023719_AddBlogCreatedTimestamp")]
    partial class AddBlogCreatedTimestamp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OrderManagementModel.DBModel.Authority.Authority_Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("Account");

                    b.ToTable("Authority_Account");
                });

            modelBuilder.Entity("OrderManagementModel.DBModel.Authority.Authority_PermissionBase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Control")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.HasIndex("Action");

                    b.HasIndex("Area");

                    b.HasIndex("Control");

                    b.ToTable("Authority_PermissionBase");
                });

            modelBuilder.Entity("OrderManagementModel.DBModel.Authority.Authority_RelatedAccountRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.HasIndex("RoleID");

                    b.ToTable("Authority_RelatedAccountRole");
                });

            modelBuilder.Entity("OrderManagementModel.DBModel.Authority.Authority_RelatedRoleBasePer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BasePerID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BasePerID");

                    b.HasIndex("RoleID");

                    b.ToTable("Authority_RelatedRoleBasePer");
                });

            modelBuilder.Entity("OrderManagementModel.DBModel.Authority.Authority_Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ID");

                    b.HasIndex("RoleName");

                    b.ToTable("Authority_Role");
                });

            modelBuilder.Entity("OrderManagementModel.DBModel.Authority.Authority_RelatedAccountRole", b =>
                {
                    b.HasOne("OrderManagementModel.DBModel.Authority.Authority_Account", "Account")
                        .WithMany("RelatedRoles")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderManagementModel.DBModel.Authority.Authority_Role", "Role")
                        .WithMany("RelatedAccounts")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderManagementModel.DBModel.Authority.Authority_RelatedRoleBasePer", b =>
                {
                    b.HasOne("OrderManagementModel.DBModel.Authority.Authority_PermissionBase", "BasePer")
                        .WithMany("RelatedRoles")
                        .HasForeignKey("BasePerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderManagementModel.DBModel.Authority.Authority_Role", "Role")
                        .WithMany("RelatedRoleBases")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
