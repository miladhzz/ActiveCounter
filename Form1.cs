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

        // فیلدهای جدید برای ثبت زمانها
        private List<TimerRecord> timerRecords = new List<TimerRecord>();
        private DateTime? startTime;
        private DateTime? stopTime;
        private DateTime? lastStopTime;

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            timer1.Stop();

            blinkTimer.Interval = 1000;
            blinkTimer.Tick += BlinkTimer_Tick;

            UpdateLabels();
            UpdateUI();
            InitializeDataGridView(); // این متد باید فراخوانی شود
            dataGridViewRecords.CellFormatting += DataGridViewRecords_CellFormatting; // این رویداد باید متصل شود
        }
        private void DataGridViewRecords_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= timerRecords.Count)
                return;

            // فرمت نمایش زمان شروع
            if (dataGridViewRecords.Columns[e.ColumnIndex].DataPropertyName == "StartTime")
            {
                if (e.Value is DateTime startTime && startTime != DateTime.MinValue)
                {
                    e.Value = startTime.ToString("yyyy/MM/dd HH:mm:ss");
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = string.Empty; // اگر زمان شروع خالی است
                    e.FormattingApplied = true;
                }
            }

            // فرمت نمایش زمان توقف
            if (dataGridViewRecords.Columns[e.ColumnIndex].DataPropertyName == "StopTime")
            {
                if (e.Value is DateTime stopTime && stopTime != DateTime.MinValue)
                {
                    e.Value = stopTime.ToString("yyyy/MM/dd HH:mm:ss");
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = string.Empty; // اگر زمان توقف خالی است
                    e.FormattingApplied = true;
                }
            }
        }
        private void InitializeDataGridView()
        {
            dataGridViewRecords.AutoGenerateColumns = false;
            dataGridViewRecords.Columns.Clear();

            // ستون زمان شروع
            DataGridViewTextBoxColumn startTimeColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StartTime",
                HeaderText = "زمان شروع",
                Width = 150
            };
            dataGridViewRecords.Columns.Add(startTimeColumn);

            // ستون زمان توقف
            DataGridViewTextBoxColumn stopTimeColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StopTime",
                HeaderText = "زمان توقف",
                Width = 150
            };
            dataGridViewRecords.Columns.Add(stopTimeColumn);

            // ستون مدت زمان کار
            DataGridViewTextBoxColumn workDurationColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "WorkDurationString",
                HeaderText = "مدت زمان کار",
                Width = 150
            };
            dataGridViewRecords.Columns.Add(workDurationColumn);

            // ستون مدت زمان توقف
            DataGridViewTextBoxColumn stopDurationColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StopDurationString",
                HeaderText = "مدت زمان توقف",
                Width = 150
            };
            dataGridViewRecords.Columns.Add(stopDurationColumn);

            dataGridViewRecords.DataSource = timerRecords;
        }
        private void StartTimer()
        {
            isRunning = true;
            startTime = DateTime.Now;

            // محاسبه مدت زمان توقف برای آخرین رکورد توقف شده
            if (timerRecords.Count > 0)
            {
                TimerRecord lastRecord = timerRecords[timerRecords.Count - 1];
                if (lastRecord.StopTime != DateTime.MinValue)
                {
                    lastRecord.StopDuration = startTime.Value - lastRecord.StopTime;
                }
            }

            // ایجاد رکورد جدید با زمان شروع
            TimerRecord record = new TimerRecord
            {
                StartTime = startTime.Value,
                StopTime = DateTime.MinValue, // زمان توقف خالی میماند
                WorkDuration = TimeSpan.Zero,
                StopDuration = TimeSpan.Zero
            };

            timerRecords.Add(record);
            dataGridViewRecords.DataSource = null;
            dataGridViewRecords.DataSource = timerRecords;

            timer1.Start();
            UpdateUI();
        }

        private void StopTimer()
        {
            isRunning = false;
            stopTime = DateTime.Now;
            lastStopTime = stopTime;

            // محاسبه مدت زمان کار
            if (startTime.HasValue && stopTime.HasValue && timerRecords.Count > 0)
            {
                TimeSpan workDuration = stopTime.Value - startTime.Value;

                // بهروزرسانی آخرین رکورد با زمان توقف و مدت زمان کار
                TimerRecord lastRecord = timerRecords[timerRecords.Count - 1];
                lastRecord.StopTime = stopTime.Value;
                lastRecord.WorkDuration = workDuration;

                // محاسبه مدت زمان توقف برای رکورد قبلی (اگر وجود دارد)
                if (timerRecords.Count > 1)
                {
                    TimerRecord previousRecord = timerRecords[timerRecords.Count - 2];
                    if (previousRecord.StopTime != DateTime.MinValue && lastRecord.StartTime != DateTime.MinValue)
                    {
                        previousRecord.StopDuration = lastRecord.StartTime - previousRecord.StopTime;
                    }
                }

                dataGridViewRecords.DataSource = null;
                dataGridViewRecords.DataSource = timerRecords;
            }

            timer1.Stop();
            UpdateUI();
        }
        private void ResetTimer()
        {
            // ۱. تایمر را متوقف کن
            StopTimer();

            // ۲. مقادیر ساعت، دقیقه و ثانیه را ریست کن
            hours = minutes = seconds = 0;

            // ۳. لیست رکوردها را خالی کن
            timerRecords.Clear();

            // ۴. فیلدهای زمان را ریست کن
            startTime = null;
            stopTime = null;
            lastStopTime = null;

            // ۵. DataGridView را بهروزرسانی کن
            dataGridViewRecords.DataSource = null;
            dataGridViewRecords.DataSource = timerRecords;

            // ۶. واسط کاربری را بهروزرسانی کن
            UpdateLabels();
            UpdateUI();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}