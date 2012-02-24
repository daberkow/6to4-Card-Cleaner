using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace _6to4_Card_Cleaner
{
    public partial class SetIPv6_Form : Form
    {
        delegate void StringParameterDelegate(string value);

        private bool internal_console = false;

        public SetIPv6_Form()
        {
            InitializeComponent();
            ThreadStart update_Checker = new ThreadStart(this.load_status);
            Thread new_thread = new Thread(update_Checker);
            new_thread.Name = "6to4 IPv6Form - Load Status";
            new_thread.Start();
        }

        private void write_setcurrentsetting(string writedata)
        {
            if (setableCurrentSetting.InvokeRequired)
            {
                StringParameterDelegate d = new StringParameterDelegate(write_setcurrentsetting);
                this.Invoke(d, new object[] { writedata });
                //setableCurrentSetting.BeginInvoke(new StringParameterDelegate(write_setcurrentsetting), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
                Console.WriteLine("Architecture Discovered: " + writedata);
            }
            else
            {
                if (setableCurrentSetting.InvokeRequired) // having problems try another layer?
                {
                    StringParameterDelegate d = new StringParameterDelegate(write_setcurrentsetting);
                    this.Invoke(d, new object[] { writedata });
                    //setableCurrentSetting.BeginInvoke(new StringParameterDelegate(write_setcurrentsetting), new object[] { writedata });
                    return;
                }else
                    try
                    {
                        this.setableCurrentSetting.Text = writedata;
                    }
                    catch
                    {
                    }
            }
        }
        private void load_status()
        {
            write_setcurrentsetting("Scanning");
            try
            {
                write_setcurrentsetting(scan_IPv6());
            }
            catch
            {
            }
            /*
            switch (scan_IPv6())
            {
                case "0": // 0x0
                    write_setcurrentsetting("0");
                    return 2;
                case "4294967295": //0xfffffff
                    write_setcurrentsetting("\t\t\t\t\tComplete");
                    return 0;
                case "-1": //keps coming up as negative one
                    write_setcurrentsetting("\t\t\t\t\tComplete");
                    return 0;
                case "32": //0x20
                    write_setcurrentsetting("\t\t\t\t\tComplete");
                    return 3;
                case "16": //0x10
                    write_setcurrentsetting("\t\t\t\t\tComplete");
                    return 3;
                case "1": //0x01
                    write_setcurrentsetting("\t\t\t\t\tComplete");
                    return 3;
                case "17": //0x11
                    write_setcurrentsetting("\t\t\t\t\tComplete");
                    return 3;
                default:
                    break;

                case 0:
                    write_setcurrentsetting("Disabled");
                    break;
                case 1:
                     write_setcurrentsetting("Inconclusive");
                    break;
                case 2:
                     write_setcurrentsetting("Installed");
                    break;
                case 3:
                     write_setcurrentsetting("Limited Install");
                    break;
                default:
                     write_setcurrentsetting("Error");
                    break;
            }*/
             //0 is doesnt exist, 1 is not sure(not used), 2 is it does, 3 it does in a limited matter
        }
        public int set_IPV6_Driver(int passed_IP_set) //0 is failed, 1 is passed
        {

            RegistryKey OurKey = Registry.LocalMachine;
            OurKey = OurKey.OpenSubKey("SYSTEM"); OurKey = OurKey.OpenSubKey("CurrentControlSet"); OurKey = OurKey.OpenSubKey("services"); OurKey = OurKey.OpenSubKey("TCPIP6"); OurKey = OurKey.OpenSubKey("Parameters", true);
            try
            {
                OurKey.SetValue("DisabledComponents", passed_IP_set, RegistryValueKind.DWord);
                object temp_key = OurKey.GetValue("DisabledComponents");
                if (Convert.ToInt32(temp_key.ToString()) == passed_IP_set)
                {
                    temp_key = null;
                    OurKey = null;
                    return 1;
                }
                else
                {
                    OurKey.SetValue("DisabledComponents", passed_IP_set, RegistryValueKind.DWord);
                    object temp_key_2 = OurKey.GetValue("DisabledComponents");
                    if (Convert.ToInt32(temp_key_2.ToString()) == passed_IP_set) // second attemp
                    {
                        OurKey = null;
                        return 1;
                    }
                    else
                    {
                        OurKey = null;
                        return 0;
                    }
                }
            }
            catch // im admin only catches if key doesnt exist, which in most cases probley may
            {
                return 0;
            }
        }

        public string scan_IPv6() //0 is doesnt exist, 1 is not sure(not used), 2 is it does, 3 it does in a limited matter
        {
            RegistryKey OurKey = Registry.LocalMachine;
            OurKey = OurKey.OpenSubKey("SYSTEM", false); OurKey = OurKey.OpenSubKey("CurrentControlSet", false); OurKey = OurKey.OpenSubKey("services", false); OurKey = OurKey.OpenSubKey("TCPIP6", false); OurKey = OurKey.OpenSubKey("Parameters", false);
            try
            {
                object temp_key = OurKey.GetValue("DisabledComponents");
                string converted_temp_key = temp_key.ToString();
                switch (converted_temp_key)
                /*
                 * Type 0 to enable all IPv6 components. 
                    Note The value "0" is the default setting.
                    Type 0xffffffff to disable all IPv6 components, except the IPv6 loopback interface. This value also configures Windows Vista to use Internet Protocol version 4 (IPv4) instead of IPv6 in prefix policies.
                    Type 0x20 to use IPv4 instead of IPv6 in prefix policies.
                    Type 0x10 to disable native IPv6 interfaces.
                    Type 0x01 to disable all tunnel IPv6 interfaces.
                    Type 0x11 to disable all IPv6 interfaces except for the IPv6 loopback interface.
                 *                 case "Enable IPv6 - Windows Default":
                    set_IPV6_Driver(0);
                    break;
                case "Disable IPv6 except critical components - Use if card problem persists":
                    set_IPV6_Driver(-1);
                    break;
                case "Use IPv4 instead of IPv6 in prefix policies": //0x20
                    set_IPV6_Driver(32);
                    break;
                case "Disable native IPv6 interfaces": //0x10
                    set_IPV6_Driver(16);
                    break;
                case "Disable all tunnel IPv6 interfaces": //0x01
                    set_IPV6_Driver(1);
                    break;
                case "Disable all IPv6 interfaces except for the IPv6 loopback interface": //0x11
                    set_IPV6_Driver(17);
                    break;
                 */
                {
                    case "0": // 0x0
                        OurKey = null;
                        temp_key = null;
                        return "Enabled IPv6 - Windows Default";
                    case "-1": //keps coming up as negative one
                        OurKey = null;
                        temp_key = null;
                        return "Disabled IPv6 except critical components";
                    case "32": //0x20
                        OurKey = null;
                        temp_key = null;
                        return "Using IPv4 instead of IPv6 in prefix policies";
                    case "16": //0x10
                        OurKey = null;
                        temp_key = null;
                        return "Disabled native IPv6 interfaces";
                    case "1": //0x01
                        OurKey = null;
                        temp_key = null;
                        return "Disabled all tunnel IPv6 interfaces";
                    case "17": //0x11
                        OurKey = null;
                        temp_key = null;
                        return "All IPv6 interfaces disabled except for the IPv6 loopback";
                    default:
                        OurKey = null;
                        temp_key = null;
                        return "Out of Range Value";
                }
            }
            catch // im admin only catches if key doesnt exist, which in most cases probley may
            {
                
            }
            
            //method 2
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "ipconfig";
            p.StartInfo.Arguments = "/all";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            for (int i = 0; i < 5; i++)
            {
                if (p.HasExited)
                    i = 6;
                else
                    Thread.Sleep(500);
            }

            List<string> output = new List<string>();

            while (!p.StandardOutput.EndOfStream)
            {
                try
                {
                    output.Add(p.StandardOutput.ReadLine());
                }
                catch
                {
                }
            }

            /*p = null;
            if (p != null)*/
                p.Dispose();

            List<int> cardLines = new List<int>();
            for (int i = 0; i < output.Count; i++)
            {
                if (!output[i].StartsWith("  ") && output[i] != "")
                    cardLines.Add(i);
            }
            for (int i = 0; i < cardLines.Count; i++)
            {
                int startposition = cardLines[i];
                int endposition;
                if (i == cardLines.Count - 1)
                    endposition = output.Count;
                else
                    endposition = cardLines[i + 1];
                if (!output[startposition].StartsWith("Tunnel"))
                {
                    for (int k = startposition; k < endposition; k++)
                    {
                        if (output[k].Contains("IPv6 Address"))
                        {
                            cardLines = null;
                            return "Enabled IPv6 - Windows Default";
                            //scanning each card for info
                        }
                    }
                }
            }
            cardLines = null;
            return "IPv6 Appears to be disabled except for critical components";
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            ThreadStart update_Checker = new ThreadStart(this.load_status);
            Thread new_thread = new Thread(update_Checker);
            new_thread.Name = "6to4 IPv6Form - Load Status";
            new_thread.Start();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("http://support.microsoft.com/kb/929852", null);
        }

        private void SetButton_Click(object sender, EventArgs e)
        {
            /*
             * Type 0 to enable all IPv6 components. 
                Note The value "0" is the default setting.
                Type 0xffffffff to disable all IPv6 components, except the IPv6 loopback interface. This value also configures Windows Vista to use Internet Protocol version 4 (IPv4) instead of IPv6 in prefix policies.
                Type 0x20 to use IPv4 instead of IPv6 in prefix policies.
                Type 0x10 to disable native IPv6 interfaces.
                Type 0x01 to disable all tunnel IPv6 interfaces.
                Type 0x11 to disable all IPv6 interfaces except for the IPv6 loopback interface.
             * 
             */
            if (comboBox1.SelectedItem != null)
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Enable IPv6 - Windows Default":
                        set_IPV6_Driver(0);
                        break;
                    case "Disable IPv6 except critical components - Use if card problem persists":
                        set_IPV6_Driver(-1);
                        break;
                    case "Use IPv4 instead of IPv6 in prefix policies": //0x20
                        set_IPV6_Driver(32);
                        break;
                    case "Disable native IPv6 interfaces": //0x10
                        set_IPV6_Driver(16);
                        break;
                    case "Disable all tunnel IPv6 interfaces": //0x01
                        set_IPV6_Driver(1);
                        break;
                    case "Disable all IPv6 interfaces except for the IPv6 loopback interface": //0x11
                        set_IPV6_Driver(17);
                        break;
                    default:
                        set_IPV6_Driver(0);
                        break;
                }
                write_setcurrentsetting("Scanning");
                try
                {
                    write_setcurrentsetting(scan_IPv6());
                }
                catch
                {
                }
            }
            GC.Collect();
        }

        private void SetIPv6_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("You will need to restart before any setting changes will take effect.");
        }

    }

}
