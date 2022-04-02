using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
     public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Services> Services { get; set; }
        
    }

}