using ITRootsTask.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace ITRootsTask.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static void Seed()
        {
            SqlSeed(Create(), "AddEditUser.sql");
            SqlSeed(Create(), "DelteUser.sql");
            SqlSeed(Create(), "GetUser.sql");
        }

        private static void SqlSeed(ApplicationDbContext context, string path)
        {
            try
            {
                var seedPath = HttpContext.Current.Server.MapPath("~/Seed/");
                var script = File.ReadAllText(Path.Combine(seedPath, path));
                context.SP.SqlQuery(script).ToList();
            }
            catch (Exception ex) { }
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }


        [NotMapped]
        public DbSet<SP> SP { get; set; }

    }
}