using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    public class Graph
    {
        public double[] signalWave = new double[4096];
        //public double[] signalWave = new double[8192];
        public double[] fftY = new double[2048];
        public double[] fftX = new double[2048];

        public double[] SignalWave { get => signalWave; set => signalWave = value; }
        public double[] FftY { get => fftY; set => fftY = value; }
        public double[] FftX { get => fftX; set => fftX = value; }

        public void initializeGraphs(ScottPlot.FormsPlot plotSignal,ScottPlot.FormsPlot plotFFT)
        {
            plotSignal.Plot.PlotSignal(this.SignalWave);
            plotFFT.Plot.AddSignalXY(this.fftX, this.fftY);
            plotSignal.Plot.SetAxisLimitsX(0, 2048);
            plotSignal.Plot.SetAxisLimitsY(-20000, 20000);

            plotFFT.Plot.SetAxisLimitsX(0, 2000);
            plotFFT.Plot.SetAxisLimitsY(0, 10000);

            plotFFT.Plot.XLabel("Częstotliwość");
        }
    }
}
