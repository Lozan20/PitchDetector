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
            double[] sig = Program.graphData.SignalWaveYin;
            float f0Min = 10;
            float f0Max = 650;
            int tauMax = Convert.ToInt32(Program.rD.RATE / f0Min);
            int tauMin = Convert.ToInt32(Program.rD.RATE / f0Max);
            var df = differenceFunctionScipy(sig, tauMax);
            var cmdf = cumulativeMeanNormalizedDifferenceFunction(df, tauMax);
            pitch = getPitch(Program.rD.RATE, cmdf, tauMin, tauMax, 0.1f);
            Program.form.YinFrequency.Text = pitch.ToString();
            return pitch;

        }
        public static double[] differenceFunctionScipy(double [] x, int tau_max)
        {
            var x2 = new double[x.Length];
            for(int i=0;i<x.Length;i++)
            {
                x2[i] = x[i] * x[i];
            }
            var x_cumsum = Numpy.np.cumsum(x2).GetData<double>();
            //var x_cumsum = Numpy.np.concatenate((Numpy.np.array<double>(0), Numpy.np.cumsum(x2))).GetData<double>();

            var x_reversed = Enumerable.Reverse(x).ToArray();
            var conv = Numpy.np.convolve(x,x_reversed).GetData<double>();
            var first = Enumerable.Reverse(x_cumsum).ToArray();
            var second = x_cumsum[x.Length-1];
            var third = conv.Take(x.Length).ToArray();
            var tmp = new double[x.Length];
            for(int i=0;i<x.Length;i++)
            {
                tmp[i] = first[i] + second - third[i] - (2 * conv[x.Length-1 + i]); //BUG HERE
            }
            return tmp.Take(tau_max + 1).ToArray();
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
            //cmndf[0] = 0;
            return cmndf;
        }
    }
}