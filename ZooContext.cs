using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ZooAPI.Models;

namespace ZooAPI.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext(DbContextOptions<ZooContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
    }
}