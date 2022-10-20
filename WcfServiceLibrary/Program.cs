using System;
using System.ServiceModel;

namespace WCFService
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ServiceBookParser)))
            {
                host.Open();
                Console.WriteLine("Server is started");
                Console.WriteLine("Press <enter> to kill the service");
                Console.ReadLine();
            }
        }
    }
}
