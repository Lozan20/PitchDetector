using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    class TunerDFT
    {
        public static double findHighestFrequency()
        {
            var max = Program.graphData.fftY.Max();
            var maxInd = Array.IndexOf(Program.graphData.fftY, max);
            return Program.graphData.fftX[maxInd];
        }
    }
}
