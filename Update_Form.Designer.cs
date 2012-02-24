namespace _6to4_Card_Cleaner
{
    partial class Update_Form
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
            this.Status_Static = new System.Windows.Forms.Label();
            this.Status_dynamic = new System.Windows.Forms.Label();
            this.Close_button = new System.Windows.Forms.Button();
            this.Update_button = new System.Windows.Forms.Button();
            this.CVersion_Static = new System.Windows.Forms.Label();
            this.CVersion_Dynamic = new System.Windows.Forms.Label();
            this.AVersion_Static = new System.Windows.Forms.Label();
            this.AVersion_dynamic = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Status_Static
            // 
            this.Status_Static.AutoSize = true;
            this.Status_Static.Location = new System.Drawing.Point(12, 42);
            this.Status_Static.Name = "Status_Static";
            this.Status_Static.Size = new System.Drawing.Size(0, 13);
            this.Status_Static.TabIndex = 0;
            // 
            // Status_dynamic
            // 
            this.Status_dynamic.AutoSize = true;
            this.Status_dynamic.Location = new System.Drawing.Point(13, 42);
            this.Status_dynamic.Name = "Status_dynamic";
            this.Status_dynamic.Size = new System.Drawing.Size(10, 13);
            this.Status_dynamic.TabIndex = 1;
            this.Status_dynamic.Text = " ";
            // 
            // Close_button
            // 
            this.Close_button.Location = new System.Drawing.Point(247, 277);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 2;
            this.Close_button.Text = "Close";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(166, 277);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 3;
            this.Update_button.Text = "Download";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // CVersion_Static
            // 
            this.CVersion_Static.AutoSize = true;
            this.CVersion_Static.Location = new System.Drawing.Point(13, 13);
            this.CVersion_Static.Name = "CVersion_Static";
            this.CVersion_Static.Size = new System.Drawing.Size(85, 13);
            this.CVersion_Static.TabIndex = 4;
            this.CVersion_Static.Text = "Current Version: ";
            // 
            // CVersion_Dynamic
            // 
            this.CVersion_Dynamic.AutoSize = true;
            this.CVersion_Dynamic.Location = new System.Drawing.Point(105, 13);
            this.CVersion_Dynamic.Name = "CVersion_Dynamic";
            this.CVersion_Dynamic.Size = new System.Drawing.Size(0, 13);
            this.CVersion_Dynamic.TabIndex = 5;
            // 
            // AVersion_Static
            // 
            this.AVersion_Static.AutoSize = true;
            this.AVersion_Static.Location = new System.Drawing.Point(163, 13);
            this.AVersion_Static.Name = "AVersion_Static";
            this.AVersion_Static.Size = new System.Drawing.Size(82, 13);
            this.AVersion_Static.TabIndex = 6;
            this.AVersion_Static.Text = "Server Version: ";
            // 
            // AVersion_dynamic
            // 
            this.AVersion_dynamic.AutoSize = true;
            this.AVersion_dynamic.Location = new System.Drawing.Point(263, 13);
            this.AVersion_dynamic.Name = "AVersion_dynamic";
            this.AVersion_dynamic.Size = new System.Drawing.Size(0, 13);
            this.AVersion_dynamic.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(16, 63);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(306, 208);
            this.textBox1.TabIndex = 8;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(83, 279);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(77, 21);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 282);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Download v";
            // 
            // Update_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 312);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.AVersion_dynamic);
            this.Controls.Add(this.AVersion_Static);
            this.Controls.Add(this.CVersion_Dynamic);
            this.Controls.Add(this.CVersion_Static);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Status_dynamic);
            this.Controls.Add(this.Status_Static);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Update_Form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Status_Static;
        private System.Windows.Forms.Label Status_dynamic;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Label CVersion_Static;
        private System.Windows.Forms.Label CVersion_Dynamic;
        private System.Windows.Forms.Label AVersion_Static;
        private System.Windows.Forms.Label AVersion_dynamic;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}