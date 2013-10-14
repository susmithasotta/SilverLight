using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;
using System.Text;
using Telerik.Windows.Controls;

namespace TrafficAllocationDashboard
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ContextMenuData_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            var header = (string)((RadMenuItem)e.OriginalSource).Header;
            switch (header)
            {
                case "Export To Excel":
                    try
                    {

                        var extension = "xls";
                        var selectedItem = "XLS";
                        var format = Telerik.Windows.Controls.ExportFormat.Html;
                        SaveFileDialog dialog = new SaveFileDialog()
                        {
                            DefaultExt = extension,
                            Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*",
                            extension, selectedItem),
                            FilterIndex = 1
                        };
                        dialog.ShowDialog();
                        using (Stream stream = dialog.OpenFile())
                        {
                            var options = new Telerik.Windows.Controls.GridViewExportOptions()
                            {
                                Format = Telerik.Windows.Controls.ExportFormat.Html,
                                ShowColumnHeaders = true,
                                Encoding = System.Text.Encoding.UTF8
                            };

                            RadGridViewCurrentViewData.Export(stream, options);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    break;
                
                case "Select Columns To Display":
                    MyPOP.IsOpen = true;
                    MyPOP.FlowDirection = FlowDirection.LeftToRight;
                    ScaleTransform  pos = new ScaleTransform();
                    pos.CenterX = 1.0;
                        pos.CenterY = 1.0;
                        MyPOP.RenderTransform  = pos;
                    break;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyPOP.IsOpen = false;
        }

        //private void MyPOP_Closed(object sender, EventArgs e)
        //{
        //    MyPOP.Visibility = System.Windows.Visibility.Collapsed;
        //}

    }
}
