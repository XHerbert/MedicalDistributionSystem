using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalDistributionSystem.Api.Common
{
    public class Common
    {
        public static string CreateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}