using System;
using System.Drawing;
using System.Windows.Forms;

namespace ActiveCounter
{
    public partial class Form1 : Form
    {
        private int hours;
        private int minutes;
        private int seconds;

        private bool isRunning;

        public Form1()
        {
            InitializeComponent();

            InitializeTimer();
            UpdateLabels();
            UpdateUI();
        }

        private void InitializeTimer()
        {
            timer1.Interval = 1000;
            timer1.Enabled = false;
        }

        private void StartTimer()
        {
            isRunning = true;
            timer1.Start();
            UpdateUI();
        }

        private void StopTimer()
        {
            isRunning = false;
            timer1.Stop();
            UpdateUI();
        }

        private void ResetTimer()
        {
            StopTimer();

            hours = 0;
            minutes = 0;
            seconds = 0;

            UpdateLabels();
        }

        private void UpdateUI()
        {
            btnStart.Enabled = !isRunning;
            btnStop.Enabled = isRunning;

            buttonReset.Enabled = !isRunning;
            textBox1.Enabled = !isRunning;
            groupBoxUpDown.Enabled = !isRunning;

            BackColor = isRunning ? Color.Green : Color.Red;
            Icon = isRunning
                ? Properties.Resources.green
                : Properties.Resources.red;
        }

        private void UpdateLabels()
        {
            lblHours.Text = $"{hours:D2}";
            lblMinutes.Text = $"{minutes:D2}";
            lblSeconds.Text = $"{seconds:D2}";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetTimer();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seconds++;

            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
            }

            if (minutes >= 60)
            {
                minutes = 0;
                hours++;
            }

            UpdateLabels();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
                return;

            if (isRunning)
                StopTimer();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !isRunning)
                StartTimer();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized &&
                checkBox1.Checked &&
                isRunning)
            {
                StopTimer();
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMin.Text, out int value))
            {
                minutes += value;
                NormalizeTime();
                UpdateLabels();
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMin.Text, out int value))
            {
                minutes -= value;

                if (minutes < 0)
                    minutes = 0;

                NormalizeTime();
                UpdateLabels();
            }
        }

        private void NormalizeTime()
        {
            while (minutes >= 60)
            {
                minutes -= 60;
                hours++;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}