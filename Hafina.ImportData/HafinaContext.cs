using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hafina.ImportData
{
    public class HafinaContext : DbContext
    {
        // Run command: Add-Migration InitialCreate -OutputDir "Data/Migrations" to add folder Migrations in Data folder
        public HafinaContext() : base()
        {
        }

        public virtual DbSet<BalanceSheet> BalanceSheet { get; set; }

        public virtual DbSet<BusinessResult> BusinessResult { get; set; }

        public virtual DbSet<Company> Company { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Hafina"].ConnectionString);
            }
        }
    }
}
