﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumHub
{

    class UserRelation
    {
        public long FollowerId { get; set; }
        public User Follower { get; set; }

        public long MasterId { get; set; }
        public User Master { get; set; }

        UserRelation() { }

        public UserRelation(long followerId, User follower, long masterId, User master)
        {
            FollowerId = followerId;
            Follower = follower;
            MasterId = masterId;
            Master = master;
        }
    }

    class ApplicationContextDB : DbContext
    {

        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MasterInfo> MasterInfos { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<UserRelation> UserRelations { get; set; }

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

            modelBuilder.Entity<UserRelation>()
            .HasKey(ur => new { ur.FollowerId, ur.MasterId });

            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.Follower)
                .WithMany(u => u.Masters)
                .HasForeignKey(ur => ur.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRelation>()
                .HasOne(ur => ur.Master)
                .WithMany(u => u.Followers)
                .HasForeignKey(ur => ur.MasterId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
