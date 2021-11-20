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

        private WaveInEvent wi { get; set; }
        public  BufferedWaveProvider bwp { get; set; }
        public int RATE { get; set; }
        public int BUFFERSIZE { get; set; }
        public RecordingDevice(int index)
        {
            RATE = 48000;
            BUFFERSIZE = 16384;
            assignDevice(index);
            assignBuffer();
            try
            {
                wi.StartRecording();
            }
            catch (NAudio.MmException e)
            {
                MessageBox.Show(e.ToString(), "No avaliable recording device", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void wi_DataAvailable(object sender, WaveInEventArgs e)
        {
            bwp.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }
        public void changeDevice(int index)
        {
            wi.StopRecording();
            assignDevice(index);
            assignBuffer();
            wi.StartRecording();
        }
        void assignDevice(int index)
        {
            wi = new WaveInEvent();
            wi.BufferMilliseconds = 100;
            wi.DeviceNumber = index;
            wi.WaveFormat = new NAudio.Wave.WaveFormat(RATE, 2);
            wi.DataAvailable += new EventHandler<WaveInEventArgs>(wi_DataAvailable);
        }
        void assignBuffer()
        {
            bwp = new BufferedWaveProvider(wi.WaveFormat);
            bwp.BufferLength = BUFFERSIZE;
            bwp.DiscardOnBufferOverflow = true;
        }


    }
}
