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
        static int frameSize = RecordingDevice.BUFFERSIZE;
        static double[] hann = MathNet.Numerics.Window.Hamming(RecordingDevice.BUFFERSIZE);
        static byte[] frames = new byte[frameSize];
        static double[] octaveBands = { 16,32,64,128,256,512};
        public double[] Hann { get => hann; set => hann = value; }
        static private double[] spectrumHPS;
        public double[] SpectrumHPS { get => spectrumHPS; set => spectrumHPS = value; }
        static double whiteNoise = 0.2;


        public TunerHPS()
        {

               
        }
        public static void hannplot()
        {
            Array temp;
            double delta = RecordingDevice.BUFFERSIZE / frameSize;
            //Console.WriteLine("3.Kopiuje dane z bufora");
            spectrumHPS = Program.graphData.FftY.Take(Program.graphData.FftY.Count()).ToArray();
            //Console.WriteLine("4.Skonczylem kopiowac dane z bufora");
            //var normalizedHPSValue = (double)Numpy.np.linalg.norm(spectrumHPS.ToArray(), 2);
            var signalPow = Math.Pow((double)Numpy.np.linalg.norm(Program.graphData.SignalWave, 2), 2) / Program.graphData.SignalWave.Length;
            if (signalPow < 1000000)
            {
                Program.form.Label3.Text = "low signal power";
                return;
            }
            Program.form.timer1.Enabled = false;
            for (int i=0; i<octaveBands.Length - 1;i++)
            {
                var start = (int)(octaveBands[i] / delta);
                var end = (int)(octaveBands[i+1] / delta);
                //if(Program.graphData.FftY.Count() <= end)
                //{
                //    end = Program.graphData.FftY.Count();
                //}
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


            var tmp_hps_spec = new double[10240];
            var temp4 = (Numpy.np.arange(0, spectrumHPS.Length, 1).GetData<int>());
            var interp = MathNet.Numerics.Interpolate.Linear(temp4.Select(Convert.ToDouble).ToArray(), spectrumHPS);
            var normalizedHPSValue = (double)Numpy.np.linalg.norm(spectrumHPS.ToArray(), 2);
            List<double> interpolated = new List<double>();
            for (double x = 0; x < spectrumHPS.Length; x=x+1.0/5.0)
            {
                interpolated.Add(interp.Interpolate(x)/normalizedHPSValue);
            }
            var second = new List<double>();
            for(int i=0;i<5;i++)
            {
                var first = interpolated.Take((int)Math.Ceiling((double)(interpolated.Count / (i + 1.0))));
                for (int l=0;l<=interpolated.Count-1;l+=(1+i))
                {
                    if (l > interpolated.Count) break;
                    second.Add(interpolated.ElementAt(l));
                }

                tmp_hps_spec = Numpy.np.multiply(first.ToArray(), second.ToArray()).GetData<double>().ToArray();
                if (tmp_hps_spec == null)
                {
                    break;
                }
                second.Clear();
            }
            var max = tmp_hps_spec.Max();
            var maxInd = Array.IndexOf(tmp_hps_spec, max) * (44100.0/8192.0 / 5.0);
            Program.form.Label3.Text = maxInd.ToString();
            Program.form.timer1.Enabled = true;
        }
    }
}
