using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using TrafficAllocationDashboard.TrafficServiceReference;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TrafficAllocationDashboard.ServiceAgent;
using System.Linq;
using TrafficAllocationDashboard.Converters;
namespace TrafficAllocationDashboard.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly CurrentViewService _currentViewService;
        public static int iFromdays;
        public static int iTodays;

        public MainPageViewModel()
        {
            if (!IsDesignTime)
            {
                _currentViewService = new CurrentViewService();
                GetNetworkViewSlicesData();
                this.GetCurrentViewCommand = new DelegateCommand(GetCurrentView, CanLoadView);
            }
        }


        private ObservableCollection<NetworkViewSlicesData> _networkViewSlicesDatas;
        private ObservableCollection<CurrentViewData> _viewData;


        public ObservableCollection<NetworkViewSlicesData> NetworkViewData
        {
            get { return _networkViewSlicesDatas; }
            set
            {
                _networkViewSlicesDatas = value;
                OnPropertyChanged("NetworkViewData");
            }
        }

        public ObservableCollection<CurrentViewData> ViewData
        {
            get { return _viewData; }
            set
            {
                _viewData = value;
                OnPropertyChanged("ViewData");
            }
        }

        private ObservableCollection<ExperimentAllocationData> _experimentAllocatioData;
        public ObservableCollection<ExperimentAllocationData> ExperimentAllocationData
        {
            get { return _experimentAllocatioData; }
            set
            {
                _experimentAllocatioData = value;
                OnPropertyChanged("ExperimentAllocationData");
            }
        }

        private ObservableCollection<RPSContributionData> _rpsContributionData;
        public ObservableCollection<RPSContributionData> RPSContributionData
        {
            get { return _rpsContributionData; }
            set
            {
                _rpsContributionData = value;
                OnPropertyChanged("RPSContributionData");
            }
        }

        private ObservableCollection<MLIYContributionData> _mliyContributionData;
        public ObservableCollection<MLIYContributionData> MLIYContributionData
        {
            get { return _mliyContributionData; }
            set
            {
                _mliyContributionData = value;
                OnPropertyChanged("MLIYContributionData");
            }
        }

        private ObservableCollection<MLCYContributionData> _mlcyContributionData;
        public ObservableCollection<MLCYContributionData> MLCYContributionData
        {
            get { return _mlcyContributionData; }
            set
            {
                _mlcyContributionData = value;
                OnPropertyChanged("MLCYContributionData");
            }
        }

        private ObservableCollection<IYContributionData> _iyContributionData;
        public ObservableCollection<IYContributionData> IYContributionData
        {
            get { return _iyContributionData; }
            set
            {
                _iyContributionData = value;
                OnPropertyChanged("IYContributionData");
            }
        }

        private ObservableCollection<CYContributionData> _cyContributionData;
        public ObservableCollection<CYContributionData> CYContributionData
        {
            get { return _cyContributionData; }
            set
            {
                _cyContributionData = value;
                OnPropertyChanged("CYContributionData");
            }
        }

        private ObservableCollection<ExperimentAllocationData> _networkExperimentAllocatioData;
        public ObservableCollection<ExperimentAllocationData> NetworkExperimentAllocationData
        {
            get { return _networkExperimentAllocatioData; }
            set
            {
                _networkExperimentAllocatioData = value;
                OnPropertyChanged("NetworkExperimentAllocationData");
            }
        }

        private ObservableCollection<RPSContributionData> _networkRpsContributionData;
        public ObservableCollection<RPSContributionData> NetworkRPSContributionData
        {
            get { return _networkRpsContributionData; }
            set
            {
                _networkRpsContributionData = value;
                OnPropertyChanged("NetworkRPSContributionData");
            }
        }

        private ObservableCollection<MLIYContributionData> _netowrkMliyContributionData;
        public ObservableCollection<MLIYContributionData> NetworkMLIYContributionData
        {
            get { return _netowrkMliyContributionData; }
            set
            {
                _netowrkMliyContributionData = value;
                OnPropertyChanged("NetworkMLIYContributionData");
            }
        }

        private ObservableCollection<CYContributionData> _netowrkcyContributionData;
        public ObservableCollection<CYContributionData> NetworkCYContributionData
        {
            get { return _netowrkcyContributionData; }
            set
            {
                _netowrkcyContributionData = value;
                OnPropertyChanged("NetworkCYContributionData");
            }
        }

        private string _selectedActuallSliceData;
        public string SelectedActuallSliceData
        {
            get { return _selectedActuallSliceData; }
            set
            {
                _selectedActuallSliceData = value;
                OnPropertyChanged("SelectedActuallSliceData");
            }
        }


        private NetworkViewSlicesData _selectedNetworkViewSliceData;
        public NetworkViewSlicesData SelectedNetworkViewSliceData
        {
            get { return _selectedNetworkViewSliceData; }
            set
            {
                _selectedNetworkViewSliceData = value;
                OnPropertyChanged("SelectedNetworkViewSliceData");
            }
        }

        private Double _allocationBudgetDiff;
        public Double AllocationBudgetDiff
        {
            get { return _allocationBudgetDiff; }
            set
            {
                _allocationBudgetDiff = value;
                OnPropertyChanged("AllocationBudgetDiff");
            }
        }

        private Double _experimentAllocationBudgetDiff;
        public Double ExperimentAllocationBudgetDiff
        {
            get { return _experimentAllocationBudgetDiff; }
            set
            {
                _experimentAllocationBudgetDiff = value;
                OnPropertyChanged("ExperimentAllocationBudgetDiff");
            }
        }

        private Double _currentdayExperimentAllocation;
        public Double CurrentdayExperimentAllocation
        {
            get { return _currentdayExperimentAllocation; }
            set
            {
                _currentdayExperimentAllocation = value;
                OnPropertyChanged("CurrentdayExperimentAllocation");
            }
        }

        private Double _currentdayRPSContribution;
        public Double CurrentdayRPSContribution
        {
            get { return _currentdayRPSContribution; }
            set
            {
                _currentdayRPSContribution = value;
                OnPropertyChanged("CurrentdayRPSContribution");
            }
        }

        private Double _currentdayMLIYContribution;
        public Double CurrentdayMLIYContribution
        {
            get { return _currentdayMLIYContribution; }
            set
            {
                _currentdayMLIYContribution = value;
                OnPropertyChanged("CurrentdayMLIYContribution");
            }
        }

        private Double _currentdayMLCYContribution;
        public Double CurrentdayMLCYContribution
        {
            get { return _currentdayMLCYContribution; }
            set
            {
                _currentdayMLCYContribution = value;
                OnPropertyChanged("CurrentdayMLCYContribution");
            }
        }

        private Double _currentdayIYContribution;
        public Double CurrentdayIYContribution
        {
            get { return _currentdayIYContribution; }
            set
            {
                _currentdayIYContribution = value;
                OnPropertyChanged("CurrentdayIYContribution");
            }
        }

        private Double _currentdayCYContribution;
        public Double CurrentdayCYContribution
        {
            get { return _currentdayCYContribution; }
            set
            {
                _currentdayCYContribution = value;
                OnPropertyChanged("CurrentdayCYContribution");
            }
        }

        private Double _currentdayNetworkExperimentAllocation;
        public Double CurrentdayNetworkExperimentAllocation
        {
            get { return _currentdayNetworkExperimentAllocation; }
            set
            {
                _currentdayNetworkExperimentAllocation = value;
                OnPropertyChanged("CurrentdayNetworkExperimentAllocation");
            }
        }

        private Double _currentdayNetworkRPSContribution;
        public Double CurrentdayNetworkRPSContribution
        {
            get { return _currentdayNetworkRPSContribution; }
            set
            {
                _currentdayNetworkRPSContribution = value;
                OnPropertyChanged("CurrentdayNetworkRPSContribution");
            }
        }

        private Double _currentdayNetworkMLIYContribution;
        public Double CurrentdayNetworkMLIYContribution
        {
            get { return _currentdayNetworkMLIYContribution; }
            set
            {
                _currentdayNetworkMLIYContribution = value;
                OnPropertyChanged("CurrentdayNetworkMLIYContribution");
            }
        }

        private Double _currentdayNetworkCYContribution;
        public Double CurrentdayNetworkCYContribution
        {
            get { return _currentdayNetworkCYContribution; }
            set
            {
                _currentdayNetworkCYContribution = value;
                OnPropertyChanged("CurrentdayNetworkCYContribution");
            }
        }

        private Double _currentdayTotalAllocation;
        public Double CurrentdayTotalAllocation
        {
            get { return _currentdayTotalAllocation; }
            set
            {
                _currentdayTotalAllocation = value;
                OnPropertyChanged("CurrentdayTotalAllocation");
            }
        }
        private Double _currentdayTotalRPS;
        public Double CurrentdayTotalRPS
        {
            get { return _currentdayTotalRPS; }
            set
            {
                _currentdayTotalRPS = value;
                OnPropertyChanged("CurrentdayTotalRPS");
            }
        }
        private Double _currentdayTotalMLIY;
        public Double CurrentdayTotalMLIY
        {
            get { return _currentdayTotalMLIY; }
            set
            {
                _currentdayTotalMLIY = value;
                OnPropertyChanged("CurrentdayTotalMLIY");
            }
        }
        private Double _currentdayTotalIY;
        public Double CurrentdayTotalIY
        {
            get { return _currentdayTotalIY; }
            set
            {
                _currentdayTotalIY = value;
                OnPropertyChanged("CurrentdayTotalIY");
            }
        }
        private Double _currentdayTotalMLCY;
        public Double CurrentdayTotalMLCY
        {
            get { return _currentdayTotalMLCY; }
            set
            {
                _currentdayTotalMLCY = value;
                OnPropertyChanged("CurrentdayTotalMLCY");
            }
        }
        private Double _currentdayTotalCY;
        public Double CurrentdayTotalCY
        {
            get { return _currentdayTotalCY; }
            set
            {
                _currentdayTotalCY = value;
                OnPropertyChanged("CurrentdayTotalCY");
            }
        }
        private Double _currentdayTotalRevenue;
        public Double CurrentdayTotalRevenue
        {
            get { return _currentdayTotalRevenue; }
            set
            {
                _currentdayTotalRevenue = value;
                OnPropertyChanged("CurrentdayTotalRevenue");
            }
        }
        private Double _currentdayTotalImpressions;
        public Double CurrentdayTotalImpressions
        {
            get { return _currentdayTotalImpressions; }
            set
            {
                _currentdayTotalImpressions = value;
                OnPropertyChanged("CurrentdayTotalImpressions");
            }
        }
        private Double _currentdayTotalClicks;
        public Double CurrentdayTotalClicks
        {
            get { return _currentdayTotalClicks; }
            set
            {
                _currentdayTotalClicks = value;
                OnPropertyChanged("CurrentdayTotalClicks");
            }
        }

        private string _reportDateUTC;
        public string ReportDateUTC
        {
            get { return _reportDateUTC; }
            set
            {
                _reportDateUTC = value;
                OnPropertyChanged("ReportDateUTC");
            }
        }

        private string _reportDatePST;
        public string ReportDatePST
        {
            get { return _reportDatePST; }
            set
            {
                _reportDatePST = value;
                OnPropertyChanged("ReportDatePST");
            }
        }

        private int _tiedControlFlight;
        public int TiedControlFlight
        {
            get { return _tiedControlFlight; }
            set
            {
                _tiedControlFlight = value;
                OnPropertyChanged("TiedControlFlight");
            }
        }
        private int _untiedControlFlight;
        public int UntiedControlFlight
        {
            get { return _untiedControlFlight; }
            set
            {
                _untiedControlFlight = value;
                OnPropertyChanged("UntiedControlFlight");
            }
        }

        private string _featureArea;
        public string FeatureArea
        {
            get { return _featureArea; }
            set
            {
                _featureArea = value;
                OnPropertyChanged("FeatureArea");
            }
        }

        private string _experimentType;
        public string ExperimentType
        {
            get { return _experimentType; }
            set
            {
                _experimentType = value;
                OnPropertyChanged("ExperimentType");
            }
        }

        private string _defaultView;
        public string DefaultView
        {
            get { return _defaultView; }
            set
            {
                _defaultView = value;
                OnPropertyChanged("DefaultView");
            }
        }

        private bool _refreshCubeData;
        public bool RefreshCubeData
        {
            get { return _refreshCubeData; }
            set
            {
                _refreshCubeData = value;
                OnPropertyChanged("RefreshCubeData");
            }
        }

        public ICommand GetCurrentViewCommand { get; set; }

        private void GetNetworkViewSlicesData()
        {
            _currentViewService.GetRefreshFlightCubeData((h, b) =>
            {
                RefreshCubeData = b.Result;
                _currentViewService.GetNetworkViewSlicesData(NetworkViewData, (s, e) =>
                {
                    NetworkViewData = e.Result;
                    _currentViewService.GetDefaultView((d, f) =>
                    {
                        SelectedActuallSliceData = f.Result;
                        SelectedNetworkViewSliceData = NetworkViewData.FirstOrDefault(c => c.ActuallViewName == SelectedActuallSliceData);
                        BindGraphsAndGridData();
                    });   //"Microsoft,O&O,United States,PaidSearch,PC,Relevance,Experiment,10,166";

                });
            });
        }

        private void BindGraphsAndGridData()
        {
            try
            {
                if (SelectedNetworkViewSliceData != null)
                {
                    string[] strSelectedView = SelectedNetworkViewSliceData.ActuallViewName.Split(',');
                    FeatureArea = strSelectedView[5];
                    ExperimentType = strSelectedView[6];
                    TiedControlFlight = Convert.ToInt16(strSelectedView[7]);
                    UntiedControlFlight = Convert.ToInt16(strSelectedView[8]);

                    CurrentdayExperimentAllocation = 0.00;
                    CurrentdayRPSContribution = 0.00;
                    CurrentdayMLIYContribution = 0.00;
                    CurrentdayIYContribution = 0.00;
                    CurrentdayMLCYContribution = 0.00;
                    CurrentdayCYContribution = 0.00;


                    //ExperimentAllocationData.Count = 0;
                    //RPSContributionData.Count = 0;
                    //MLIYContributionData.Count = 0;
                    //IYContributionData.Count = 0;
                    //MLCYContributionData.Count = 0;
                    //CYContributionData.Count = 0;

                    //int iFromdays = 42;
                    //int iTodays = 35;

                    //int iFromdays = 7;
                    //int iTodays = 1;

                    //_currentViewService.GetFromAndToDays((g, h) =>
                    //    {
                    //        string[] strFromAndToDays = h.Result.Split(',');
                    _currentViewService.GetExperimentAllocationData(SelectedNetworkViewSliceData.ActuallViewName, (j, k) =>
                    {
                        ExperimentAllocationData = k.Result;
                        RPSContributionData = new ObservableCollection<TrafficServiceReference.RPSContributionData>();
                        MLIYContributionData = new ObservableCollection<TrafficServiceReference.MLIYContributionData>();
                        IYContributionData = new ObservableCollection<TrafficServiceReference.IYContributionData>();
                        MLCYContributionData = new ObservableCollection<TrafficServiceReference.MLCYContributionData>();
                        CYContributionData = new ObservableCollection<CYContributionData>();
                        ViewData = new ObservableCollection<CurrentViewData>();
                        if (ExperimentAllocationData != null && ExperimentAllocationData.Count != 0)
                        {
                            var iFromDate = ExperimentAllocationData.FirstOrDefault();
                            var iToDate = ExperimentAllocationData.LastOrDefault();
                            string strFromDate = iFromDate.Date.ToShortDateString();
                            string strToDate = iToDate.Date.ToShortDateString();
                            TimeSpan fromts = Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(strFromDate);
                            TimeSpan tots = Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(strToDate);
                            iFromdays = fromts.Days;
                            iTodays = tots.Days;

                            //iFromdays = Convert.ToInt16(strFromAndToDays[0]);
                            //iTodays = Convert.ToInt16(strFromAndToDays[1]);

                            //Set Today date for color change of last day
                            GraphColorConverter.iColorTodays = iTodays;
                            //Set Today date for width change of last datapoint
                            GraphColorWidthConverter.iColorWidthTodays = iTodays;

                            GridCellValueVisibilityConverter.iTodays = iTodays;


                            var firstitemPST = ExperimentAllocationData.FirstOrDefault(c => c.Date == DateTime.Now.AddDays(-iFromdays).Date);
                            var itemPST = ExperimentAllocationData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);

                            var firstitem = ExperimentAllocationData.FirstOrDefault(c => c.Date == DateTime.Now.AddDays(-iFromdays).Date);
                            var lastitem = ExperimentAllocationData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);

                            if (null != lastitem)
                            {
                                CurrentdayExperimentAllocation = lastitem.ExperimentAllocation;
                                //AllocationBudgetDiff = ((CurrentdayExperimentAllocation * 100) - lastitem.AllocationBudget) / 100;

                                //if (null != firstitem && null != item)
                                //{
                                //    ReportDateUTC = firstitem.Date.Month + "/" + firstitem.Date.Day + " 0000";
                                //    ReportDateUTC += " - " + item.Date.Month + "/" + item.Date.AddDays(1).Day + " 0000(UTC)";

                                //}
                                if (null != firstitemPST && null != itemPST)
                                {
                                    ReportDateUTC = firstitem.Date.Month + "/" + firstitem.Date.Day + " 0000";
                                    ReportDateUTC += " - " + lastitem.Date.Month + "/" + lastitem.Date.AddDays(1).Day + " 0000(UTC)";

                                    ReportDatePST = itemPST.Date.Month + "/" + itemPST.Date.Day + "(UTC - full day)";
                                    //ReportDatePST = firstitemPST.Date.Month + "/" + firstitemPST.Date.Day + " " + firstitemPST.Date.Hour + "" + firstitemPST.Date.Minute;
                                    //ReportDatePST += " - " + itemPST.Date.Month + "/" + itemPST.Date.Day + " " + itemPST.Date.Hour + "" + itemPST.Date.Minute + "(PST)";
                                }
                            }

                            _currentViewService.GetCurrentViewData(SelectedNetworkViewSliceData.ActuallViewName,
                                                                   (s, e) =>
                                                                   {
                                                                       ViewData = e.Result;
                                                                       //// ViewData =  new ObservableCollection<CurrentViewData>(from d in e.Result orderby d.ExperimentName)
                                                                       // if (null != ViewData)
                                                                       // {
                                                                       //     CurrentdayTotalAllocation = ViewData.Sum(c => c.Allocation);
                                                                       //     CurrentdayTotalRPS = ViewData.Sum(c => c.RPS);
                                                                       //     CurrentdayTotalMLIY = ViewData.Sum(c => c.MLIY);
                                                                       //     CurrentdayTotalIY = ViewData.Sum(c => c.IY);
                                                                       //     CurrentdayTotalMLCY = ViewData.Sum(c => c.MLCY);
                                                                       //     CurrentdayTotalCY = ViewData.Sum(c => c.CY);
                                                                       //     CurrentdayTotalRevenue = ViewData.Sum(c => c.RevenueContribution);
                                                                       //     CurrentdayTotalImpressions = ViewData.Sum(c => c.MainlineImpression);
                                                                       //     CurrentdayTotalClicks = ViewData.Sum(c => c.Clicks);
                                                                       // }
                                                                   });
                            //_currentViewService.GetExperimentAllocationData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                            //{
                            //    ExperimentAllocationData = e.Result;
                            //    if (ExperimentAllocationData != null)
                            //    {
                            //        var firstitemPST = ExperimentAllocationData.FirstOrDefault(c => c.Date == DateTime.Now.AddDays(-iFromdays).Date);
                            //        var itemPST = ExperimentAllocationData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);

                            //        var firstitem = ExperimentAllocationData.FirstOrDefault(c => c.Date == DateTime.Now.AddDays(-iFromdays).Date);
                            //        var item = ExperimentAllocationData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);

                            //        if (null != item)
                            //        {
                            //            CurrentdayExperimentAllocation = item.ExperimentAllocation;
                            //            AllocationBudgetDiff = ((CurrentdayExperimentAllocation * 100) - item.AllocationBudget) / 100;

                            //            //if (null != firstitem && null != item)
                            //            //{
                            //            //    ReportDateUTC = firstitem.Date.Month + "/" + firstitem.Date.Day + " 0000";
                            //            //    ReportDateUTC += " - " + item.Date.Month + "/" + item.Date.AddDays(1).Day + " 0000(UTC)";

                            //            //}
                            //            if (null != firstitemPST && null != itemPST)
                            //            {
                            //                ReportDateUTC = firstitem.Date.Month + "/" + firstitem.Date.Day + " 0000";
                            //                ReportDateUTC += " - " + item.Date.Month + "/" + item.Date.AddDays(1).Day + " 0000(UTC)";

                            //                ReportDatePST = itemPST.Date.Month + "/" + itemPST.Date.Day + "(UTC - full day)";
                            //                //ReportDatePST = firstitemPST.Date.Month + "/" + firstitemPST.Date.Day + " " + firstitemPST.Date.Hour + "" + firstitemPST.Date.Minute;
                            //                //ReportDatePST += " - " + itemPST.Date.Month + "/" + itemPST.Date.Day + " " + itemPST.Date.Hour + "" + itemPST.Date.Minute + "(PST)";
                            //            }
                            //        }
                            //    }
                            //});
                            _currentViewService.GetRPSContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                            {
                                RPSContributionData = e.Result;
                                if (RPSContributionData != null)
                                {
                                    var item = RPSContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                                    if (null != item)
                                        CurrentdayRPSContribution = item.RpsContribution;
                                }
                            });
                            _currentViewService.GetMLIYContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                            {
                                MLIYContributionData = e.Result;
                                if (MLIYContributionData != null)
                                {
                                    var item = MLIYContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                                    if (null != item)
                                        CurrentdayMLIYContribution = item.MLIYContribution;
                                }
                            });
                            _currentViewService.GetMLCYContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                            {
                                MLCYContributionData = e.Result;
                                if (MLCYContributionData != null)
                                {
                                    var item = MLCYContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                                    if (null != item)
                                        CurrentdayMLCYContribution = item.MLCYContribution;
                                }
                            });

                            _currentViewService.GetIYContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                            {
                                IYContributionData = e.Result;
                                if (IYContributionData != null)
                                {
                                    var item = IYContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                                    if (null != item)
                                        CurrentdayIYContribution = item.IYContribution;
                                }
                            });
                            _currentViewService.GetCYContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                            {
                                CYContributionData = e.Result;
                                if (CYContributionData != null)
                                {
                                    var item = CYContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                                    if (null != item)
                                        CurrentdayCYContribution = item.CYContribution;
                                }
                            });
                        }
                      });
                    //_currentViewService.GetNetworkExperimentAllocationData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                    //{
                    //    NetworkExperimentAllocationData = e.Result;
                    //    if (NetworkExperimentAllocationData != null)
                    //    {
                    //        var item = NetworkExperimentAllocationData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                    //        if (null != item)
                    //        {
                    //            CurrentdayNetworkExperimentAllocation = item.ExperimentAllocation;
                    //            ExperimentAllocationBudgetDiff = (CurrentdayNetworkExperimentAllocation * 100 - item.AllocationBudget) / 100;
                    //        }
                    //    }
                    //});
                    //_currentViewService.GetNetworkRPSContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                    //{
                    //    NetworkRPSContributionData = e.Result;
                    //    if (NetworkRPSContributionData != null)
                    //    {
                    //        var item = NetworkRPSContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                    //        if (null != item)
                    //            CurrentdayNetworkRPSContribution = item.RpsContribution;
                    //    }
                    //});
                    //_currentViewService.GetNetworkMLIYContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                    //{
                    //    NetworkMLIYContributionData = e.Result;
                    //    if (NetworkMLIYContributionData != null)
                    //    {
                    //        var item = NetworkMLIYContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                    //        if (null != item)
                    //            CurrentdayNetworkMLIYContribution = item.MLIYContribution;
                    //    }
                    //});
                    //_currentViewService.GetNetworkCYContributionData(SelectedNetworkViewSliceData.ActuallViewName, (s, e) =>
                    //{
                    //    NetworkCYContributionData = e.Result;
                    //    if (NetworkCYContributionData != null)
                    //    {
                    //        var item = NetworkCYContributionData.LastOrDefault(c => c.Date == DateTime.Now.AddDays(-iTodays).Date);
                    //        if (null != item)
                    //            CurrentdayNetworkCYContribution = item.CYContribution;
                    //    }
                    //});
                }
            }
            catch
            {
            }
        }

        private void GetCurrentView(object parameter)
        {
            BindGraphsAndGridData();
        }

        private bool CanLoadView(object parameter)
        {
            return true;
        }

        //private void ExportToExcel(object parameter)
        //{
        //    var extension = "xls";
        //    var selectedItem = "XLS";
        //    var format = Telerik.Windows.Controls.ExportFormat.Html;
        //    SaveFileDialog dialog = new SaveFileDialog()
        //    {
        //        DefaultExt = extension,
        //        Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*",
        //        extension, selectedItem),
        //        FilterIndex = 1
        //    };
        //    dialog.ShowDialog();
        //    using (Stream stream = dialog.OpenFile())
        //    {
        //        var options = new Telerik.Windows.Controls.GridViewExportOptions()
        //        {
        //            Format = Telerik.Windows.Controls.ExportFormat.Html,
        //            ShowColumnHeaders = true,
        //            Encoding = System.Text.Encoding.UTF8
        //        };

        //        RadGridViewCurrentViewData.Export(stream, options);
        //    }
        //}

    }
}
