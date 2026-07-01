using System;
using System.Drawing;
using System.Windows.Forms;

namespace ActiveCounter
{
    public partial class Form1 : Form
    {
        private int hours, minutes, seconds;
        private bool isRunning;

        private readonly System.Windows.Forms.Timer blinkTimer = new();
        private bool blinkState;

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            timer1.Stop();

            blinkTimer.Interval = 1000;
            blinkTimer.Tick += BlinkTimer_Tick;

            UpdateLabels();
            UpdateUI();
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

            hours = minutes = seconds = 0;
            UpdateLabels();
        }

        private void UpdateUI()
        {
            btnStart.Enabled = !isRunning;
            btnStop.Enabled = isRunning;

            buttonReset.Enabled = !isRunning;
            textBox1.Enabled = !isRunning;
            groupBoxUpDown.Enabled = !isRunning;

            if (isRunning)
            {
                blinkTimer.Stop();
                BackColor = Color.Green;
                Icon = Properties.Resources.green;
            }
            else
            {
                blinkState = false;
                BackColor = Color.Red;
                Icon = Properties.Resources.red;
                blinkTimer.Start();
            }
        }

        private void BlinkTimer_Tick(object? sender, EventArgs e)
        {
            if (isRunning) return;

            blinkState = !blinkState;
            Icon = blinkState ? Properties.Resources.red : Properties.Resources.red2;
        }

        private void UpdateLabels()
        {
            lblHours.Text = $"{hours:D2}";
            lblMinutes.Text = $"{minutes:D2}";
            lblSeconds.Text = $"{seconds:D2}";
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

        private void btnStart_Click(object sender, EventArgs e) => StartTimer();

        private void btnStop_Click(object sender, EventArgs e) => StopTimer();

        private void buttonRest_Click(object sender, EventArgs e) => ResetTimer();

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (checkBox1.Checked && isRunning)
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
                while (minutes >= 60)
                {
                    minutes -= 60;
                    hours++;
                }
                UpdateLabels();
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxMin.Text, out int value))
            {
                int total = hours * 60 + minutes - value;
                if (total < 0) total = 0;

                hours = total / 60;
                minutes = total % 60;

                UpdateLabels();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
