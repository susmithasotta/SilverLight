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
using Telerik.Charting;
using System.Windows.Data;
using System.Globalization;
using TrafficAllocationDashboard.ServiceAgent;

namespace TrafficAllocationDashboard.Converters
{
    public class GraphColorWidthConverter : IValueConverter
    {
        public static int iColorWidthTodays;
        public object Convert(object value,
                             System.Type targetType,
                             object parameter,
                             CultureInfo culture)
        {
            CategoricalDataPoint CP = value as CategoricalDataPoint;

            int iValue;

            if (CP.Category.ToString() == DateTime.Now.AddDays(-iColorWidthTodays).Date.ToString())
            {
                iValue = 20;
            }
            else
            {
                iValue = 8; 
            }
            return iValue;
        }

        public object ConvertBack(object value,
                                  System.Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            return null;
        }
    }
}
