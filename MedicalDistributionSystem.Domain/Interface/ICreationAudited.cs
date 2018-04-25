using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.Interface
{
    public interface ICreationAudited
    {
        string F_Id { get; set; }
        string F_CreatorUserId { get; set; }
        DateTime? F_CreatorTime { get; set; }
    }
}
