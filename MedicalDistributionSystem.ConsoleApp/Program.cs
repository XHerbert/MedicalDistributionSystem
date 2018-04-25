using MedicalDistributionSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalDistributionSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MDDbContext())
            {
                db.Database.Connection.Open();
                Console.WriteLine(db.Database.Connection.State);
            }
            Console.ReadLine();
        }
    }
}
