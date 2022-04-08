using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
namespace PitchDetector
{
    public class RecordingDevice
    {

        private WaveInEvent waveInEvent { get; set; }
        public  BufferedWaveProvider bufferedWaveProvider { get; set; }
        public static int rate = 44100;
        public static int Rate { get=>rate; set=>rate=value; }
        private static int  buffersize = 16384;
        public static int Buffersize { get=>buffersize; set => buffersize = value; }
        public RecordingDevice(int index)
        {

            //BUFFERSIZE = 16384;
            assignDevice(index);
            assignBuffer();
            try
            {
                waveInEvent.StartRecording();
            }
            catch (NAudio.MmException e)
            {
                MessageBox.Show(e.ToString(), "No avaliable recording device", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void wi_DataAvailable(object sender, WaveInEventArgs e)
        {
            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }
        public void changeDevice(int index)
        {
            waveInEvent.StopRecording();
            assignDevice(index);
            assignBuffer();
            waveInEvent.StartRecording();
        }
        void assignDevice(int index)
        {
            waveInEvent = new WaveInEvent();
            waveInEvent.BufferMilliseconds = 100;
            waveInEvent.DeviceNumber = index;
            waveInEvent.WaveFormat = new NAudio.Wave.WaveFormat(Rate, 2);
            waveInEvent.DataAvailable += new EventHandler<WaveInEventArgs>(wi_DataAvailable);
        }
        void assignBuffer()
        {
            bufferedWaveProvider = new BufferedWaveProvider(waveInEvent.WaveFormat);
            bufferedWaveProvider.BufferLength = Buffersize;
            bufferedWaveProvider.DiscardOnBufferOverflow = true;
        }


    }
}
