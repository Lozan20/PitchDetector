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
            DateTime start = DateTime.Now;
            var max = Program.calculateAndPlotData.FftY.Max();
            var maxInd = Array.IndexOf(Program.calculateAndPlotData.FftY, max);
            TimeSpan timeItTook = DateTime.Now - start;
            var a = timeItTook.Milliseconds;
            return Program.calculateAndPlotData.FftX[maxInd];
            
        }
    }
}
