using MedicalDistributionSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Data.Maps
{
    public class DoctorMap:EntityTypeConfiguration<Doctor>
    {
        public DoctorMap()
        {
            this.ToTable("Doctor");
            this.HasKey(p=>p.Id);
        }
    }
}
