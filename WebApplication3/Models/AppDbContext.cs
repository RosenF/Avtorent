using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<Cars> Cars { get; set; }

        public DbSet<HiredCars> HiredCars { get; set; }

        public DbSet<Users> Users { get; set; }
    }
}