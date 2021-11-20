
using System.Windows.Forms;

namespace PitchDetector
{
	partial class Form1
	{
		/// <summary>
		/// Wymagana zmienna projektanta.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Wyczyść wszystkie używane zasoby.
		/// </summary>
		/// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Kod generowany przez Projektanta formularzy systemu Windows

		/// <summary>
		/// Metoda wymagana do obsługi projektanta — nie należy modyfikować
		/// jej zawartości w edytorze kodu.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.formsPlot2 = new ScottPlot.FormsPlot();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.closestNote = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.closestPitch = new System.Windows.Forms.Label();
            this.waveformPainter1 = new NAudio.Gui.WaveformPainter();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.currentMaxPitch = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // formsPlot1
            // 
            this.formsPlot1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot1.Location = new System.Drawing.Point(12, 69);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(1307, 311);
            this.formsPlot1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 17;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // formsPlot2
            // 
            this.formsPlot2.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot2.Location = new System.Drawing.Point(12, 386);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(670, 311);
            this.formsPlot2.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(761, 499);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(431, 51);
            this.progressBar1.TabIndex = 5;
            // 
            // closestNote
            // 
            this.closestNote.AutoSize = true;
            this.closestNote.Location = new System.Drawing.Point(913, 400);
            this.closestNote.Name = "closestNote";
            this.closestNote.Size = new System.Drawing.Size(63, 13);
            this.closestNote.TabIndex = 6;
            this.closestNote.Text = "closestNote";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // closestPitch
            // 
            this.closestPitch.AutoSize = true;
            this.closestPitch.Location = new System.Drawing.Point(985, 400);
            this.closestPitch.Name = "closestPitch";
            this.closestPitch.Size = new System.Drawing.Size(64, 13);
            this.closestPitch.TabIndex = 7;
            this.closestPitch.Text = "closestPitch";
            // 
            // waveformPainter1
            // 
            this.waveformPainter1.Location = new System.Drawing.Point(1485, 562);
            this.waveformPainter1.Name = "waveformPainter1";
            this.waveformPainter1.Size = new System.Drawing.Size(75, 23);
            this.waveformPainter1.TabIndex = 10;
            this.waveformPainter1.Text = "waveformPainter1";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(55, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // currentMaxPitch
            // 
            this.currentMaxPitch.AutoSize = true;
            this.currentMaxPitch.Location = new System.Drawing.Point(896, 446);
            this.currentMaxPitch.Name = "currentMaxPitch";
            this.currentMaxPitch.Size = new System.Drawing.Size(84, 13);
            this.currentMaxPitch.TabIndex = 12;
            this.currentMaxPitch.Text = "currentMaxPitch";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(985, 446);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(896, 579);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "HPS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(985, 579);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "label3";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1357, 697);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentMaxPitch);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.waveformPainter1);
            this.Controls.Add(this.closestPitch);
            this.Controls.Add(this.closestNote);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.formsPlot2);
            this.Controls.Add(this.formsPlot1);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        public ScottPlot.FormsPlot formsPlot1;
        public System.Windows.Forms.Timer timer1;
        public ScottPlot.FormsPlot formsPlot2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label closestNote;
        public System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label closestPitch;
        private NAudio.Gui.WaveformPainter waveformPainter1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label currentMaxPitch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        public Label Label3 { get => label3; set => label3 = value; }
    }
}

