using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Overrustlelogs.ViewModels
{
    public class OrlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=orl.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
