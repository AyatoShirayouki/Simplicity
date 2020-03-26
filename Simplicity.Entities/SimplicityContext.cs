using Microsoft.EntityFrameworkCore;
using Simplicity.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simplicity.Entities
{
    public class SimplicityContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserProject> UsersProjects { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public SimplicityContext(DbContextOptions<SimplicityContext> options):base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProject>()
             .HasOne(gm => gm.User)
             .WithMany(g => g.UsersProjects)
             .HasForeignKey(gm => gm.UserID);

            modelBuilder.Entity<UserProject>()
                .HasOne(gm => gm.Project)
                .WithMany(m => m.UsersProjects)
                .HasForeignKey(gm => gm.ProjectID);

            modelBuilder.Entity<Ticket>()
              .HasOne(c => c.Creator)
              .WithMany()
              .HasForeignKey(c => c.CreatorID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
              .HasOne(c => c.Assignee)
              .WithMany()
              .HasForeignKey(c => c.AssigneeID)
              .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
