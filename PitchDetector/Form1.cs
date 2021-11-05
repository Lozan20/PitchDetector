using System;
using System.Windows.Forms;
using NAudio.Dsp;
using NAudio.Wave; // installed with nuget
using System.Numerics;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace PitchDetector
{
    public partial class Form1 : Form
    {
        bool graphFFT = false;
        public WaveIn wi;
        public BufferedWaveProvider bwp;
        public Int32 envelopeMax;
        double[] wave = new double[2048];
        double[] Ys = new double[4096];
        double[] fft = new double[1024];
        double[] dft = new double[1024];
        double[] fft_X = new double[1024];
        double pitch_reference = 440;
        List<String> sounds = new List<String> { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

        private int RATE = 48000; // sample rate of the sound card
        private int BUFFERSIZE = 16384; // must be a multiple of 2 //16k
        public Form1()
        {
            
            InitializeComponent();
            // get the WaveIn class started

            WaveIn wi = new WaveIn();
            
            wi.DeviceNumber = 0;
            wi.WaveFormat = new NAudio.Wave.WaveFormat(RATE, 2);
            
            //wi.BufferMilliseconds = (int)((double)BUFFERSIZE / (double)RATE * 1000.0);
            formsPlot1.Plot.PlotSignal(wave);
            //formsPlot2.Plot.PlotSignal(fft);
            double fftMaxfreq = RATE / 2;
            int graphPointCount = BUFFERSIZE / 2;
            double spacing = fftMaxfreq / graphPointCount;
            formsPlot2.Plot.AddSignalXY(fft_X,fft);

            formsPlot1.Plot.SetAxisLimitsX(0, 2048);
            formsPlot1.Plot.SetAxisLimitsY(-20000, 20000);
            formsPlot2.Plot.SetAxisLimitsX(0,1000);
            formsPlot2.Plot.SetAxisLimitsY(0, 200);
            formsPlot2.Plot.XLabel("Częstotliwość");

            // create a wave buffer and start the recording
            wi.DataAvailable += new EventHandler<WaveInEventArgs>(wi_DataAvailable);
            bwp = new BufferedWaveProvider(wi.WaveFormat);
            bwp.BufferLength = BUFFERSIZE * 2;
            bwp.DiscardOnBufferOverflow = true;
            try
            {
                wi.StartRecording();
            }
            catch (NAudio.MmException e)
            {
                MessageBox.Show(e.ToString(),"No avaliable recording device",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateAudioGraph();
        }
        void wi_DataAvailable(object sender, WaveInEventArgs e)
        {
            bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }
        public void UpdateAudioGraph()
        {
            // read the bytes from the stream
            int frameSize = BUFFERSIZE;
            var frames = new byte[frameSize];
            bwp.Read(frames, 0, frameSize);
            if (frames.Length == 0) return;
            if (frames[frameSize - 2] == 0) return;

            

            // convert it to int32 manually (and a double for scottplot)
            //int SAMPLE_RESOLUTION = 32;
            int BYTES_PER_POINT = 4;
            int graphPointCount = frames.Length / BYTES_PER_POINT;
            Int32[] vals = new Int32[frames.Length / BYTES_PER_POINT];
            double[] Ys2 = new double[frames.Length / BYTES_PER_POINT];

            double[] pcm = new double[graphPointCount];
            double[] fft = new double[graphPointCount];
            double[] fftReal = new double[graphPointCount / 2];

            timer1.Enabled = false;
            for (int i = 0; i < vals.Length; i++)
            {
                // bit shift the byte buffer into the right variable format
                byte hByte = frames[i * 2 + 1];
                byte lByte = frames[i * 2 + 0];
                vals[i] = (short)((hByte << 8) | lByte);
                Ys[i] = vals[i];
                
            }
            int j = 1;
            for(int i=0;i<Ys.Length/2;i++)
            {
                wave[i] = Ys[j];
                j += 2;
            }
            timer1.Enabled = true;
            //graphFFT = true;
            formsPlot1.Render();
            
            //formsPlot2.Render();

            //formsPlot2.Plot.PlotScatter(fft_plot);





            //Application.DoEvents();
        }
        public void FFT()
        {
             // this is where we will store the output (fft)
            System.Numerics.Complex[] fftComplex = new System.Numerics.Complex[Ys.Length]; // the FFT function requires complex format
            timer2.Enabled = false;
            for (int i = 0; i < 1024; i++)
            {
                fftComplex[i] = new System.Numerics.Complex(Ys[i], 0); // make it complex format (imaginary = 0)
            }
            Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);
            for (int i = 0; i < 1024; i++)
            {
                fft[i] = fftComplex[i].Magnitude * 20;
                fft_X[i] = (double)i / Ys.Length * RATE; // back to double
                                  //fft[i] = Math.Log10(fft[i]); // convert to dB
            }

            timer2.Enabled = true;
            //todo: this could be much faster by reusing variables
        }

        private void timer2_Tick(object sender, EventArgs e)
        { 
            FFT();
            formsPlot2.Render();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "1") wi.DeviceNumber = 1;
            if (textBox1.Text == "0") wi.DeviceNumber = 0;
        }
        private KeyValuePair<String,double> determineNote(double maxPitch)
        {
            var temp = 0;
            if(maxPitch > 0)
            {
                int i = (int)Math.Round(Math.Log(maxPitch / pitch_reference, 2) * 12);
                if(i<0)
                {
                    temp = 12 + i % 12;
                }
                else
                {
                    temp = i % 12;
                }
                int oktawa = (int)(3 + Math.Floor((temp + 9.0)/12.0));
                var closestNote = sounds[temp] + oktawa.ToString();

                double closestPitch = pitch_reference * Math.Pow(2, (double)i / 12);
                return new KeyValuePair<string, double>(closestNote, closestPitch);
            }
            return new KeyValuePair<string, double>("NULL", 0);


        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            var max = fft.Max() ;
            var maxInd = Array.IndexOf(fft, max);
            var temp = determineNote(fft_X[maxInd]);
            KeyValuePair<String, double> finale = new KeyValuePair<String, double>(temp.Key, temp.Value);
            label1.Text = finale.Key;
            label2.Text = finale.Value.ToString();
        }
    }
}
