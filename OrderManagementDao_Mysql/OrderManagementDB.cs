using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderManagementModel.DBModel.Authority;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementDao_Mysql
{
    public class OrderManagementDB : DbContext
    {
        public DbSet<Authority_Account> Authority_Account { get; set; }
        public DbSet<Authority_Role> Authority_Role { get; set; }
        public DbSet<Authority_PermissionBase> Authority_PermissionBase { get; set; }
        public DbSet<Authority_RelatedAccountRole> Authority_RelatedAccountRole { get; set; }
        public DbSet<Authority_RelatedRoleBasePer> Authority_RelatedRoleBasePer { get; set; }

        //  public IConfiguration Configuration { get; }

        string CurrentConnectionString;

        public static string ConnectionString;
        ///// <summary>
        ///// 连接字符串配置名字
        ///// </summary>
        //public static string ConnectionConfigName= "HqOrder";

        public OrderManagementDB(string connectionString)
        {
            CurrentConnectionString = connectionString;
        }
        public OrderManagementDB()
        {
            CurrentConnectionString = ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ///添加mysql模块
            optionsBuilder.UseMySql(CurrentConnectionString);

            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authority_Account>
                (build =>
                    {
                        build.HasKey(p => p.ID);

                        build.Property(p => p.Name)
                         .HasColumnType("varchar(200)")
                         .IsRequired();
                        build.Property(p => p.Account)
                         .HasColumnType("varchar(200)")
                         .IsRequired();

                        build.Property(p => p.Pass)
                         .HasColumnType("varchar(200)")
                         .IsRequired();

                        build.HasMany(p => p.RelatedRoles)
                        .WithOne(p => p.Account)
                        .HasPrincipalKey(p => p.ID)
                        .HasForeignKey(p => p.AccountID);

                        build.HasIndex(p => p.Account);

                    }
                );

            modelBuilder.Entity<Authority_Role>
                (build =>
                    {
                        build.HasKey(p => p.ID);

                        build.Property(p => p.RoleName)
                         .HasColumnType("nvarchar(200)")
                         .IsRequired();

                        build.HasMany(p => p.RelatedAccounts)
                        .WithOne(p => p.Role)
                        .HasPrincipalKey(p => p.ID)
                        .HasForeignKey(p => p.RoleID);

                        build.HasMany(p => p.RelatedRoleBases)
                        .WithOne(p => p.Role)
                        .HasPrincipalKey(p => p.ID)
                        .HasForeignKey(p => p.RoleID);

                        build.HasIndex(p => p.RoleName);

                    }
                );

            modelBuilder.Entity<Authority_PermissionBase>
                (build =>
                    {
                        build.HasKey(p => p.ID);

                        build.Property(p => p.Area)
                         .HasColumnType("nvarchar(400)")
                         .IsRequired();

                        build.Property(p => p.Control)
                         .HasColumnType("nvarchar(200)")
                         .IsRequired();

                        build.Property(p => p.Action)
                         .HasColumnType("nvarchar(200)")
                         .IsRequired();

                        build.HasMany(p => p.RelatedRoles)
                        .WithOne(p => p.BasePer)
                        .HasPrincipalKey(p => p.ID)
                        .HasForeignKey(p => p.BasePerID);

                        build.HasIndex(p => p.Area);
                        build.HasIndex(p => p.Control);
                        build.HasIndex(p => p.Action);
                    }
                );


            modelBuilder.Entity<Authority_RelatedAccountRole>
                (build =>
                    {
                        build.HasKey(p => p.ID);
                    }
                );

            modelBuilder.Entity<Authority_RelatedRoleBasePer>
                (build =>
                    {
                        build.HasKey(p => p.ID);

                    }
                );

        }
    }
}
