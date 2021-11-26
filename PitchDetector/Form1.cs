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
        public Form1()
        {
            InitializeComponent();
            initializeComboBox();
            Program.graphData.initializeGraphs(formsPlot1, formsPlot2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReadDataFromSource.getConvertedData("DFT");

            formsPlot1.Render();
            FFT.performFFT();

            formsPlot2.Render();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Note note = new Note(TunerDFT.findHighestFrequency());
            TunerHPS.hannplot();
            closestNote.Text = note.Sound;
            closestPitch.Text = note.Pitch.ToString();
            label1.Text = note.CurrentMaxPitch.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.rD.changeDevice(comboBox1.SelectedIndex);
        }
        private void initializeComboBox()
        {
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                comboBox1.Items.Add(WaveIn.GetCapabilities(i).ProductName);
            }
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
