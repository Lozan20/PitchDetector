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
            System.Numerics.Complex[] fftComplex = new System.Numerics.Complex[Program.graphData.signalWave.Length * 2];
            for (int i = 0; i < Program.graphData.signalWave.Length; i++)
            {
                fftComplex[i] = new System.Numerics.Complex(Program.graphData.signalWave[i], 0);
            }
            Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);


            
            for (int i = 0; i < Program.graphData.signalWave.Length / 2; i++)
            {
                Program.graphData.fftY[i] = fftComplex[i].Magnitude;
                Program.graphData.fftX[i] = (double)i / (Program.graphData.signalWave.Length * 2) * Program.rD.RATE;
            }
        }
    }
}
