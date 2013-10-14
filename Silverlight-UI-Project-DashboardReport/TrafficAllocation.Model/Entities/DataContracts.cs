using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace TrafficAllocation.Model.Entities
{
    [DataContract]
    public class FlightCubeData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _trafficType;
        private string _environmentType;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _impressions;
        private int _clicks;
        private double _rpm;
        private double _impressionYield;
        private double _clickYield;
        private DateTime _lastRefreshed;
        private string _network;
        private string _partner;
        private string _country;
        private string _medium;
        private string _device;
        private string _pageplacement;
        private string _experimentType;
        private string _featureAreaType;
        private DateTime _startDate;
        private DateTime _modifiedDate;
        private string _experimentAuthor;

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public string EnvironmentType
        {
            get { return _environmentType; }
            set
            {
                _environmentType = value;
                PropertyChangedEvent("EnvironmentType");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        [DataMember]
        public int Impressions
        {
            get { return _impressions; }
            set
            {
                _impressions = value;
                PropertyChangedEvent("Impressions");
            }
        }

        [DataMember]
        public int Clicks
        {
            get { return _clicks; }
            set
            {
                _clicks = value;
                PropertyChangedEvent("Clicks");
            }
        }

        [DataMember]
        public double RPM
        {
            get { return _rpm; }
            set
            {
                _rpm = value;
                PropertyChangedEvent("RPM");
            }
        }

        [DataMember]
        public double ImpressionYield
        {
            get { return _impressionYield; }
            set
            {
                _impressionYield = value;
                PropertyChangedEvent("ImpressionYield");
            }
        }

        [DataMember]
        public double ClickYield
        {
            get { return _clickYield; }
            set
            {
                _clickYield = value;
                PropertyChangedEvent("ClickYield");
            }
        }

        [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }

        [DataMember]
        public string Network
        {
            get { return _network; }
            set { _network = value; PropertyChangedEvent("Network"); }
        }

        [DataMember]
        public string Partner
        {
            get { return _partner; }
            set { _partner = value; PropertyChangedEvent("Partner"); }
        }

        [DataMember]
        public string Country
        {
            get { return _country; }
            set { _country = value; PropertyChangedEvent("Country"); }
        }

        [DataMember]
        public string Medium
        {
            get { return _medium; }
            set { _medium = value; PropertyChangedEvent("Medium"); }
        }

        [DataMember]
        public string Device
        {
            get { return _device; }
            set { _device = value; PropertyChangedEvent("Device"); }
        }

        [DataMember]
        public string PagePlacement
        {
            get { return _pageplacement; }
            set { _pageplacement = value; PropertyChangedEvent("PagePlacement"); }
        }

        [DataMember]
        public string ExperimentType
        {
            get { return _experimentType; }
            set { _experimentType = value; PropertyChangedEvent("ExperimentType"); }
        }

        [DataMember]
        public string FeatureAreaType
        {
            get { return _featureAreaType; }
            set { _featureAreaType = value; PropertyChangedEvent("FeatureAreaType"); }
        }
        [DataMember]
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                PropertyChangedEvent("StartDate");
            }
        }

        [DataMember]
        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set
            {
                _modifiedDate = value;
                PropertyChangedEvent("ModifiedDate");
            }
        }

        [DataMember]
        public string ExperimentAuthor
        {
            get { return _experimentAuthor; }
            set
            {
                _experimentAuthor = value;
                PropertyChangedEvent("ExperimentAuthor");
            }
        }

        #endregion

        #region constructor
        public FlightCubeData()
        {

        }
        public FlightCubeData(
                            DateTime date
                            , int flightNo
                            , string experimentName
                            , string trafficType
                            , string environmentType
                            , string network
                            , string partner
                            , string country
                            , string medium
                            , string device
                            , string pagePlacement
                            , string experimentType
                            , string featureAreaType
                            , DateTime startDate
                            , DateTime modifiedDate
                            , string experimentAuthor
                            , double grossRevenueUSD
                            , int impressions
                            , int clicks
                            , int srpv
                            , double rpm
                            , double impressionYield
                            , double clickYield
                            , DateTime lastrefreshed)
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.EnvironmentType = environmentType;
            this.Network = network;
            this.Partner = partner;
            this.Country = country;
            this.Medium = medium;
            this.Device = device;
            this.PagePlacement = pagePlacement;
            this.ExperimentType = experimentType;
            this.FeatureAreaType = featureAreaType;
            this.StartDate = startDate;
            this.ModifiedDate = modifiedDate;
            this.ExperimentAuthor = experimentAuthor;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.Impressions = impressions;
            this.Clicks = clicks;
            this.Srpv = srpv;
            this.RPM = rpm;
            this.ImpressionYield = impressionYield;
            this.ClickYield = clickYield;
            this.LastRefreshed = lastrefreshed;
        }

        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    [DataContract]
    public class NetworkViewSlicesData : INotifyPropertyChanged
    {
        #region properties

        private string _viewName;
        private string _actuallviewName;
        private string _viewSliceName;

        [DataMember]
        public string ViewName
        {
            get { return _viewName; }
            set { _viewName = value; PropertyChangedEvent("ViewName"); }
        }

        [DataMember]
        public string ActuallViewName
        {
            get { return _actuallviewName; }
            set { _actuallviewName = value; PropertyChangedEvent("ActuallViewName"); }
        }

        [DataMember]
        public string ViewSliceName
        {
            get { return _viewSliceName; }
            set { _viewSliceName = value; PropertyChangedEvent("ViewSliceName"); }
        }

        #endregion

        #region constructor
        public NetworkViewSlicesData()
        {

        }
        public NetworkViewSlicesData(
                            string viewName,
                            string actuallViewName,
                            string viewSliceName
                         )
        {
            this.ViewName = viewName;
            this.ActuallViewName = actuallViewName;
            this.ViewSliceName = viewSliceName;
        }

        #endregion

        #region Propertychangedevent
        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    [DataContract]
    public class CurrentViewData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _featureAreaType;
        private string _trafficType;
        private string _environmentType;
        //private double _daysRunning;
        private double _allocation;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _impressions;
        //private int _clicks;
         private DateTime _lastRefreshed;
         private double _totalSrpv;
         private double _rps;
         private double _mlIy;
         private double _iy;
         private double _mlcy;
         private double _cy;
         private double _revenueContribution;
         private double _mainlineImpression;
         private double _clicks;
        //To display in POPup window in Grid row
         private int _controlFlightNo;
         private int _periodSRPVs;
         private int _flightSRPVs;
         private double _flightRPS;
         private double _controlFlightRPS;
         private double _flightMLIY;
         private double _controlFlightMLIY;
         private double _flightIY;
         private double _controlFlightIY;
         private double _flightMLCY;
         private double _controlFlightMLCY;
         private double _flightCY;
         private double _controlFlightCY;
         private double _flightRevenue;
         private double _controlFlightRevenue;
         private double _flightImpressions;
         private double _controlFlightImpressions;
         private double _flightClicks;
         private double _controlFlightClicks;
         private int _daysRunning;
         private DateTime _lastModifiedDate;
         private string _experimentType;
         private string _experimentAuthor;

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string FeatureAreaType
        {
            get { return _featureAreaType; }
            set
            {
                _featureAreaType = value;
                PropertyChangedEvent("FeatureAreaType");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public string EnvironmentType
        {
            get { return _environmentType; }
            set
            {
                _environmentType = value;
                PropertyChangedEvent("EnvironmentType");
            }
        }

        //[DataMember]
        //public double DaysRunning
        //{
        //    get { return _daysRunning; }
        //    set
        //    {
        //        _daysRunning = value;
        //        PropertyChangedEvent("DaysRunning");
        //    }
        //}

        [DataMember]
        public double Allocation
        {
            get { return _allocation; }
            set
            {
                _allocation = value;
                PropertyChangedEvent("Allocation");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        [DataMember]
        public int Impressions
        {
            get { return _impressions; }
            set
            {
                _impressions = value;
                PropertyChangedEvent("Impressions");
            }
        }

        //[DataMember]
        //public int Clicks
        //{
        //    get { return _clicks; }
        //    set
        //    {
        //        _clicks = value;
        //        PropertyChangedEvent("Clicks");
        //    }
        //}

        [DataMember]
        public double TotalSrpv
        {
            get { return _totalSrpv; }
            set { _totalSrpv = value; PropertyChangedEvent("TotalSrpv"); }
        }
      

        [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }
        [DataMember]
        public double RPS
        {
            get { return _rps; }
            set { _rps = value; PropertyChangedEvent("RPS"); }
        }
        [DataMember]
        public double MLIY
        {
            get { return _mlIy; }
            set { _mlIy = value; PropertyChangedEvent("MLIY"); }
        }
        [DataMember]
        public double IY
        {
            get { return _iy; }
            set { _iy = value; PropertyChangedEvent("IY"); }
        }
        [DataMember]
        public double MLCY
        {
            get { return _mlcy; }
            set { _mlcy = value; PropertyChangedEvent("MLCY"); }
        }
        [DataMember]
        public double CY
        {
            get { return _cy; }
            set { _cy = value; PropertyChangedEvent("CY"); }
        }
        [DataMember]
        public double RevenueContribution
        {
            get { return _revenueContribution; }
            set { _revenueContribution = value; PropertyChangedEvent("RevenueContribution"); }
        }
         [DataMember]
        public double MainlineImpression
        {
            get { return _mainlineImpression; }
            set { _mainlineImpression = value; PropertyChangedEvent("MainlineImpression"); }
        }

         [DataMember]
         public double Clicks
         {
             get { return _clicks; }
             set { _clicks = value; PropertyChangedEvent("Clicks"); }
         }

         [DataMember]
         public int ControlFlightNo
         {
             get { return _controlFlightNo; }
             set
             {
                 _controlFlightNo = value;
                 PropertyChangedEvent("ControlFlightNo");
             }
         }

         [DataMember]
         public int PeriodSRPVs
         {
             get { return _periodSRPVs; }
             set
             {
                 _periodSRPVs = value;
                 PropertyChangedEvent("PeriodSRPVs");
             }
         }

         [DataMember]
         public int FlightSRPVs
         {
             get { return _flightSRPVs; }
             set
             {
                 _flightSRPVs = value;
                 PropertyChangedEvent("FlightSRPVs");
             }
         }

         [DataMember]
         public double FlightRPS
         {
             get { return _flightRPS; }
             set
             {
                 _flightRPS = value;
                 PropertyChangedEvent("FlightRPS");
             }
         }

         [DataMember]
         public double ControlFlightRPS
         {
             get { return _controlFlightRPS; }
             set
             {
                 _controlFlightRPS = value;
                 PropertyChangedEvent("ControlFlightRPS");
             }
         }

         [DataMember]
         public double FlightMLIY
         {
             get { return _flightMLIY; }
             set
             {
                 _flightMLIY = value;
                 PropertyChangedEvent("FlightMLIY");
             }
         }

         [DataMember]
         public double ControlFlightMLIY
         {
             get { return _controlFlightMLIY; }
             set
             {
                 _controlFlightMLIY = value;
                 PropertyChangedEvent("ControlFlightMLIY");
             }
         }

         [DataMember]
         public double FlightIY
         {
             get { return _flightIY; }
             set
             {
                 _flightIY = value;
                 PropertyChangedEvent("FlightIY");
             }
         }

         [DataMember]
         public double ControlFlightIY
         {
             get { return _controlFlightIY; }
             set
             {
                 _controlFlightIY = value;
                 PropertyChangedEvent("ControlFlightIY");
             }
         }

         [DataMember]
         public double FlightMLCY
         {
             get { return _flightMLCY; }
             set
             {
                 _flightMLCY = value;
                 PropertyChangedEvent("FlightMLCY");
             }
         }

         [DataMember]
         public double ControlFlightMLCY
         {
             get { return _controlFlightMLCY; }
             set
             {
                 _controlFlightMLCY = value;
                 PropertyChangedEvent("ControlFlightMLCY");
             }
         }

         [DataMember]
         public double FlightCY
         {
             get { return _flightCY; }
             set
             {
                 _flightCY = value;
                 PropertyChangedEvent("FlightCY");
             }
         }

         [DataMember]
         public double ControlFlightCY
         {
             get { return _controlFlightCY; }
             set
             {
                 _controlFlightCY = value;
                 PropertyChangedEvent("ControlFlightCY");
             }
         }

         [DataMember]
         public double FlightRevenue
         {
             get { return _flightRevenue; }
             set
             {
                 _flightRevenue = value;
                 PropertyChangedEvent("FlightRevenue");
             }
         }

         [DataMember]
         public double ControlFlightRevenue
         {
             get { return _controlFlightRevenue; }
             set
             {
                 _controlFlightRevenue = value;
                 PropertyChangedEvent("ControlFlightRevenue");
             }
         }

         [DataMember]
         public double FlightImpressions
         {
             get { return _flightImpressions; }
             set
             {
                 _flightImpressions = value;
                 PropertyChangedEvent("FlightImpressions");
             }
         }

         [DataMember]
         public double ControlFlightImpressions
         {
             get { return _controlFlightImpressions; }
             set
             {
                 _controlFlightImpressions = value;
                 PropertyChangedEvent("ControlFlightImpressions");
             }
         }

         [DataMember]
         public double FlightClicks
         {
             get { return _flightClicks; }
             set
             {
                 _flightClicks = value;
                 PropertyChangedEvent("FlightClicks");
             }
         }

         [DataMember]
         public double ControlFlightClicks
         {
             get { return _controlFlightClicks; }
             set
             {
                 _controlFlightClicks = value;
                 PropertyChangedEvent("ControlFlightClicks");
             }
         }

         [DataMember]
         public int DaysRunning
         {
             get { return _daysRunning; }
             set
             {
                 _daysRunning = value;
                 PropertyChangedEvent("DaysRunning");
             }
         }

         [DataMember]
         public DateTime LastModifiedDate
         {
             get { return _lastModifiedDate; }
             set
             {
                 _lastModifiedDate = value;
                 PropertyChangedEvent("LastModifiedDate");
             }
         }

         [DataMember]
         public string ExperimentType
         {
             get { return _experimentType; }
             set
             {
                 _experimentType = value;
                 PropertyChangedEvent("ExperimentType");
             }
         }

         [DataMember]
         public string ExperimentAuthor
         {
             get { return _experimentAuthor; }
             set
             {
                 _experimentAuthor = value;
                 PropertyChangedEvent("ExperimentAuthor");
             }
         }
        #endregion

        #region constructor
        public CurrentViewData()
        {

        }
        public CurrentViewData(
                            DateTime date
                            , int flightNo
                            , string experimentName
                            , string trafficType
                            , string environmentType
                            //, double daysRunning
                            , double allocation
                            , double grossRevenueUSD
                            , int srpv
                            , int impressions
                            , double clicks
                            , DateTime lastrefreshed)
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.EnvironmentType = environmentType;
            //this.DaysRunning = daysRunning;
            this.Allocation = allocation;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.Srpv = srpv;
            this.Impressions = impressions;
            this.Clicks = clicks;
       
            this.LastRefreshed = lastrefreshed;
        }

        public CurrentViewData(
                           int flightNo
                           , string experimentName
                           , string featureAreaType
                           , string trafficType
                           , string environmentType
                           , double allocation
                           , double rps
                           , double mlIY
                           , double iY
                           , double mlCY
                           , double cY
                           , double revenueContribution
                           , double mainlineImpression
                           , double clicks
                           , int controlFlightNo
                           , int flightSRPVs
                           , int periodSRPVs
                           , double flightRPS
                           , double controlFlightRPS
                           , double flightMLIY
                           , double controlFlightMLIY
                           , double flightIY
                           , double controlFlightIY
                           , double flightMLCY
                           , double controlFlightMLCY
                           , double flightCY
                           , double controlFlightCY
                           , double flightRevenue
                           , double controlFlightRevenue
                           , double flightImpressions
                           , double controlFlightImpressions
                           , double flightClicks
                           , double controlFlightClicks
                           , int daysRunning
                           , DateTime lastModifiedDate
                           , string experimentType
                           , string experimentAuthor
                           )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.FeatureAreaType = featureAreaType;
            this.TrafficType = trafficType;
            this.EnvironmentType = environmentType;
            this.Allocation = allocation;
            this.RPS = rps;
            this.MLIY = mlIY;
            this.IY = iY;
            this.MLCY = mlCY;
            this.CY = cY;
            this.RevenueContribution = revenueContribution;
            this.MainlineImpression = mainlineImpression;
            this.Clicks = clicks;
            this.ControlFlightNo = controlFlightNo;
            this.FlightSRPVs = flightSRPVs;
            this.PeriodSRPVs = periodSRPVs;
            this.FlightRPS = flightRPS;
            this.ControlFlightRPS = controlFlightRPS;
            this.FlightMLIY = flightMLIY;
            this.ControlFlightMLIY = controlFlightMLIY;
            this.FlightIY = flightIY;
            this.ControlFlightIY = controlFlightIY;
            this.FlightMLCY = flightMLCY;
            this.ControlFlightMLCY = controlFlightMLCY;
            this.FlightCY = flightCY;
            this.ControlFlightCY = controlFlightCY;
            this.FlightRevenue = flightRevenue;
            this.ControlFlightRevenue = controlFlightRevenue;
            this.FlightImpressions = flightImpressions;
            this.ControlFlightImpressions = controlFlightImpressions;
            this.FlightClicks = flightClicks;
            this.ControlFlightClicks = controlFlightClicks;
            this.DaysRunning = daysRunning;
            this.LastModifiedDate = lastModifiedDate;
            this.ExperimentType = experimentType;
            this.ExperimentAuthor = experimentAuthor;
        }

        public CurrentViewData(
                        DateTime date
                        , double revenue
                        , int sRPV
                        , int clicks
                        , int impressions
                        , double rpm
                        , double clickYield
                        , double impressionYield
                        )
        {
            this.Date = date;
            this.GrossRevenueUSD = revenue;
            this.Srpv = sRPV;
            this.Clicks = clicks;
            this.Impressions = impressions;
            this.RPS = rpm;
            this.CY = clickYield;
            this.IY = impressionYield;
        }

        public CurrentViewData(
                          DateTime date
                         , double totalSrpv
                        )
        {
            this.Date = date;
           this.TotalSrpv = totalSrpv;
        }

        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    [DataContract]
    public class ExperimentAllocationData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _trafficType;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _impressions;
        private int _clicks;
        private DateTime _lastRefreshed;
        private double _experimentAllocation;
        private double _totalSRPV;
        private double _allocationBudget;

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        [DataMember]
        public int Impressions
        {
            get { return _impressions; }
            set
            {
                _impressions = value;
                PropertyChangedEvent("Impressions");
            }
        }

        [DataMember]
        public int Clicks
        {
            get { return _clicks; }
            set
            {
                _clicks = value;
                PropertyChangedEvent("Clicks");
            }
        }

        [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }
        [DataMember]
        public double ExperimentAllocation
        {
            get { return _experimentAllocation; }
            set { _experimentAllocation = value; PropertyChangedEvent("ExperimentAllocation"); }
        }

        [DataMember]
        public double TotalSrpv
        {
            get { return _totalSRPV; }
            set { _totalSRPV = value; PropertyChangedEvent("TotalSrpv"); }
        }

        [DataMember]
        public double AllocationBudget
        {
            get { return _allocationBudget; }
            set { _allocationBudget = value; PropertyChangedEvent("AllocationBudget"); }
        }

        #endregion

        #region constructor
        public ExperimentAllocationData()
        {

        }
        public ExperimentAllocationData(
                            DateTime date
                            , string experimentName
                            , string trafficType
                            , double grossRevenueUSD
                            , int srpv
                            , int impressions
                            , int clicks
                     )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.Srpv = srpv;
            this.Impressions = impressions;
            this.Clicks = clicks;
        }

        public ExperimentAllocationData(
                            DateTime date
                           , string experimentName
                           , int SRPV
                           , Double experimentAllocation
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.Srpv = SRPV;
            this.ExperimentAllocation = experimentAllocation;
        }

        public ExperimentAllocationData(
                            DateTime date
                           ,double totalSRPV)
        {
            this.Date = date;
            this.TotalSrpv = totalSRPV;
        }

        public ExperimentAllocationData(
                            DateTime date
                           , double experimentAllocation
                           , double allocationBudget
                           , DateTime lastrefreshed)
        {
            this.Date = date;
            this.ExperimentAllocation = experimentAllocation;
            this.AllocationBudget = allocationBudget;
            this.LastRefreshed = lastrefreshed;
        }

        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    [DataContract]
    public class RPSContributionData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _featureAreaType;
        private string _trafficType;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _impressions;
        private int _clicks;
        private DateTime _lastRefreshed;
        private double _rpsContribution;
        private double _totalSRPV;
        private double _controlRPM;
        private double _revenueContribution;
        private double _adjustedRevenue;
        private double _revenueDelta;
        private double _adjustedRPM;
        private double _rpmDelta;
        private double _rPM;
        private double _totalRevenue;
        private double _rpmControl;
    
        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string FeatureAreaType
        {
            get { return _featureAreaType; }
            set
            {
                _featureAreaType = value;
                PropertyChangedEvent("FeatureAreaType");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        [DataMember]
        public int Impressions
        {
            get { return _impressions; }
            set
            {
                _impressions = value;
                PropertyChangedEvent("Impressions");
            }
        }

        [DataMember]
        public int Clicks
        {
            get { return _clicks; }
            set
            {
                _clicks = value;
                PropertyChangedEvent("Clicks");
            }
        }

        [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }
      
        [DataMember]
        public double RpsContribution
        {
            get { return _rpsContribution; }
            set { _rpsContribution = value; PropertyChangedEvent("RpsContribution"); }
        }
      
        [DataMember]
        public double TotalSrpv
        {
            get { return _totalSRPV; }
            set { _totalSRPV = value; PropertyChangedEvent("TotalSrpv"); }
        }
        [DataMember]
        public double ControlRPM
        {
            get { return _controlRPM; }
            set { _controlRPM = value; PropertyChangedEvent("ControlRPM"); }
        }
        [DataMember]
        public double RevenueContribution
        {
            get { return _revenueContribution; }
            set { _revenueContribution = value; PropertyChangedEvent("RevenueContribution"); }
        }
        [DataMember]
        public double AdjustedRevenue
        {
            get { return _adjustedRevenue; }
            set { _adjustedRevenue = value; PropertyChangedEvent("AdjustedRevenue"); }
        }
        [DataMember]
        public double RevenueDelta
        {
            get { return _revenueDelta; }
            set { _revenueDelta = value; PropertyChangedEvent("RevenueDelta"); }
        }
        [DataMember]
        public double AdjustedRPM
        {
            get { return _adjustedRPM; }
            set { _adjustedRPM = value; PropertyChangedEvent("AdjustedRPM"); }
        }
        [DataMember]
        public double RpmDelta
        {
            get { return _rpmDelta; }
            set { _rpmDelta = value; PropertyChangedEvent("RpmDelta"); }
        }

        [DataMember]
        public double RPM
        {
            get { return _rPM; }
            set { _rPM = value; PropertyChangedEvent("RPM"); }
        }
        [DataMember]
        public double TotalRevenue
        {
            get { return _totalRevenue; }
            set { _totalRevenue = value; PropertyChangedEvent("TotalRevenue"); }
        }
        [DataMember]
        public double RPMControl
        {
            get { return _rpmControl; }
            set { _rpmControl = value; PropertyChangedEvent("RPMControl"); }
        }
        #endregion

        #region constructor
        public RPSContributionData()
        {

        }

        public RPSContributionData(
                             DateTime date
                            , double rpm)
        {
            this.Date = date;
            this.RPM = rpm;
        }

        public RPSContributionData(
                             DateTime date
                             , int flightNo
                            , double totalSRPV
                            , double totalRevenue)
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.TotalSrpv = totalSRPV;
            this.TotalRevenue = totalRevenue;
        }

        public RPSContributionData(
                            DateTime date
                           , string featureAreaType
                           , string trafficType
                           , int SRPV
                           , double grossRevenueUSD
                           , double rpm
                           , double rpmControl
                          )
        {
            this.Date = date;
            this.FeatureAreaType = featureAreaType;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.RPM = rpm;
            this.RPMControl = rpmControl;
        }

        public RPSContributionData(
                            DateTime date
                           , string featureAreaType
                           , string trafficType
                           , double grossRevenueUSD
                           , double revenueContribution
                           , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.FeatureAreaType = featureAreaType;
            this.TrafficType = trafficType;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.RevenueContribution = revenueContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public RPSContributionData(
                           DateTime date
                           , double grossRevenueUSD
                           , double revenueContribution
                           , double adjustedrevenueContribution
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.RevenueContribution = revenueContribution;
            this.AdjustedRevenue = adjustedrevenueContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public RPSContributionData(
                           DateTime date
                           , double grossRevenueUSD
                           , int srpv
                           , double rpm
                           , double adjustedrevenue
                           , double adjustedrpm
                           , double rpmDelta
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.Srpv = srpv;
            this.RPM = rpm;
            this.AdjustedRevenue = adjustedrevenue;
            this.AdjustedRPM = adjustedrpm;
            this.RpmDelta = rpmDelta;
            this.LastRefreshed = lastrefreshed;
        }


        public RPSContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double controlRPM
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ControlRPM = controlRPM;
        }

        public RPSContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double rpm
                           , double controlRPM
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.RPM = rpm;
            this.ControlRPM = controlRPM;
        }

        public RPSContributionData(
                          int flightNo
                         , string experimentName
                         , string trafficType
                         , int SRPV
                         , double controlRPM
                        )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ControlRPM = controlRPM;
        }

        public RPSContributionData(
                          DateTime date
                        , int flightNo
                        , string experimentName
                        , string trafficType
                        , int SRPV
                        , double controlRPM
                       )
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ControlRPM = controlRPM;
        }

        public RPSContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , double grossRevenueUSD
                           , double revenueContribution
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.RevenueContribution = revenueContribution;
        }

        public RPSContributionData(
                            DateTime date
                           , double grossRevenueUSD
                           , int srpv
                           , double controlRPM
                           , double revenueContribution
                          )
        {
            this.Date = date;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.Srpv = srpv;
            this.ControlRPM = controlRPM;
            this.RevenueContribution = revenueContribution;
        }

        public RPSContributionData(
                            int flightNo
                           , string experimentName
                           , string trafficType
                           , double grossRevenueUSD
                           , double revenueContribution
                          )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.RevenueContribution = revenueContribution;
        }

        public RPSContributionData(
                            DateTime date
                           , int srpv
                           , double grossRevenueUSD
                           , double rpm
                           , double revenueContribution
                           , double adjustedRevenue
                           , double revenueDelta
                           , double adjustedRPM
                           , double rpmDelta
                           , double rpsContribution
                          )
        {
            this.Date = date;
            this.Srpv = srpv;
            this.GrossRevenueUSD = grossRevenueUSD;
            this.RPM = rpm;
            this.RevenueContribution = revenueContribution;
            this.AdjustedRevenue = adjustedRevenue;
            this.RevenueDelta = revenueDelta;
            this.AdjustedRPM = adjustedRPM;
            this.RpmDelta = rpmDelta;
            this.RpsContribution = rpsContribution;
        }


        public RPSContributionData(
                            DateTime date
                          , double rpsContribution
                          , DateTime lastrefreshed
                                )
        {
            this.Date = date;
            this.RpsContribution = rpsContribution;
            this.LastRefreshed = lastrefreshed;
        }

        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    [DataContract]
    public class MLIYContributionData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _featureAreaType;
        private string _trafficType;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _impressions;
        private DateTime _lastRefreshed;
        private double _totalSRPV;
        private double _impressionYield;
        private double _mliyControl;
        private double _mainlineImpressions;
        private double _adjustedMainlineImpressions;
        private double _adjustedMLIYDelta;
        private double _mLIYDelta;
        private double _mLIYContribution;


        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        [DataMember]
        public int Impressions
        {
            get { return _impressions; }
            set
            {
                _impressions = value;
                PropertyChangedEvent("Impressions");
            }
        }

       [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }
      
        [DataMember]
        public double TotalSrpv
        {
            get { return _totalSRPV; }
            set { _totalSRPV = value; PropertyChangedEvent("TotalSrpv"); }
        }
      
        [DataMember]
        public double ImpressionYield
        {
            get { return _impressionYield; }
            set { _impressionYield = value; PropertyChangedEvent("ImpressionYield"); }
        }
        [DataMember]
        public string FeatureAreaType
        {
            get { return _featureAreaType; }
            set { _featureAreaType = value; PropertyChangedEvent("FeatureAreaType"); }
        }
        [DataMember]
        public double MLIYControl
        {
            get { return _mliyControl; }
            set { _mliyControl = value; PropertyChangedEvent("MLIYControl"); }
        }
        [DataMember]
        public double MainlineImpressions
        {
            get { return _mainlineImpressions; }
            set { _mainlineImpressions = value; PropertyChangedEvent("MainlineImpressions"); }
        }
        [DataMember]
        public double AdjustedMainlineImpressions
        {
            get { return _adjustedMainlineImpressions; }
            set { _adjustedMainlineImpressions = value; PropertyChangedEvent("AdjustedMainlineImpressions"); }
        }

        [DataMember]
        public double AdjustedMLIYDelta
        {
            get { return _adjustedMLIYDelta; }
            set { _adjustedMLIYDelta = value; PropertyChangedEvent("AdjustedMLIYDelta"); }
        }
        [DataMember]
        public double MLIYDelta
        {
            get { return _mLIYDelta; }
            set { _mLIYDelta = value; PropertyChangedEvent("MLIYDelta"); }
        }
        [DataMember]
        public double MLIYContribution
        {
            get { return _mLIYContribution; }
            set { _mLIYContribution = value; PropertyChangedEvent("MLIYContribution"); }
        }

        #endregion

        #region constructor
        public MLIYContributionData()
        {

        }
       
       public MLIYContributionData(
                           DateTime date
                           , int flightNo
                          , double totalSRPV
                          , double impressionYield
                          , int impressions)
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.TotalSrpv = totalSRPV;
            this.ImpressionYield = impressionYield;
            this.Impressions = impressions;
        }
  
        public MLIYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double impressionYield
                           , int impressions
                           , double mliyControl
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ImpressionYield = impressionYield;
            this.Impressions = impressions;
            this.MLIYControl = mliyControl;
        }

        public MLIYContributionData(
                            int flightNo
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double impressionYield
                           , int impressions
                           , double mliyControl
                          )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ImpressionYield = impressionYield;
            this.Impressions = impressions;
            this.MLIYControl = mliyControl;
        }

        public MLIYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int impressions
                           , double mainlineImpressions
                           , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Impressions = impressions;
            this.MainlineImpressions = mainlineImpressions;
            this.LastRefreshed = lastrefreshed;
        }

        public MLIYContributionData(
                            int flightNo
                           , string experimentName
                           , string trafficType
                           , int impressions
                           , double mainlineImpressions
                           , DateTime lastrefreshed
                          )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Impressions = impressions;
            this.MainlineImpressions = mainlineImpressions;
            this.LastRefreshed = lastrefreshed;
        }

        public MLIYContributionData(
                           DateTime date
                           , int impressions
                           , double mainlineImpressions
                           , double adjustedmainlineImpressions
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Impressions = impressions;
            this.MainlineImpressions = mainlineImpressions;
            this.AdjustedMainlineImpressions = adjustedmainlineImpressions;
            this.LastRefreshed = lastrefreshed;
        }

        public MLIYContributionData(
                           int flightNo
                          , double mainlineImpressions
                          , DateTime lastrefreshed
                          , int value
                         )
        {
            this.FlightNo = flightNo;
            this.MainlineImpressions = mainlineImpressions;
            this.LastRefreshed = lastrefreshed;
        }

        public MLIYContributionData(
                           DateTime date
                           , int impressions
                           , int srpv
                           , double impressionYield
                           , double adjustedMaillineImpressions
                           , double adjustedMLIY
                           , double mliyDelta
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Impressions = impressions;
            this.Srpv = srpv;
            this.ImpressionYield = impressionYield;
            this.AdjustedMainlineImpressions = adjustedMaillineImpressions;
            this.AdjustedMLIYDelta = adjustedMLIY;
            this.MLIYDelta = mliyDelta;
            this.LastRefreshed = lastrefreshed;
        }

        public MLIYContributionData(
                            DateTime date
                          , double mliyContribution
                          , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.MLIYContribution = mliyContribution;
            this.LastRefreshed = lastrefreshed;
        }


        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    [DataContract]
    public class MLCYContributionData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _featureAreaType;
        private string _trafficType;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _clicks;
        private DateTime _lastRefreshed;
        private double _totalSRPV;
        private double _clickYield;
        private double _mlcyControl;
        private double _mainlineClicks;
        private double _adjustedMainlineClicks;
        private double _adjustedMLCYDelta;
        private double _mLCYDelta;
        private double _mLCYContribution;


        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        [DataMember]
        public int Clicks
        {
            get { return _clicks; }
            set
            {
                _clicks = value;
                PropertyChangedEvent("Clicks");
            }
        }

        [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }

        [DataMember]
        public double TotalSrpv
        {
            get { return _totalSRPV; }
            set { _totalSRPV = value; PropertyChangedEvent("TotalSrpv"); }
        }

        [DataMember]
        public double ClickYield
        {
            get { return _clickYield; }
            set { _clickYield = value; PropertyChangedEvent("ClickYield"); }
        }
        [DataMember]
        public string FeatureAreaType
        {
            get { return _featureAreaType; }
            set { _featureAreaType = value; PropertyChangedEvent("FeatureAreaType"); }
        }
        [DataMember]
        public double MLCYControl
        {
            get { return _mlcyControl; }
            set { _mlcyControl = value; PropertyChangedEvent("MLCYControl"); }
        }
        [DataMember]
        public double MainlineClicks
        {
            get { return _mainlineClicks; }
            set { _mainlineClicks = value; PropertyChangedEvent("MainlineClicks"); }
        }
        [DataMember]
        public double AdjustedMainlineClicks
        {
            get { return _adjustedMainlineClicks; }
            set { _adjustedMainlineClicks = value; PropertyChangedEvent("AdjustedMainlineClicks"); }
        }

        [DataMember]
        public double AdjustedMLCYDelta
        {
            get { return _adjustedMLCYDelta; }
            set { _adjustedMLCYDelta = value; PropertyChangedEvent("AdjustedMLCYDelta"); }
        }
        [DataMember]
        public double MLCYDelta
        {
            get { return _mLCYDelta; }
            set { _mLCYDelta = value; PropertyChangedEvent("MLCYDelta"); }
        }
        [DataMember]
        public double MLCYContribution
        {
            get { return _mLCYContribution; }
            set { _mLCYContribution = value; PropertyChangedEvent("MLCYContribution"); }
        }

        #endregion

        #region constructor
        public MLCYContributionData()
        {

        }

        public MLCYContributionData(
                            DateTime date
                            , int flightNo
                           , double totalSRPV
                           , double clickYield
                           , int clicks)
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.TotalSrpv = totalSRPV;
            this.ClickYield = clickYield;
            this.Clicks = clicks;
        }

        public MLCYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double clickYield
                           , int clicks
                           , double mlcyControl
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ClickYield = clickYield;
            this.Clicks = clicks;
            this.MLCYControl = mlcyControl;
        }

        public MLCYContributionData(
                            int flightNo
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double clickYield
                           , int clicks
                           , double mlcyControl
                          )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ClickYield = clickYield;
            this.Clicks = clicks;
            this.MLCYControl = mlcyControl;
        }

        public MLCYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int clicks
                           , double mainlineClicks
                           , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Clicks = clicks;
            this.MainlineClicks = mainlineClicks;
            this.LastRefreshed = lastrefreshed;
        }

        public MLCYContributionData(
                            int flightNo
                           , string experimentName
                           , string trafficType
                           , int clicks
                           , double mainlineClicks
                           , DateTime lastrefreshed
                          )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Clicks = clicks;
            this.MainlineClicks = mainlineClicks;
            this.LastRefreshed = lastrefreshed;
        }

        public MLCYContributionData(
                           DateTime date
                           , int clicks
                           , double mainlineClicks
                           , double adjustedmainlineClicks
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Clicks = clicks;
            this.MainlineClicks = mainlineClicks;
            this.AdjustedMainlineClicks = adjustedmainlineClicks;
            this.LastRefreshed = lastrefreshed;
        }

        public MLCYContributionData(
                           int flightNo
                          , double mainlineClicks
                          , DateTime lastrefreshed
                          , int value
                         )
        {
            this.FlightNo = flightNo;
            this.MainlineClicks = mainlineClicks;
            this.LastRefreshed = lastrefreshed;
        }

        public MLCYContributionData(
                           DateTime date
                           , int clicks
                           , int srpv
                           , double clickYield
                           , double adjustedMaillineClicks
                           , double adjustedMLCY
                           , double mlcyDelta
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Clicks = clicks;
            this.Srpv = srpv;
            this.ClickYield = clickYield;
            this.AdjustedMainlineClicks = adjustedMaillineClicks;
            this.AdjustedMLCYDelta = adjustedMLCY;
            this.MLCYDelta = mlcyDelta;
            this.LastRefreshed = lastrefreshed;
        }

        public MLCYContributionData(
                            DateTime date
                          , double mlcyContribution
                          , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.MLCYContribution = mlcyContribution;
            this.LastRefreshed = lastrefreshed;
        }


        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }


    [DataContract]
    public class CYContributionData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _featureAreaType;
        private string _trafficType;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _clicks;
        private DateTime _lastRefreshed;
        private double _totalSRPV;
        private double _clickYield;
        private double _cyControl;
        private double _adjustedClicksContribution;
        private double _adjustedCYDelta;
        private double _cyDelta;
        private double _cyContribution;
        private double _clicksContribution;


        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        //[DataMember]
        //public int Clicks
        //{
        //    get { return _clicks; }
        //    set
        //    {
        //        _clicks = value;
        //        PropertyChangedEvent("Clicks");
        //    }
        //}

        [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }

        [DataMember]
        public double TotalSrpv
        {
            get { return _totalSRPV; }
            set { _totalSRPV = value; PropertyChangedEvent("TotalSrpv"); }
        }

        [DataMember]
        public double ClickYield
        {
            get { return _clickYield; }
            set { _clickYield = value; PropertyChangedEvent("ClickYield"); }
        }
        [DataMember]
        public string FeatureAreaType
        {
            get { return _featureAreaType; }
            set { _featureAreaType = value; PropertyChangedEvent("FeatureAreaType"); }
        }
        [DataMember]
        public double CYControl
        {
            get { return _cyControl; }
            set { _cyControl = value; PropertyChangedEvent("CYControl"); }
        }
        [DataMember]
        public int Clicks
        {
            get { return _clicks; }
            set { _clicks = value; PropertyChangedEvent("Clicks"); }
        }
        [DataMember]
        public double AdjustedClicksContribution
        {
            get { return _adjustedClicksContribution; }
            set { _adjustedClicksContribution = value; PropertyChangedEvent("AdjustedClicksContribution"); }
        }

        [DataMember]
        public double AdjustedCYDelta
        {
            get { return _adjustedCYDelta; }
            set { _adjustedCYDelta = value; PropertyChangedEvent("AdjustedCYDelta"); }
        }
        [DataMember]
        public double CYDelta
        {
            get { return _cyDelta; }
            set { _cyDelta = value; PropertyChangedEvent("CYDelta"); }
        }
        [DataMember]
        public double CYContribution
        {
            get { return _cyContribution; }
            set { _cyContribution = value; PropertyChangedEvent("CYContribution"); }
        }

        [DataMember]
        public double ClicksContribution
        {
            get { return _clicksContribution; }
            set { _clicksContribution = value; PropertyChangedEvent("ClicksContribution"); }
        }


        #endregion

        #region constructor
        public CYContributionData()
        {

        }

        public CYContributionData(
                           int flightNo
                          , double clickContribution
                          , DateTime lastrefreshed
                          , int value
                         )
        {
            this.FlightNo = flightNo;
            this.ClicksContribution = clickContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public CYContributionData(
                            DateTime date
                            , int flightNo
                           , double totalSRPV
                           , double clickYield
                           , int clicks
                            )
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.TotalSrpv = totalSRPV;
            this.ClickYield = clickYield;
            this.Clicks = clicks;
        }

        public CYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , int clicks
                           , double clickYield
                           , double cyControl
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.Clicks = clicks;
            this.ClickYield = clickYield;
            this.CYControl = cyControl;
        }

        public CYContributionData(
                            int flightNo
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double clickYield
                           , int clicks
                           , double cyControl
                          )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ClickYield = clickYield;
            this.Clicks = clicks;
            this.CYControl = cyControl;
        }



        public CYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int clicks
                           , double clickContribution
                           , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Clicks = clicks;
            this.ClicksContribution = clickContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public CYContributionData(
                           int flightNo
                          , string experimentName
                          , string trafficType
                          , int clicks
                          , double clickContribution
                          , DateTime lastrefreshed
                         )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Clicks = clicks;
            this.ClicksContribution = clickContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public CYContributionData(
                           DateTime date
                           , int clicks
                           , double clicksContribution
                           , double adjustedClicksContribution
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Clicks = clicks;
            this.ClicksContribution = clicksContribution;
            this.AdjustedClicksContribution = adjustedClicksContribution;
            this.LastRefreshed = lastrefreshed;
        }


        public CYContributionData(
                           DateTime date
                           , int clicks
                           , int srpv
                           , double clickYield
                           , double adjustedClicksContribution
                           , double adjustedCY
                           , double cyDelta
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Clicks = clicks;
            this.Srpv = srpv;
            this.ClickYield = clickYield;
            this.AdjustedClicksContribution = adjustedClicksContribution;
            this.AdjustedCYDelta = adjustedCY;
            this.CYDelta = cyDelta;
            this.LastRefreshed = lastrefreshed;
        }

        public CYContributionData(
                            DateTime date
                          , double cyContribution
                          , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.CYContribution = cyContribution;
            this.LastRefreshed = lastrefreshed;
        }


        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    [DataContract]
    public class IYContributionData : INotifyPropertyChanged
    {
        #region properties

        private DateTime _date;
        private int _flightNo;
        private string _experimentName;
        private string _featureAreaType;
        private string _trafficType;
        private double _grossRevenueUSD;
        private int _srpv;
        private int _impressions;
        private DateTime _lastRefreshed;
        private double _totalSRPV;
        private double _impressionYield;
        private double _iyControl;
        private double _adjustedImpressionsContribution;
        private double _adjustedIYDelta;
        private double _iyDelta;
        private double _iyContribution;
        private double _impressionsContribution;


        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChangedEvent("Date");
            }
        }

        [DataMember]
        public int FlightNo
        {
            get { return _flightNo; }
            set
            {
                _flightNo = value;
                PropertyChangedEvent("FlightNo");
            }
        }

        [DataMember]
        public string ExperimentName
        {
            get { return _experimentName; }
            set
            {
                _experimentName = value;
                PropertyChangedEvent("ExperimentName");
            }
        }

        [DataMember]
        public string TrafficType
        {
            get { return _trafficType; }
            set
            {
                _trafficType = value;
                PropertyChangedEvent("TrafficType");
            }
        }

        [DataMember]
        public double GrossRevenueUSD
        {
            get { return _grossRevenueUSD; }
            set
            {
                _grossRevenueUSD = value;
                PropertyChangedEvent("GrossRevenueUSD");
            }
        }
        [DataMember]
        public int Srpv
        {
            get { return _srpv; }
            set
            {
                _srpv = value;
                PropertyChangedEvent("Srpv");
            }
        }

        [DataMember]
        public DateTime LastRefreshed
        {
            get { return _lastRefreshed; }
            set
            {
                _lastRefreshed = value;
                PropertyChangedEvent("LastRefreshed");
            }
        }

        [DataMember]
        public double TotalSrpv
        {
            get { return _totalSRPV; }
            set { _totalSRPV = value; PropertyChangedEvent("TotalSrpv"); }
        }

        [DataMember]
        public double ImpressionYield
        {
            get { return _impressionYield; }
            set { _impressionYield = value; PropertyChangedEvent("ImpressionYield"); }
        }
        [DataMember]
        public string FeatureAreaType
        {
            get { return _featureAreaType; }
            set { _featureAreaType = value; PropertyChangedEvent("FeatureAreaType"); }
        }
        [DataMember]
        public double IYControl
        {
            get { return _iyControl; }
            set { _iyControl = value; PropertyChangedEvent("IYControl"); }
        }
        [DataMember]
        public int Impressions
        {
            get { return _impressions; }
            set { _impressions = value; PropertyChangedEvent("Impressions"); }
        }
        [DataMember]
        public double AdjustedImpressionsContribution
        {
            get { return _adjustedImpressionsContribution; }
            set { _adjustedImpressionsContribution = value; PropertyChangedEvent("AdjustedImpressionsContribution"); }
        }

        [DataMember]
        public double AdjustedIYDelta
        {
            get { return _adjustedIYDelta; }
            set { _adjustedIYDelta = value; PropertyChangedEvent("AdjustedIYDelta"); }
        }
        [DataMember]
        public double IYDelta
        {
            get { return _iyDelta; }
            set { _iyDelta = value; PropertyChangedEvent("IYDelta"); }
        }
        [DataMember]
        public double IYContribution
        {
            get { return _iyContribution; }
            set { _iyContribution = value; PropertyChangedEvent("IYContribution"); }
        }

        [DataMember]
        public double ImpressionsContribution
        {
            get { return _impressionsContribution; }
            set { _impressionsContribution = value; PropertyChangedEvent("ImpressionsContribution"); }
        }


        #endregion

        #region constructor
        public IYContributionData()
        {

        }

        public IYContributionData(
                           int flightNo
                          , double impressionsContribution
                          , DateTime lastrefreshed
                          , int value
                         )
        {
            this.FlightNo = flightNo;
            this.ImpressionsContribution = impressionsContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public IYContributionData(
                            DateTime date
                            , int flightNo
                           , double totalSRPV
                           , double impressionYield
                           , int impressions
                            )
        {
            this.Date = date;
            this.FlightNo = flightNo;
            this.TotalSrpv = totalSRPV;
            this.ImpressionYield = impressionYield;
            this.Impressions = impressions;
        }

        public IYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , int impressions
                           , double impressionYield
                           , double iyControl
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.Impressions = impressions;
            this.ImpressionYield = impressionYield;
            this.IYControl = iyControl;
        }

        public IYContributionData(
                            int flightNo
                           , string experimentName
                           , string trafficType
                           , int SRPV
                           , double impressionYield
                           , int impressions
                           , double iyControl
                          )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Srpv = SRPV;
            this.ImpressionYield = impressionYield;
            this.Impressions = impressions;
            this.IYControl = iyControl;
        }



        public IYContributionData(
                            DateTime date
                           , string experimentName
                           , string trafficType
                           , int impressions
                           , double impressionContribution
                           , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Impressions = impressions;
            this.ImpressionsContribution = impressionContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public IYContributionData(
                           int flightNo
                          , string experimentName
                          , string trafficType
                          , int impressions
                          , double impressionContribution
                          , DateTime lastrefreshed
                         )
        {
            this.FlightNo = flightNo;
            this.ExperimentName = experimentName;
            this.TrafficType = trafficType;
            this.Impressions = impressions;
            this.ImpressionsContribution = impressionContribution;
            this.LastRefreshed = lastrefreshed;
        }

        public IYContributionData(
                           DateTime date
                           , int impressions
                           , double impressionContribution
                           , double adjustedImpressionContribution
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Impressions = impressions;
            this.ImpressionsContribution = impressionContribution;
            this.AdjustedImpressionsContribution = adjustedImpressionContribution;
            this.LastRefreshed = lastrefreshed;
        }


        public IYContributionData(
                           DateTime date
                           , int impressions
                           , int srpv
                           , double impressionYield
                           , double adjustedImpressionsContribution
                           , double adjustedIYDelta
                           , double iyDelta
                           , DateTime lastrefreshed
                           , int value
                          )
        {
            this.Date = date;
            this.Impressions = impressions;
            this.Srpv = srpv;
            this.ImpressionYield = impressionYield;
            this.AdjustedImpressionsContribution = adjustedImpressionsContribution;
            this.AdjustedIYDelta = adjustedIYDelta;
            this.IYDelta = iyDelta;
            this.LastRefreshed = lastrefreshed;
        }

        public IYContributionData(
                            DateTime date
                          , double iyContribution
                          , DateTime lastrefreshed
                          )
        {
            this.Date = date;
            this.IYContribution = iyContribution;
            this.LastRefreshed = lastrefreshed;
        }


        #endregion

        #region Propertychangedevent

        public event PropertyChangedEventHandler PropertyChanged;
        private void PropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
