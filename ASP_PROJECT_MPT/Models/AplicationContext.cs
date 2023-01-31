using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ASP_PROJECT_MPT.Models
{
    public class AplicationContext : DbContext    
    {
        public DbSet<User> Users { get; set; }
        public AplicationContext(DbContextOptions<AplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

    }
}
