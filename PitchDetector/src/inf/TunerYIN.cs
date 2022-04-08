using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    class TunerYIN
    {

        public static double yin()
        {
            double pitch = 0;
            double[] sig = Program.calculateAndPlotData.SignalWaveYin;
            float f0Min = 50;
            float f0Max = 400;
            int tauMax = Convert.ToInt32(RecordingDevice.RATE / f0Min);
            int tauMin = Convert.ToInt32(RecordingDevice.RATE / f0Max);
            var df = differenceFunction(sig, tauMax);
            var cmdf = cumulativeMeanNormalizedDifferenceFunction(df, tauMax);
            pitch = getPitch(RecordingDevice.RATE, cmdf, tauMin, tauMax, 0.1f);
            Program.form.YinFrequency.Text = pitch.ToString();
            Program.form.Label5.Text = new Note(pitch).Sound;
            return pitch;

        }
        public static double[] differenceFunction(double [] sig, int tau_max)
        {
            var xPow = new double[sig.Length];
            for(int i=0;i<sig.Length;i++)
            {
                xPow[i] = sig[i] * sig[i];
            }
            double[] sigCumSum = Numpy.np.cumsum(xPow).GetData<double>();
            double[] sigReversed = Enumerable.Reverse(sig).ToArray();
            double[] conv = Numpy.np.convolve(sig, sigReversed).GetData<double>();
            double[] first = Enumerable.Reverse(sigCumSum).ToArray();
            double second = sigCumSum[sig.Length-1];
            double[] third = conv.Take(sig.Length).ToArray();
            double[] df = new double[sig.Length];
            for(int i=0;i< sig.Length;i++)
            {
                df[i] = first[i] + second - third[i] - (2 * conv[sig.Length-1 + i]);
            }
            return df.Take(tau_max + 1).ToArray();
        }
        public static double getPitch(int sr,double[] cmdf,int tau_min,int tau_max,double harmo_th)
        {
            harmo_th = 0.2;
            int tau = tau_min;
            while (tau < tau_max)
            {
                if (cmdf[tau] < harmo_th)
                {
                    while ((tau + 1 < tau_max) && (cmdf[tau + 1] < cmdf[tau]))
                    {
                        tau += 1;
                    }
                    return ((double)sr / (double)tau);
                }
                tau += 1;
            }
            return 0;
        }
        public static double[] cumulativeMeanNormalizedDifferenceFunction(double[] df, int N)
        {
            var cmndf = new double[N+1];
            var sum = Numpy.np.cumsum(df).GetData<double>();
            foreach (var i in Enumerable.Range(1, df.Length - 1))
            {
                cmndf[i] = (df[i] * (double)i / sum[i]);
            }
            cmndf[0] = 1;
            return cmndf;
        }
    }
}
