using System;
using System.IO;
using System.Linq;
using MasterReport.Data.Entites;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterReport.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "MyDb.db" };

            var connectionString = connectionStringBuilder.ToString();

            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
        */
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlite($"Data Source={Path.Combine(Environment.CurrentDirectory, "db.db")}");
        //}

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            mb.Entity<DataSourceType>()
                .HasData(
                    new DataSourceType { DataSourceTypeId = 1, DataSourceName = "Microsoft SQL Server" },
                    new DataSourceType { DataSourceTypeId = 2, DataSourceName = "MySQL" },
                    new DataSourceType { DataSourceTypeId = 3, DataSourceName = "PostgreSQL" }
                    );

            mb.Entity<ExecutionType>()
                .HasData(
                    new ExecutionType { ExecutionTypeId = 1, TypeName = "Manual" },
                    new ExecutionType { ExecutionTypeId = 2, TypeName = "Scheduled" }
                    );

            mb.Entity<User>()
                .HasData(new User
                {
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Admin",
                    Email = "admin@admin.com",
                    Password = "$2a$14$4yrM/g9wcEbukfdtmvB4KOiZPLut5.VPX.MAsYUkNzCV/hyRB3DA2", //changeme
                    LastLogin = DateTime.MinValue,
                    Active = true
                });

            mb.Entity<SslOption>()
                .HasData(
                    new SslOption{SslOptionsId = 1,Option = "None"},
                    new SslOption{SslOptionsId = 2, Option = "Auto" },
                    new SslOption{SslOptionsId = 3, Option = "SslOnConnect" },
                    new SslOption{SslOptionsId = 4, Option = "StartTls" },
                    new SslOption{SslOptionsId = 5, Option = "StartTlsWhenAvailable" }
                    );

            base.OnModelCreating(mb);
        }

        public DbSet<DataSource> DataSources { get; set; }
        public DbSet<DataSourceType> DataSourceTypes { get; set; }
        public DbSet<Destiny> Destinies { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<EmailAccount> EmailAccounts { get; set; }
        public DbSet<ExecutionHistory> ExecutionHistories { get; set; }
        public DbSet<ExecutionType> ExecutionTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<SslOption> SslOptions { get; set; }
        public DbSet<User> Users { get; set; }

    }

}
