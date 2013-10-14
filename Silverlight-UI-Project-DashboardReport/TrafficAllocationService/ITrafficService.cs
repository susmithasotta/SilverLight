using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections.ObjectModel;
using TrafficAllocation.Model.Entities;

namespace TrafficAllocationService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITrafficService
    {

        [OperationContract]
        ObservableCollection<NetworkViewSlicesData> GetNetworkViewSlices();

        [OperationContract]
        ObservableCollection<FlightCubeData> GetFlightCubeData();

        [OperationContract]
        ObservableCollection<CurrentViewData> GetCurrentViewData(string selectedView);
      
        [OperationContract]
        ObservableCollection<ExperimentAllocationData> GetExperimentAllocationData(string selectedView);

        [OperationContract]
        ObservableCollection<RPSContributionData> GetRPSContributionData(string selectedView);

        [OperationContract]
        ObservableCollection<MLIYContributionData> GetMLIYContributionData(string selectedView);

        [OperationContract]
        ObservableCollection<MLCYContributionData> GetMLCYContributionData(string selectedView);

        [OperationContract]
        ObservableCollection<IYContributionData> GetIYContributionData(string selectedView);

        [OperationContract]
        ObservableCollection<CYContributionData> GetCYContributionData(string selectedView);

        [OperationContract]
        ObservableCollection<ExperimentAllocationData> GetNetworkExperimentAllocationData(string selectedView);

        [OperationContract]
        ObservableCollection<RPSContributionData> GetNetworkRPSContributionData(string selectedView);

        [OperationContract]
        ObservableCollection<MLIYContributionData> GetNetworkMLIYContributionData(string selectedView);

        [OperationContract]
        ObservableCollection<CYContributionData> GetNetworkCYContributionData(string selectedView);

        [OperationContract]
        bool RefreshFlightCubeData();

        [OperationContract]
        string GetDefaultView();

        [OperationContract]
        string GetFromAndTodays();
    }

}
