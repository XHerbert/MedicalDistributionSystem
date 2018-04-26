using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data.Maps
{
    public class MemberMap: EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            this.ToTable("Member");
            this.HasKey(p => p.Id);
        }
    }
}
