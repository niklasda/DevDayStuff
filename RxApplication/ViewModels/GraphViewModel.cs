using System;
using System.Threading;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using RxDemoCode.Interfaces;

//using RxDemoCode.Demos;

namespace RxApplication.ViewModels
{
    public class GraphViewModel : ViewModelBase
    {
        public GraphViewModel()
        {
            var plotModel = new PlotModel { Title = "Example 1", Subtitle = "Graph" };
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -1, Maximum = 10 });
            plotModel.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });

            GraphData = plotModel;

            Update2();
        }

        private PlotModel _graphData;
        public PlotModel GraphData
        {
            get { return _graphData; }
            set
            {
                if (_graphData != value)
                {
                    _graphData = value;
                    RaisePropertyChanged();
                }
            }
        }

        // private Func<double, double, double, double> Function { get; set; }
        // Function = (t, x, a) => Math.Cos(t * a) * (x == 0 ? 1 : Math.Sin(x * a) / x);

        private void Update()
        {
            var s = (LineSeries)GraphData.Series[0];

            double x = s.Points.Count > 0 ? s.Points[s.Points.Count - 1].X + 1 : 0;
            if (s.Points.Count >= 200)
                s.Points.RemoveAt(0);

            var r = new Random(DateTime.Now.Millisecond);
            double y = r.Next(-1, 2) * r.NextDouble();

            s.Points.Add(new DataPoint(x, y));
        }

        private void Update2()
        {
            var s = (LineSeries) GraphData.Series[0];

            Action<string> callback = x =>
            {
                double y = double.Parse(x);
                s.Points.Add(new DataPoint(y, y));
                GraphData.InvalidatePlot(true);
            };

            var d = ServiceLocator.Current.GetInstance<IDemo1Service>();

            d.Demo1(callback);

        }
    }
}