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
    public class BlackBarLineConverter : IValueConverter
    {
        public object Convert(object value,
                             System.Type targetType,
                             object parameter,
                             CultureInfo culture)
        {
            CurrentViewService _currentViewService = new CurrentViewService();

            int iTodays = 0;
            CategoricalDataPoint CP = value as CategoricalDataPoint;

            SolidColorBrush color;

            if (CP.Category.ToString() == DateTime.Now.AddDays(-iTodays).Date.ToString())
            {
                color = new SolidColorBrush(Colors.White);
            }
            else
            {
                color = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)); //Black
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
