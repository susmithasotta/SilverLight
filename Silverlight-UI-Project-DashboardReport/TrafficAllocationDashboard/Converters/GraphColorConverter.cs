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
using System.Windows.Data;
using System.Globalization;
using TrafficAllocationDashboard.TrafficServiceReference;
using Telerik.Charting;
using TrafficAllocationDashboard.ServiceAgent;
using TrafficAllocationDashboard.ViewModel;

namespace TrafficAllocationDashboard.Converters
{
    public class GraphColorConverter : IValueConverter
    {
        public static int iColorTodays;

        public object Convert(object value,
                              System.Type targetType,
                              object parameter,
                              CultureInfo culture)
        {

            CategoricalDataPoint CP = value as CategoricalDataPoint;

            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));

            if (CP.Category.ToString() == DateTime.Now.AddDays(-iColorTodays).Date.ToString())
            {
                color = new SolidColorBrush(Color.FromArgb(0xFF, 0xC0, 0x00, 0x00));
            }
            else
            {
                color = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
                //color = new SolidColorBrush(Color.FromArgb(0xFF, 0x87, 0xCE, 0xFA)); Light blue
                //color = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xBF, 0xFF)); 
            }

            return color;
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
