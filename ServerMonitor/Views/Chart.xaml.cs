﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Telerik.UI.Xaml.Controls.Chart;
using Template10.Common;
using Template10.Services.NavigationService;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
/// <summary>
/// 创建：fjl  创建时间：2018/05/26
/// Chart页面后台
/// </summary>
namespace ServerMonitor.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Chart : Page
    {
        public ChartPalette DefaultPalette { get { return ChartPalettes.DefaultLight; } }

        public Chart()
        {   
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
            CreateSeries();
        }

        public void CreateSeries()
        {
            RadCartesianChart chart = this.RequestTimeLineChar as RadCartesianChart;
            chart.Series.Clear();
            //指定建立五个序列
            for (int i = 0; i < 5; i++)
            {
                CategoricalSeriesBase series = null;
                if (chart == null)//若图表为null
                {
                    return;
                }
                //创建指定类型的序列
                //series = new LineSeries()
                //{
                //    Stroke = DefaultPalette.FillEntries.Brushes[i],
                //    StrokeThickness = 3,
                //    CategoryBinding = new PropertyNameDataPointBinding("RequestTime"),
                //    ValueBinding = new PropertyNameDataPointBinding("ResponseTime"),
                //    PointTemplate = chart.Resources["PointTemplate"] as DataTemplate
                //};
                series = new PointSeries()
                {
                    CategoryBinding = new PropertyNameDataPointBinding("RequestTime"),
                    ValueBinding = new PropertyNameDataPointBinding("ResponseTime"),
                    PointTemplate = chart.Resources["PointTemplate"] as DataTemplate
                };
                series.SetBinding(ChartSeries.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Infos.LineChartCollection[" + i + "]") });
                series.ClipToPlotArea = false;
                //序列添加到图表
                chart.Series.Add(series);
            }
        }
    }
}
