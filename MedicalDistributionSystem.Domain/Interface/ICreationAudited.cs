using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Interface
{
    public interface ICreationAudited
    {
        int Id { get; set; }
        int CreatorUserId { get; set; }
        DateTime? CreatorTime { get; set; }
    }
}
