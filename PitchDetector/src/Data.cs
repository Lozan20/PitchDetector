using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector.src
{
    public class Data
    {
        private static double[] signalWave;
        private static double[] signalWaveYin;
        private static double[] fftY;
        private static double[] fftX;
        public double[] SignalWave { get => signalWave; set => signalWave = value; }
        public double[] FftY { get => fftY; set => fftY = value; }
        public double[] FftX { get => fftX; set => fftX = value; }
        public double[] SignalWaveYin { get => signalWaveYin; set => signalWaveYin = value; }
        public Data()
        {
            signalWave = new double[RecordingDevice.Buffersize / 2];
            signalWaveYin = new double[RecordingDevice.Buffersize / 4];
            fftY = new double[RecordingDevice.Buffersize / 4];
            fftX = new double[RecordingDevice.Buffersize / 4];
        }
    }
}
