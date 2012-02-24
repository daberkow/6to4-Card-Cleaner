namespace _6to4_Card_Cleaner
{
    partial class MainApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApp));
            this.Remove_Only_button = new System.Windows.Forms.Button();
            this.Scan_Button = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LabelWinVer = new System.Windows.Forms.Label();
            this.setableWinVer = new System.Windows.Forms.Label();
            this.labelArch = new System.Windows.Forms.Label();
            this.SetableArch = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SetableStatus = new System.Windows.Forms.Label();
            this.labelNumoCards = new System.Windows.Forms.Label();
            this.SetableCardNumber = new System.Windows.Forms.Label();
            this.labelRemoved = new System.Windows.Forms.Label();
            this.SetableRemoved = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.About_button = new System.Windows.Forms.Button();
            this.Update_Button = new System.Windows.Forms.Button();
            this.Remove_all_button = new System.Windows.Forms.Button();
            this.labelipv6status = new System.Windows.Forms.Label();
            this.setableipv6enabled = new System.Windows.Forms.Label();
            this.ReenableIPv6 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Remove_Only_button
            // 
            this.Remove_Only_button.AccessibleDescription = "Remove virtual network cards";
            this.Remove_Only_button.AccessibleName = "Remove Cards Enable";
            this.Remove_Only_button.Enabled = false;
            this.Remove_Only_button.Location = new System.Drawing.Point(265, 355);
            this.Remove_Only_button.Name = "Remove_Only_button";
            this.Remove_Only_button.Size = new System.Drawing.Size(109, 39);
            this.Remove_Only_button.TabIndex = 0;
            this.Remove_Only_button.Text = "Only Remove Cards";
            this.Remove_Only_button.UseVisualStyleBackColor = true;
            this.Remove_Only_button.Click += new System.EventHandler(this.Remove_button_Click);
            // 
            // Scan_Button
            // 
            this.Scan_Button.AccessibleDescription = "Scan for virtual cards";
            this.Scan_Button.AccessibleName = "Scan Button";
            this.Scan_Button.Location = new System.Drawing.Point(11, 355);
            this.Scan_Button.Name = "Scan_Button";
            this.Scan_Button.Size = new System.Drawing.Size(116, 39);
            this.Scan_Button.TabIndex = 1;
            this.Scan_Button.Text = "Scan for Cards";
            this.Scan_Button.UseVisualStyleBackColor = true;
            this.Scan_Button.Click += new System.EventHandler(this.Scan_Button_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(133, 355);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(126, 39);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 2;
            // 
            // LabelWinVer
            // 
            this.LabelWinVer.AccessibleDescription = "Windows Version";
            this.LabelWinVer.AccessibleName = "Windows Version";
            this.LabelWinVer.AutoSize = true;
            this.LabelWinVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelWinVer.Location = new System.Drawing.Point(8, 3);
            this.LabelWinVer.Name = "LabelWinVer";
            this.LabelWinVer.Size = new System.Drawing.Size(136, 17);
            this.LabelWinVer.TabIndex = 3;
            this.LabelWinVer.Text = "Windows Version:";
            // 
            // setableWinVer
            // 
            this.setableWinVer.AutoSize = true;
            this.setableWinVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setableWinVer.Location = new System.Drawing.Point(340, 3);
            this.setableWinVer.Name = "setableWinVer";
            this.setableWinVer.Size = new System.Drawing.Size(96, 17);
            this.setableWinVer.TabIndex = 4;
            this.setableWinVer.Text = "Scan Not Run";
            // 
            // labelArch
            // 
            this.labelArch.AutoSize = true;
            this.labelArch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArch.Location = new System.Drawing.Point(8, 22);
            this.labelArch.Name = "labelArch";
            this.labelArch.Size = new System.Drawing.Size(174, 17);
            this.labelArch.TabIndex = 5;
            this.labelArch.Text = "Windows Architecture: ";
            // 
            // SetableArch
            // 
            this.SetableArch.AutoSize = true;
            this.SetableArch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetableArch.Location = new System.Drawing.Point(340, 22);
            this.SetableArch.Name = "SetableArch";
            this.SetableArch.Size = new System.Drawing.Size(96, 17);
            this.SetableArch.TabIndex = 6;
            this.SetableArch.Text = "Scan Not Run";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(8, 115);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(103, 17);
            this.labelStatus.TabIndex = 7;
            this.labelStatus.Text = "Current Status:";
            // 
            // SetableStatus
            // 
            this.SetableStatus.AutoSize = true;
            this.SetableStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetableStatus.Location = new System.Drawing.Point(117, 115);
            this.SetableStatus.Name = "SetableStatus";
            this.SetableStatus.Size = new System.Drawing.Size(30, 17);
            this.SetableStatus.TabIndex = 8;
            this.SetableStatus.Text = "Idle";
            // 
            // labelNumoCards
            // 
            this.labelNumoCards.AutoSize = true;
            this.labelNumoCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumoCards.Location = new System.Drawing.Point(8, 71);
            this.labelNumoCards.Name = "labelNumoCards";
            this.labelNumoCards.Size = new System.Drawing.Size(148, 15);
            this.labelNumoCards.TabIndex = 9;
            this.labelNumoCards.Text = "Number of Cards Present:";
            // 
            // SetableCardNumber
            // 
            this.SetableCardNumber.AutoSize = true;
            this.SetableCardNumber.Location = new System.Drawing.Point(340, 73);
            this.SetableCardNumber.Name = "SetableCardNumber";
            this.SetableCardNumber.Size = new System.Drawing.Size(75, 13);
            this.SetableCardNumber.TabIndex = 10;
            this.SetableCardNumber.Text = "Scan Not Run";
            // 
            // labelRemoved
            // 
            this.labelRemoved.AutoSize = true;
            this.labelRemoved.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRemoved.Location = new System.Drawing.Point(8, 90);
            this.labelRemoved.Name = "labelRemoved";
            this.labelRemoved.Size = new System.Drawing.Size(159, 15);
            this.labelRemoved.TabIndex = 11;
            this.labelRemoved.Text = "Number of Cards Removed:";
            // 
            // SetableRemoved
            // 
            this.SetableRemoved.AutoSize = true;
            this.SetableRemoved.Location = new System.Drawing.Point(340, 92);
            this.SetableRemoved.Name = "SetableRemoved";
            this.SetableRemoved.Size = new System.Drawing.Size(13, 13);
            this.SetableRemoved.TabIndex = 12;
            this.SetableRemoved.Text = "0";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox.Location = new System.Drawing.Point(11, 139);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(521, 210);
            this.textBox.TabIndex = 13;
            // 
            // About_button
            // 
            this.About_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.About_button.Location = new System.Drawing.Point(488, 113);
            this.About_button.Name = "About_button";
            this.About_button.Size = new System.Drawing.Size(45, 22);
            this.About_button.TabIndex = 14;
            this.About_button.Text = "About";
            this.About_button.UseVisualStyleBackColor = true;
            this.About_button.Click += new System.EventHandler(this.About_button_Click);
            // 
            // Update_Button
            // 
            this.Update_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Update_Button.Location = new System.Drawing.Point(433, 113);
            this.Update_Button.Name = "Update_Button";
            this.Update_Button.Size = new System.Drawing.Size(49, 22);
            this.Update_Button.TabIndex = 16;
            this.Update_Button.Text = "Update";
            this.Update_Button.UseVisualStyleBackColor = true;
            this.Update_Button.Click += new System.EventHandler(this.Update_Button_Click);
            // 
            // Remove_all_button
            // 
            this.Remove_all_button.Enabled = false;
            this.Remove_all_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Remove_all_button.Location = new System.Drawing.Point(380, 355);
            this.Remove_all_button.Name = "Remove_all_button";
            this.Remove_all_button.Size = new System.Drawing.Size(152, 39);
            this.Remove_all_button.TabIndex = 17;
            this.Remove_all_button.Text = "Disable IPv6, Remove Cards, Patch";
            this.Remove_all_button.UseVisualStyleBackColor = true;
            this.Remove_all_button.Click += new System.EventHandler(this.Remove_all_button_Click);
            // 
            // labelipv6status
            // 
            this.labelipv6status.AutoSize = true;
            this.labelipv6status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelipv6status.Location = new System.Drawing.Point(8, 43);
            this.labelipv6status.Name = "labelipv6status";
            this.labelipv6status.Size = new System.Drawing.Size(95, 17);
            this.labelipv6status.TabIndex = 18;
            this.labelipv6status.Text = "IPv6 Status:";
            // 
            // setableipv6enabled
            // 
            this.setableipv6enabled.AutoSize = true;
            this.setableipv6enabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setableipv6enabled.Location = new System.Drawing.Point(340, 43);
            this.setableipv6enabled.Name = "setableipv6enabled";
            this.setableipv6enabled.Size = new System.Drawing.Size(96, 17);
            this.setableipv6enabled.TabIndex = 19;
            this.setableipv6enabled.Text = "Scan Not Run";
            // 
            // ReenableIPv6
            // 
            this.ReenableIPv6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReenableIPv6.Location = new System.Drawing.Point(337, 113);
            this.ReenableIPv6.Name = "ReenableIPv6";
            this.ReenableIPv6.Size = new System.Drawing.Size(90, 22);
            this.ReenableIPv6.TabIndex = 20;
            this.ReenableIPv6.Text = "IPv6 Setting";
            this.ReenableIPv6.UseVisualStyleBackColor = true;
            this.ReenableIPv6.Click += new System.EventHandler(this.ReenableIPv6_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::_6to4_Card_Cleaner.Properties.Resources.ChangeCon1;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(457, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 75);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // MainApp
            // 
            this.AccessibleDescription = "Program removes virutal network cards that can randomly duplicate";
            this.AccessibleName = "6to4 Card Cleaner";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 397);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ReenableIPv6);
            this.Controls.Add(this.setableipv6enabled);
            this.Controls.Add(this.labelipv6status);
            this.Controls.Add(this.Remove_all_button);
            this.Controls.Add(this.Update_Button);
            this.Controls.Add(this.About_button);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.SetableRemoved);
            this.Controls.Add(this.labelRemoved);
            this.Controls.Add(this.SetableCardNumber);
            this.Controls.Add(this.labelNumoCards);
            this.Controls.Add(this.SetableStatus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.SetableArch);
            this.Controls.Add(this.labelArch);
            this.Controls.Add(this.setableWinVer);
            this.Controls.Add(this.LabelWinVer);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Scan_Button);
            this.Controls.Add(this.Remove_Only_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "6to4 Card Cleaner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainApp_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Remove_Only_button;
        private System.Windows.Forms.Button Scan_Button;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label LabelWinVer;
        private System.Windows.Forms.Label setableWinVer;
        private System.Windows.Forms.Label labelArch;
        private System.Windows.Forms.Label SetableArch;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label SetableStatus;
        private System.Windows.Forms.Label labelNumoCards;
        private System.Windows.Forms.Label SetableCardNumber;
        private System.Windows.Forms.Label labelRemoved;
        private System.Windows.Forms.Label SetableRemoved;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button About_button;
        private System.Windows.Forms.Button Update_Button;
        private System.Windows.Forms.Button Remove_all_button;
        private System.Windows.Forms.Label labelipv6status;
        private System.Windows.Forms.Label setableipv6enabled;
        private System.Windows.Forms.Button ReenableIPv6;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

