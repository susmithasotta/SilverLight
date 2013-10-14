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


namespace TrafficAllocationDashboard.Converters
{
    public class GridHeaderColorConverter : IValueConverter
    {
        //public object convert(object item, DependencyObject container)    
        //{ 
        //    var style = new Style(typeof(GridViewHeaderCell));
        //    var headercell = (GridViewHeaderCell)container;
        //    var obj = GridViewHeaderCell.NameProperty; 
        //    if (obj.ToString() == "RPS")
        //    {           
        //        style.Setters.Add(new Setter(GridViewHeaderCell.BackgroundProperty, new SolidColorBrush(Colors.Red)));        
        //    } 
        //    else 
        //    {
        //        style.Setters.Add(new Setter(GridViewHeaderCell.BackgroundProperty, new SolidColorBrush(Colors.Blue)));        
        //    } 
        //    return style;    
        //}

        public object Convert(object value,
                            System.Type targetType,
                            object parameter,
                            CultureInfo culture)
        {

            var style = new Style(typeof(GridViewHeaderCell));

            if (style.ToString() == "RPS")
            {
                style.Setters.Add(new Setter(GridViewHeaderCell.BackgroundProperty, new SolidColorBrush(Colors.Red)));
            }
            else
            {
                style.Setters.Add(new Setter(GridViewHeaderCell.BackgroundProperty, new SolidColorBrush(Colors.White)));
            }

            return style;
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
