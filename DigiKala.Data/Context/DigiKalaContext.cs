using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DigiKala.Data.Context
{
    public class DigiKalaContext:DbContext
    {
        public DigiKalaContext(DbContextOptions<DigiKalaContext> options):base(options)
        {

        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
        }
    }
}
