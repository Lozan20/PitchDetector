﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Numpy;

namespace PitchDetector
{

	public static class Program
	{
		public static RecordingDevice rD;
		public static Form1 form;
		public static Graph graphData;
		[STAThread]
		static void Main()
		{
			graphData = new Graph();
			rD = new RecordingDevice(0); // select first device by default 
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			form = new Form1();
			
			Application.Run(form);
		}
	}
}
