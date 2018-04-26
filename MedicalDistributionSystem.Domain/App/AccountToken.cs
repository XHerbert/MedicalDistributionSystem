using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.Domain.App
{
    public class AccountToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Account { get; set; }
        public int AccountType { get; set; }
        public DateTime? CreatorTime { get; set; }
    }
}
