using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrafficCachingService.TrafficServiceReference;

namespace TrafficCachingService
{
    public class TrafficCaching
    {
        static void Main(string[] args)
        {
            try
            {
                TrafficServiceClient objClient = new TrafficServiceClient();
                objClient.RefreshFlightCubeData();
            }
            catch(Exception ex)
            {
                Console.Write("Exception Occured - ", ex.Message);
            }
        }
    }
}
