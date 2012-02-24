using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

namespace _6to4_Card_Cleaner
{
    public partial class Update_Form : Form
    {
        delegate void StringParameterDelegate(string value);

        private bool internal_console = false;
        private List<string> internal_aVersion = new List<string>(); // write here so after scan on open, we can check and later
        private string internal_cVersion = "";
        private string interal_save_Location = "";
        private string internal_primary_web_server = "";
        private string internal_secondary_web_server = "";
        private string internal_exe_name = "";

        public Update_Form(string passed_Version, string passed_Primary_server, string passed_Secondary_server, string passed_exe_name)
        {
            internal_cVersion = passed_Version;
            internal_primary_web_server = passed_Primary_server;
            internal_secondary_web_server = passed_Secondary_server;
            internal_exe_name = passed_exe_name;
            InitializeComponent();
            CVersion_Dynamic.Text = passed_Version;
       
            ThreadStart job = new ThreadStart(find_new_version);
            Thread thread = new Thread(job);
            thread.Name = "6to4 Updater - Find New Versions";
            thread.Start();   
        }
        private void write_textbox(string writedata)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new StringParameterDelegate(write_textbox), new object[] { writedata });
                return;
            }
            if (internal_console)
            {

            }
            else
            {
                if (writedata == "SUPERCLEAN")
                    textBox1.Text = "";
                else
                    textBox1.Text = textBox1.Text + writedata;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
                //textbox.Refresh();
            }
        }
        public void easy_set(bool passed_console, string passed_save_Location, string passed_Version)
        {
            internal_console = passed_console;
            interal_save_Location = passed_save_Location;
            internal_cVersion = passed_Version;
        }

        private void write_AVersion_dynamic(string writedata)
        {
            if (AVersion_dynamic.InvokeRequired)
            {
                AVersion_dynamic.BeginInvoke(new StringParameterDelegate(write_AVersion_dynamic), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
                Console.WriteLine("Available Version: " + writedata);
            }
            else // not console version
            {
                AVersion_dynamic.Text = writedata;
            }
        }
        private void write_status(string writedata)
        {
            if (Status_dynamic.InvokeRequired)
            {
                Status_dynamic.BeginInvoke(new StringParameterDelegate(write_status), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
                Console.WriteLine("Updater Status: " + writedata);
            }
            else // not console version
            {
                if (Status_dynamic.InvokeRequired)
                {
                    Status_dynamic.Invoke(new StringParameterDelegate(write_status), new object[] { writedata });
                    return;
                }
                else
                {
                    try
                    {
                        Status_dynamic.Text = writedata;
                    }
                    catch
                    {
                        Status_dynamic.BeginInvoke(new StringParameterDelegate(write_status), new object[] { writedata });
                        return;
                    }
                }
            }
        }
        private void write_combobox(string writedata)
        {
            if (comboBox1.InvokeRequired)
            {
                comboBox1.BeginInvoke(new StringParameterDelegate(write_combobox), new object[] { writedata });
                return;
            }
            if (comboBox1.InvokeRequired)
            {
                comboBox1.BeginInvoke(new StringParameterDelegate(write_combobox), new object[] { writedata });
                return;
            }
            else
            {
                if (!comboBox1.Items.Contains(writedata))
                    this.comboBox1.Items.AddRange(new object[] {writedata });
            }
            
        }
        private void write_window_title(string writedata)
        {
            if (comboBox1.InvokeRequired)
            {
                comboBox1.BeginInvoke(new StringParameterDelegate(write_window_title), new object[] { writedata });
                return;
            }
            if (comboBox1.InvokeRequired)
            {
                comboBox1.BeginInvoke(new StringParameterDelegate(write_window_title), new object[] { writedata });
                return;
            }
            else
            {
                this.Text = writedata;
                    
            }

        }
        private void find_new_version()
        {
            WebClient Client = new WebClient();
            
            write_status("Connecting to server...");
            try
            {
                write_window_title("Updater, using Primary Server");
                string temp_buffer = Client.DownloadString(internal_primary_web_server + "LATEST");
                string[] temp_string_array = temp_buffer.Split(new string[] { "\n" }, StringSplitOptions.None);
                //internal_aVersion = Client.DownloadString(internal_web_server + "LATEST");
                for (int i = 0; i < temp_string_array.Length; i++)
                {
                    write_combobox(temp_string_array[i]);
                    internal_aVersion.Add(temp_string_array[i]);
                }
                write_status("Closed connection to server");
                write_AVersion_dynamic(internal_aVersion[0]);
                write_status("");
                temp_buffer = null;
                temp_string_array = null;
            }
            catch
            {
                try
                {
                    write_window_title("Updater, using Secondary Server");
                    string temp_buffer = Client.DownloadString(internal_secondary_web_server + "LATEST");
                    string[] temp_string_array = temp_buffer.Split(new string[] { "\n" }, StringSplitOptions.None);
                    //internal_aVersion = Client.DownloadString(internal_web_server + "LATEST");
                    for (int i = 0; i < temp_string_array.Length; i++)
                    {
                        write_combobox(temp_string_array[i]);
                        internal_aVersion.Add(temp_string_array[i]);
                    }
                    write_status("Closed connection to server");
                    write_AVersion_dynamic(internal_aVersion[0]);
                    write_status("");
                    temp_buffer = null;
                    temp_string_array = null;
                }
                catch
                {
                    write_status("Something went wrong finding new version");
                }
                
            }
            /*Client = null;
            if (Client != null)*/
                Client.Dispose();
        }

        public List<string> public_find_new_version()
        {
            WebClient Client = new WebClient();
            List<string> temp_pool = new List<string>();
            try
            {
                write_window_title("Updater, using Primary Server");
                string temp_buffer = Client.DownloadString(internal_primary_web_server + "LATEST");
                string[] temp_string_array = temp_buffer.Split(new string [] {"\n"},StringSplitOptions.None);
                for (int i = 0; i < temp_string_array.Length; i++)
                {
                    temp_pool.Add(temp_string_array[i]);
                }
                temp_buffer = null;
                temp_string_array = null;
                return temp_pool;
                
            }
            catch
            {
                try
                {
                    write_window_title("Updater, using Secondary Server");
                    string temp_buffer = Client.DownloadString(internal_secondary_web_server + "LATEST");
                    string[] temp_string_array = temp_buffer.Split(new string[] { "\n" }, StringSplitOptions.None);
                    for (int i = 0; i < temp_string_array.Length; i++)
                    {
                        temp_pool.Add(temp_string_array[i]);
                    }
                    temp_buffer = null;
                    temp_string_array = null;
                    return temp_pool;
                }
                catch
                {
                    write_window_title("Updater");
                    temp_pool.Add("0.0");
                    return temp_pool;
                }
            }
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            GC.Collect();
            this.Close();
        }

        private void Update_button_Click(object sender, EventArgs e)
        {
            Update_Process();
            GC.Collect();
        }

        void Client_DownloadFileCompleted(object sender,AsyncCompletedEventArgs e)
        {
            write_status("Download Complete - Close this version, and open new one...");
        }

        void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            write_status(e.ProgressPercentage + "% downloaded");
        }

        public bool Update_Process()
        {
            ThreadStart job = new ThreadStart(find_new_version);
            Thread thread = new Thread(job);
            thread.Name = "6to4 Updater - Look for Versions";
            thread.Start();
            WebClient Client = new WebClient();
            Uri webaddress;
            if (comboBox1.SelectedItem != null)
            {
                if (internal_console == false) // graphics version
                {
                    write_status("Need File Location");
                    SaveFileDialog savy = new SaveFileDialog();
                    savy.Filter = "Executables *.exe|*.exe";
                    savy.DefaultExt = ".exe";
                    savy.AddExtension = true;
                    savy.OverwritePrompt = true;
                    savy.FileName = internal_exe_name.Substring(0,internal_exe_name.Length -4);
                    if (DialogResult.OK == savy.ShowDialog())
                        interal_save_Location = savy.FileName;
                    //else location isnt changed
                    savy = null;
                    if (savy != null)
                        savy.Dispose();
                }
                write_status("Opening connection");
                webaddress = new Uri(internal_primary_web_server + comboBox1.SelectedItem.ToString() + "/" + internal_exe_name);
                try
                {
                    write_window_title("Updater, using Primary Server");
                    Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                    Client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                    Client.DownloadFileAsync(webaddress, interal_save_Location); // we are admin so where ever,
                    while (Client.IsBusy)
                        Thread.Sleep(500);
                    Client.Dispose();
                    return true;
                }
                catch
                {
                    try
                    {
                        write_window_title("Updater, using Secondary Server");
                        webaddress = new Uri(internal_secondary_web_server + comboBox1.SelectedItem.ToString() + "/" + internal_exe_name);
                        Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Client_DownloadProgressChanged);
                        Client.DownloadFileCompleted += new AsyncCompletedEventHandler(Client_DownloadFileCompleted);
                        Client.DownloadFileAsync(webaddress, interal_save_Location); // we are admin so where ever,
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                write_textbox("SUPERCLEAN");
                write_textbox("Please select a version from the drop down");
                write_window_title("Updater");
            }

            return false;

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            WebClient Client = new WebClient();
            try
            {
                write_textbox("SUPERCLEAN");
                write_window_title("Updater, using Primary Server");
                write_textbox( Client.DownloadString(internal_primary_web_server + comboBox1.SelectedItem.ToString() + "/INFO"));
            }
            catch
            {
                try
                {
                    write_window_title("Updater, using Secondary Server");
                    write_textbox(Client.DownloadString(internal_secondary_web_server + comboBox1.SelectedItem.ToString() + "/INFO"));
                }
                catch
                {
                    write_window_title("Updater");
                    write_textbox("Error getting INFO");
                }
            }
            Client = null;
            if (Client != null)
                Client.Dispose();
        }

    }
}