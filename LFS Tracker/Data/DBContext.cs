using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LFS_Tracker.Models;

namespace LFS_Tracker.Data
{
    public class DBContext : DbContext
    {
        public DBContext (DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Package { get; set; }
        public DbSet<LfsInstance> LfsInstance { get; set; }
    }
}
