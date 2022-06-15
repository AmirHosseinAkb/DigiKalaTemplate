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
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r => !r.IsDeleted);

            modelBuilder.Entity<Role>()
                .HasData(
                    new Role() {RoleId=1,RoleTiltle="مدیر سایت",IsDeleted=false,IsDefaultForNewUsers=false },
                    new Role() {RoleId=2,RoleTiltle="دستیار مدیر",IsDeleted=false,IsDefaultForNewUsers=false },
                    new Role() {RoleId=3,RoleTiltle="کاربر عادی",IsDeleted=false,IsDefaultForNewUsers=true }
                );
            
            base.OnModelCreating(modelBuilder); 
        }
    }
}
