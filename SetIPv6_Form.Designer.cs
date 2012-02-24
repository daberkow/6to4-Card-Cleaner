namespace _6to4_Card_Cleaner
{
    partial class SetIPv6_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SetButton = new System.Windows.Forms.Button();
            this.ScanButton = new System.Windows.Forms.Button();
            this.labelCurentSetting = new System.Windows.Forms.Label();
            this.setableCurrentSetting = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SetButton
            // 
            this.SetButton.Location = new System.Drawing.Point(367, 66);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(130, 23);
            this.SetButton.TabIndex = 1;
            this.SetButton.Text = "Set IPv6";
            this.SetButton.UseVisualStyleBackColor = true;
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // ScanButton
            // 
            this.ScanButton.Location = new System.Drawing.Point(12, 66);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(79, 23);
            this.ScanButton.TabIndex = 2;
            this.ScanButton.Text = "Scan";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // labelCurentSetting
            // 
            this.labelCurentSetting.AutoSize = true;
            this.labelCurentSetting.Location = new System.Drawing.Point(9, 13);
            this.labelCurentSetting.Name = "labelCurentSetting";
            this.labelCurentSetting.Size = new System.Drawing.Size(105, 13);
            this.labelCurentSetting.TabIndex = 3;
            this.labelCurentSetting.Text = "Curent IPv6 Setting: ";
            // 
            // setableCurrentSetting
            // 
            this.setableCurrentSetting.AutoSize = true;
            this.setableCurrentSetting.Location = new System.Drawing.Point(120, 13);
            this.setableCurrentSetting.Name = "setableCurrentSetting";
            this.setableCurrentSetting.Size = new System.Drawing.Size(73, 13);
            this.setableCurrentSetting.TabIndex = 4;
            this.setableCurrentSetting.Text = "Need to Scan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Set IPv6 to:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.Items.AddRange(new object[] {
            "Enable IPv6 - Windows Default",
            "Disable IPv6 except critical components - Use if card problem persists",
            "Use IPv4 instead of IPv6 in prefix policies",
            "Disable native IPv6 interfaces",
            "Disable all tunnel IPv6 interfaces",
            "Disable all IPv6 interfaces except for the IPv6 loopback interface"});
            this.comboBox1.Location = new System.Drawing.Point(123, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(374, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(407, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Help with Options";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // SetIPv6_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 97);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.setableCurrentSetting);
            this.Controls.Add(this.labelCurentSetting);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.SetButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetIPv6_Form";
            this.ShowIcon = false;
            this.Text = "SetIPv6_Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SetIPv6_Form_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetButton;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.Label labelCurentSetting;
        private System.Windows.Forms.Label setableCurrentSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;

    }
}