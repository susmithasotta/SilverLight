using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TrafficAllocation.Model.Entities;
using Microsoft.AnalysisServices.AdomdClient;
using System.Configuration;
using System.Data;

namespace TrafficAllocation.Model
{
    public class FlightCubeDAL
    {
        string strConnectionstring = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        string strAllocationBudget = ConfigurationManager.AppSettings["AllocationBudget"].ToString();
        string strExperimentAllocationBudget = ConfigurationManager.AppSettings["ExperimentAllocationBudget"].ToString();
        public static int iFromDays = Convert.ToInt16(ConfigurationManager.AppSettings["FromDateDays"]);
        public static int iToDays = Convert.ToInt16(ConfigurationManager.AppSettings["ToDateDays"]);

        public static ObservableCollection<FlightCubeData> ObjFlightCubeData;

        public FlightCubeDAL()
        {
            //if (ObjFlightCubeData == null || ObjFlightCubeData.Count == 0)
            //    GetFlightCubeData();
        }

        private string PrependZeroIfLengthIsOne(string str)
        {
            if (str.Length == 1)
            {
                str = "0" + str;
            }
            return str;
        }

        private string MDXQuery()
        {
            string strMDXQuery = string.Empty;
            try
            {
                int mdxNoOfDays = iFromDays;
                int tomdxNoOfDays = iToDays;
                strMDXQuery = ConfigurationManager.AppSettings["MDXQuery"];
                string strFromDay = DateTime.Now.AddDays(-mdxNoOfDays).Day.ToString();
                string strToDay = DateTime.Now.AddDays(-tomdxNoOfDays).Day.ToString();

                strMDXQuery = strMDXQuery.Replace("@FromDate", DateTime.Now.AddDays(-mdxNoOfDays).Year + "-" + PrependZeroIfLengthIsOne(Convert.ToString(DateTime.Now.AddDays(-mdxNoOfDays).Month)) + "-" + PrependZeroIfLengthIsOne(strFromDay) + "T00:00:00");
                strMDXQuery = strMDXQuery.Replace("@ToDate", DateTime.Now.AddDays(-tomdxNoOfDays).Year + "-" + PrependZeroIfLengthIsOne(Convert.ToString(DateTime.Now.AddDays(-tomdxNoOfDays).Month)) + "-" + PrependZeroIfLengthIsOne(strToDay) + "T00:00:00");
            }
            catch
            {
            }
            finally
            {
            }
            return strMDXQuery;
        }


        #region Flight DB data from TrafficAllocationDashboardDate
        private void GetLatestDateFromFactDB()
        {
            string result;
            try
            {
                var flightCubeDBEntities = new FlightCubeDBEntities();
                var flightresult = flightCubeDBEntities.TrafficAllocationDashboardDates.ToList();
                var item = (from d in flightresult
                           select d).LastOrDefault();
                result = item.datekey.ToString();
                TimeSpan ts = Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(result);

                int iTodaysDiff = Convert.ToInt16(ts.Days);
                iToDays = iTodaysDiff;
                iFromDays = iTodaysDiff + 6;
            }
            catch
            {

            }
            // return result;
        }

        #endregion Flight DB data from TrafficAllocationDashboardDate

        #region Flight Cube Data
        public ObservableCollection<FlightCubeData> GetFlightCubeData()
        {
            GetLatestDateFromFactDB();
            AdomdConnection conn = new AdomdConnection(strConnectionstring);
            conn.Open();
            try
            {
                AdomdDataReader objDataReader = null;
                FlightCubeData ObjFlightsData = null;

                ObjFlightCubeData = new ObservableCollection<FlightCubeData>();

                //DataTable objDataTable = null;
                //using (var adapter = new AdomdDataAdapter(MDXQuery(), conn))
                //{
                //    objDataReader = cmd.ExecuteReader();
                //    var ds = new DataSet();
                //    adapter.Fill(ds);
                //    if (ds.Tables.Count > 0)
                //    {
                //        objDataTable = ds.Tables[0];
                //    }
                //}

                using (AdomdCommand cmd = new AdomdCommand(MDXQuery(), conn))
                {
                    objDataReader = cmd.ExecuteReader();
                    while (objDataReader.Read())
                    {
                        try
                        {
                            ObjFlightsData = new FlightCubeData(
                                ////Year
                                //                            Convert.ToInt32(objDataReader[0])
                                ////Quarter
                                //                            , objDataReader[1].ToString()
                                ////Month
                                //                            , objDataReader[2].ToString()
                                //Date
                                //,
                                                            Convert.ToDateTime(objDataReader[3])
                                //flightNo
                                                            , Convert.ToInt32(objDataReader[4])
                                //ExperimentName
                                                            , objDataReader[5].ToString()
                                //trafficType
                                                            , objDataReader[6].ToString()
                                //environmentType
                                                            , objDataReader[7].ToString()
                                //network
                                                            , objDataReader[8].ToString()
                                //partner
                                                            , objDataReader[9].ToString()
                                //country
                                                            , objDataReader[10].ToString()
                                //medium
                                                            , objDataReader[11].ToString()
                                //device
                                                            , objDataReader[12].ToString()
                                //pagePlacement
                                                            , objDataReader[13].ToString()
                                //ExperimentType
                                                            , objDataReader[14].ToString()
                                //featureAreaType
                                                            , objDataReader[15].ToString()
                                //StartDate
                                                            , Convert.ToDateTime(objDataReader[16])
                                //ModifiedDate
                                                            , Convert.ToDateTime(objDataReader[17])
                                //experimentAuthor
                                                            , objDataReader[18].ToString()
                                //GrossRevenue
                                                            , Convert.ToDouble(objDataReader[19])
                                //Impressions
                                                            , Convert.ToInt32(objDataReader[20])
                                //Clicks
                                                            , Convert.ToInt32(objDataReader[21])
                                //SRPV
                                                            , Convert.ToInt32(objDataReader[22])
                                //RPM
                                                            , Convert.ToDouble(objDataReader[23])
                                //Impression Yield
                                                            , Convert.ToDouble(objDataReader[24])
                                //Click Yield
                                                            , Convert.ToDouble(objDataReader[25])
                              
                                //LastRefreshed
                                                            , DateTime.Now
                                                            );
                            ObjFlightCubeData.Add(ObjFlightsData);
                        }
                        catch (Exception)
                        {
                        }

                    }

                }
                //var trafficAllocationEntities = new TrafficAllocationEntities();

                //var flightCubeDataResult = trafficAllocationEntities.sp_GetFlightCubeData().ToList();

                //var result = flightCubeDataResult.GroupBy(
                //    x => new { x.day, x.flight, x.ExperimentName, x.TrafficType, x.EnvironmentType, x.pub_owner, x.country, x.medium, x.device, x.PagePlacement, x.FeatureAreaType }).
                //    Select(fcd => new FlightCubeData(Convert.ToDateTime(fcd.Key.day)
                //                                                     , Convert.ToInt32(fcd.Key.flight)
                //                                                     , fcd.Key.ExperimentName
                //                                                     , fcd.Key.TrafficType
                //                                                     , fcd.Key.EnvironmentType
                //                                                     , fcd.Key.pub_owner
                //                                                     , fcd.Key.pub_owner
                //                                                     , fcd.Key.country
                //                                                     , fcd.Key.medium
                //                                                     , fcd.Key.device
                //                                                     , fcd.Key.PagePlacement
                //                                                     , fcd.Key.FeatureAreaType
                //                                                     , Convert.ToDouble(fcd.Sum(item => item.revenue))
                //                                                     , Convert.ToInt32(fcd.Sum(item => item.srpv))
                //                                                     , Convert.ToInt32(fcd.Sum(item => item.impressions))
                //                                                     , Convert.ToInt32(fcd.Sum(item => item.clicks))
                //                                                     , Convert.ToDouble(fcd.Sum(item => item.rpm))
                //                                                     , Convert.ToDouble(fcd.Sum(item => item.impressionYield))
                //                                                     , DateTime.Now));
                //ObjFlightCubeData = new ObservableCollection<FlightCubeData>(result.ToList());

            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }
            return ObjFlightCubeData;
        }
        #endregion

        #region Network View Slices Data
        public ObservableCollection<NetworkViewSlicesData> GetNetworkViewSlicesData()
        {
            ObservableCollection<NetworkViewSlicesData> result = null;
            try
            {
                var trafficAllocationEntities = new TrafficAllocationEntities();

                var networkviewslicesResult = trafficAllocationEntities.NetworkViewSlices.ToList();

                result = new ObservableCollection<NetworkViewSlicesData>(from d in networkviewslicesResult
                                                                         where d.IsActive == 1
                                                                         group d by new { d.ViewName, d.Network, d.Partner, d.Country, d.Medium, d.Device, d.ActaullViewName, d.ViewSliceName }
                                                                             into grp
                                                                             select new NetworkViewSlicesData(grp.Key.ViewName, grp.Key.ActaullViewName, grp.Key.ViewSliceName));
                //                                                      .GroupBy(
                //x => new { x.ViewName, x.Network, x.Partner, x.Country, x.Medium, x.Device, x.ActaullViewName }).
                //Select(fcd => new NetworkViewSlicesData(fcd.Key.ViewName, fcd.Key.ActaullViewName)));

            }
            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region Current View Data
        public ObservableCollection<CurrentViewData> GetCurrentViewData(string selectedView)
        {
            ObservableCollection<CurrentViewData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                string[] search = new string[] { "Main Line", "Unknown" };

                //Get only flights data along with Revenue and SRPV, Clicks and Impressions for todays date.
                var currentDaydata = new ObservableCollection<CurrentViewData>(from data in ObjFlightCubeData
                                                                               where
                                                                               data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                               &&
                                                                               data.Network == strViewArray[0]
                                                                               && data.Partner == strViewArray[1]
                                                                               && data.Country == strViewArray[2]
                                                                               && data.Medium == strViewArray[3]
                                                                               && data.Device == strViewArray[4]
                                                                               group data by
                                                                                   new { data.Date }
                                                                                   into grp
                                                                                   select new CurrentViewData(
                                                                                       grp.Key.Date
                                                                                       //Revenue
                                                                                       , grp.Sum(item => item.GrossRevenueUSD)
                                                                                       //SRPV
                                                                                       , grp.Sum(item => item.Srpv)
                                                                                       //Clicks
                                                                                       , grp.Sum(item => item.Clicks)
                                                                                       //Impressions
                                                                                       , grp.Sum(item => item.Impressions)
                                                                                       //RPM
                                                                                       , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 1000)
                                                                                       // Click YIeld
                                                                                       , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                       //Impression Yield
                                                                                       , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                       )).ToList();

                //Get only flights data along with Revenue and SRPV, Clicks and Impressions for todays date with ML filter.
                var currentDayMLdata = new ObservableCollection<CurrentViewData>(from data in ObjFlightCubeData
                                                                                 from s in search
                                                                                 where
                                                                                 data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                 &&
                                                                                 data.Network == strViewArray[0]
                                                                                 && data.Partner == strViewArray[1]
                                                                                 && data.Country == strViewArray[2]
                                                                                 && data.Medium == strViewArray[3]
                                                                                 && data.Device == strViewArray[4]
                                                                                 && data.PagePlacement.Contains(s)
                                                                                 group data by
                                                                                     new { data.Date }
                                                                                     into grp
                                                                                     select new CurrentViewData(
                                                                                         grp.Key.Date
                                                                                         //Revenue
                                                                                         , grp.Sum(item => item.GrossRevenueUSD)
                                                                                         //SRPV
                                                                                         , grp.Sum(item => item.Srpv)
                                                                                         //Clicks
                                                                                         , grp.Sum(item => item.Clicks)
                                                                                         //Impressions
                                                                                         , grp.Sum(item => item.Impressions)
                                                                                         //RPM
                                                                                        , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 1000)
                                                                                         // Click YIeld
                                                                                        , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                         //Impression Yield
                                                                                        , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                        )).ToList();


