using System;
using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Series;

namespace RxApplication.ViewModels
{
    public class GraphViewModel : ViewModelBase
    {
        public GraphViewModel()
        {
            _graphData = new PlotModel { Title = "Example 1" };
            _graphData.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
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
    }
}