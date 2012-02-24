using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _6to4_Card_Cleaner
{
    public partial class Visual_Form : Form
    {
        List<System.Windows.Forms.Panel> panels = new List<Panel>();
        delegate void INTValue(int value1);
        delegate void StringParameterDelegate(string value);
        delegate void BlankOfBlank(int value1, int value2);
        int X_Location = 4;
        int Y_Location = 2;
        public Visual_Form()
        {
            
        }
        public void startup()
        {
            InitializeComponent();
        }
        public int number_of_cards()
        {
            return panels.Count;
        }
        private void write_controls(int passed_panel_number)
        {
            if (textBox1 != null)
            {
                if (textBox1.InvokeRequired)
                {
                    textBox1.BeginInvoke(new INTValue(write_controls), new object[] { passed_panel_number });
                    return;
                }
                textBox1.Controls.Add(panels[passed_panel_number]);
            }
            
        }
        private void write_textbox(string write_data)
        {
            if (textBox1 != null)
            {
                if (textBox1.InvokeRequired)
                {
                    textBox1.BeginInvoke(new StringParameterDelegate(write_textbox), new object[] { write_data });
                    return;
                }
                textBox1.Text += write_data;
            }

        }
        private void write_progress(int donez, int totalz)
        {
            if (progressBar1 != null)
            {
                if (progressBar1.InvokeRequired)
                {
                    progressBar1.BeginInvoke(new BlankOfBlank(write_progress), new object[] { donez, totalz });
                    return;
                }
                if (totalz != 0)
                    progressBar1.Maximum = totalz;
                for (int i = 0; i < donez; i++)
                {
                    progressBar1.PerformStep();
                }
            }
        }

        public void add_total(int total)
        {
            for (int i = 0; i < total; i++)
            {
                Panel Temp_Panel = new Panel();
                Temp_Panel.BackColor = System.Drawing.Color.Aqua;
                Temp_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                Temp_Panel.BringToFront();
                Temp_Panel.Location = new System.Drawing.Point(X_Location, Y_Location);
                Temp_Panel.Name = "Panel" + i;
                Temp_Panel.Size = new System.Drawing.Size(10, 16);
                panels.Add(Temp_Panel);
                Temp_Panel = null;
                if (Temp_Panel != null)
                    Temp_Panel.Dispose();
                write_controls(panels.Count - 1);
                //Controls.Add(panels[panels.Count - 1]);
                
                if ((X_Location + 16) > 575)
                {
                    write_textbox("\r\n");
                    Y_Location += 18;
                    X_Location = 4;

                }
                else
                    X_Location += 16;
                //panels[panels.Count - 1].Show();
            }
            write_progress(0, panels.Count);
            if (panels.Count >= 200)
                MessageBox.Show("Achievement Unlocked: Over 200!", "Achievement Unlocked", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public bool change_bool(int card_number)
        {
            if (panels.Count >= card_number)
            {
                if (panels[card_number].BackColor == System.Drawing.Color.Aqua)
                {
                    panels[card_number].BackColor = System.Drawing.Color.RoyalBlue;
                    write_progress(1, 0);
                }

                return true;
            }
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            add_total(random.Next(0, 200));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (panels.Count != 0)
            {
                Random random = new Random();
                change_bool(random.Next(0, panels.Count - 1));
            }
        }
    }
}
