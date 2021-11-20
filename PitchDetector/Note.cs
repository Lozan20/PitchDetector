using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitchDetector
{
    class Note
    {
        static double pitchReference = 440;
        static List<string> sounds = new List<String> { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
        private string closestSound;
        private double closestPitch;
        private double currentMaxPitch;

        public string Sound { get => closestSound; set => closestSound = value; }
        public double Pitch { get => closestPitch; set => closestPitch = value; }
        public double CurrentMaxPitch { get => currentMaxPitch; set => currentMaxPitch = value; }

        public Note(double maxPitch)
        {
            determineNote(maxPitch);
            currentMaxPitch = maxPitch;
        }
        public void determineNote(double maxPitch)
        {
            var temp = 0;
            if (maxPitch > 0)
            {
                int i = (int)Math.Round(Math.Log(maxPitch / pitchReference, 2) * 12);
                if (i < 0)
                {
                    temp = 12 + i % 12;
                }
                else
                {
                    temp = i % 12;
                }
                if (temp == 12) temp = 0;
                int oktawa = (int)(4 + Math.Floor((temp + 9.0) / 12.0));
                this.closestSound = sounds[temp] + oktawa.ToString();
                this.closestPitch = pitchReference * Math.Pow(2, (double)i / 12);

            }
        }

    }

}
