using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigiKala.Data.Entities.User;

namespace DigiKala.Data.Context
{
    public class DigiKalaContext:DbContext
    {
        public DigiKalaContext(DbContextOptions<DigiKalaContext> options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);
                
            base.OnModelCreating(modelBuilder); 
        }
    }
}
