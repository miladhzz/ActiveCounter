namespace ActiveCounter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            btnStart = new Button();
            btnStop = new Button();
            lblHours = new Label();
            lblMinutes = new Label();
            lblSeconds = new Label();
            buttonReset = new Button();
            textBox1 = new TextBox();
            checkBoxFocus = new CheckBox();
            buttonUp = new Button();
            buttonDown = new Button();
            textBoxMin = new TextBox();
            groupBoxUpDown = new GroupBox();
            groupBoxUpDown.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(88, 138);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(112, 34);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(340, 138);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(112, 34);
            btnStop.TabIndex = 1;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // lblHours
            // 
            lblHours.AutoSize = true;
            lblHours.Font = new Font("Segoe UI", 12F);
            lblHours.Location = new Point(144, 63);
            lblHours.Name = "lblHours";
            lblHours.Size = new Size(78, 32);
            lblHours.TabIndex = 2;
            lblHours.Text = "label1";
            // 
            // lblMinutes
            // 
            lblMinutes.AutoSize = true;
            lblMinutes.Font = new Font("Segoe UI", 12F);
            lblMinutes.Location = new Point(232, 63);
            lblMinutes.Name = "lblMinutes";
            lblMinutes.Size = new Size(78, 32);
            lblMinutes.TabIndex = 3;
            lblMinutes.Text = "label1";
            // 
            // lblSeconds
            // 
            lblSeconds.AutoSize = true;
            lblSeconds.Font = new Font("Segoe UI", 12F);
            lblSeconds.Location = new Point(320, 63);
            lblSeconds.Name = "lblSeconds";
            lblSeconds.Size = new Size(78, 32);
            lblSeconds.TabIndex = 4;
            lblSeconds.Text = "label1";
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(207, 202);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(112, 34);
            buttonReset.TabIndex = 5;
            buttonReset.Text = "reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 15F);
            textBox1.Location = new Point(99, 254);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(364, 47);
            textBox1.TabIndex = 6;
            // 
            // checkBoxFocus
            // 
            checkBoxFocus.AutoSize = true;
            checkBoxFocus.Location = new Point(198, 12);
            checkBoxFocus.Name = "checkBoxFocus";
            checkBoxFocus.Size = new Size(171, 29);
            checkBoxFocus.TabIndex = 7;
            checkBoxFocus.Text = "فعالسازی فوکوس";
            checkBoxFocus.UseVisualStyleBackColor = true;
            // 
            // buttonUp
            // 
            buttonUp.Font = new Font("Segoe UI", 12F);
            buttonUp.Location = new Point(47, 23);
            buttonUp.Name = "buttonUp";
            buttonUp.Size = new Size(74, 56);
            buttonUp.TabIndex = 8;
            buttonUp.Text = "+";
            buttonUp.UseVisualStyleBackColor = true;
            buttonUp.Click += buttonUp_Click;
            // 
            // buttonDown
            // 
            buttonDown.Font = new Font("Segoe UI", 12F);
            buttonDown.Location = new Point(47, 152);
            buttonDown.Name = "buttonDown";
            buttonDown.Size = new Size(74, 56);
            buttonDown.TabIndex = 9;
            buttonDown.Text = "-";
            buttonDown.UseVisualStyleBackColor = true;
            buttonDown.Click += buttonDown_Click;
            // 
            // textBoxMin
            // 
            textBoxMin.Font = new Font("Segoe UI", 15F);
            textBoxMin.Location = new Point(32, 88);
            textBoxMin.Name = "textBoxMin";
            textBoxMin.Size = new Size(109, 47);
            textBoxMin.TabIndex = 10;
            textBoxMin.Text = "10";
            // 
            // groupBoxUpDown
            // 
            groupBoxUpDown.Controls.Add(textBoxMin);
            groupBoxUpDown.Controls.Add(buttonUp);
            groupBoxUpDown.Controls.Add(buttonDown);
            groupBoxUpDown.Location = new Point(694, 49);
            groupBoxUpDown.Name = "groupBoxUpDown";
            groupBoxUpDown.Size = new Size(171, 229);
            groupBoxUpDown.TabIndex = 11;
            groupBoxUpDown.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(877, 325);
            Controls.Add(groupBoxUpDown);
            Controls.Add(checkBoxFocus);
            Controls.Add(textBox1);
            Controls.Add(buttonReset);
            Controls.Add(lblSeconds);
            Controls.Add(lblMinutes);
            Controls.Add(lblHours);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Form1";
            Activated += MainForm_Activated;
            Deactivate += MainForm_Deactivate;
            Resize += MainForm_Resize;
            groupBoxUpDown.ResumeLayout(false);
            groupBoxUpDown.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Button btnStart;
        private Button btnStop;
        private Label lblHours;
        private Label lblMinutes;
        private Label lblSeconds;
        private Button buttonReset;
        private TextBox textBox1;
        private CheckBox checkBoxFocus;
        private Button buttonUp;
        private Button buttonDown;
        private TextBox textBoxMin;
        private GroupBox groupBoxUpDown;
    }
}
