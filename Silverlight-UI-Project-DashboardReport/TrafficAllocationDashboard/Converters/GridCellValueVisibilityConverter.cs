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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using TrafficAllocationDashboard.TrafficServiceReference;

namespace TrafficAllocationDashboard.Converters
{
    public class GridCellValueVisibilityConverter : IValueConverter
    {
        public static int iTodays;
        public object Convert(object value,
                             System.Type targetType,
                             object parameter,
                             CultureInfo culture)
        {
            //if (value.ToString() == "0")
            //{
            //    return "";
            //}
            //else if (value.ToString() == DateTime.Now.AddDays(-iTodays).Date.ToString())
            //{
            //    return "";
            //}
            if (value.ToString() != "0" && value.ToString().Contains("-"))
            {
                if (value.ToString().Contains("."))
                {
                    value = value.ToString().Substring(1, value.ToString().LastIndexOf('.') - 1);
                    if (value.ToString() == string.Empty)
                        value = 0;
                    else
                        value = "(" + String.Format("{0:N0}", int.Parse(value.ToString())) + ")";
                }
                else
                    value = "(" + value.ToString().Substring(1, value.ToString().Length - 1) + ")";
                return value;
            }
            return value;
        }

        public object ConvertBack(object value,
                                  System.Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            return value;
        }
    }
}
