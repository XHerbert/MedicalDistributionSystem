using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data.Maps
{
    public class ProxyMap:EntityTypeConfiguration<Proxy>
    {
        public ProxyMap()
        {
            this.ToTable("Proxy");
            this.HasKey(p => p.Id);
        }
    }
}
