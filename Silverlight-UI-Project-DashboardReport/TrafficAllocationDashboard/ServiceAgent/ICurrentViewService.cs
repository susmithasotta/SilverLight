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
using System.Collections.ObjectModel;
using TrafficAllocationDashboard.TrafficServiceReference;

namespace TrafficAllocationDashboard.ServiceAgent
{
    public interface ICurrentViewService
    {
        void GetNetworkViewSlicesData(ObservableCollection<NetworkViewSlicesData> networkViewData, EventHandler<GetNetworkViewSlicesCompletedEventArgs> callBack);
        void GetCurrentViewData(string selectedView, EventHandler<GetCurrentViewDataCompletedEventArgs> callBack);
        void GetExperimentAllocationData(string selectedView, EventHandler<GetExperimentAllocationDataCompletedEventArgs> callBack);
        void GetRPSContributionData(string selectedView, EventHandler<GetRPSContributionDataCompletedEventArgs> callBack);
        void GetMLIYContributionData(string selectedView, EventHandler<GetMLIYContributionDataCompletedEventArgs> callBack);
        void GetMLCYContributionData(string selectedView, EventHandler<GetMLCYContributionDataCompletedEventArgs> callBack);
        void GetIYContributionData(string selectedView, EventHandler<GetIYContributionDataCompletedEventArgs> callBack);
        void GetCYContributionData(string selectedView, EventHandler<GetCYContributionDataCompletedEventArgs> callBack);
        void GetNetworkExperimentAllocationData(string selectedView, EventHandler<GetNetworkExperimentAllocationDataCompletedEventArgs> callBack);
        void GetNetworkRPSContributionData(string selectedView, EventHandler<GetNetworkRPSContributionDataCompletedEventArgs> callBack);
        void GetNetworkMLIYContributionData(string selectedView, EventHandler<GetNetworkMLIYContributionDataCompletedEventArgs> callBack);
        void GetNetworkCYContributionData(string selectedView, EventHandler<GetNetworkCYContributionDataCompletedEventArgs> callBack);
        void GetDefaultView(EventHandler<GetDefaultViewCompletedEventArgs> callBack);
        void GetRefreshFlightCubeData(EventHandler<RefreshFlightCubeDataCompletedEventArgs> callBack);
        void GetFromAndToDays(EventHandler<GetFromAndTodaysCompletedEventArgs> callBack);
    }

}
