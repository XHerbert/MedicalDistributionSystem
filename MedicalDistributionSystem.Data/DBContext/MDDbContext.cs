using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data
{
    public class MDDbContext:DbContext
    {
        public MDDbContext()
            :base(GetConnection())
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        private static string GetConnection()
        {
            string name = Environment.MachineName;
            return System.Configuration.ConfigurationManager.ConnectionStrings[name].ToString();
        }

        public static bool TestConnection()
        {
            using (var db = new MDDbContext())
            {
                return db.Database.Connection.State.ToString().Equals("Open");
            }
        }
    }
}
