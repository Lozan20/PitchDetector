using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    class FFT
    {
        public static void performFFT()
        {
            System.Numerics.Complex[] fftComplex = new System.Numerics.Complex[Program.calculateAndPlotData.SignalWave.Length * 2];
            for (int i = 0; i < Program.calculateAndPlotData.SignalWave.Length; i++)
            {
                fftComplex[i] = new System.Numerics.Complex(Program.calculateAndPlotData.SignalWave[i], 0);
            }

            Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);

            for (int i = 0; i < Program.calculateAndPlotData.SignalWave.Length / 2; i++)
            {
                Program.calculateAndPlotData.FftY[i] = fftComplex[i].Magnitude;
                Program.calculateAndPlotData.FftX[i] = (double)i / (Program.calculateAndPlotData.SignalWave.Length * 2) * RecordingDevice.Rate;
            }
        }

    }
}


