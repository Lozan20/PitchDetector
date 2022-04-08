using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    public static class ReadDataFromSource
    {
        static int frameSize = RecordingDevice.Buffersize;
        static double[] hamm = MathNet.Numerics.Window.Hamming(RecordingDevice.Buffersize);
        static byte [] frames = new byte[frameSize];
        static int BYTES_PER_POINT = 2;
        static double[] shiftedFrames = new double[frameSize/BYTES_PER_POINT];

        public static void getConvertedData()
        {
            Program.rD.bufferedWaveProvider.Read(frames, 0, frameSize);
            if (frames.Length == 0) return;
            if (frames[frameSize - 2] == 0) return;

            Program.form.timer1.Enabled = false;
            int j = 0;
            int k = 1;
            hamm = MathNet.Numerics.Window.Hamming(shiftedFrames.Length);
            for (int i = 0; i < frames.Length / BYTES_PER_POINT; i++)
            {
                shiftedFrames[i] = BitConverter.ToInt16(frames, i * 2);
                Program.calculateAndPlotData.SignalWave[i] = shiftedFrames[i] * hamm[i];
                if (i % 2 == 0)
                {
                    Program.calculateAndPlotData.SignalWaveYin[j] = Program.calculateAndPlotData.SignalWave[k];
                    j++;
                    k += 2;
                }
            }
            Program.form.timer1.Enabled = true;

        }

    }
}
