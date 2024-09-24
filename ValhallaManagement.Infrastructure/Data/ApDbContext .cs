using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValhallaManagement.Core.Entities;

namespace ValhallaManagement.Infrastructure.Data
{
    public class ApDbContext : DbContext
    {
        public DbSet<Vikingo> Vikingos { get; set; }

        public ApDbContext(DbContextOptions<ApDbContext> options) : base(options) { }
    }
}
