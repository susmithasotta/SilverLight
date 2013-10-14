using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections.ObjectModel;
using TrafficAllocation.Model.Entities;
using TrafficAllocation.Model;
using System.Web;
using System.Web.Caching;
using System.Configuration;

namespace TrafficAllocationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : ITrafficService
    {
        static FlightCubeDAL flightCubeDal = new FlightCubeDAL();
        public static ObservableCollection<FlightCubeData> ObjFlightCubeData;
        public string strDefaultView = ConfigurationManager.AppSettings["DefaultView"];
        static string trafficCacheKey = ConfigurationManager.AppSettings["TrafficCache"];
        int iFromDays = Convert.ToInt16(ConfigurationManager.AppSettings["FromDateDays"]);
        int iToDays = Convert.ToInt16(ConfigurationManager.AppSettings["ToDateDays"]);

        //static string trafficCacheKey = "trafficCacheKeytest";
       // static string trafficCacheKey = "testtrafficCacheKey";

        public ObservableCollection<NetworkViewSlicesData> GetNetworkViewSlices()
        {
            return flightCubeDal.GetNetworkViewSlicesData();
        }

        public ObservableCollection<FlightCubeData> GetFlightCubeData()
        {
            return flightCubeDal.GetFlightCubeData();
        }


        public ObservableCollection<CurrentViewData> GetCurrentViewData(string selectedView)
        {
            //// selectedView = "Bing O&O US Search PC";
            // if (selectedView.Contains("Bing"))
            //     selectedView = selectedView.Replace("Bing", "Microsoft");
            // else if (selectedView.Contains("Yahoo"))
            //     selectedView = selectedView.Replace("Yahoo", "Yahoo!");
            // if (selectedView.Contains("Search"))
            //     selectedView = selectedView.Replace("Search", "PaidSearch");
            // selectedView = selectedView + " Relevance";
            // selectedView = selectedView.Replace(" ", ",");
            // if (selectedView.Contains("US"))
            //     selectedView = selectedView.Replace("US", "United States");
            return flightCubeDal.GetCurrentViewData(selectedView.Trim());
        }

        public ObservableCollection<ExperimentAllocationData> GetExperimentAllocationData(string selectedView)
        {
            // selectedView = "Bing O&O US Search PC";
            return flightCubeDal.GetExperimentAllocationData(selectedView.Trim());
        }

        public ObservableCollection<RPSContributionData> GetRPSContributionData(string selectedView)
        {
            return flightCubeDal.GetRPSContributionData(selectedView.Trim());
        }

        public ObservableCollection<MLIYContributionData> GetMLIYContributionData(string selectedView)
        {
            return flightCubeDal.GetMLIYContributionData(selectedView.Trim());
        }

        public ObservableCollection<MLCYContributionData> GetMLCYContributionData(string selectedView)
        {
            return flightCubeDal.GetMLCYContributionData(selectedView.Trim());
        }

        public ObservableCollection<IYContributionData> GetIYContributionData(string selectedView)
        {
            return flightCubeDal.GetIYContributionData(selectedView.Trim());
        }

        public ObservableCollection<CYContributionData> GetCYContributionData(string selectedView)
        {
            return flightCubeDal.GetCYContributionData(selectedView.Trim());
        }

        public ObservableCollection<ExperimentAllocationData> GetNetworkExperimentAllocationData(string selectedView)
        {
            return flightCubeDal.GetNetworkExperimentAllocationData(selectedView.Trim());
        }
        public ObservableCollection<RPSContributionData> GetNetworkRPSContributionData(string selectedView)
        {
            return flightCubeDal.GetNetworkRPSContributionData(selectedView.Trim());
        }

        public ObservableCollection<MLIYContributionData> GetNetworkMLIYContributionData(string selectedView)
        {
            return flightCubeDal.GetNetworkMLIYContributionData(selectedView.Trim());
        }

        public ObservableCollection<CYContributionData> GetNetworkCYContributionData(string selectedView)
        {
            return flightCubeDal.GetNetworkCYContributionData(selectedView.Trim());
        }

        public bool RefreshFlightCubeData()
        {
            try
            {
                if (ObjFlightCubeData == null)
                {
                    ObjFlightCubeData = flightCubeDal.GetFlightCubeData();
                }
                if (ObjFlightCubeData != null && ObjFlightCubeData.Count > 0)
                {
                    if (HttpRuntime.Cache.Get(trafficCacheKey) != null)
                        HttpRuntime.Cache.Remove(trafficCacheKey);
                    HttpRuntime.Cache.Add(trafficCacheKey, ObjFlightCubeData, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                    FlightCubeDAL.ObjFlightCubeData = ObjFlightCubeData;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string GetDefaultView()
        {
            return strDefaultView;
        }

        public string GetFromAndTodays()
        {
            string strFromToDays = FlightCubeDAL.iFromDays + "," + FlightCubeDAL.iToDays;
            return strFromToDays;
        }
    }
}
