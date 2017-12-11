using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learningNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace learningNetCore.Data
{
    public class learningNetCoreDbContext : DbContext
    {
        public learningNetCoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
