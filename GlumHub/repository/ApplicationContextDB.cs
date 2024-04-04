﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{
    class ApplicationContextDB : DbContext
    {

        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MasterInfo> MasterInfos { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public ApplicationContextDB()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-0M3BPJP;Initial Catalog=GlumHub;Integrated Security=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Client)
                .WithMany()
                .HasForeignKey(b => b.Clientid)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Master)
                .WithMany()
                .HasForeignKey(b => b.MasterId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
