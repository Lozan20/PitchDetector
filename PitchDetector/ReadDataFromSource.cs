using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    public static class ReadDataFromSource
    {
        static int frameSize = RecordingDevice.BUFFERSIZE;
        static double[] hann = MathNet.Numerics.Window.Hamming(RecordingDevice.BUFFERSIZE);
        static byte [] frames = new byte[frameSize];
        static int BYTES_PER_POINT = 2;
        static double[] shiftedFrames = new double[frameSize/BYTES_PER_POINT];

        public static void getConvertedData(string type)
        {
            Program.rD.bwp.Read(frames, 0, frameSize);
            if (frames.Length == 0) return;
            if (frames[frameSize - 2] == 0) return;
            if(type == "HPS")
            {
                return;
            }
            Program.form.timer1.Enabled = false;
            //for (int i = 0; i < Program.rD.BUFFERSIZE; i++)
            //{
            //    frames[i] = frames[i] * (byte)hann[i];
            //}
            int j = 0;
            int k = 1;

            //for (int i = 0; i < frames.Length / BYTES_PER_POINT; i++)
            //{
            //    // bit shift the byte buffer into the right variable format
            //    Program.graphData.signalWave[i] = BitConverter.ToInt16(frames, i * 2) * hann[i];

            //}

            for (int i = 0; i < frames.Length / BYTES_PER_POINT; i++)
            {
                // bit shift the byte buffer into the right variable format
                Program.graphData.SignalWave[i] = BitConverter.ToInt16(frames, i * 2) * hann[i]; //poprawic hanninga bo sie nakladaja co 2*n wartosci

                if (i % 2 == 0)
                {
                    Program.graphData.SignalWaveYin[j] = Program.graphData.SignalWave[k];
                    j++;
                    k += 2;
                }
            }
            Program.form.timer1.Enabled = true;

        }

    }
}
