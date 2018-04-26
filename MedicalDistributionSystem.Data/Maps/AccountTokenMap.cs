using MedicalDistributionSystem.Domain.App;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data.Maps
{
    public class AccountTokenMap:EntityTypeConfiguration<AccountToken>
    {
        public AccountTokenMap()
        {
            this.ToTable("AccountToken");
            this.HasKey(p => p.Id);
        }
    }
}
