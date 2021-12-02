using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    public class Graph
    {
        public double[] signalWave = new double[RecordingDevice.BUFFERSIZE/2];
        private double[] signalWaveYin = new double[RecordingDevice.BUFFERSIZE / 4];
        public double[] fftY = new double[RecordingDevice.BUFFERSIZE / 4];
        public double[] fftX = new double[RecordingDevice.BUFFERSIZE / 4];

        public double[] SignalWave { get => signalWave; set => signalWave = value; }
        public double[] FftY { get => fftY; set => fftY = value; }
        public double[] FftX { get => fftX; set => fftX = value; }
        public double[] SignalWaveYin { get => signalWaveYin; set => signalWaveYin = value; }

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