                var totalSrpv = new ObservableCollection<CurrentViewData>(from data in ObjFlightCubeData
                                                                          where
                                                                              data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                              &&
                                                                               data.Network == strViewArray[0]
                                                                               && data.Partner == strViewArray[1]
                                                                               && data.Country == strViewArray[2]
                                                                               && data.Medium == strViewArray[3]
                                                                               && data.Device == strViewArray[4]
                                                                          group data by new { data.Date }
                                                                              into grp
                                                                              select new CurrentViewData(grp.Key.Date, grp.Sum(item => item.Srpv))).ToList();

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                   where
                                                                                         data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                          && data.Network == strViewArray[0]
                                                                                       && data.Partner == strViewArray[1]
                                                                                       && data.Country == strViewArray[2]
                                                                                       && data.Medium == strViewArray[3]
                                                                                       && data.Device == strViewArray[4]
                                                                                       && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                   //&& data.FeatureAreaType == strViewArray[5]
                                                                                   group data by new { data.Date, data.FlightNo }
                                                                                       into grp
                                                                                       select new RPSContributionData(
                                                                                           //Date
                                                                                           grp.Key.Date
                                                                                           //Fight No
                                                                                           , grp.Key.FlightNo
                                                                                           //Total SRPV
                                                                                           , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                           //Total Revenue
                                                                                           , Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD))
                                                                                           )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                     where
                                                                                         data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                          && data.Network == strViewArray[0]
                                                                                         && data.Partner == strViewArray[1]
                                                                                         && data.Country == strViewArray[2]
                                                                                         && data.Medium == strViewArray[3]
                                                                                         && data.Device == strViewArray[4]
                                                                                         && data.FlightNo == Convert.ToInt16(Convert.ToInt16(strViewArray[8]))
                                                                                     //&& data.FeatureAreaType == strViewArray[5]
                                                                                     group data by new { data.Date, data.FlightNo }
                                                                                         into grp
                                                                                         select new RPSContributionData(
                                                                                             //Date
                                                                                           grp.Key.Date
                                                                                             //Fight No
                                                                                           , grp.Key.FlightNo
                                                                                             //Total SRPV
                                                                                           , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                             //Total Revenue
                                                                                           , Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD))
                                                                                           )).ToList();



                //Get each Tied and Untied RPM Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight SPRV else untied Flight SRPV
                var tieduntiedControlRPM = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                         where
                                                                                          data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                           && data.Network == strViewArray[0]
                                                                                             && data.Partner == strViewArray[1]
                                                                                             && data.Country == strViewArray[2]
                                                                                             && data.Medium == strViewArray[3]
                                                                                             && data.Device == strViewArray[4]
                                                                                             && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                             && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                             && data.ExperimentName != ""
                                                                                         group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                             into grp
                                                                                             select new RPSContributionData(
                                                                                                 //Date
                                                                                                            grp.Key.FlightNo
                                                                                                 //Experiment Name
                                                                                                          , grp.Key.ExperimentName
                                                                                                 //Traffic Type
                                                                                                          , grp.Key.TrafficType
                                                                                                 // SRPV
                                                                                                          , grp.Sum(item => item.Srpv)
                                                                                                 //Control RPM 
                                                                                                          , grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 ((Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                     select Convert.ToDouble((Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv)) * 1000)).FirstOrDefault())) / 1000)) :
                                                                                                                  Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 ((Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                     select Convert.ToDouble((Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv)) * 1000)).FirstOrDefault())) / 1000))
                                                                                               ));



                //Revenue Contribution. Formula is Revenue - ControlRPM
                var revenueContribution = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                        where
                                                                                          data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                           && data.Network == strViewArray[0]
                                                                                            && data.Partner == strViewArray[1]
                                                                                            && data.Country == strViewArray[2]
                                                                                            && data.Medium == strViewArray[3]
                                                                                            && data.Device == strViewArray[4]
                                                                                            && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                            && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                            && data.ExperimentName != ""
                                                                                        group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                            into grp
                                                                                            select new RPSContributionData(
                                                                                                //Date
                                                                                                grp.Key.FlightNo
                                                                                                //ExperimentName
                                                                                                , grp.Key.ExperimentName
                                                                                                //Traffic Type
                                                                                                , grp.Key.TrafficType
                                                                                                //Revenue
                                                                                                , grp.Sum(item => item.GrossRevenueUSD)
                                                                                                //RevenueContribution
                                                                                                ,
                                                                                                Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) -
                                                                                                Convert.ToDouble((from f in tieduntiedControlRPM
                                                                                                                  where f.FlightNo == grp.Key.FlightNo && f.ExperimentName == grp.Key.ExperimentName
                                                                                                                  select f.ControlRPM).FirstOrDefault())))
                                                                                                                    ).ToList();




                //Fetch Flight 10 Tied SRPV
                var tiedMLIYFlightdata = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                        from s in search
                                                                                        //join d in pagePlacements
                                                                                        //on data.PagePlacement equals d
                                                                                        where
                                                                                        data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                            && data.Network == strViewArray[0]
                                                                                            && data.Partner == strViewArray[1]
                                                                                            && data.Country == strViewArray[2]
                                                                                            && data.Medium == strViewArray[3]
                                                                                            && data.Device == strViewArray[4]
                                                                                            && data.PagePlacement.Contains(s)
                                                                                            //   && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                            && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                        //&& data.FeatureAreaType == strViewArray[5]
                                                                                        group data by new { data.Date, data.FlightNo }
                                                                                            into grp
                                                                                            select new MLIYContributionData(
                                                                                                //Date
                                                                                    grp.Key.Date
                                                                                                //FlightNo
                                                                                    , grp.Key.FlightNo
                                                                                                //SRPV
                                                                                    , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                                //Impression Yield
                                                                                    , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                                //Impressions
                                                                                    , grp.Sum(item => item.Impressions)
                                                                                    )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedMLIYFlightdata = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                          from s in search
                                                                                          //join d in pagePlacements
                                                                                          //on data.PagePlacement equals d
                                                                                          where
                                                                                          data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                              && data.Network == strViewArray[0]
                                                                                              && data.Partner == strViewArray[1]
                                                                                              && data.Country == strViewArray[2]
                                                                                              && data.Medium == strViewArray[3]
                                                                                              && data.Device == strViewArray[4]
                                                                                              && data.PagePlacement.Contains(s)
                                                                                              // && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                            && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                          //&& data.FeatureAreaType == strViewArray[5]
                                                                                          group data by new { data.Date, data.FlightNo }
                                                                                              into grp
                                                                                              select new MLIYContributionData(
                                                                                                  //Date
                                                                                                grp.Key.Date
                                                                                                  //FlightNo
                                                                                              , grp.Key.FlightNo
                                                                                                  //tOTAL SRPV
                                                                                              , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                                  //Impression Yield
                                                                                              , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                                  //Impressions
                                                                                              , grp.Sum(item => item.Impressions)
                                                                                              )).ToList();



                //Get each Tied and Untied MLIY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ImpressionYield else untied Flight ImpressionYield
                var tieduntiedMLIYContribution = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                from s in search
                                                                                                //join d in pagePlacements
                                                                                                //on data.PagePlacement equals d
                                                                                                where
                                                                                                data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                                    && data.Network == strViewArray[0]
                                                                                                    && data.Partner == strViewArray[1]
                                                                                                    && data.Country == strViewArray[2]
                                                                                                    && data.Medium == strViewArray[3]
                                                                                                    && data.Device == strViewArray[4]
                                                                                                    && data.PagePlacement.Contains(s)
                                                                                                    //&& (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                    && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                    && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                                group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                                    into grp
                                                                                                    select new MLIYContributionData(grp.Key.FlightNo
                                                                                                                , grp.Key.ExperimentName
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                                                , grp.Sum(item => item.Impressions)
                                                                                                        // MLIY Control
                                                                                                                , grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 ((Convert.ToDouble((from f in tiedMLIYFlightdata
                                                                                                                                     select Convert.ToDouble((Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100)) :
                                                                                                                  Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 ((Convert.ToDouble((from f in untiedMLIYFlightdata
                                                                                                                                     select Convert.ToDouble((Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100))
                                                                                                                     ));


                //Get each Tied and Untied Mainline Impressions
                var tieduntiedMainlineImpressions = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                   from s in search
                                                                                                   //join d in pagePlacements
                                                                                                   //on data.PagePlacement equals d
                                                                                                   where
                                                                                                   data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                                       && data.Network == strViewArray[0]
                                                                                                       && data.Partner == strViewArray[1]
                                                                                                       && data.Country == strViewArray[2]
                                                                                                       && data.Medium == strViewArray[3]
                                                                                                       && data.Device == strViewArray[4]
                                                                                                       && data.PagePlacement.Contains(s)
                                                                                                       // && (data.PagePlacement in ('Main Line','Unknown'))
                                                                                                       && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                       && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                                   group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                                       into grp
                                                                                                       select new MLIYContributionData(grp.Key.FlightNo
                                                                                                          , grp.Key.ExperimentName
                                                                                                          , grp.Key.TrafficType
                                                                                                          , grp.Sum(item => item.Impressions)
                                                                                                           //MainLine Impressions
                                                                                                          , Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                            Convert.ToDouble((from f in tieduntiedMLIYContribution
                                                                                                                              where f.FlightNo == grp.Key.FlightNo && f.ExperimentName == grp.Key.ExperimentName
                                                                                                                              select f.MLIYControl).FirstOrDefault()))
                                                                                                          , DateTime.Now
                                                                                                                               ));

                //Get total Mainline Impressions
                var totalMainlineImpressions = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                              from s in search
                                                                                              //join d in pagePlacements
                                                                                              //on data.PagePlacement equals d
                                                                                              where
                                                                                              data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                                  && data.Network == strViewArray[0]
                                                                                                  && data.Partner == strViewArray[1]
                                                                                                  && data.Country == strViewArray[2]
                                                                                                  && data.Medium == strViewArray[3]
                                                                                                  && data.Device == strViewArray[4]
                                                                                                  && data.PagePlacement.Contains(s)
                                                                                                  //  && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                  && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                  && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                              group data by new { data.FlightNo }
                                                                                                  into grp
                                                                                                  select new MLIYContributionData(grp.Key.FlightNo
                                                                                                      //MainLine Impressions
                                                                                                          , Convert.ToDouble((from f in tieduntiedMainlineImpressions
                                                                                                                              where f.FlightNo == grp.Key.FlightNo
                                                                                                                              select f.MainlineImpressions).FirstOrDefault())
                                                                                                           , DateTime.Now
                                                                                                           , 1
                                                                                                           ));



                /* START IY*/

                //Fetch Flight 10 Tied Impressions
                var tiedIYFlightdata = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                     data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                         && data.Network == strViewArray[0]
                                                                                      && data.Partner == strViewArray[1]
                                                                                      && data.Country == strViewArray[2]
                                                                                      && data.Medium == strViewArray[3]
                                                                                      && data.Device == strViewArray[4]
                                                                                      && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                    //&& data.FeatureAreaType == strViewArray[5]
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new IYContributionData(
                                                                                            //Date
                                                                                grp.Key.Date
                                                                                            //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                            //Total SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Impression Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                            //Impressions
                                                                                , grp.Sum(item => item.Impressions)
                                                                                )).ToList();

                //Fetch Flight 166 Untied Impressions
                var untiedIYFlightdata = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                      where
                                                                                     data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                         && data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                      //&& data.FeatureAreaType == strViewArray[5]
                                                                                      group data by new { data.Date, data.FlightNo }
                                                                                          into grp
                                                                                          select new IYContributionData(
                                                                                              //Date
                                                                                            grp.Key.Date
                                                                                              //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                              //Total SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                              //Impression Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                              //Impressions
                                                                                          , grp.Sum(item => item.Impressions)
                                                                                          )).ToList();


                //Get each Tied and Untied IY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ImpressionYield else untied Flight ImpressionYield
                var tieduntiedIYContribution = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                            where
                                                                                data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                    && data.Network == strViewArray[0]
                                                                                                && data.Partner == strViewArray[1]
                                                                                                && data.Country == strViewArray[2]
                                                                                                && data.Medium == strViewArray[3]
                                                                                                && data.Device == strViewArray[4]
                                                                                                && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                            group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                                into grp
                                                                                                select new IYContributionData(grp.Key.FlightNo
                                                                                                            , grp.Key.ExperimentName
                                                                                                            , grp.Key.TrafficType
                                                                                                            , grp.Sum(item => item.Srpv)
                                                                                                            , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                                            , grp.Sum(item => item.Impressions)
                                                                                                    // IY Control
                                                                                                            , grp.Key.TrafficType == "Tied" ?
                                                                                                             Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                             ((Convert.ToDouble((from f in tiedIYFlightdata
                                                                                                                                 select Convert.ToDouble((Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100)) :
                                                                                                              Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                             ((Convert.ToDouble((from f in untiedIYFlightdata
                                                                                                                                 select Convert.ToDouble((Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100))
                                                                                                                 ));


                //Get each Tied and Untied Impressions
                var tieduntiedImpressions = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                         where
                                                                          data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                              && data.Network == strViewArray[0]
                                                                                             && data.Partner == strViewArray[1]
                                                                                             && data.Country == strViewArray[2]
                                                                                             && data.Medium == strViewArray[3]
                                                                                             && data.Device == strViewArray[4]
                                                                                             && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                             && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                         group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                             into grp
                                                                                             select new IYContributionData(grp.Key.FlightNo
                                                                                                               , grp.Key.ExperimentName
                                                                                                               , grp.Key.TrafficType
                                                                                                               , grp.Sum(item => item.Impressions)
                                                                                                 //Impressions Contibution
                                                                                                               , Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                                 Convert.ToDouble((from f in tieduntiedIYContribution
                                                                                                                                   where f.FlightNo == grp.Key.FlightNo && f.ExperimentName == grp.Key.ExperimentName
                                                                                                                                   select f.IYControl).FirstOrDefault()))
                                                                                                               , DateTime.Now
                                                                                                                                    ));

                //Get total Impressions
                var totalImpressions = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                         data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                             && data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                        && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                    group data by new { data.FlightNo }
                                                                                        into grp
                                                                                        select new IYContributionData(grp.Key.FlightNo
                                                                                            // Impressions Contribution
                                                                                                               , Convert.ToDouble((from f in tieduntiedImpressions
                                                                                                                                   where f.FlightNo == grp.Key.FlightNo
                                                                                                                                   select f.ImpressionsContribution).FirstOrDefault())
                                                                                                                , DateTime.Now
                                                                                                                , 1
                                                                                                                ));

                /* END IY*/


                /*START MLCY */

                //Fetch Flight 10 Tied SRPV
                var tiedMLCYFlightdata = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                        where
                                                                                        data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                            && data.Network == strViewArray[0]
                                                                                            && data.Partner == strViewArray[1]
                                                                                            && data.Country == strViewArray[2]
                                                                                            && data.Medium == strViewArray[3]
                                                                                            && data.Device == strViewArray[4]
                                                                                            && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                            && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                        //&& data.FeatureAreaType == strViewArray[5]
                                                                                        group data by new { data.Date, data.FlightNo }
                                                                                            into grp
                                                                                            select new MLCYContributionData(
                                                                                                //Date
                                                                                    grp.Key.Date
                                                                                                //FlightNo
                                                                                    , grp.Key.FlightNo
                                                                                                //Total SRPV
                                                                                    , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                                //Click Yield
                                                                                    , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                                //Clicks
                                                                                    , grp.Sum(item => item.Clicks)
                                                                                    )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedMLCYFlightdata = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                          where
                                                                                        data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                            && data.Network == strViewArray[0]
                                                                                          && data.Partner == strViewArray[1]
                                                                                          && data.Country == strViewArray[2]
                                                                                          && data.Medium == strViewArray[3]
                                                                                          && data.Device == strViewArray[4]
                                                                                          && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                          && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                          //&& data.FeatureAreaType == strViewArray[5]
                                                                                          group data by new { data.Date, data.FlightNo }
                                                                                              into grp
                                                                                              select new MLCYContributionData(
                                                                                                  //Date
                                                                                                grp.Key.Date
                                                                                                  //FlightNo
                                                                                              , grp.Key.FlightNo
                                                                                                  //Total SRPV
                                                                                              , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                                  //Click Yield
                                                                                              , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                                  //Clicks
                                                                                               , grp.Sum(item => item.Clicks)
                                                                                              )).ToList();



                //Get each Tied and Untied MLCY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ClickYield else untied Flight ClickYield
                var tieduntiedMLCYContribution = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                                where
                                                                                    data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                        && data.Network == strViewArray[0]
                                                                                                    && data.Partner == strViewArray[1]
                                                                                                    && data.Country == strViewArray[2]
                                                                                                    && data.Medium == strViewArray[3]
                                                                                                    && data.Device == strViewArray[4]
                                                                                                    && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                    && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                    && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                                group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                                    into grp
                                                                                                    select new MLCYContributionData(grp.Key.FlightNo
                                                                                                                , grp.Key.ExperimentName
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                                                , grp.Sum(item => item.Clicks)
                                                                                                        // MLCY Control
                                                                                                                , grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 ((Convert.ToDouble((from f in tiedMLCYFlightdata
                                                                                                                                     select Convert.ToDouble((Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100)) :
                                                                                                                  Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 ((Convert.ToDouble((from f in untiedMLCYFlightdata
                                                                                                                                     select Convert.ToDouble((Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100))
                                                                                                                     ));


                //Get each Tied and Untied Mainline Clicks
                var tieduntiedMainlineClicks = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                              where
                                                                               data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                   && data.Network == strViewArray[0]
                                                                                                  && data.Partner == strViewArray[1]
                                                                                                  && data.Country == strViewArray[2]
                                                                                                  && data.Medium == strViewArray[3]
                                                                                                  && data.Device == strViewArray[4]
                                                                                                  && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                  && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                  && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                              group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                                  into grp
                                                                                                  select new MLCYContributionData(grp.Key.FlightNo
                                                                                                     , grp.Key.ExperimentName
                                                                                                     , grp.Key.TrafficType
                                                                                                     , grp.Sum(item => item.Clicks)
                                                                                                      //MainLine Clicks
                                                                                                     , Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                                       Convert.ToDouble((from f in tieduntiedMLCYContribution
                                                                                                                         where f.FlightNo == grp.Key.FlightNo && f.ExperimentName == grp.Key.ExperimentName
                                                                                                                         select f.MLCYControl).FirstOrDefault()))
                                                                                                     , DateTime.Now
                                                                                                                          ));

                //Get total Mainline Clicks
                var totalMainlineClicks = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                         where
                                                                              data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                  && data.Network == strViewArray[0]
                                                                                             && data.Partner == strViewArray[1]
                                                                                             && data.Country == strViewArray[2]
                                                                                             && data.Medium == strViewArray[3]
                                                                                             && data.Device == strViewArray[4]
                                                                                             && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                             && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                             && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                         group data by new { data.FlightNo }
                                                                                             into grp
                                                                                             select new MLCYContributionData(grp.Key.FlightNo
                                                                                                 //MainLine Clicks
                                                                                                     , Convert.ToDouble((from f in tieduntiedMainlineClicks
                                                                                                                         where f.FlightNo == grp.Key.FlightNo
                                                                                                                         select f.MainlineClicks).FirstOrDefault())
                                                                                                      , DateTime.Now
                                                                                                      , 1
                                                                                                      ));


                /* END MLCY*/



                //Fetch Flight 10 Tied Clicks
                var tiedCYFlightdata = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                     data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                         && data.Network == strViewArray[0]
                                                                                      && data.Partner == strViewArray[1]
                                                                                      && data.Country == strViewArray[2]
                                                                                      && data.Medium == strViewArray[3]
                                                                                      && data.Device == strViewArray[4]
                                                                                      && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                    //&& data.FeatureAreaType == strViewArray[5]
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new CYContributionData(
                                                                                            //Date
                                                                                grp.Key.Date
                                                                                            //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                            //Total SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Click Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                            //Clicks
                                                                                , grp.Sum(item => item.Clicks)
                                                                                )).ToList();

                //Fetch Flight 166 Untied Clicks
                var untiedCYFlightdata = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                      where
                                                                                     data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                         && data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                      //&& data.FeatureAreaType == strViewArray[5]
                                                                                      group data by new { data.Date, data.FlightNo }
                                                                                          into grp
                                                                                          select new CYContributionData(
                                                                                              //Date
                                                                                            grp.Key.Date
                                                                                              //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                              //Total SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                              //Click Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                              //Clicks
                                                                                          , grp.Sum(item => item.Clicks)
                                                                                          )).ToList();


                //Get each Tied and Untied CY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ClicksYield else untied Flight ClicksYield
                var tieduntiedCYContribution = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                            where
                                                                                data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                    && data.Network == strViewArray[0]
                                                                                                && data.Partner == strViewArray[1]
                                                                                                && data.Country == strViewArray[2]
                                                                                                && data.Medium == strViewArray[3]
                                                                                                && data.Device == strViewArray[4]
                                                                                                && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                            group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                                into grp
                                                                                                select new CYContributionData(grp.Key.FlightNo
                                                                                                            , grp.Key.ExperimentName
                                                                                                            , grp.Key.TrafficType
                                                                                                            , grp.Sum(item => item.Srpv)
                                                                                                            , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                                            , grp.Sum(item => item.Clicks)
                                                                                                    // CY Control
                                                                                                            , grp.Key.TrafficType == "Tied" ?
                                                                                                             Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                             ((Convert.ToDouble((from f in tiedCYFlightdata
                                                                                                                                 select Convert.ToDouble((Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100)) :
                                                                                                              Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                             ((Convert.ToDouble((from f in untiedCYFlightdata
                                                                                                                                 select Convert.ToDouble((Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv)) * 100)).FirstOrDefault())) / 100))
                                                                                                                 ));


                //Get each Tied and Untied Clicks
                var tieduntiedClicks = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                     data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                         && data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                        && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                    group data by new { data.FlightNo, data.ExperimentName, data.TrafficType }
                                                                                        into grp
                                                                                        select new CYContributionData(grp.Key.FlightNo
                                                                                                          , grp.Key.ExperimentName
                                                                                                          , grp.Key.TrafficType
                                                                                                          , grp.Sum(item => item.Clicks)
                                                                                            //Clicks Contibution
                                                                                                          , Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                                            Convert.ToDouble((from f in tieduntiedCYContribution
                                                                                                                              where f.FlightNo == grp.Key.FlightNo && f.ExperimentName == grp.Key.ExperimentName
                                                                                                                              select f.CYControl).FirstOrDefault()))
                                                                                                          , DateTime.Now
                                                                                                                               ));

                //Get total Clicks
                var totalClicks = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                               where
                                                                    data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                        && data.Network == strViewArray[0]
                                                                                   && data.Partner == strViewArray[1]
                                                                                   && data.Country == strViewArray[2]
                                                                                   && data.Medium == strViewArray[3]
                                                                                   && data.Device == strViewArray[4]
                                                                                   && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                   && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                               group data by new { data.FlightNo }
                                                                                   into grp
                                                                                   select new CYContributionData(grp.Key.FlightNo
                                                                                       // Clicks Contribution
                                                                                                          , Convert.ToDouble((from f in tieduntiedClicks
                                                                                                                              where f.FlightNo == grp.Key.FlightNo
                                                                                                                              select f.ClicksContribution).FirstOrDefault())
                                                                                                           , DateTime.Now
                                                                                                           , 1
                                                                                                           ));


                result = new ObservableCollection<CurrentViewData>(from data in ObjFlightCubeData
                                                                   where
                                                                     data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                        && data.Network == strViewArray[0]
                                                                        && data.Partner == strViewArray[1]
                                                                        && data.Country == strViewArray[2]
                                                                        && data.Medium == strViewArray[3]
                                                                        && data.Device == strViewArray[4]
                                                                        && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                        && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                   orderby data.ExperimentName ascending
                                                                   group data by
                                                                   new { data.FlightNo, data.ExperimentName, data.FeatureAreaType, data.TrafficType, data.EnvironmentType, data.StartDate, data.ModifiedDate, data.ExperimentType, data.ExperimentAuthor }
                                                                       into grp
                                                                       select new CurrentViewData
                                                                        (grp.Key.FlightNo
                                                                        , grp.Key.ExperimentName
                                                                        , grp.Key.FeatureAreaType
                                                                        , grp.Key.TrafficType
                                                                        , grp.Key.EnvironmentType == "Production" ? "Prod" : "XLite"
                                                                           //Allocation
                                                                        , Convert.ToDouble(grp.Sum(item => item.Srpv) /
                                                                          Convert.ToDouble(Convert.ToDouble((from d in totalSrpv
                                                                                                             //   where d.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                                             select d.TotalSrpv).FirstOrDefault())))
                                                                           //RPS 
                                                                        , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from d in currentDaydata select d.GrossRevenueUSD).Sum()) +
                                                                                                   Convert.ToDouble((from f in revenueContribution
                                                                                                                     where f.FlightNo == grp.Key.FlightNo
                                                                                                                     select f.RevenueContribution).FirstOrDefault())) /
                                                                                                    Convert.ToDouble((from d in currentDaydata select d.Srpv).Sum()) * 1000)
                                                                                                      - (from d in currentDaydata select d.RPS).Sum()) / (from d in currentDaydata select d.RPS).Sum())

                                                                         //MLIY
                                                                           //, Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Impressions) /
                                                                           // Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from f in totalMainlineImpressions
                                                                           //                                                     where f.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                           //                                                     select f.MainlineImpressions).FirstOrDefault())
                                                                           // + grp.Sum(item => item.Impressions)) / 100)) - grp.Sum(item => item.ImpressionYield))
                                                                           // / grp.Sum(item => item.ImpressionYield))

                                                                            //, Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Impressions)) +
                                                                           //                           Convert.ToDouble((from f in totalMainlineImpressions
                                                                           //                                             where f.FlightNo == grp.Key.FlightNo
                                                                           //                                             select f.MainlineImpressions).FirstOrDefault())) /
                                                                           //                            Convert.ToDouble((grp.Sum(item => item.Srpv) / 100)))
                                                                           //                              - grp.Sum(item => item.ImpressionYield)) / grp.Sum(item => item.ImpressionYield))

                                                                         , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from d in currentDayMLdata select d.Impressions).Sum()) +
                                                                                                   Convert.ToDouble((from f in totalMainlineImpressions
                                                                                                                     where f.FlightNo == grp.Key.FlightNo
                                                                                                                     select f.MainlineImpressions).FirstOrDefault())) /
                                                                                                    Convert.ToDouble((from d in currentDayMLdata select d.Srpv).Sum()) * 100)
                                                                                                      - (from d in currentDayMLdata select d.IY).Sum()) / (from d in currentDayMLdata select d.IY).Sum())

                                                                           //IY Contibution
                                                                           //, Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv) /
                                                                           //Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from f in totalImpressions
                                                                           //                                                    where f.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                           //                                                    select f.ImpressionsContribution).FirstOrDefault())
                                                                           //+ grp.Sum(item => item.Impressions)) / 100)) - grp.Sum(item => item.ImpressionYield))
                                                                           //   / grp.Sum(item => item.ImpressionYield))

                                                                         , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from d in currentDaydata select d.Impressions).Sum()) +
                                                                                                   Convert.ToDouble((from f in totalImpressions
                                                                                                                     where f.FlightNo == grp.Key.FlightNo
                                                                                                                     select f.ImpressionsContribution).FirstOrDefault())) /
                                                                                                    Convert.ToDouble((from d in currentDaydata select d.Srpv).Sum()) * 100)
                                                                                                      - (from d in currentDaydata select d.IY).Sum()) / (from d in currentDaydata select d.IY).Sum())

                                                                           //MLCY
                                                                           //, Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv) /
                                                                           // Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from f in totalMainlineClicks
                                                                           //                                                     where f.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                           //                                                     select f.MainlineClicks).FirstOrDefault())
                                                                           // + grp.Sum(item => item.Clicks)) / 100)) - grp.Sum(item => item.ClickYield))
                                                                           // / grp.Sum(item => item.ClickYield))

                                                                         , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from d in currentDayMLdata select d.Clicks).Sum()) +
                                                                                                   Convert.ToDouble((from f in totalMainlineClicks
                                                                                                                     where f.FlightNo == grp.Key.FlightNo
                                                                                                                     select f.MainlineClicks).FirstOrDefault())) /
                                                                                                    Convert.ToDouble((from d in currentDayMLdata select d.Srpv).Sum()) * 100)
                                                                                                      - (from d in currentDayMLdata select d.CY).Sum()) / (from d in currentDayMLdata select d.CY).Sum())
                                                                           //CY Contibution
                                                                           //, Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv) /
                                                                           //Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from f in totalClicks
                                                                           //                                                    where f.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                           //                                                    select f.ClicksContribution).FirstOrDefault())
                                                                           //+ grp.Sum(item => item.Clicks)) / 100)) - grp.Sum(item => item.ClickYield))
                                                                           //   / grp.Sum(item => item.ClickYield))

                                                                            , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from d in currentDaydata select d.Clicks).Sum()) +
                                                                                                   Convert.ToDouble((from f in totalClicks
                                                                                                                     where f.FlightNo == grp.Key.FlightNo
                                                                                                                     select f.ClicksContribution).FirstOrDefault())) /
                                                                                                     Convert.ToDouble((from d in currentDaydata select d.Srpv).Sum()) * 100)
                                                                                                      - (from d in currentDaydata select d.CY).Sum()) / (from d in currentDaydata select d.CY).Sum())

                                                                        //Revenue Contribution
                                                                        ,
                                                                         Convert.ToDouble((from f in revenueContribution
                                                                                           where f.FlightNo == grp.Key.FlightNo
                                                                                           select f.RevenueContribution).FirstOrDefault())
                                                                        // Impressions Contribution
                                                                        ,
                                                                         Convert.ToDouble((from f in tieduntiedImpressions
                                                                                           where f.FlightNo == grp.Key.FlightNo
                                                                                           select f.ImpressionsContribution).FirstOrDefault())

                                                                        //Clicks Contribution
                                                                        ,
                                                                        Convert.ToDouble((from f in tieduntiedClicks
                                                                                          where f.FlightNo == grp.Key.FlightNo
                                                                                          select f.ClicksContribution).FirstOrDefault())
                                                                        //Control Flight No
                                                                        , grp.Key.TrafficType == "Tied" ?
                                                                          Convert.ToInt16(strViewArray[7]) : Convert.ToInt16(strViewArray[8])
                                                                           //Flight SRPVs
                                                                        , grp.Sum(item => item.Srpv)
                                                                           //Period SRPVs
                                                                        , Convert.ToInt32((from d in totalSrpv
                                                                                           // where d.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                           select d.TotalSrpv).FirstOrDefault())
                                                                           //Flight RPS
                                                                        , Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) / Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                           //Control Flight RPS
                                                                      , grp.Key.TrafficType == "Tied" ?
                                                                                //Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) /
                                                                                Convert.ToDouble((from f in tiedFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()) :
                                                                              //  Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) /
                                                                                Convert.ToDouble((from f in untiedFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())

                                                                      // Flight MLIY
                                                                       , Convert.ToDouble((from data in tieduntiedMLIYContribution
                                                                                           where data.FlightNo == grp.Key.FlightNo
                                                                                           select Convert.ToDouble(Convert.ToDouble(data.Impressions) / Convert.ToDouble(data.Srpv))).FirstOrDefault())
                                                                       //Control Flight MLIY
                                                                       , grp.Key.TrafficType == "Tied" ?
                                                                                Convert.ToDouble((from f in tiedMLIYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()) :
                                                                                Convert.ToDouble((from f in untiedMLIYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())
                                                                       // Flight IY
                                                                       , Convert.ToDouble((from data in tieduntiedIYContribution
                                                                                           where data.FlightNo == grp.Key.FlightNo
                                                                                           select Convert.ToDouble(Convert.ToDouble(data.Impressions) / Convert.ToDouble(data.Srpv))).FirstOrDefault())
                                                                       //Control Flight IY
                                                                       , grp.Key.TrafficType == "Tied" ?
                                                                               // Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Impressions)) /
                                                                                Convert.ToDouble((from f in tiedIYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()) :
                                                                               // Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Impressions)) /
                                                                                Convert.ToDouble((from f in untiedIYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())
                                                                       // Flight MLCY
                                                                       , Convert.ToDouble((from data in tieduntiedMLCYContribution
                                                                                           where data.FlightNo == grp.Key.FlightNo
                                                                                           select Convert.ToDouble(Convert.ToDouble(data.Clicks) / Convert.ToDouble(data.Srpv))).FirstOrDefault())
                                                                           //Control Flight MLCY
                                                                       , grp.Key.TrafficType == "Tied" ?
                                                                                Convert.ToDouble((from f in tiedMLCYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()) :
                                                                                Convert.ToDouble((from f in untiedMLCYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())
                                                                       // Flight CY
                                                                       , Convert.ToDouble((from data in tieduntiedCYContribution
                                                                                           where data.FlightNo == grp.Key.FlightNo
                                                                                           select Convert.ToDouble(Convert.ToDouble(data.Clicks) / Convert.ToDouble(data.Srpv))).FirstOrDefault())
                                                                       //Control Flight CY
                                                                       , grp.Key.TrafficType == "Tied" ?
                                                                               // Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Clicks)) /
                                                                                Convert.ToDouble((from f in tiedCYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()) :
                                                                                //Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Clicks)) /
                                                                                Convert.ToDouble((from f in untiedCYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())
                                                                        //Flight Revenue
                                                                        ,Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD))
                                                                        //Control Flight Revenue
                                                                        , grp.Key.TrafficType == "Tied" ?
                                                                            Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) /
                                                                            Convert.ToDouble((from f in tiedFlightdata
                                                                                                select Convert.ToDouble(Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())) :
                                                                            Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) /
                                                                            Convert.ToDouble((from f in untiedFlightdata
                                                                                                select Convert.ToDouble(Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()))

                                                                        //,grp.Key.TrafficType == "Tied" ?
                                                                        //        Convert.ToDouble((from f in tiedFlightdata
                                                                        //                   select Convert.ToDouble(f.TotalRevenue)).FirstOrDefault()) :
                                                                        //        Convert.ToDouble((from f in untiedFlightdata
                                                                        //                   select Convert.ToDouble(f.TotalRevenue)).FirstOrDefault())
                                                                         //Flight Impressions
                                                                        , grp.Sum(item => item.Impressions)
                                                                        //Control Flight Impressions
                                                                          , grp.Key.TrafficType == "Tied" ?
                                                                                Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Impressions)) / 
                                                                                Convert.ToDouble((from f in tiedIYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())) :
                                                                                Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Impressions)) / 
                                                                                Convert.ToDouble((from f in untiedIYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Impressions) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()))
                                                                        //, grp.Key.TrafficType == "Tied" ? 
                                                                        //        Convert.ToDouble((from f in tiedIYFlightdata
                                                                        //                   select Convert.ToDouble(f.Impressions)).FirstOrDefault()) :
                                                                        //        Convert.ToDouble((from f in untiedIYFlightdata
                                                                        //                   select Convert.ToDouble(f.Impressions)).FirstOrDefault())
                                                                        //Flight Clicks
                                                                        , grp.Sum(item => item.Clicks)
                                                                        //Control Flight Clicks
                                                                         , grp.Key.TrafficType == "Tied" ?
                                                                                Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Clicks)) / 
                                                                                Convert.ToDouble((from f in tiedCYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault())) :
                                                                               Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Clicks)) / 
                                                                               Convert.ToDouble((from f in untiedCYFlightdata
                                                                                                  select Convert.ToDouble(Convert.ToDouble(f.Clicks) / Convert.ToDouble(f.TotalSrpv))).FirstOrDefault()))
                                                                        //, grp.Key.TrafficType == "Tied" ?
                                                                        //        Convert.ToDouble((from f in tiedCYFlightdata
                                                                        //                          select Convert.ToDouble(f.Clicks)).FirstOrDefault()) :
                                                                        //        Convert.ToDouble((from f in untiedCYFlightdata
                                                                        //                          select Convert.ToDouble(f.Clicks)).FirstOrDefault())
                                                                        
                                                                        //Days Running based on the start date
                                                                        , Convert.ToInt32(DateTime.Now.AddDays(-iToDays).Date.Subtract(grp.Key.StartDate).Days)
                                                                        //Last Modified Date
                                                                        , grp.Key.ModifiedDate
                                                                        // Experiment Type
                                                                        , grp.Key.ExperimentType
                                                                        // Experiment Author
                                                                        , grp.Key.ExperimentAuthor
                                                                        ));

                //result.Add(new CurrentViewData(0
                //                                , "Total"
                //                                , ""
                //                                , ""
                //                                , ""
                //                                , result.Sum(c => c.Allocation)
                //                                , result.Sum(c => c.RPS)
                //                                , result.Sum(c => c.MLIY)
                //                                , result.Sum(c => c.IY)
                //                                , result.Sum(c => c.MLCY)
                //                                , result.Sum(c => c.CY)
                //                                , result.Sum(c => c.RevenueContribution)
                //                                , result.Sum(c => c.MainlineImpression)
                //                                , result.Sum(c => c.Clicks)
                //                                , 0
                //                                , 0
                //                                , 0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0.0
                //                                , 0
                //                                , DateTime.Now.AddDays(-iToDays).Date
                //                                , ""
                //                                , ""
                //                                ));

                result = new ObservableCollection<CurrentViewData>(from data in result
                                                                   orderby data.FlightNo ascending
                                                                   select new CurrentViewData(
                                                                        data.FlightNo
                                                                        , data.ExperimentName
                                                                        , data.FeatureAreaType
                                                                        , data.TrafficType
                                                                        , data.EnvironmentType
                                                                        , data.Allocation
                                                                        , data.RPS
                                                                        , data.MLIY
                                                                        , data.IY
                                                                        , data.MLCY
                                                                        , data.CY
                                                                        , data.RevenueContribution
                                                                        , data.MainlineImpression
                                                                        , data.Clicks
                                                                        , data.ControlFlightNo
                                                                        , data.FlightSRPVs
                                                                        , data.PeriodSRPVs
                                                                        , data.FlightRPS
                                                                        , data.ControlFlightRPS
                                                                        , data.FlightMLIY
                                                                        , data.ControlFlightMLIY
                                                                        , data.FlightIY
                                                                        , data.ControlFlightIY
                                                                        , data.FlightMLCY
                                                                        , data.ControlFlightMLCY
                                                                        , data.FlightCY
                                                                        , data.ControlFlightCY
                                                                        , data.FlightRevenue
                                                                        , data.ControlFlightRevenue
                                                                        , data.FlightImpressions
                                                                        , data.ControlFlightImpressions
                                                                        , data.FlightClicks
                                                                        , data.ControlFlightClicks
                                                                        , data.DaysRunning
                                                                        , data.LastModifiedDate
                                                                        , data.ExperimentType
                                                                        , data.ExperimentAuthor 
                                                                       ));
            }
            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region Experiment Allocation Data
        public ObservableCollection<ExperimentAllocationData> GetExperimentAllocationData(string selectedView)
        {
            ObservableCollection<ExperimentAllocationData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //var flightsdata = new ObservableCollection<ExperimentAllocationData>(from data in ObjFlightCubeData
                //                                                                     where
                //                                                                       data.Network == strViewArray[0]
                //                                                                       && data.Partner == strViewArray[1]
                //                                                                       && data.Country == strViewArray[2]
                //                                                                       && data.Medium == strViewArray[3]
                //                                                                       && data.Device == strViewArray[4]
                //                                                                     //&& data.FeatureAreaType == strViewArray[5]
                //                                                                     group data by new { data.Date, data.ExperimentName, data.TrafficType }
                //                                                                         into grp
                //                                                                     select new ExperimentAllocationData(
                //                                                           grp.Key.Date
                //                                                           , grp.Key.ExperimentName
                //                                                           , grp.Key.TrafficType
                //                                                           , Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD))
                //                                                           , grp.Sum(item => item.Srpv)
                //                                                           , grp.Sum(item => item.Impressions)
                //                                                           , grp.Sum(item => item.Clicks)
                //                                                           )).ToList();

                //Get total SPRV's based on the dates
                var totalSrpv = new ObservableCollection<ExperimentAllocationData>(from data in ObjFlightCubeData
                                                                                   where
                                                                                     data.Network == strViewArray[0]
                                                                                     && data.Partner == strViewArray[1]
                                                                                     && data.Country == strViewArray[2]
                                                                                     && data.Medium == strViewArray[3]
                                                                                     && data.Device == strViewArray[4]
                                                                                   group data by new { data.Date }
                                                                                       into grp
                                                                                       select new ExperimentAllocationData(grp.Key.Date, grp.Sum(item => item.Srpv))).ToList();

                //Get each experimentName allocation, formula is each experiment SRPV/Total SPRV
                var sprvbasedonExperiment = new ObservableCollection<ExperimentAllocationData>(from data in ObjFlightCubeData
                                                                                               where
                                                                                                   data.Network == strViewArray[0]
                                                                                                   && data.Partner == strViewArray[1]
                                                                                                   && data.Country == strViewArray[2]
                                                                                                   && data.Medium == strViewArray[3]
                                                                                                   && data.Device == strViewArray[4]
                                                                                                   && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                   && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                               group data by new { data.Date, data.ExperimentName }
                                                                                                   into grp
                                                                                                   select new ExperimentAllocationData(grp.Key.Date
                                                                                                           , grp.Key.ExperimentName
                                                                                                           , grp.Sum(item => item.Srpv)
                                                                                                           , Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) /
                                                                                                            Convert.ToDouble((from f in totalSrpv
                                                                                                                              where f.Date == grp.Key.Date
                                                                                                                              select f.TotalSrpv).FirstOrDefault())))
                                                                                                                                   ).ToList();

                //Total experimentallocation for each day
                result = new ObservableCollection<ExperimentAllocationData>(from data in sprvbasedonExperiment
                                                                            group data by new { data.Date }
                                                                                into grp
                                                                                select new ExperimentAllocationData(grp.Key.Date
                                                                                        , grp.Sum(item => item.ExperimentAllocation)
                                                                                        , Convert.ToDouble(strAllocationBudget)
                                                                                        , DateTime.Now));

            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region RPS Contribution Data
        public ObservableCollection<RPSContributionData> GetRPSContributionData(string selectedView)
        {
            ObservableCollection<RPSContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                   where
                                                                                       data.Network == strViewArray[0]
                                                                                       && data.Partner == strViewArray[1]
                                                                                       && data.Country == strViewArray[2]
                                                                                       && data.Medium == strViewArray[3]
                                                                                       && data.Device == strViewArray[4]
                                                                                       && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                   //&& data.FeatureAreaType == strViewArray[5]
                                                                                   group data by new { data.Date, data.FlightNo }
                                                                                       into grp
                                                                                       select new RPSContributionData(
                                                                                           //Date
                                                                                           grp.Key.Date
                                                                                           //Fight No
                                                                                           , grp.Key.FlightNo
                                                                                           //Total SRPV
                                                                                           , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                           //Total Revenue
                                                                                           , Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD))
                                                                                           )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                     where
                                                                                         data.Network == strViewArray[0]
                                                                                         && data.Partner == strViewArray[1]
                                                                                         && data.Country == strViewArray[2]
                                                                                         && data.Medium == strViewArray[3]
                                                                                         && data.Device == strViewArray[4]
                                                                                         && data.FlightNo == Convert.ToInt16(Convert.ToInt16(strViewArray[8]))
                                                                                     //&& data.FeatureAreaType == strViewArray[5]
                                                                                     group data by new { data.Date, data.FlightNo }
                                                                                         into grp
                                                                                         select new RPSContributionData(
                                                                                             //Date
                                                                                           grp.Key.Date
                                                                                             //Fight No
                                                                                           , grp.Key.FlightNo
                                                                                             //Total SRPV
                                                                                           , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                             //Total Revenue
                                                                                           , Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD))
                                                                                           )).ToList();


                //Get each Tied and Untied RPM Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight SPRV else untied Flight SRPV
                var tieduntiedControlRPM = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                         where
                                                                                             //data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                             // && 
                                                                                           data.Network == strViewArray[0]
                                                                                             && data.Partner == strViewArray[1]
                                                                                             && data.Country == strViewArray[2]
                                                                                             && data.Medium == strViewArray[3]
                                                                                             && data.Device == strViewArray[4]
                                                                                             && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                             && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                             && data.ExperimentName != ""
                                                                                         group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                             into grp
                                                                                             select new RPSContributionData(
                                                                                                 //Date
                                                                                                            grp.Key.Date
                                                                                                 //Experiment Name
                                                                                                          , grp.Key.ExperimentName
                                                                                                 //Traffic Type
                                                                                                          , grp.Key.TrafficType
                                                                                                 // SRPV
                                                                                                          , grp.Sum(item => item.Srpv)
                                                                                                 //RPM
                                                                                                 , grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                   where f.Date == grp.Key.Date
                                                                                                                                   select Convert.ToDouble((Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv)) * 1000)).FirstOrDefault())
                                                                                                                  :
                                                                                                                 Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                   where f.Date == grp.Key.Date
                                                                                                                                   select Convert.ToDouble((Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv)) * 1000)).FirstOrDefault())
                                                                                                 //Control RPM 
                                                                                                          , grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) / 1000) *
                                                                                                                 (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble((Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv)) * 1000)).FirstOrDefault())))
                                                                                                                  :
                                                                                                                  Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) / 1000) *
                                                                                                                 (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble((Convert.ToDouble(f.TotalRevenue) / Convert.ToDouble(f.TotalSrpv)) * 1000)).FirstOrDefault())))
                                                                                               ));



                //Revenue Contribution. Formula is Revenue - ControlRPM
                var revenueContribution = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                        where
                                                                                            //data.Date == DateTime.Now.Date.AddDays(-iToDays)
                                                                                            // && 
                                                                                           data.Network == strViewArray[0]
                                                                                            && data.Partner == strViewArray[1]
                                                                                            && data.Country == strViewArray[2]
                                                                                            && data.Medium == strViewArray[3]
                                                                                            && data.Device == strViewArray[4]
                                                                                            && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                            && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                            && data.ExperimentName != ""
                                                                                        group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                            into grp
                                                                                            select new RPSContributionData(
                                                                                                //Date
                                                                                                grp.Key.Date
                                                                                                //ExperimentName
                                                                                                , grp.Key.ExperimentName
                                                                                                //Traffic Type
                                                                                                , grp.Key.TrafficType
                                                                                                //Revenue
                                                                                                , grp.Sum(item => item.GrossRevenueUSD)
                                                                                                //RevenueContribution
                                                                                                ,
                                                                                                Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) -
                                                                                                Convert.ToDouble((from f in tieduntiedControlRPM
                                                                                                                  where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == grp.Key.TrafficType
                                                                                                                  select f.ControlRPM).FirstOrDefault())))
                                                                                                                    ).ToList();


                var totaldata = new ObservableCollection<CurrentViewData>(from data in ObjFlightCubeData
                                                                          where
                                                                          data.Network == strViewArray[0]
                                                                          && data.Partner == strViewArray[1]
                                                                          && data.Country == strViewArray[2]
                                                                          && data.Medium == strViewArray[3]
                                                                          && data.Device == strViewArray[4]
                                                                          group data by
                                                                              new { data.Date }
                                                                              into grp
                                                                              select new CurrentViewData(
                                                                                  grp.Key.Date
                                                                                  //Revenue
                                                                                  , grp.Sum(item => item.GrossRevenueUSD)
                                                                                  //SRPV
                                                                                  , grp.Sum(item => item.Srpv)
                                                                                  //Clicks
                                                                                  , grp.Sum(item => item.Clicks)
                                                                                  //Impressions
                                                                                  , grp.Sum(item => item.Impressions)
                                                                                  //RPM
                                                                                  , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 1000)
                                                                                  // Click Yield
                                                                                  , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                  //Impression Yield
                                                                                  , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                  )).ToList();


                result = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                       where
                                                                           data.Network == strViewArray[0]
                                                                           && data.Partner == strViewArray[1]
                                                                           && data.Country == strViewArray[2]
                                                                           && data.Medium == strViewArray[3]
                                                                           && data.Device == strViewArray[4]
                                                                       group data by new { data.Date }
                                                                           into grp
                                                                           select new RPSContributionData(
                                                                               //Date
                                                                                grp.Key.Date
                                                                               //RPS Contribution
                                                                                , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble((from d in totaldata
                                                                                                                                                                        where d.Date == grp.Key.Date
                                                                                                                                                                        select d.GrossRevenueUSD).Sum()) +
                                                                                                   Convert.ToDouble((from f in revenueContribution
                                                                                                                     where f.Date == grp.Key.Date
                                                                                                                     select f.RevenueContribution).Sum())) /
                                                                                                    Convert.ToDouble((from d in totaldata where d.Date == grp.Key.Date select d.Srpv).Sum()) * 1000)
                                                                                                      - (from d in totaldata where d.Date == grp.Key.Date select d.RPS).Sum())
                                                                                                      / (from d in totaldata where d.Date == grp.Key.Date select d.RPS).Sum())

                                                                              // LastRefreshed
                                                                                 , DateTime.Now
                                                                                 ));

            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region MLIY Contribution Data
        public ObservableCollection<MLIYContributionData> GetMLIYContributionData(string selectedView)
        {
            ObservableCollection<MLIYContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                    //&& data.FeatureAreaType == strViewArray[5]
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new MLIYContributionData(
                                                                                            //Date
                                                                                grp.Key.Date
                                                                                            //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                            //SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Impression Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                            //Impressions
                                                                                , grp.Sum(item => item.Impressions)
                                                                                )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                      where
                                                                                          data.Network == strViewArray[0]
                                                                                          && data.Partner == strViewArray[1]
                                                                                          && data.Country == strViewArray[2]
                                                                                          && data.Medium == strViewArray[3]
                                                                                          && data.Device == strViewArray[4]
                                                                                          && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                          && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                      //&& data.FeatureAreaType == strViewArray[5]
                                                                                      group data by new { data.Date, data.FlightNo }
                                                                                          into grp
                                                                                          select new MLIYContributionData(
                                                                                              //Date
                                                                                            grp.Key.Date
                                                                                              //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                              //SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                              //Impression Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                              //Impressions
                                                                                          , grp.Sum(item => item.Impressions)
                                                                                          )).ToList();



                //Get each Tied and Untied MLIY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ImpressionYield else untied Flight ImpressionYield
                var tieduntiedMLIYContribution = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                where
                                                                                                    data.Network == strViewArray[0]
                                                                                                    && data.Partner == strViewArray[1]
                                                                                                    && data.Country == strViewArray[2]
                                                                                                    && data.Medium == strViewArray[3]
                                                                                                    && data.Device == strViewArray[4]
                                                                                                    && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                    && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                    && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                                group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                                    into grp
                                                                                                    select new MLIYContributionData(grp.Key.Date
                                                                                                                , grp.Key.ExperimentName
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                                                , grp.Sum(item => item.Impressions)
                                                                                                        // MLIY Control
                                                                                                                ,
                                                                                                                grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ImpressionYield)).FirstOrDefault()) / 100) :
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ImpressionYield)).FirstOrDefault()) / 100)
                                                                                                                     ));


                //Get each Tied and Untied Mainline Impressions
                var tieduntiedMainlineImpressions = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                   where
                                                                                                       data.Network == strViewArray[0]
                                                                                                       && data.Partner == strViewArray[1]
                                                                                                       && data.Country == strViewArray[2]
                                                                                                       && data.Medium == strViewArray[3]
                                                                                                       && data.Device == strViewArray[4]
                                                                                                       && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                       && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                       && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                                   group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                                       into grp
                                                                                                       select new MLIYContributionData(grp.Key.Date
                                                                                                          , grp.Key.ExperimentName
                                                                                                          , grp.Key.TrafficType
                                                                                                          , grp.Sum(item => item.Impressions)
                                                                                                           //MainLine Impressions
                                                                                                           , grp.Key.TrafficType == "Tied" ?
                                                                                                            Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                            Convert.ToDouble((from f in tieduntiedMLIYContribution
                                                                                                                              where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Tied"
                                                                                                                              select f.MLIYControl).FirstOrDefault()))
                                                                                                                              :
                                                                                                            Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                            Convert.ToDouble((from f in tieduntiedMLIYContribution
                                                                                                                              where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Untied"
                                                                                                                              select f.MLIYControl).FirstOrDefault()))
                                                                                                          , DateTime.Now
                                                                                                                               ));

                //Get total Mainline Impressions
                var totalAdjustedMainlineImpressions = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                      where
                                                                                                          data.Network == strViewArray[0]
                                                                                                          && data.Partner == strViewArray[1]
                                                                                                          && data.Country == strViewArray[2]
                                                                                                          && data.Medium == strViewArray[3]
                                                                                                          && data.Device == strViewArray[4]
                                                                                                          && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                      //&& data.FeatureAreaType == strViewArray[5]
                                                                                                      group data by new { data.Date }
                                                                                                          into grp
                                                                                                          select new MLIYContributionData(grp.Key.Date
                                                                                                              //Impressions
                                                                                                              , grp.Sum(item => item.Impressions)
                                                                                                              //MainLine Impressions
                                                                                                              , Convert.ToDouble((from f in tieduntiedMainlineImpressions
                                                                                                                                  where f.Date == grp.Key.Date
                                                                                                                                  select f.MainlineImpressions).Sum())
                                                                                                              //Adjusted MainLine Impressions
                                                                                                              , Convert.ToDouble(grp.Sum(item => item.Impressions) +
                                                                                                                  Convert.ToDouble((from f in tieduntiedMainlineImpressions
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select f.MainlineImpressions).Sum()))

                                                                                                                   , DateTime.Now
                                                                                                                   , 1
                                                                                                                   ));

                //Get MLIY Delta
                var mliyDelta = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                               where
                                                                                   data.Network == strViewArray[0]
                                                                                   && data.Partner == strViewArray[1]
                                                                                   && data.Country == strViewArray[2]
                                                                                   && data.Medium == strViewArray[3]
                                                                                   && data.Device == strViewArray[4]
                                                                                   && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                               //&& data.FeatureAreaType == strViewArray[5]
                                                                               group data by new { data.Date }
                                                                                   into grp
                                                                                   select new MLIYContributionData(grp.Key.Date
                                                                                       //Impressions
                                                                                                        , grp.Sum(item => item.Impressions)
                                                                                       //Srpv
                                                                                                        , grp.Sum(item => item.Srpv)
                                                                                       //Impression Yield
                                                                                                        , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                       //Adjusted Mainline Impressions
                                                                                                          , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedMainlineImpressions
                                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                                               select f.AdjustedMainlineImpressions).FirstOrDefault())
                                                                                                            + grp.Sum(item => item.Impressions))
                                                                                       //Adjusted MLIY Delta
                                                                                                           , Convert.ToDouble((from f in totalAdjustedMainlineImpressions
                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                               select f.AdjustedMainlineImpressions).FirstOrDefault())
                                                                                                                /
                                                                                                             (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100)

                                                                                       // MLIY Delta
                                                                                                           , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedMainlineImpressions
                                                                                                                                                where f.Date == grp.Key.Date
                                                                                                                                                select f.AdjustedMainlineImpressions).FirstOrDefault())
                                                                                                                /
                                                                                                             (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100))
                                                                                                             - Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)

                                                                                                           , DateTime.Now
                                                                                                           , 1
                                                                                                           ));




                //Total MLIY Contribution for each day
                result = new ObservableCollection<MLIYContributionData>(from data in mliyDelta
                                                                        group data by new { data.Date }
                                                                            into grp
                                                                            select new MLIYContributionData(
                                                                                //Date
                                                                                grp.Key.Date
                                                                                //MLIY Contribution
                                                                                 , Convert.ToDouble(grp.Sum(item => item.MLIYDelta)
                                                                                 / Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100))
                                                                                // LastRefreshed
                                                                                 , DateTime.Now
                                                                                 ));


            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region MLCY Contribution Data
        public ObservableCollection<MLCYContributionData> GetMLCYContributionData(string selectedView)
        {
            ObservableCollection<MLCYContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                    //&& data.FeatureAreaType == strViewArray[5]
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new MLCYContributionData(
                                                                                            //Date
                                                                                grp.Key.Date
                                                                                            //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                            //SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Click Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                            //Clicks
                                                                                , grp.Sum(item => item.Clicks)
                                                                                )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                      where
                                                                                          data.Network == strViewArray[0]
                                                                                          && data.Partner == strViewArray[1]
                                                                                          && data.Country == strViewArray[2]
                                                                                          && data.Medium == strViewArray[3]
                                                                                          && data.Device == strViewArray[4]
                                                                                          && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                          && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                      //&& data.FeatureAreaType == strViewArray[5]
                                                                                      group data by new { data.Date, data.FlightNo }
                                                                                          into grp
                                                                                          select new MLCYContributionData(
                                                                                              //Date
                                                                                            grp.Key.Date
                                                                                              //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                              //SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                              //Clicks Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                              //Clicks
                                                                                          , grp.Sum(item => item.Clicks)
                                                                                          )).ToList();



                //Get each Tied and Untied MLCY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ClickYield else untied Flight ClickYield
                var tieduntiedMLCYContribution = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                                where
                                                                                                    data.Network == strViewArray[0]
                                                                                                    && data.Partner == strViewArray[1]
                                                                                                    && data.Country == strViewArray[2]
                                                                                                    && data.Medium == strViewArray[3]
                                                                                                    && data.Device == strViewArray[4]
                                                                                                    && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                    && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                    && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                                group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                                    into grp
                                                                                                    select new MLCYContributionData(grp.Key.Date
                                                                                                                , grp.Key.ExperimentName
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                                                , grp.Sum(item => item.Impressions)
                                                                                                        // MLCY Control
                                                                                                                ,
                                                                                                                grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ClickYield)).FirstOrDefault()) / 100) :
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ClickYield)).FirstOrDefault()) / 100)
                                                                                                                     ));


                //Get each Tied and Untied Mainline Clicks
                var tieduntiedMainlineClicks = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                              where
                                                                                                  data.Network == strViewArray[0]
                                                                                                  && data.Partner == strViewArray[1]
                                                                                                  && data.Country == strViewArray[2]
                                                                                                  && data.Medium == strViewArray[3]
                                                                                                  && data.Device == strViewArray[4]
                                                                                                  && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                  && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                  && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                              group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                                  into grp
                                                                                                  select new MLCYContributionData(grp.Key.Date
                                                                                                     , grp.Key.ExperimentName
                                                                                                     , grp.Key.TrafficType
                                                                                                     , grp.Sum(item => item.Impressions)
                                                                                                      //MainLine Clicks
                                                                                                      , grp.Key.TrafficType == "Tied" ?
                                                                                                       Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                                       Convert.ToDouble((from f in tieduntiedMLCYContribution
                                                                                                                         where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Tied"
                                                                                                                         select f.MLCYControl).FirstOrDefault()))
                                                                                                                         :
                                                                                                       Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                                       Convert.ToDouble((from f in tieduntiedMLCYContribution
                                                                                                                         where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Untied"
                                                                                                                         select f.MLCYControl).FirstOrDefault()))
                                                                                                     , DateTime.Now
                                                                                                                          ));

                //Get total Mainline Clicks
                var totalAdjustedMainlineClicks = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                                                 where
                                                                                                     data.Network == strViewArray[0]
                                                                                                     && data.Partner == strViewArray[1]
                                                                                                     && data.Country == strViewArray[2]
                                                                                                     && data.Medium == strViewArray[3]
                                                                                                     && data.Device == strViewArray[4]
                                                                                                     && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                 //&& data.FeatureAreaType == strViewArray[5]
                                                                                                 group data by new { data.Date }
                                                                                                     into grp
                                                                                                     select new MLCYContributionData(grp.Key.Date
                                                                                                         //Clicks
                                                                                                         , grp.Sum(item => item.Clicks)
                                                                                                         //MainLine Clicks
                                                                                                         , Convert.ToDouble((from f in tieduntiedMainlineClicks
                                                                                                                             where f.Date == grp.Key.Date
                                                                                                                             select f.MainlineClicks).Sum())
                                                                                                         //Adjusted MainLine Clicks
                                                                                                         , Convert.ToDouble(grp.Sum(item => item.Clicks) +
                                                                                                             Convert.ToDouble((from f in tieduntiedMainlineClicks
                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                               select f.MainlineClicks).Sum()))

                                                                                                              , DateTime.Now
                                                                                                              , 1
                                                                                                              ));

                //Get MLCY Delta
                var mlcyDelta = new ObservableCollection<MLCYContributionData>(from data in ObjFlightCubeData
                                                                               where
                                                                                   data.Network == strViewArray[0]
                                                                                   && data.Partner == strViewArray[1]
                                                                                   && data.Country == strViewArray[2]
                                                                                   && data.Medium == strViewArray[3]
                                                                                   && data.Device == strViewArray[4]
                                                                                   && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                               //&& data.FeatureAreaType == strViewArray[5]
                                                                               group data by new { data.Date }
                                                                                   into grp
                                                                                   select new MLCYContributionData(grp.Key.Date
                                                                                       //Clicks
                                                                                                        , grp.Sum(item => item.Clicks)
                                                                                       //Srpv
                                                                                                        , grp.Sum(item => item.Srpv)
                                                                                       //Click Yield
                                                                                                        , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                       //Adjusted Mainline Clicks
                                                                                                          , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedMainlineClicks
                                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                                               select f.AdjustedMainlineClicks).FirstOrDefault())
                                                                                                            + grp.Sum(item => item.Clicks))
                                                                                       //Adjusted MLCY Delta
                                                                                                           , Convert.ToDouble((from f in totalAdjustedMainlineClicks
                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                               select f.AdjustedMainlineClicks).FirstOrDefault())
                                                                                                                /
                                                                                                             (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100)

                                                                                       // MLCY Delta
                                                                                                           , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedMainlineClicks
                                                                                                                                                where f.Date == grp.Key.Date
                                                                                                                                                select f.AdjustedMainlineClicks).FirstOrDefault())
                                                                                                                /
                                                                                                             (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100))
                                                                                                             - Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)

                                                                                                           , DateTime.Now
                                                                                                           , 1
                                                                                                           ));




                //Total MLCY Contribution for each day
                result = new ObservableCollection<MLCYContributionData>(from data in mlcyDelta
                                                                        group data by new { data.Date }
                                                                            into grp
                                                                            select new MLCYContributionData(
                                                                                //Date
                                                                                grp.Key.Date
                                                                                //MLCY Contribution
                                                                                 , Convert.ToDouble(grp.Sum(item => item.MLCYDelta)
                                                                                 / Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100))
                                                                                // LastRefreshed
                                                                                 , DateTime.Now
                                                                                 ));


            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region CY Contribution Data
        public ObservableCollection<CYContributionData> GetCYContributionData(string selectedView)
        {
            ObservableCollection<CYContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                  where
                                                                                      data.Network == strViewArray[0]
                                                                                      && data.Partner == strViewArray[1]
                                                                                      && data.Country == strViewArray[2]
                                                                                      && data.Medium == strViewArray[3]
                                                                                      && data.Device == strViewArray[4]
                                                                                      && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                  //&& data.FeatureAreaType == strViewArray[5]
                                                                                  group data by new { data.Date, data.FlightNo }
                                                                                      into grp
                                                                                      select new CYContributionData(
                                                                                          //Date
                                                                                grp.Key.Date
                                                                                          //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                          //SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                          //Click Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                          //Clicks
                                                                                , grp.Sum(item => item.Clicks)
                                                                                )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                    //&& data.FeatureAreaType == strViewArray[5]
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new CYContributionData(
                                                                                            //Date
                                                                                            grp.Key.Date
                                                                                            //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                            //SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Click Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                            //Clicks
                                                                                          , grp.Sum(item => item.Clicks)
                                                                                          )).ToList();



                //Get each Tied and Untied CY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ClickYield else untied Flight ClickYield
                var tieduntiedCYContribution = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                            where
                                                                                                data.Network == strViewArray[0]
                                                                                                && data.Partner == strViewArray[1]
                                                                                                && data.Country == strViewArray[2]
                                                                                                && data.Medium == strViewArray[3]
                                                                                                && data.Device == strViewArray[4]
                                                                                                && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                            group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                                into grp
                                                                                                select new CYContributionData(
                                                                                                    grp.Key.Date
                                                                                                                , grp.Key.ExperimentName
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , grp.Sum(item => item.Clicks)
                                                                                                                , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                                    // CY Control
                                                                                                                ,
                                                                                                                grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ClickYield)).FirstOrDefault()) / 100) :
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ClickYield)).FirstOrDefault()) / 100)
                                                                                                                     ));


                //Get each Tied and Untied Clicks
                var tieduntiedClicks = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                        && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                    group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                        into grp
                                                                                        select new CYContributionData(
                                                                                            grp.Key.Date
                                                                                           , grp.Key.ExperimentName
                                                                                           , grp.Key.TrafficType
                                                                                           , grp.Sum(item => item.Clicks)
                                                                                            //Clicks Contribution
                                                                                            , grp.Key.TrafficType == "Tied" ?
                                                                                             Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                             Convert.ToDouble((from f in tieduntiedCYContribution
                                                                                                               where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Tied"
                                                                                                               select f.CYControl).FirstOrDefault()))
                                                                                                               :
                                                                                             Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                             Convert.ToDouble((from f in tieduntiedCYContribution
                                                                                                               where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Untied"
                                                                                                               select f.CYControl).FirstOrDefault()))
                                                                                           , DateTime.Now
                                                                                                                ));

                //Get total Adjusted Clicks
                var totalAdjustedClicks = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                       where
                                                                                           data.Network == strViewArray[0]
                                                                                           && data.Partner == strViewArray[1]
                                                                                           && data.Country == strViewArray[2]
                                                                                           && data.Medium == strViewArray[3]
                                                                                           && data.Device == strViewArray[4]
                                                                                       group data by new { data.Date }
                                                                                           into grp
                                                                                           select new CYContributionData(
                                                                                                            grp.Key.Date
                                                                                               //Clicks
                                                                                                              , grp.Sum(item => item.Clicks)
                                                                                               //Total Clicks Contribution
                                                                                                              , Convert.ToDouble((from f in tieduntiedClicks
                                                                                                                                  where f.Date == grp.Key.Date
                                                                                                                                  select f.ClicksContribution).Sum())
                                                                                               //Adjusted Clicks Contribution
                                                                                                              , Convert.ToDouble(grp.Sum(item => item.Clicks) +
                                                                                                                  Convert.ToDouble((from f in tieduntiedClicks
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select f.ClicksContribution).Sum()))

                                                                                                                   , DateTime.Now
                                                                                                                   , 1
                                                                                                                   ));

                //Get CY Delta
                var cyDelta = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                           where
                                                                               data.Network == strViewArray[0]
                                                                               && data.Partner == strViewArray[1]
                                                                               && data.Country == strViewArray[2]
                                                                               && data.Medium == strViewArray[3]
                                                                               && data.Device == strViewArray[4]
                                                                           //&& data.FeatureAreaType == strViewArray[5]
                                                                           group data by new { data.Date }
                                                                               into grp
                                                                               select new CYContributionData(grp.Key.Date
                                                                                   //Clicks
                                                                                                    , grp.Sum(item => item.Clicks)
                                                                                   //Srpv
                                                                                                    , grp.Sum(item => item.Srpv)
                                                                                   //Clicks Yield
                                                                                                    , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                   //Adjusted Clicks Contribution
                                                                                                      , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedClicks
                                                                                                                                           where f.Date == grp.Key.Date
                                                                                                                                           select f.AdjustedClicksContribution).FirstOrDefault())
                                                                                                        + grp.Sum(item => item.Clicks))
                                                                                   //Adjusted CY Delta
                                                                                                       , Convert.ToDouble((from f in totalAdjustedClicks
                                                                                                                           where f.Date == grp.Key.Date
                                                                                                                           select f.AdjustedClicksContribution).FirstOrDefault())
                                                                                                            /
                                                                                                         (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100)

                                                                                   // CY Delta
                                                                                                       , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedClicks
                                                                                                                                            where f.Date == grp.Key.Date
                                                                                                                                            select f.AdjustedClicksContribution).FirstOrDefault())
                                                                                                            /
                                                                                                         (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100))
                                                                                                         - Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)

                                                                                                       , DateTime.Now
                                                                                                       , 1
                                                                                                       ));




                //Total CY Contribution for each day
                result = new ObservableCollection<CYContributionData>(from data in cyDelta
                                                                      group data by new { data.Date }
                                                                          into grp
                                                                          select new CYContributionData(
                                                                              //Date
                                                                                grp.Key.Date
                                                                              //CY Contribution
                                                                                 , Convert.ToDouble(grp.Sum(item => item.CYDelta)
                                                                                 / Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100))
                                                                              // LastRefreshed
                                                                                 , DateTime.Now
                                                                                 ));


            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region IY Contribution Data
        public ObservableCollection<IYContributionData> GetIYContributionData(string selectedView)
        {
            ObservableCollection<IYContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                  where
                                                                                      data.Network == strViewArray[0]
                                                                                      && data.Partner == strViewArray[1]
                                                                                      && data.Country == strViewArray[2]
                                                                                      && data.Medium == strViewArray[3]
                                                                                      && data.Device == strViewArray[4]
                                                                                      && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                  //&& data.FeatureAreaType == strViewArray[5]
                                                                                  group data by new { data.Date, data.FlightNo }
                                                                                      into grp
                                                                                      select new IYContributionData(
                                                                                          //Date
                                                                                grp.Key.Date
                                                                                          //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                          //SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                          //Impression Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                          //Impressions
                                                                                , grp.Sum(item => item.Impressions)
                                                                                )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                    //&& data.FeatureAreaType == strViewArray[5]
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new IYContributionData(
                                                                                            //Date
                                                                                            grp.Key.Date
                                                                                            //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                            //SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Impression Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                            //Impressions
                                                                                          , grp.Sum(item => item.Impressions)
                                                                                          )).ToList();



                //Get each Tied and Untied IY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ImpressionYield else untied Flight ImpressionYield
                var tieduntiedIYContribution = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                            where
                                                                                                data.Network == strViewArray[0]
                                                                                                && data.Partner == strViewArray[1]
                                                                                                && data.Country == strViewArray[2]
                                                                                                && data.Medium == strViewArray[3]
                                                                                                && data.Device == strViewArray[4]
                                                                                                && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                                && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                            group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                                into grp
                                                                                                select new IYContributionData(
                                                                                                    grp.Key.Date
                                                                                                                , grp.Key.ExperimentName
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , grp.Sum(item => item.Impressions)
                                                                                                                , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                                    // IY Control
                                                                                                                ,
                                                                                                                grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ImpressionYield)).FirstOrDefault()) / 100) :
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ImpressionYield)).FirstOrDefault()) / 100)
                                                                                                                     ));


                //Get each Tied and Untied Impressions
                var tieduntiedImpressions = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                         where
                                                                                             data.Network == strViewArray[0]
                                                                                             && data.Partner == strViewArray[1]
                                                                                             && data.Country == strViewArray[2]
                                                                                             && data.Medium == strViewArray[3]
                                                                                             && data.Device == strViewArray[4]
                                                                                             && (strViewArray[5] == "ALL" ? true : data.FeatureAreaType == strViewArray[5])
                                                                                             && (strViewArray[6] == "ALL" ? true : data.ExperimentType == strViewArray[6])
                                                                                         group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                             into grp
                                                                                             select new IYContributionData(
                                                                                                 grp.Key.Date
                                                                                                , grp.Key.ExperimentName
                                                                                                , grp.Key.TrafficType
                                                                                                , grp.Sum(item => item.Impressions)
                                                                                                 //Impressions Contribution
                                                                                                 , grp.Key.TrafficType == "Tied" ?
                                                                                                  Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                  Convert.ToDouble((from f in tieduntiedIYContribution
                                                                                                                    where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Tied"
                                                                                                                    select f.IYControl).FirstOrDefault()))
                                                                                                                    :
                                                                                                  Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                  Convert.ToDouble((from f in tieduntiedIYContribution
                                                                                                                    where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName && f.TrafficType == "Untied"
                                                                                                                    select f.IYControl).FirstOrDefault()))
                                                                                                , DateTime.Now
                                                                                                                     ));

                //Get total Adjusted Impressions
                var totalAdjustedImpressions = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                                            where
                                                                                                data.Network == strViewArray[0]
                                                                                                && data.Partner == strViewArray[1]
                                                                                                && data.Country == strViewArray[2]
                                                                                                && data.Medium == strViewArray[3]
                                                                                                && data.Device == strViewArray[4]
                                                                                            group data by new { data.Date }
                                                                                                into grp
                                                                                                select new IYContributionData(
                                                                                                                 grp.Key.Date
                                                                                                    //Impressions
                                                                                                                   , grp.Sum(item => item.Impressions)
                                                                                                    //Total Impressions Contribution
                                                                                                                   , Convert.ToDouble((from f in tieduntiedImpressions
                                                                                                                                       where f.Date == grp.Key.Date
                                                                                                                                       select f.ImpressionsContribution).Sum())
                                                                                                    //Adjusted Impressions Contribution
                                                                                                                   , Convert.ToDouble(grp.Sum(item => item.Impressions) +
                                                                                                                       Convert.ToDouble((from f in tieduntiedImpressions
                                                                                                                                         where f.Date == grp.Key.Date
                                                                                                                                         select f.ImpressionsContribution).Sum()))

                                                                                                                        , DateTime.Now
                                                                                                                        , 1
                                                                                                                        ));

                //Get IY Delta
                var iyDelta = new ObservableCollection<IYContributionData>(from data in ObjFlightCubeData
                                                                           where
                                                                               data.Network == strViewArray[0]
                                                                               && data.Partner == strViewArray[1]
                                                                               && data.Country == strViewArray[2]
                                                                               && data.Medium == strViewArray[3]
                                                                               && data.Device == strViewArray[4]
                                                                           //&& data.FeatureAreaType == strViewArray[5]
                                                                           group data by new { data.Date }
                                                                               into grp
                                                                               select new IYContributionData(grp.Key.Date
                                                                                   //Impressions
                                                                                                    , grp.Sum(item => item.Impressions)
                                                                                   //Srpv
                                                                                                    , grp.Sum(item => item.Srpv)
                                                                                   //Impressions Yield
                                                                                                    , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                   //Adjusted Impressions Contribution
                                                                                                      , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedImpressions
                                                                                                                                           where f.Date == grp.Key.Date
                                                                                                                                           select f.AdjustedImpressionsContribution).FirstOrDefault())
                                                                                                        + grp.Sum(item => item.Impressions))
                                                                                   //Adjusted IY Delta
                                                                                                       , Convert.ToDouble((from f in totalAdjustedImpressions
                                                                                                                           where f.Date == grp.Key.Date
                                                                                                                           select f.AdjustedImpressionsContribution).FirstOrDefault())
                                                                                                            /
                                                                                                         (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100)

                                                                                   // IY Delta
                                                                                                       , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedImpressions
                                                                                                                                            where f.Date == grp.Key.Date
                                                                                                                                            select f.AdjustedImpressionsContribution).FirstOrDefault())
                                                                                                            /
                                                                                                         (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100))
                                                                                                         - Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)

                                                                                                       , DateTime.Now
                                                                                                       , 1
                                                                                                       ));




                //Total IY Contribution for each day
                result = new ObservableCollection<IYContributionData>(from data in iyDelta
                                                                      group data by new { data.Date }
                                                                          into grp
                                                                          select new IYContributionData(
                                                                              //Date
                                                                                grp.Key.Date
                                                                              //IY Contribution
                                                                                 , Convert.ToDouble(grp.Sum(item => item.IYDelta)
                                                                                 / Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100))
                                                                              // LastRefreshed
                                                                                 , DateTime.Now
                                                                                 ));


            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region Network Experiment Allocation Data
        public ObservableCollection<ExperimentAllocationData> GetNetworkExperimentAllocationData(string selectedView)
        {
            ObservableCollection<ExperimentAllocationData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Get total SPRV's based on the dates
                var totalSrpv = new ObservableCollection<ExperimentAllocationData>(from data in ObjFlightCubeData
                                                                                   where
                                                                                     data.Network == strViewArray[0]
                                                                                     && data.Partner == strViewArray[1]
                                                                                     && data.Country == strViewArray[2]
                                                                                     && data.Medium == strViewArray[3]
                                                                                     && data.Device == strViewArray[4]
                                                                                   group data by new { data.Date }
                                                                                       into grp
                                                                                       select new ExperimentAllocationData(grp.Key.Date, grp.Sum(item => item.Srpv))).ToList();

                //Get each experimentName allocation, formula is each experiment SRPV/Total SPRV
                var sprvbasedonExperiment = new ObservableCollection<ExperimentAllocationData>(from data in ObjFlightCubeData
                                                                                               where
                                                                                                   data.Network == strViewArray[0]
                                                                                                   && data.Partner == strViewArray[1]
                                                                                                   && data.Country == strViewArray[2]
                                                                                                   && data.Medium == strViewArray[3]
                                                                                                   && data.Device == strViewArray[4]
                                                                                               //  && data.FeatureAreaType == strViewArray[5]
                                                                                               group data by new { data.Date, data.ExperimentName }
                                                                                                   into grp
                                                                                                   select new ExperimentAllocationData(grp.Key.Date
                                                                                                           , grp.Key.ExperimentName
                                                                                                           , grp.Sum(item => item.Srpv)
                                                                                                           , Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) /
                                                                                                            Convert.ToDouble((from f in totalSrpv
                                                                                                                              where f.Date == grp.Key.Date
                                                                                                                              select f.TotalSrpv).FirstOrDefault())))
                                                                                                                                   ).ToList();

                //Total experimentallocation for each day
                result = new ObservableCollection<ExperimentAllocationData>(from data in sprvbasedonExperiment
                                                                            group data by new { data.Date }
                                                                                into grp
                                                                                select new ExperimentAllocationData(grp.Key.Date
                                                                                        , grp.Sum(item => item.ExperimentAllocation)
                                                                                        , Convert.ToDouble(strAllocationBudget)
                                                                                        , DateTime.Now)); ;

            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region Network RPS Contribution Data
        public ObservableCollection<RPSContributionData> GetNetworkRPSContributionData(string selectedView)
        {
            ObservableCollection<RPSContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                   where
                                                                                       data.Network == strViewArray[0]
                                                                                       && data.Partner == strViewArray[1]
                                                                                       && data.Country == strViewArray[2]
                                                                                       && data.Medium == strViewArray[3]
                                                                                       && data.Device == strViewArray[4]
                                                                                       && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                   group data by new { data.Date, data.FlightNo }
                                                                                       into grp
                                                                                       select new RPSContributionData(
                                                                                           //Date
                                                                                           grp.Key.Date
                                                                                           //Fight No
                                                                                           , grp.Key.FlightNo
                                                                                           //Total SRPV
                                                                                           , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                           //RPM
                                                                                           , Convert.ToDouble(grp.Sum(item => item.RPM))
                                                                                           )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                     where
                                                                                         data.Network == strViewArray[0]
                                                                                         && data.Partner == strViewArray[1]
                                                                                         && data.Country == strViewArray[2]
                                                                                         && data.Medium == strViewArray[3]
                                                                                         && data.Device == strViewArray[4]
                                                                                         && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                     group data by new { data.Date, data.FlightNo }
                                                                                         into grp
                                                                                         select new RPSContributionData(
                                                                                             //Date
                                                                                           grp.Key.Date
                                                                                             //Fight No
                                                                                           , grp.Key.FlightNo
                                                                                             //Total SRPV
                                                                                           , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                             //RPM
                                                                                           , Convert.ToDouble(grp.Sum(item => item.RPM))
                                                                                           )).ToList();



                //Get each Tied and Untied RPM Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight SPRV else untied Flight SRPV
                var tieduntiedControlRPM = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                         where
                                                                                             data.Network == strViewArray[0]
                                                                                             && data.Partner == strViewArray[1]
                                                                                             && data.Country == strViewArray[2]
                                                                                             && data.Medium == strViewArray[3]
                                                                                             && data.Device == strViewArray[4]
                                                                                         //&& data.FeatureAreaType == strViewArray[5]
                                                                                         group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                             into grp
                                                                                             select new RPSContributionData(
                                                                                                 //Date
                                                                                                            grp.Key.Date
                                                                                                 //Experiment Name
                                                                                                          , grp.Key.ExperimentName
                                                                                                 //Traffic Type
                                                                                                          , grp.Key.TrafficType
                                                                                                 // SRPV
                                                                                                          , grp.Sum(item => item.Srpv)
                                                                                                 //Control RPM
                                                                                                          , grp.Key.TrafficType == "Tied" ?
                                                                                                           Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                           (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                              where f.Date == grp.Key.Date
                                                                                                                              select f.RPM).FirstOrDefault())) / 1000) :
                                                                                                            Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                            (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                               select f.RPM).FirstOrDefault())) / 1000)
                                                                                                                                  )).ToList();


                //Revenue Contribution. Formula is Revenue - ControlRPM
                var revenueContribution = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                        where
                                                                                            data.Network == strViewArray[0]
                                                                                            && data.Partner == strViewArray[1]
                                                                                            && data.Country == strViewArray[2]
                                                                                            && data.Medium == strViewArray[3]
                                                                                            && data.Device == strViewArray[4]
                                                                                        //&& data.FeatureAreaType == strViewArray[5]
                                                                                        group data by new { data.Date, data.ExperimentName, data.TrafficType }
                                                                                            into grp
                                                                                            select new RPSContributionData(
                                                                                                //Date
                                                                                                grp.Key.Date
                                                                                                //ExperimentName
                                                                                                , grp.Key.ExperimentName
                                                                                                //Traffic Type
                                                                                                , grp.Key.TrafficType
                                                                                                //Revenue
                                                                                                , grp.Sum(item => item.GrossRevenueUSD)
                                                                                                //RevenueContribution
                                                                                                ,
                                                                                                Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) -
                                                                                                Convert.ToDouble((from f in tieduntiedControlRPM
                                                                                                                  where f.Date == grp.Key.Date && f.ExperimentName == grp.Key.ExperimentName
                                                                                                                  select f.ControlRPM).FirstOrDefault())))
                                                                                                                    ).ToList();

                //RPM datewise
                var rpmdata = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                            where
                                                                                data.Network == strViewArray[0]
                                                                                && data.Partner == strViewArray[1]
                                                                                && data.Country == strViewArray[2]
                                                                                && data.Medium == strViewArray[3]
                                                                                && data.Device == strViewArray[4]
                                                                            group data by new { data.Date }
                                                                                into grp
                                                                                select new RPSContributionData(
                                                                                    //Date
                                                                                  grp.Key.Date
                                                                                    //RPM
                                                                                  , (Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 1000
                                                                                  )).ToList();

                //Adjusted Revenue USD. Formula is Revenue + Total Revenue with out FeatureArea Filter
                var rpsContribution = new ObservableCollection<RPSContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                    group data by new { data.Date }
                                                                                        into grp
                                                                                        select new RPSContributionData(
                                                                                            // Date
                                                                                            grp.Key.Date
                                                                                            //SRPV
                                                                                            , grp.Sum(item => item.Srpv)
                                                                                            //Revenue
                                                                                            , grp.Sum(item => item.GrossRevenueUSD)
                                                                                            //RPM
                                                                                            , grp.Sum(item => item.RPM)
                                                                                            //Revenue Contribution
                                                                                            , Convert.ToDouble((from f in revenueContribution
                                                                                                                where f.Date == grp.Key.Date
                                                                                                                select f.RevenueContribution).Sum())
                                                                                            //Adjusted Revenue
                                                                                            , Convert.ToDouble(Convert.ToDouble((from f in revenueContribution
                                                                                                                                 where f.Date == grp.Key.Date
                                                                                                                                 select f.RevenueContribution).Sum()) + Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)))
                                                                                            //Revenue Delta
                                                                                             , Convert.ToDouble((Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) +
                                                                                               Convert.ToDouble((from f in revenueContribution
                                                                                                                 where f.Date == grp.Key.Date
                                                                                                                 select f.RevenueContribution).Sum()))) - grp.Sum(item => item.GrossRevenueUSD))
                                                                                            //Adjusted RPM
                                                                                            , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) +
                                                                                               Convert.ToDouble((from f in revenueContribution
                                                                                                                 where f.Date == grp.Key.Date
                                                                                                                 select f.RevenueContribution).Sum())) /
                                                                                                Convert.ToDouble((grp.Sum(item => item.Srpv) / 1000)))
                                                                                            //RPM Delta   
                                                                                             , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) +
                                                                                               Convert.ToDouble((from f in revenueContribution
                                                                                                                 where f.Date == grp.Key.Date
                                                                                                                 select f.RevenueContribution).Sum())) /
                                                                                                Convert.ToDouble((grp.Sum(item => item.Srpv) / 1000)))
                                                                                                  - Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 1000))
                                                                                            // RPS Contribution
                                                                                            , Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(Convert.ToDouble(grp.Sum(item => item.GrossRevenueUSD)) +
                                                                                               Convert.ToDouble((from f in revenueContribution
                                                                                                                 where f.Date == grp.Key.Date
                                                                                                                 select f.RevenueContribution).Sum())) /
                                                                                                Convert.ToDouble((grp.Sum(item => item.Srpv) / 1000)))
                                                                                                  - Convert.ToDouble((from f in rpmdata
                                                                                                                      where f.Date == grp.Key.Date
                                                                                                                      select f.RPM).Sum())) / Convert.ToDouble((from f in rpmdata
                                                                                                                                                                where f.Date == grp.Key.Date
                                                                                                                                                                select f.RPM).Sum()))
                                                                                                                )).ToList();



                //Total RPS Contribution for each day
                result = new ObservableCollection<RPSContributionData>(from data in rpsContribution
                                                                       group data by new { data.Date }
                                                                           into grp
                                                                           select new RPSContributionData(grp.Key.Date
                                                                                        , Convert.ToDouble(grp.Sum(item => item.RpsContribution)), DateTime.Now));

            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region Network MLIY Contribution Data
        public ObservableCollection<MLIYContributionData> GetNetworkMLIYContributionData(string selectedView)
        {
            ObservableCollection<MLIYContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new MLIYContributionData(
                                                                                            //Date
                                                                                grp.Key.Date
                                                                                            //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                            //SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Impression Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                            //Impressions
                                                                                , grp.Sum(item => item.Impressions)
                                                                                )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                      where
                                                                                          data.Network == strViewArray[0]
                                                                                          && data.Partner == strViewArray[1]
                                                                                          && data.Country == strViewArray[2]
                                                                                          && data.Medium == strViewArray[3]
                                                                                          && data.Device == strViewArray[4]
                                                                                          && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                          && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                      group data by new { data.Date, data.FlightNo }
                                                                                          into grp
                                                                                          select new MLIYContributionData(
                                                                                              //Date
                                                                                            grp.Key.Date
                                                                                              //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                              //SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                              //Impression Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ImpressionYield))
                                                                                              //Impressions
                                                                                          , grp.Sum(item => item.Impressions)
                                                                                          )).ToList();



                //Get each Tied and Untied MLIY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ImpressionYield else untied Flight ImpressionYield
                var tieduntiedMLIYContribution = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                where
                                                                                                    data.Network == strViewArray[0]
                                                                                                    && data.Partner == strViewArray[1]
                                                                                                    && data.Country == strViewArray[2]
                                                                                                    && data.Medium == strViewArray[3]
                                                                                                    && data.Device == strViewArray[4]
                                                                                                    && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                //&& data.FeatureAreaType == strViewArray[5]
                                                                                                group data by new { data.Date, data.FeatureAreaType, data.TrafficType }
                                                                                                    into grp
                                                                                                    select new MLIYContributionData(grp.Key.Date
                                                                                                                , grp.Key.FeatureAreaType
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                                                , grp.Sum(item => item.Impressions)
                                                                                                        // MLIY Control
                                                                                                                ,
                                                                                                                grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ImpressionYield)).FirstOrDefault()) / 100) :
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ImpressionYield)).FirstOrDefault()) / 100)
                                                                                                                     ));


                //Get each Tied and Untied Mainline Impressions
                var tieduntiedMainlineImpressions = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                   where
                                                                                                       data.Network == strViewArray[0]
                                                                                                       && data.Partner == strViewArray[1]
                                                                                                       && data.Country == strViewArray[2]
                                                                                                       && data.Medium == strViewArray[3]
                                                                                                       && data.Device == strViewArray[4]
                                                                                                       && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                   //&& data.FeatureAreaType == strViewArray[5]
                                                                                                   group data by new { data.Date, data.FeatureAreaType, data.TrafficType }
                                                                                                       into grp
                                                                                                       select new MLIYContributionData(grp.Key.Date
                                                                                                          , grp.Key.FeatureAreaType
                                                                                                          , grp.Key.TrafficType
                                                                                                          , grp.Sum(item => item.Impressions)
                                                                                                           //MainLine Impressions
                                                                                                           , grp.Key.TrafficType == "Tied" ?
                                                                                                            Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                            Convert.ToDouble((from f in tieduntiedMLIYContribution
                                                                                                                              where f.Date == grp.Key.Date && f.TrafficType == "Tied"
                                                                                                                              select f.MLIYControl).FirstOrDefault()))
                                                                                                                              :
                                                                                                            Convert.ToDouble(grp.Sum(item => item.Impressions) -
                                                                                                            Convert.ToDouble((from f in tieduntiedMLIYContribution
                                                                                                                              where f.Date == grp.Key.Date && f.TrafficType == "Untied"
                                                                                                                              select f.MLIYControl).FirstOrDefault()))
                                                                                                          , DateTime.Now
                                                                                                                               ));

                //Get total Mainline Impressions
                var totalAdjustedMainlineImpressions = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                                                      where
                                                                                                          data.Network == strViewArray[0]
                                                                                                          && data.Partner == strViewArray[1]
                                                                                                          && data.Country == strViewArray[2]
                                                                                                          && data.Medium == strViewArray[3]
                                                                                                          && data.Device == strViewArray[4]
                                                                                                          && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                                                      group data by new { data.Date }
                                                                                                          into grp
                                                                                                          select new MLIYContributionData(grp.Key.Date
                                                                                                              //Impressions
                                                                                                              , grp.Sum(item => item.Impressions)
                                                                                                              //MainLine Impressions
                                                                                                              , Convert.ToDouble((from f in tieduntiedMainlineImpressions
                                                                                                                                  where f.Date == grp.Key.Date
                                                                                                                                  select f.MainlineImpressions).Sum())
                                                                                                              //Adjusted MainLine Impressions
                                                                                                              , Convert.ToDouble(grp.Sum(item => item.Impressions) +
                                                                                                                  Convert.ToDouble((from f in tieduntiedMainlineImpressions
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select f.MainlineImpressions).Sum()))

                                                                                                                   , DateTime.Now
                                                                                                                   , 1
                                                                                                                   ));

                //Get MLIY Delta
                var mliyDelta = new ObservableCollection<MLIYContributionData>(from data in ObjFlightCubeData
                                                                               where
                                                                                   data.Network == strViewArray[0]
                                                                                   && data.Partner == strViewArray[1]
                                                                                   && data.Country == strViewArray[2]
                                                                                   && data.Medium == strViewArray[3]
                                                                                   && data.Device == strViewArray[4]
                                                                                   && (data.PagePlacement == "Main Line" || data.PagePlacement == "Unknown")
                                                                               group data by new { data.Date }
                                                                                   into grp
                                                                                   select new MLIYContributionData(grp.Key.Date
                                                                                       //Impressions
                                                                                                        , grp.Sum(item => item.Impressions)
                                                                                       //Srpv
                                                                                                        , grp.Sum(item => item.Srpv)
                                                                                       //Impression Yield
                                                                                                        , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                       //Adjusted Mainline Impressions
                                                                                                          , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedMainlineImpressions
                                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                                               select f.AdjustedMainlineImpressions).FirstOrDefault())
                                                                                                            + grp.Sum(item => item.Impressions))
                                                                                       //Adjusted MLIY Delta
                                                                                                           , Convert.ToDouble((from f in totalAdjustedMainlineImpressions
                                                                                                                               where f.Date == grp.Key.Date
                                                                                                                               select f.AdjustedMainlineImpressions).FirstOrDefault())
                                                                                                                /
                                                                                                             (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100)

                                                                                       // MLIY Delta
                                                                                                           , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedMainlineImpressions
                                                                                                                                                where f.Date == grp.Key.Date
                                                                                                                                                select f.AdjustedMainlineImpressions).FirstOrDefault())
                                                                                                                /
                                                                                                             (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100))
                                                                                                             - Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)

                                                                                                           , DateTime.Now
                                                                                                           , 1
                                                                                                           ));




                //Total MLIY Contribution for each day
                result = new ObservableCollection<MLIYContributionData>(from data in mliyDelta
                                                                        group data by new { data.Date }
                                                                            into grp
                                                                            select new MLIYContributionData(
                                                                                //Date
                                                                                grp.Key.Date
                                                                                //MLIY Contribution
                                                                                 , Convert.ToDouble(grp.Sum(item => item.MLIYDelta)
                                                                                 / Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Impressions)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100))
                                                                                // LastRefreshed
                                                                                 , DateTime.Now
                                                                                 ));



            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion

        #region Network CY Contribution Data
        public ObservableCollection<CYContributionData> GetNetworkCYContributionData(string selectedView)
        {
            ObservableCollection<CYContributionData> result = null;
            try
            {
                string[] strViewArray = selectedView.Split(',');

                //Fetch Flight 10 Tied SRPV
                var tiedFlightdata = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                  where
                                                                                      data.Network == strViewArray[0]
                                                                                      && data.Partner == strViewArray[1]
                                                                                      && data.Country == strViewArray[2]
                                                                                      && data.Medium == strViewArray[3]
                                                                                      && data.Device == strViewArray[4]
                                                                                      && data.FlightNo == Convert.ToInt16(strViewArray[7])
                                                                                  group data by new { data.Date, data.FlightNo }
                                                                                      into grp
                                                                                      select new CYContributionData(
                                                                                          //Date
                                                                                grp.Key.Date
                                                                                          //FlightNo
                                                                                , grp.Key.FlightNo
                                                                                          //SRPV
                                                                                , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                          //Click Yield
                                                                                , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                          //Clicks
                                                                                , grp.Sum(item => item.Clicks)
                                                                                )).ToList();

                //Fetch Flight 166 Untied SRPV
                var untiedFlightdata = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                        && data.FlightNo == Convert.ToInt16(strViewArray[8])
                                                                                    group data by new { data.Date, data.FlightNo }
                                                                                        into grp
                                                                                        select new CYContributionData(
                                                                                            //Date
                                                                                            grp.Key.Date
                                                                                            //FlightNo
                                                                                          , grp.Key.FlightNo
                                                                                            //SRPV
                                                                                          , Convert.ToDouble(grp.Sum(item => item.Srpv))
                                                                                            //Click Yield
                                                                                          , Convert.ToDouble(grp.Sum(item => item.ClickYield))
                                                                                            //Clicks
                                                                                          , grp.Sum(item => item.Clicks)
                                                                                          )).ToList();



                //Get each Tied and Untied CY Contribution , formula is each experiment SRPV/If TrafficType = Tied then Tied Flight ClickYield else untied Flight ClickYield
                var tieduntiedCYContribution = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                            where
                                                                                                data.Network == strViewArray[0]
                                                                                                && data.Partner == strViewArray[1]
                                                                                                && data.Country == strViewArray[2]
                                                                                                && data.Medium == strViewArray[3]
                                                                                                && data.Device == strViewArray[4]
                                                                                            //&& data.FeatureAreaType == strViewArray[5]
                                                                                            group data by new { data.Date, data.FeatureAreaType, data.TrafficType }
                                                                                                into grp
                                                                                                select new CYContributionData(
                                                                                                    grp.Key.Date
                                                                                                                , grp.Key.FeatureAreaType
                                                                                                                , grp.Key.TrafficType
                                                                                                                , grp.Sum(item => item.Srpv)
                                                                                                                , grp.Sum(item => item.Clicks)
                                                                                                                , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                                    // CY Control
                                                                                                                ,
                                                                                                                grp.Key.TrafficType == "Tied" ?
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in tiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ClickYield)).FirstOrDefault()) / 100) :
                                                                                                                 Convert.ToDouble(grp.Sum(item => item.Srpv)) *
                                                                                                                 (Convert.ToDouble((from f in untiedFlightdata
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select Convert.ToDouble(f.ClickYield)).FirstOrDefault()) / 100)
                                                                                                                     ));


                //Get each Tied and Untied Clicks
                var tieduntiedClicks = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                    where
                                                                                        data.Network == strViewArray[0]
                                                                                        && data.Partner == strViewArray[1]
                                                                                        && data.Country == strViewArray[2]
                                                                                        && data.Medium == strViewArray[3]
                                                                                        && data.Device == strViewArray[4]
                                                                                    //&& data.FeatureAreaType == strViewArray[5]
                                                                                    group data by new { data.Date, data.FeatureAreaType, data.TrafficType }
                                                                                        into grp
                                                                                        select new CYContributionData(
                                                                                            grp.Key.Date
                                                                                           , grp.Key.FeatureAreaType
                                                                                           , grp.Key.TrafficType
                                                                                           , grp.Sum(item => item.Clicks)
                                                                                            //Clicks Contribution
                                                                                            , grp.Key.TrafficType == "Tied" ?
                                                                                             Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                             Convert.ToDouble((from f in tieduntiedCYContribution
                                                                                                               where f.Date == grp.Key.Date && f.TrafficType == "Tied"
                                                                                                               select f.CYControl).FirstOrDefault()))
                                                                                                               :
                                                                                             Convert.ToDouble(grp.Sum(item => item.Clicks) -
                                                                                             Convert.ToDouble((from f in tieduntiedCYContribution
                                                                                                               where f.Date == grp.Key.Date && f.TrafficType == "Untied"
                                                                                                               select f.CYControl).FirstOrDefault()))
                                                                                           , DateTime.Now
                                                                                                                ));

                //Get total Adjusted Clicks
                var totalAdjustedClicks = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                                       where
                                                                                           data.Network == strViewArray[0]
                                                                                           && data.Partner == strViewArray[1]
                                                                                           && data.Country == strViewArray[2]
                                                                                           && data.Medium == strViewArray[3]
                                                                                           && data.Device == strViewArray[4]
                                                                                       group data by new { data.Date }
                                                                                           into grp
                                                                                           select new CYContributionData(
                                                                                                            grp.Key.Date
                                                                                               //Clicks
                                                                                                              , grp.Sum(item => item.Clicks)
                                                                                               //Total Clicks Contribution
                                                                                                              , Convert.ToDouble((from f in tieduntiedClicks
                                                                                                                                  where f.Date == grp.Key.Date
                                                                                                                                  select f.ClicksContribution).Sum())
                                                                                               //Adjusted Clicks Contribution
                                                                                                              , Convert.ToDouble(grp.Sum(item => item.Clicks) +
                                                                                                                  Convert.ToDouble((from f in tieduntiedClicks
                                                                                                                                    where f.Date == grp.Key.Date
                                                                                                                                    select f.ClicksContribution).Sum()))

                                                                                                                   , DateTime.Now
                                                                                                                   , 1
                                                                                                                   ));

                //Get CY Delta
                var cyDelta = new ObservableCollection<CYContributionData>(from data in ObjFlightCubeData
                                                                           where
                                                                               data.Network == strViewArray[0]
                                                                               && data.Partner == strViewArray[1]
                                                                               && data.Country == strViewArray[2]
                                                                               && data.Medium == strViewArray[3]
                                                                               && data.Device == strViewArray[4]
                                                                           group data by new { data.Date }
                                                                               into grp
                                                                               select new CYContributionData(grp.Key.Date
                                                                                   //Clicks
                                                                                                    , grp.Sum(item => item.Clicks)
                                                                                   //Srpv
                                                                                                    , grp.Sum(item => item.Srpv)
                                                                                   //Clicks Yield
                                                                                                    , Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)
                                                                                   //Adjusted Clicks Contribution
                                                                                                      , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedClicks
                                                                                                                                           where f.Date == grp.Key.Date
                                                                                                                                           select f.AdjustedClicksContribution).FirstOrDefault())
                                                                                                        + grp.Sum(item => item.Clicks))
                                                                                   //Adjusted CY Delta
                                                                                                       , Convert.ToDouble((from f in totalAdjustedClicks
                                                                                                                           where f.Date == grp.Key.Date
                                                                                                                           select f.AdjustedClicksContribution).FirstOrDefault())
                                                                                                            /
                                                                                                         (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100)

                                                                                   // CY Delta
                                                                                                       , Convert.ToDouble(Convert.ToDouble((from f in totalAdjustedClicks
                                                                                                                                            where f.Date == grp.Key.Date
                                                                                                                                            select f.AdjustedClicksContribution).FirstOrDefault())
                                                                                                            /
                                                                                                         (Convert.ToDouble(grp.Sum(item => item.Srpv)) / 100))
                                                                                                         - Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100)

                                                                                                       , DateTime.Now
                                                                                                       , 1
                                                                                                       ));




                //Total CY Contribution for each day
                result = new ObservableCollection<CYContributionData>(from data in cyDelta
                                                                      group data by new { data.Date }
                                                                          into grp
                                                                          select new CYContributionData(
                                                                              //Date
                                                                                grp.Key.Date
                                                                              //CY Contribution
                                                                                 , Convert.ToDouble(grp.Sum(item => item.CYDelta)
                                                                                 / Convert.ToDouble((Convert.ToDouble(grp.Sum(item => item.Clicks)) / Convert.ToDouble(grp.Sum(item => item.Srpv))) * 100))
                                                                              // LastRefreshed
                                                                                 , DateTime.Now
                                                                                 ));


            }

            catch (Exception)
            {

            }
            return result;
        }
        #endregion
    }
}
