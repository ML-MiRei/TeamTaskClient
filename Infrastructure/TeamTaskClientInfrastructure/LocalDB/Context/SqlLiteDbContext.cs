using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTaskClient.Infrastructure.LocalDB.Models;

namespace TeamTaskClient.Infrastructure.LocalDB.Context
{
    internal class SqlLiteDbContext : DbContext
    {
        public SqlLiteDbContext()
        {
            Database.EnsureCreated();
        }


        public DbSet<User> Users { get; set; } 
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Tag);
            modelBuilder.Entity<Project>().HasKey(p => p.ID);
            modelBuilder.Entity<ProjectTask>().HasKey(p => p.ID);
            modelBuilder.Entity<Notification>().HasKey(p => p.ID);
            modelBuilder.Entity<Chat>().HasKey(p => p.ID);
            modelBuilder.Entity<Message>().HasKey(p => p.ID);
            modelBuilder.Entity<Team>().HasKey(t => t.Tag);

            modelBuilder.Entity<Message>().HasOne(p => p.Chat).WithMany(p => p.Messages);
            modelBuilder.Entity<ProjectTask>().HasOne(p => p.Project).WithMany(p => p.Tasks);
            modelBuilder.Entity<Team>().HasMany(p => p.Projects).WithMany(p => p.Teams);
            modelBuilder.Entity<Team>().HasMany(p => p.Users).WithMany(p => p.Teams);
            modelBuilder.Entity<User>().HasMany(p => p.Projects).WithMany(p => p.Users);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../cash.db");

            var t = optionsBuilder.Options;

            base.OnConfiguring(optionsBuilder);
        }
    }
}
