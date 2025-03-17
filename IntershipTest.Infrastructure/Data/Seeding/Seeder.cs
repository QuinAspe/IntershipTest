using IntershipTest.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTest.Infrastructure.Data.Seeding
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
               new Team { Id = 1, Name = "Team A" },
               new Team { Id = 2, Name = "Team B" },
               new Team { Id = 3, Name = "Team C" }
            );
            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1995, 5, 15), TeamId = 1 },
                new Player { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1996, 8, 25), TeamId = 1 },
                new Player { Id = 3, FirstName = "Mike", LastName = "Johnson", DateOfBirth = new DateTime(1994, 2, 10), TeamId = 2 },
                new Player { Id = 4, FirstName = "Emily", LastName = "Davis", DateOfBirth = new DateTime(1993, 12, 5), TeamId = 2 },
                new Player { Id = 5, FirstName = "James", LastName = "Williams", DateOfBirth = new DateTime(1997, 7, 21), TeamId = 3 }
            );
        }
    }
}
