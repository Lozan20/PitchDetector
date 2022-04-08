using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    public class Graph
    {
        public void initializeGraphs(ScottPlot.FormsPlot plotSignal,ScottPlot.FormsPlot plotFFT)
        {
            plotSignal.Plot.PlotSignal(Program.calculateAndPlotData.SignalWave);
            plotFFT.Plot.AddSignalXY(Program.calculateAndPlotData.FftX, Program.calculateAndPlotData.FftY);

            plotSignal.Plot.SetAxisLimitsX(0, 8192);
            plotSignal.Plot.SetAxisLimitsY(-35000, 35000);
            plotFFT.Plot.SetAxisLimitsX(0, 1000);
            plotFFT.Plot.SetAxisLimitsY(0, 500);

            plotFFT.Plot.XLabel("Częstotliwość");
            plotFFT.Plot.YLabel("Amplituda");
            plotSignal.Plot.XLabel("Numer próbki");
            plotSignal.Plot.YLabel("Amplituda");
        }
    }
}
