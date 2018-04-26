using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data.DBContext
{
    public class MedicalInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<MDDbContext>
    {
        public override void InitializeDatabase(MDDbContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(MDDbContext context)
        {
            base.Seed(context);
        }
    }
}
