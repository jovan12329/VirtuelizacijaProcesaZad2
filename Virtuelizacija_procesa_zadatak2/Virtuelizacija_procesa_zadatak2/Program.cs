using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Virtuelizacija_procesa_zadatak2.Implementations;

namespace Virtuelizacija_procesa_zadatak2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(SendFile)))
            {
               
                host.Open();
                Console.WriteLine("Ready to proceed files!");
                Console.ReadKey();
            }

        }
    }
}
