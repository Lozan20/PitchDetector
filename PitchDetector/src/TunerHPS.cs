using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet;
using NumSharp;
using Numpy;


namespace PitchDetector
{
    class TunerHPS
    {

        static int frameSize = RecordingDevice.Buffersize;
        static int wspolczynnikDecymacji = 5;
        static byte[] frames = new byte[frameSize];
        static double[] octaveBands = { 16,32,64,128,256,512,1024};
        static private double[] spectrumHPS;
        public double[] SpectrumHPS { get => spectrumHPS; set => spectrumHPS = value; }
        static double whiteNoise = 0.1;
        static double[] hpsSpec = new double[10240];

        public TunerHPS()
        {

        }
        public static void hannplot()
        {
            
            Array temp;
            double delta = RecordingDevice.Buffersize / (frameSize/2);
            spectrumHPS = Program.calculateAndPlotData.FftY.Take(Program.calculateAndPlotData.FftY.Count()).ToArray();
            var signalPow = Math.Pow((double)Numpy.np.linalg.norm(Program.calculateAndPlotData.SignalWave, 2), 2) / Program.calculateAndPlotData.SignalWave.Length;
            if (signalPow < 1000000)
            {
                Program.form.Label3.Text = "low signal power";
                return;
            }
            var ms = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Program.form.timer1.Enabled = false;
            for (int i=0; i<octaveBands.Length - 1;i++)
            {
                var start = (int)(octaveBands[i] / delta);
                var end = (int)(octaveBands[i+1] / delta);
                if(Program.calculateAndPlotData.FftY.Count() <= end)
                {
                    end = Program.calculateAndPlotData.FftY.Count();
                }
                temp = spectrumHPS.Skip(start).Take(end-start).ToArray();
                var averageSignalPower = Math.Pow(Math.Pow((double)Numpy.np.linalg.norm(temp,2),2)/(end-start),0.5);
                for (int l = start; l<end;l++)
                {
                    if(spectrumHPS[l] < averageSignalPower * whiteNoise)
                    {
                        spectrumHPS[l] = 0.0;
                    }
                }
            }

            
            var temp4 = (Numpy.np.arange(0, spectrumHPS.Length, 1).GetData<int>());
            var interp = MathNet.Numerics.Interpolate.Linear(temp4.Select(Convert.ToDouble).ToArray(), spectrumHPS);
            var normalizedHPSValue = (double)Numpy.np.linalg.norm(spectrumHPS.ToArray(), 2);
            List<double> magnitudeSpec = new List<double>();
            for (double x = 0; x < spectrumHPS.Length; x=x+1.0/5.0)
            {
                magnitudeSpec.Add(interp.Interpolate(x)/normalizedHPSValue);
            }
            var magnitudeSpecSecondStep = new List<double>();
            IEnumerable<double> magnitudeSpecFirstStep;

            var freq = hps(magnitudeSpec);

            var time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - ms;

            Program.form.Label3.Text = freq.ToString();

            Program.form.Label6.Text = new Note(freq).Sound;
            Program.form.timer1.Enabled = true;
        }
        public static double hps(List<double> magnitudeSpec)
        {
            IEnumerable<double> magnitudeSpecFirstStep;
            List<double> magnitudeSpecSecondStep = new List<double>();
            for (int i = 0; i < wspolczynnikDecymacji; i++)
            {
                magnitudeSpecFirstStep = magnitudeSpec.Take((int)Math.Ceiling((double)(magnitudeSpec.Count / (i + 1.0))));
                for (int l = 0; l <= magnitudeSpec.Count - 1; l += (1 + i))
                {
                    if (l > magnitudeSpec.Count)
                    {
                        break;
                    }
                    magnitudeSpecSecondStep.Add(magnitudeSpec.ElementAt(l));
                }

                hpsSpec = Numpy.np.multiply(magnitudeSpecFirstStep.ToArray(), magnitudeSpecSecondStep.ToArray()).GetData<double>().ToArray();
                if (hpsSpec == null)
                {
                    break;
                }
                magnitudeSpecSecondStep.Clear();
            }
            double maxValue = hpsSpec.Max();
            double freq = Array.IndexOf(hpsSpec, maxValue) * 
            (RecordingDevice.Rate / (double)Program.calculateAndPlotData.SignalWave.Length / wspolczynnikDecymacji);
            return freq;

        }
    }
}
