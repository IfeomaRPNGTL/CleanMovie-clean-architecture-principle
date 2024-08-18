﻿using CleanMovie.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMovie.Infrastruture
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one to many (member and rental)
            modelBuilder.Entity<Member>()
                .HasOne<Rental>(s => s.Rental)
                .WithMany(r => r.Members)
                .HasForeignKey(s => s.RentalId);

            // Many to many (MovieRental and Movie)
            modelBuilder.Entity<MovieRental>()
                .HasKey(g => new { g.RentalId, g.MovieId });

            // Handle decimal to avoid precision loss
            modelBuilder.Entity<Rental>()
                .Property(p => p.TotalCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Movie>()
               .Property(p => p.RentalCost)
               .HasColumnType("decimal(18,2)");

        }

           

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<MovieRental> MovieRentals { get; set; }
    }
}
