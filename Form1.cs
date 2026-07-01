using System;
using System.Drawing;
using System.Windows.Forms;

namespace ActiveCounter
{
    public partial class Form1 : Form
    {
        private int hours, minutes, seconds;
        private bool isRunning;
        private bool isMinimized = false; // متغیر جدید برای tracking minimize

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
            isMinimized = false;
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
                Icon = Properties.Resources.green; // اگر آیکون داری
            }
            else
            {
                blinkState = false;
                BackColor = Color.Red;
                Icon = Properties.Resources.red; // اگر آیکون داری
                blinkTimer.Start();
            }
        }

        private void BlinkTimer_Tick(object? sender, EventArgs e)
        {
            if (isRunning) return;

            blinkState = !blinkState;
            BackColor = blinkState ? Color.Red : Color.OrangeRed;
            Icon = blinkState ? Properties.Resources.red : Properties.Resources.red2; // اگر آیکون داری
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

        private void buttonReset_Click(object sender, EventArgs e) => ResetTimer();

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (checkBoxFocus.Checked && isRunning)
                StopTimer();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            // فقط در صورتی که minimize نبوده باشه و فوکوس فعال باشه
            if (checkBoxFocus.Checked && !isRunning && !isMinimized)
                StartTimer();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (!checkBoxFocus.Checked) return;

            if (WindowState == FormWindowState.Minimized)
            {
                if (isRunning)
                {
                    isMinimized = true;
                    StopTimer();
                }
            }
            else if (WindowState == FormWindowState.Normal && isMinimized)
            {
                isMinimized = false;
                if (!isRunning)
                {
                    StartTimer();
                }
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

        private void checkBoxFocus_CheckedChanged(object sender, EventArgs e)
        {
            // وقتی چک‌باکس غیرفعال میشه، وضعیت minimize رو ریست کن
            if (!checkBoxFocus.Checked)
                isMinimized = false;
        }

        // جلوگیری از وارد کردن کاراکترهای غیرعددی در TextBoxMin
        private void textBoxMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }
    }
}