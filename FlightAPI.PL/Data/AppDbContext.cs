using FlightAPI.PL.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FlightAPI.PL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
    }
}
