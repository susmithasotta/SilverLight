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
using TrafficAllocationDashboard.TrafficServiceReference;
using System.Collections.ObjectModel;

namespace TrafficAllocationDashboard.ServiceAgent
{
    public class CurrentViewService : ICurrentViewService
    {
        private readonly TrafficServiceClient _objClient = new TrafficServiceClient();

        public void GetNetworkViewSlicesData(ObservableCollection<NetworkViewSlicesData> networkViewData, EventHandler<GetNetworkViewSlicesCompletedEventArgs> callBack)
        {
            _objClient.GetNetworkViewSlicesCompleted += callBack;
            _objClient.GetNetworkViewSlicesAsync();

        }
        public void GetCurrentViewData(string selectedView, EventHandler<GetCurrentViewDataCompletedEventArgs> callBack)
        {
            _objClient.GetCurrentViewDataCompleted += callBack;
            _objClient.GetCurrentViewDataAsync(selectedView);
        }

        public void GetExperimentAllocationData(string selectedView, EventHandler<GetExperimentAllocationDataCompletedEventArgs> callBack)
        {
            _objClient.GetExperimentAllocationDataCompleted += callBack;
            _objClient.GetExperimentAllocationDataAsync(selectedView);
        }

        public void GetRPSContributionData(string selectedView, EventHandler<GetRPSContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetRPSContributionDataCompleted += callBack;
            _objClient.GetRPSContributionDataAsync(selectedView);
        }

        public void GetMLIYContributionData(string selectedView, EventHandler<GetMLIYContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetMLIYContributionDataCompleted += callBack;
            _objClient.GetMLIYContributionDataAsync(selectedView);
        }

        public void GetMLCYContributionData(string selectedView, EventHandler<GetMLCYContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetMLCYContributionDataCompleted += callBack;
            _objClient.GetMLCYContributionDataAsync(selectedView);
        }

        public void GetIYContributionData(string selectedView, EventHandler<GetIYContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetIYContributionDataCompleted += callBack;
            _objClient.GetIYContributionDataAsync(selectedView);
        }

        public void GetCYContributionData(string selectedView, EventHandler<GetCYContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetCYContributionDataCompleted += callBack;
            _objClient.GetCYContributionDataAsync(selectedView);
        }

        public void GetNetworkExperimentAllocationData(string selectedView, EventHandler<GetNetworkExperimentAllocationDataCompletedEventArgs> callBack)
        {
            _objClient.GetNetworkExperimentAllocationDataCompleted += callBack;
            _objClient.GetNetworkExperimentAllocationDataAsync(selectedView);
        }

        public void GetNetworkRPSContributionData(string selectedView, EventHandler<GetNetworkRPSContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetNetworkRPSContributionDataCompleted += callBack;
            _objClient.GetNetworkRPSContributionDataAsync(selectedView);
        }

        public void GetNetworkMLIYContributionData(string selectedView, EventHandler<GetNetworkMLIYContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetNetworkMLIYContributionDataCompleted += callBack;
            _objClient.GetNetworkMLIYContributionDataAsync(selectedView);
        }

        public void GetNetworkCYContributionData(string selectedView, EventHandler<GetNetworkCYContributionDataCompletedEventArgs> callBack)
        {
            _objClient.GetNetworkCYContributionDataCompleted += callBack;
            _objClient.GetNetworkCYContributionDataAsync(selectedView);
        }

        public void GetDefaultView(EventHandler<GetDefaultViewCompletedEventArgs> callBack)
        {
            _objClient.GetDefaultViewCompleted += callBack;
            _objClient.GetDefaultViewAsync();
        }

        public void GetRefreshFlightCubeData(EventHandler<RefreshFlightCubeDataCompletedEventArgs> callBack)
        {
            _objClient.RefreshFlightCubeDataCompleted += callBack;
            _objClient.RefreshFlightCubeDataAsync();
        }

        public void GetFromAndToDays(EventHandler<GetFromAndTodaysCompletedEventArgs> callBack)
        {
            _objClient.GetFromAndTodaysCompleted += callBack;
            _objClient.GetFromAndTodaysAsync();
        }
    }
}
