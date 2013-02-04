using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Reflection; // gets assbmley/embedded data
using System.IO;
using System.Diagnostics;
using System.Threading;
//using System.Xml;
using Microsoft.Win32;

namespace _6to4_Card_Cleaner
{
    public partial class MainApp : Form
    {
        //The delegates are needed to have writing outside of your thread working.
        delegate void StringParameterDelegate(string value);
        delegate void StringParameterDelegateP(string value, bool value1);
        delegate void boolParameterDelegate(bool value);
        delegate void BoolParameterDelegate(bool value1, bool value2, bool value3);
        delegate void BlankOfBlank(string value1, int value2);
        delegate void IPv6InstallLevel(int value1);
        //These are all varibles used throughout code
        private bool internal_debug = false;
        private string internal_file_location = "";
        private int internal_cards = 0;
        private string internal_cpu_style = "";
        private int internal_threadcount = 0;
        private bool internal_console = false;
        //bool internal_logger = false; //Depricated Command line code
        private bool internal_verbose = false;
        private string internal_version = "";
        private int something_has_changed = 0;
        private static Int16 running = 0; // 0 is program is closing, 1 is tray, 2 is gui 3 is actively running scan, one implies running
        private Visual_Form FLASHY = null;
        private Process k = null;
        private static System.Windows.Forms.NotifyIcon Tray_Icon = null;

        public MainApp(bool passed_debug_mode, string passed_version, bool passed_verbose)
        {
            //This checks if there is a new version, and if so alerts user
            ThreadStart update_Checker = new ThreadStart(this.check_updates_wrapper);
            Thread new_thread = new Thread(update_Checker);
            new_thread.Name = "6 to 4 - Update Checker";
            new_thread.Start();
            //Start the actual window
            InitializeComponent();
            //Tell all threads, amin is still running, is janky but works
            running = 2;
            //Starts animating icon while devcon is running
            ThreadStart Animation = new ThreadStart(this.animation);
            Thread animation_thread = new Thread(Animation);
            animation_thread.Name = "6 to 4 - Animation Thread";
            animation_thread.Start();
           
            //Set internals from passed
            internal_version = passed_version;
            internal_debug = passed_debug_mode;
            internal_verbose = passed_verbose;
            //Easter egg
            if (internal_verbose == true)
            {
                FLASHY = new Visual_Form();
                verbose_wrapper();
            }
        }
        private void verbose_wrapper()
        {
            //This is just a little easter egg
            FLASHY.startup();
            FLASHY.Show();
        }
        public void easy_set(bool passed_console, bool output)
        {
            // set data if using command line verison, that I really didnt put that much into
            internal_console = passed_console;
        }
        private void Scan_Button_Click(object sender, EventArgs e)
        {
            if (internal_threadcount == 0)
            {//makes sure the user doesnt just keep clicking the button
                Process theProc = System.Diagnostics.Process.GetCurrentProcess();
                ProcessThreadCollection theThreads = theProc.Threads;
                internal_threadcount = theThreads.Count;
                ThreadStart job = new ThreadStart(scan_me);
                Thread thread = new Thread(job);
                thread.Name = "6to4 - Scan for Cards";
                thread.Start();
            }
            else
            {
                MessageBox.Show("Thread Already Running"); // Cancel thread
            }

        }

        //All of these write methods are writing data to interface from other threads
        private void write_textbox(string writedata)
        {
            if (textBox.InvokeRequired)
            {
                textBox.BeginInvoke(new StringParameterDelegate(write_textbox), new object[]{writedata});
                return;
            }
            if (internal_console)
            {
               // if (internal_logger)     {      }     else    {
                    Console.WriteLine(writedata);
               // }
                
            }else
            {
                if (writedata == "SUPERCLEAN")
                    textBox.Text = "";
                else
                    textBox.Text = textBox.Text + writedata;
                textBox.SelectionStart = textBox.Text.Length;
                textBox.ScrollToCaret();
                //textbox.Refresh();
            }
        }
        private void write_statusbox(string writedata)
        {
            if (SetableStatus.InvokeRequired)
            {
                SetableStatus.BeginInvoke(new StringParameterDelegate(write_statusbox), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
            }
            else
            {
                SetableStatus.Text = writedata;
                refresh_tray();
            }
        }
        private void write_arch(string writedata)
        {
            if (SetableArch.InvokeRequired)
            {
                SetableArch.BeginInvoke(new StringParameterDelegate(write_arch), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
                Console.WriteLine("Architecture Discovered: " + writedata);
            }
            else
            {
                SetableArch.Text = writedata;
            }
        }
        private void write_winver(string writedata)
        {
            if (setableWinVer.InvokeRequired)
            {
                setableWinVer.BeginInvoke(new StringParameterDelegate(write_winver), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
                Console.WriteLine("Windows Version : " + writedata);
            }
            else
            {
                setableWinVer.Text = writedata;
            }
        }
        private void write_progress(string donez, int totalz)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.BeginInvoke(new BlankOfBlank(write_progress), new object[] { donez, totalz });
                return;
            }
            if (totalz != 0)
            {
                if(progressBar1.Maximum != totalz)
                    progressBar1.Maximum = totalz;
            }
            if (progressBar1.Value != Convert.ToInt32(donez))
                progressBar1.Value = Convert.ToInt32(donez);
        }
        private void write_button_set(bool ScanisCancel, bool RemoveActive, bool RemoveisCancel) // bool RemoveActive, bool RemoveisCancel
        {
            if (Remove_Only_button.InvokeRequired)
            {
                Remove_Only_button.BeginInvoke(new BoolParameterDelegate(write_button_set), new object[] { ScanisCancel, RemoveActive, RemoveisCancel });
                return;
            } if (internal_console)
            {
            }
            else
            {
                /*
                if (ScanisCancel == true)
                    Scan_Button.Text = "Cancel";
                else
                    if (ScanisCancel == false)
                        Scan_Button.Text = "Scan for Cards";
                if (RemoveisCancel == true)
                    Remove_button.Text = "Cancel";
                else
                    if (RemoveisCancel == false)
                        Remove_button.Text = "Remove Cards";
                 */
                if (RemoveActive == true)
                {
                    Remove_Only_button.Enabled = true;
                    Remove_all_button.Enabled = true;
                }
                else
                    if (RemoveActive == false)
                    {
                        Remove_Only_button.Enabled = false;
                        Remove_all_button.Enabled = false;
                    }
            }
        }
        private void write_removed(string writedata)
        {
            if (SetableRemoved.InvokeRequired)
            {
                SetableRemoved.BeginInvoke(new StringParameterDelegate(write_removed), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
            }
            else
            {
                SetableRemoved.Text = writedata;
                refresh_tray();
            }
        }
        private void write_cardnumber(string writedata, bool allowed_lower)
        {
            if (SetableCardNumber.InvokeRequired)
            {
                SetableCardNumber.BeginInvoke(new StringParameterDelegateP(write_cardnumber), new object[] { writedata, allowed_lower });
                return;
            }
            if (internal_console)
            {
                Console.WriteLine("Cards found: " + writedata);
            }
            else
            {
                if (!allowed_lower)
                {
                    if (SetableCardNumber.Text != "Scan Not Run")
                    {
                        if (Int32.Parse(SetableCardNumber.Text) < Int32.Parse(writedata))
                        {
                            SetableCardNumber.Text = writedata;
                            refresh_tray();
                        }
                        else
                        {
                            write_textbox("Error writing version number, no privilage, please report error to 6to4@buildingtents.com");
                        }
                    }
                    else
                    {
                        SetableCardNumber.Text = writedata;
                        refresh_tray();
                    }
                }
                else
                {
                    SetableCardNumber.Text = writedata;
                    refresh_tray();
                }
               
            }
        }
        private void write_ipv6Installed(int writedata)
        {//0 is doesnt exist, 1 is not sure(not used), 2 is it does, 3 it does in a limited matter
            if (setableipv6enabled.InvokeRequired)
            {
                setableipv6enabled.BeginInvoke(new IPv6InstallLevel(write_ipv6Installed), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
                switch (writedata)
                {
                    case 0:
                        Console.WriteLine("IPv6 Status: " +  "Disabled");
                        break;
                    case 1:
                        Console.WriteLine("IPv6 Status: " + "Inconclusive");
                        break;
                    case 2:
                        Console.WriteLine("IPv6 Status: " + "Enabled");
                        break;
                    case 3:
                        Console.WriteLine("IPv6 Status: " + "Limited Execution");
                        break;
                    default:
                        Console.WriteLine("IPv6 Status: " + "Error");
                        break;
                }
            }
            else
            {
                switch (writedata)
                {
                    case 0:
                        setableipv6enabled.Text = "Disabled";
                        break;
                    case 1:
                        setableipv6enabled.Text = "Inconclusive";
                        break;
                    case 2:
                        setableipv6enabled.Text = "Enabled";
                        break;
                    case 3:
                        setableipv6enabled.Text = "Limited Execution";
                        break;
                    default:
                        setableipv6enabled.Text = "Error";
                        break;
                }
            }
        }
        private void refresh_tray()
        {
            if (running == 1)
            {
                string temp_status, temp_card_removed, temp_card_total;
                if (SetableStatus.Text.Length > 32)
                {
                    temp_status = SetableStatus.Text.Substring(0, 32);
                }
                else
                {
                    temp_status = SetableStatus.Text;
                }
                if (SetableRemoved.Text.Length > 5)
                {
                    temp_card_removed = SetableRemoved.Text.Substring(0, 5);
                }
                else
                {
                    temp_card_removed = SetableRemoved.Text;
                }
                if (SetableCardNumber.Text.Length > 5)
                {
                    temp_card_total = SetableCardNumber.Text.Substring(0, 5);
                }
                else
                {
                    temp_card_total = SetableCardNumber.Text;
                }

                if (Tray_Icon != null)
                {
                    string temp_text = temp_status + "\r\nRemoved: " + temp_card_removed + "\r\nTotal: " + temp_card_total;
                    if (temp_text.Length >= 64)
                    {
                        Tray_Icon.Text = temp_text.Substring(0, 63);
                    }
                    else
                    {
                        Tray_Icon.Text = temp_text;
                    }
                    temp_text = null;
                }
                temp_status = temp_card_removed = temp_card_total = null;

            }
        }

        //This is the scan code
        public void scan_me()
        {
            write_textbox("SUPERCLEAN"); //How I clear the textbox
            write_button_set(true, false, false); // Change button infomation
            if (os_check_go() == true) // check if OS is new enough to run, or user allows it
            {
                write_textbox("\t\t\t\t\tComplete");
                arch_check(internal_console); // Check ach type
                write_statusbox("System Architecture Scan Complete");
                write_textbox("\t\t\t\t\tComplete");
                string devconLoc = PrepDevcon(internal_cpu_style);
                if (devconLoc != "ERROR")
                {// at this point we have os and arch, with a preped file
                    internal_file_location = devconLoc; //Setup devcon in temp folder
                    write_statusbox("Idle");
                    write_textbox("\t\t\t\tComplete");
                    internal_cards = Card_Number(internal_file_location, true);
                    write_statusbox("Idle");
                    if (internal_verbose == true) // some flashy
                        FLASHY.add_total(internal_cards);
                    write_ipv6Installed(scan_IPv6());
                    write_statusbox("Idle");

                    write_textbox("\r\nScan Complete");
                    write_button_set(false, true, false);
                    internal_threadcount = 0; // allow button to be used again
                }
                else
                {
                    write_statusbox("Idle");
                    write_textbox("\t\t\t\tFAILED");
                }

            }
            else
            {
                //textBox.Text = textBox.Text + "\t\t\t\t\tScan Aborted...";
                write_textbox("\t\t\t\t\tScan Aborted...");
                //SetableStatus.Text = "Idle";
                write_statusbox("Idle");
            }
        }
        public bool os_check_go()
        {
            //SetableStatus.Text = "Operating System Scan...";
            write_statusbox("Operating System Scan...");
            //textBox.Text = "Operating System Scan:";
            write_textbox("Operating System Scan:");
            System.OperatingSystem osInfo = System.Environment.OSVersion;
            //setableWinVer.Text = osInfo.Version.ToString();
            write_winver(osInfo.Version.ToString());
            if (osInfo.Version.Major > 6 || osInfo.Version.Major < 5)
            {
                DialogResult result = MessageBox.Show("Error: Unknown Windows Version\r\n \r\nDo you want to attempt to run?", "Warning: 0001", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    osInfo = null;
                    return false;
                }
                else
                {
                    osInfo = null;
                    return true;
                }
            }
            if (osInfo.Version.Major == 5 && osInfo.Version.Minor == 0)
            {
                write_textbox("\r\nWindows 2000 not supported, sorry but it is " + DateTime.Now.Year.ToString());
                osInfo = null;
                return false;
            }
            if (osInfo.Version.Major == 6) // Vista or 7
            {
                osInfo = null;
                return true;
            }
            if (osInfo.Version.Major == 5 && osInfo.Version.Minor == 1)
            {
                osInfo = null;
                return true;
            }
            osInfo = null;
            return false;
        }
        private string arch_check(bool console) //Previously PrepDevcon_1
        {
            /*
             *  No Itanium autodetect, I have no idea how it ids
             */
            write_statusbox("System Architecture Scan...");
            write_textbox("\r\nSystem Architecture Scan:");
            string cpu_type;
            if (System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432") != null)
                cpu_type = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"); //Windows does this dance to find what arch it is
            else
                cpu_type = System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            write_arch(cpu_type);
            internal_cpu_style = cpu_type;
            return cpu_type;
        }
        private string PrepDevcon(string archType)
          {// Got a lot of code from site, but modified
           //http://www.cs.nyu.edu/~vs667/articles/embed_executable_tutorial/

            write_statusbox("Preparing Removal Components...");
            write_textbox("\r\nPreparing Removal Components:");
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] arrResources = currentAssembly.GetManifestResourceNames();
            foreach (string resourceName in arrResources)
            {
                if (internal_debug == true)
                { // this could have been embedded then just left out at release but I am too lazy to change that now, build >1.2.3
                    return "C:\\Users\\Dan\\Documents\\Visual Studio 2010\\Projects\\Devcon_Simulator\\bin\\Debug\\devcon.exe";
                }
                if (resourceName.EndsWith(".exe") && resourceName.Contains(archType))
                {
                    try
                    {
                        string temp_Location = Environment.GetEnvironmentVariable("TEMP");
                        string saveAsName = temp_Location + "\\devcon.exe";
                        FileInfo fileInfoOutputFile = new FileInfo(saveAsName);

                        if (fileInfoOutputFile.Exists)
                        {
                            fileInfoOutputFile.Delete(); //overwrite if wrong version exists
                        }
                        //OPEN NEWLY CREATING FILE FOR WRITTING
                        FileStream streamToOutputFile = fileInfoOutputFile.OpenWrite();
                        //GET THE STREAM TO THE RESOURCES
                        Stream streamToResourceFile =
                                            currentAssembly.GetManifestResourceStream(resourceName);

                        //---------------------------------
                        //SAVE TO DISK OPERATION
                        //---------------------------------
                        const int size = 4096;
                        byte[] bytes = new byte[4096];
                        int numBytes;
                        while ((numBytes = streamToResourceFile.Read(bytes, 0, size)) > 0)
                        {
                            streamToOutputFile.Write(bytes, 0, numBytes);
                        }

                        bytes = null;
                        streamToOutputFile.Close();
                        streamToResourceFile.Close();
                        streamToOutputFile = null;
                        if(streamToOutputFile != null)
                            streamToOutputFile.Dispose();
                        streamToResourceFile = null;
                        if(streamToResourceFile != null)
                            streamToResourceFile.Dispose();
                        return saveAsName;
                    }
                    catch
                    {
                        write_textbox("Something went wrong extracting file.");
                            return null;
                    }
                }//end_if

            }//end_foreach
            return "ERROR";
        }
        private string PrepKB980486(string archType)
        {   //To run fix from microsoft
            write_statusbox("Preparing KB980486 Components...");
            write_textbox("\r\nPreparing KB980486 Components:");
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string[] arrResources = currentAssembly.GetManifestResourceNames();
            foreach (string resourceName in arrResources)
            {
                if (resourceName.EndsWith(".msu") && resourceName.Contains(archType))
                {
                    try
                    {
                        string temp_Location = Environment.GetEnvironmentVariable("TEMP");
                        string saveAsName = temp_Location + "\\KB980486.msu";
                        FileInfo fileInfoOutputFile = new FileInfo(saveAsName);

                        if (fileInfoOutputFile.Exists)
                        {
                            fileInfoOutputFile.Delete(); //overwrite if wrong version exists
                        }
                        //OPEN NEWLY CREATING FILE FOR WRITTING
                        FileStream streamToOutputFile = fileInfoOutputFile.OpenWrite();
                        //GET THE STREAM TO THE RESOURCES
                        Stream streamToResourceFile =
                                            currentAssembly.GetManifestResourceStream(resourceName);

                        //---------------------------------
                        //SAVE TO DISK OPERATION
                        //---------------------------------
                        const int size = 4096;
                        byte[] bytes = new byte[4096];
                        int numBytes;
                        while ((numBytes = streamToResourceFile.Read(bytes, 0, size)) > 0)
                        {
                            streamToOutputFile.Write(bytes, 0, numBytes);
                        }

                        bytes = null;
                        streamToOutputFile.Close();
                        streamToResourceFile.Close();
                        streamToOutputFile = null;
                        if (streamToOutputFile != null)
                            streamToOutputFile.Dispose();
                        streamToResourceFile = null;
                        if (streamToResourceFile != null)
                            streamToResourceFile.Dispose();
                        return saveAsName;
                    }
                    catch
                    {
                        write_textbox("Something went wrong extracting file.");
                        return null;
                    }
                }//end_if

            }//end_foreach
            return "ERROR";
        }
        private int Card_Number(string file_location, bool passed_verbose)
        {
            
            //textBox.Text = textBox.Text + "\r\nCard Scan:  (This stage takes up to a about a minute)";
            if (passed_verbose)
            {
                write_textbox("\r\nCard Scan:  (This stage takes up to a about a minute)");
                //SetableStatus.Text = "Card Scan:";
                write_statusbox("Card Scan:");
            }
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = file_location;
            p.StartInfo.Arguments = "find *6to4mp*";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
           // Console.Write(output);
            int cardNumber;
            if (output.Contains("No matching devices found.\r\n"))
            {
                cardNumber = 0;
                //SetableCardNumber.Text = "0";
                write_cardnumber("0", true);
            }
            else
            {
                int left_barrier = output.LastIndexOf("\r\n", (output.Length - 30));
                string numberOCards = output.Substring(left_barrier + 2, (output.Length - 30 - left_barrier));

                cardNumber = Convert.ToInt32(numberOCards);
                if(passed_verbose)
                    write_cardnumber(numberOCards, false);
                
                //SetableCardNumber.Text = numberOCards;
            }

            if (passed_verbose)
            {
                //textBox.Text = textBox.Text + "\t\tComplete"; //card scan complete
                write_textbox("\t\tComplete");
                //SetableStatus.Text = "Waiting for core components to close...";
                write_statusbox("Waiting for components to close...");
            }

            //SetableStatus.Text = "Components Closed...";
            if(passed_verbose)
                write_statusbox("Components Closed...");
            //p.WaitForExit();
            p = null;
            if (p != null)
                p.Dispose();
            return cardNumber;
        }
        private void Remove_button_Click(object sender, EventArgs e)
        {
            if (internal_threadcount == 0)
            {
                Process theProc = System.Diagnostics.Process.GetCurrentProcess();
                // Console.WriteLine(theProc.ProcessName);
                ProcessThreadCollection theThreads = theProc.Threads;
                internal_threadcount = theThreads.Count;
                ThreadStart job = new ThreadStart(remove_button_wrapper);
                Thread thread = new Thread(job);
                thread.Name = "6to4 - Remove Cards Button";
                thread.Start();
            }
            else
            {
                MessageBox.Show("Thread Already Running"); // Cancel thread
            }
        }
        private void remove_button_wrapper()
        {
            write_button_set(false, false, false);
            remove_cards();
            write_button_set(true, false, false);
        }
        #region secondary card removal method
        /*
        public void Jank_Remove_Cards()
        {
            something_has_changed++;
            string temp_Location = Environment.GetEnvironmentVariable("TEMP");
            string saveAsName = temp_Location + "\\6to4remove.bat";
            File.WriteAllText(saveAsName, "cd %temp%\r\necho off\r\ncls\r\necho This can take some time, and the first card may take up to a few minutes\r\necho on\r\n\"" + internal_file_location + "\" remove *6to4mp*" + /*"\r\nmkdir %windir%\\testdir*//* "\r\necho off\r\necho off\r\necho Removal Complete, if you are having connection problems, restart now and try to connect again.\r\npause");
            write_removed("Unknown");
            write_textbox("\r\nStarting external removal program");
            write_statusbox("Starting External Removal");
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.Verb = "runas";
            p.StartInfo.FileName = saveAsName;
            p.Start();
            p.WaitForExit();
            write_statusbox("Idle");
            write_textbox("\r\nRemoval Complete. Feel free to scan again...");
            write_textbox("\r\n\r\nRestart to connect to servers...");
            internal_threadcount = 0;
        }
     /*   void p_output_has_arrived(object sender, DataReceivedEventArgs e)
        {
            TOTAL_STORAGE += e.Data;
        }*/
#endregion
       
        public void remove_cards()
        {//Code to run devcon in background
            running = 3;
            write_statusbox("Removing Cards...");
            write_textbox("\r\nRemoving Cards (This can take 10 minutes if you have 200 cards)");
            write_textbox("\r\nSorry if the program doesnt give too much information about how many cards have been removed, that is still being worked on...");
            write_textbox("\r\nAs long as the Icon is changing, the program is either scanning for cards, or removing them...");
            //Changed way we get output, kept here if it didnt work
            //Process p = new Process();
            // Redirect the output stream of the child process.
            /*
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.Verb = "runas";
            p.StartInfo.FileName = internal_file_location;
            p.StartInfo.Arguments = "remove *6to4mp*";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            */
            k = new Process();
            k.StartInfo.UseShellExecute = false;
            k.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.Verb = "runas";
            k.StartInfo.FileName = internal_file_location;
            k.StartInfo.Arguments = "remove *6to4mp*";
            k.StartInfo.CreateNoWindow = true;
            k.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            //List<string> output = new List<string>(); I think this was debug code
            int fail_remove = 0;
            int suc_removed = 0;
            //bool message_go_away = false;
            string TOTAL_STORAGE = "";
            write_progress("0", internal_cards); 
            ThreadStart update_Checker = new ThreadStart(this.status_check);
            Thread new_thread = new Thread(update_Checker);
            new_thread.Name = "Update Checker";
            new_thread.Start();
            while (!k.StandardOutput.EndOfStream)
            {
                if (running == 0)
                {
                    k.Close();
                    break;
                }
                TOTAL_STORAGE += Convert.ToChar(k.StandardOutput.Read());
                if (TOTAL_STORAGE.Length > 4)
                {
                    if (TOTAL_STORAGE.EndsWith("\r\n"))
                    {
                        write_textbox("\r\n" + TOTAL_STORAGE);
                        if (TOTAL_STORAGE.Contains("Removed"))
                        {
                            suc_removed++;
                            if (internal_verbose == true)
                            {
                                if (TOTAL_STORAGE.Contains("ROOT\\*6TO4MP\\"))
                                {
                                    try
                                    {
                                        int temp_length = TOTAL_STORAGE.IndexOf("\t\t") - 15;
                                        int temp_card_number = Convert.ToInt32(TOTAL_STORAGE.Substring(15, temp_length));
                                        FLASHY.change_bool(temp_card_number);
                                    }
                                    catch
                                    {
                                    }
                                }
                                //FLASHY.change_bool
                            }
                            write_removed(suc_removed.ToString());
                        }
                        if (TOTAL_STORAGE.Contains("Failed"))
                            fail_remove++;
                        TOTAL_STORAGE = "";
                       /* if (suc_removed != 0 && internal_cards != 0)
                        {
                            if ((suc_removed / internal_cards) <= 1)
                                write_progress(1, 0);
                        }*/
                    }
                }
            }

            write_removed(suc_removed.ToString());
            write_textbox("\r\nRemoval Complete");
            write_statusbox("Idle");
            internal_threadcount = 0;
            //if (suc_removed > 250)
             //   MessageBox.Show("Achievement Unlocked: Have over 250 Virtual Cards"); 
            running = 2;
            Activate_tray(false);
            something_has_changed++;
        }
        private void About_button_Click(object sender, EventArgs e)
        {
            if (internal_threadcount == 0)
            {
                Process theProc = System.Diagnostics.Process.GetCurrentProcess();
                // Console.WriteLine(theProc.ProcessName);
                ProcessThreadCollection theThreads = theProc.Threads;
                internal_threadcount = theThreads.Count;
                ThreadStart job = new ThreadStart(About_button_Click_wrapper);
                Thread thread = new Thread(job);
                thread.Name = "6 to 4 - About Box";
                thread.Start();
            }
            else
            {
                MessageBox.Show("Thread Already Running"); // Cancel thread
            }
        }
        private void About_button_Click_wrapper()
        {
            AboutBox_Form about_box = new AboutBox_Form(internal_version);
            about_box.StartPosition = FormStartPosition.CenterScreen;
            about_box.ShowDialog();
            about_box = null;
            if (about_box != null)
                about_box.Dispose();
            internal_threadcount = 0;
        }
        private void Update_Button_Click(object sender, EventArgs e)
        {
            Update_Button_Click_wrapper();
        }
        private void Update_Button_Click_wrapper()
        {
            Update_Form update_box = new Update_Form(internal_version, "https://raw.github.com/daberkow/6to4-Card-Cleaner/master/updates/", "http://programs2.buildingtents.com/6to4CardCleaner/", "6to4 Card Cleaner.exe");
            update_box.StartPosition = FormStartPosition.CenterScreen;
            update_box.ShowDialog();
            update_box = null;
            if (update_box != null)
                update_box.Dispose();
            internal_threadcount = 0;
        }
        public int scan_IPv6() //0 is doesnt exist, 1 is not sure(not used), 2 is it does, 3 it does in a limited matter
        {
            write_statusbox("Scanning for IPv6, Method 1");
            write_textbox("\r\nScanning for IPv6, Method 1: ");
            RegistryKey OurKey = Registry.LocalMachine;
            OurKey = OurKey.OpenSubKey("SYSTEM"); OurKey = OurKey.OpenSubKey("CurrentControlSet"); OurKey = OurKey.OpenSubKey("services"); OurKey = OurKey.OpenSubKey("TCPIP6"); OurKey = OurKey.OpenSubKey("Parameters");
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
                     */
                {
                    case "0": // 0x0
                        write_textbox("\t\t\t\t\tComplete");
                        write_statusbox("Idle");
                        OurKey = null;
                        converted_temp_key = null;
                        return 2;
                    case "4294967295": //0xfffffff
                        write_textbox("\t\t\t\t\tComplete");
                        write_statusbox("Idle");
                        OurKey = null;
                        converted_temp_key = null;
                        return 0;
                    case "-1": //keps coming up as negative one
                        write_textbox("\t\t\t\t\tComplete");
                        write_statusbox("Idle");
                        converted_temp_key = null;
                        OurKey = null;
                        return 0;
                    case "32": //0x20
                        write_textbox("\t\t\t\t\tComplete");
                        write_statusbox("Idle");
                        converted_temp_key = null;
                        OurKey = null;
                        return 3;
                    case "16": //0x10
                        write_textbox("\t\t\t\t\tComplete");
                        write_statusbox("Idle");
                        converted_temp_key = null;
                        OurKey = null;
                        return 3;
                    case "1": //0x01
                        write_textbox("\t\t\t\t\tComplete");
                        write_statusbox("Idle");
                        converted_temp_key = null;
                        OurKey = null;
                        return 3;
                    case "17": //0x11
                        write_textbox("\t\t\t\t\tComplete");
                        write_statusbox("Idle");
                        converted_temp_key = null;
                        OurKey = null;
                        return 3;
                    default:
                        write_textbox("\t\t\t\t\tInconclusive");
                        write_statusbox("Idle");
                        converted_temp_key = null;
                        OurKey = null;
                        break;
                }
            }
            catch // im admin only catches if key doesnt exist, which in most cases probley may
            {
                write_textbox("\t\t\t\t\tInconclusive");
                write_statusbox("Idle");
            }

            //method 2
            write_statusbox("Scanning for IPv6, Method 2");
            write_textbox("\r\nScanning for IPv6, Method 2: ");
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "ipconfig";
            p.StartInfo.Arguments = "/all";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            write_statusbox("Scanning for IPv6, Method 2: Computing");

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

            p = null;
            if (p != null)
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
                if(i==cardLines.Count - 1)
                    endposition = output.Count;
                else
                    endposition = cardLines[i+1];
                if (!output[startposition].StartsWith("Tunnel"))
                {
                    for (int k = startposition; k < endposition; k++)
                    {
                        if (output[k].Contains("IPv6 Address"))
                        {
                            write_textbox("\t\t\t\t\tComplete");
                            cardLines = null;
                            return 2;
                            //scanning each card for info
                        }
                    }
                }
            }
            write_textbox("\t\t\t\t\tComplete");
            cardLines = null;
            return 0;
        }
        private void check_updates_wrapper()
        {// needed for graphics threading, and for console compatibility
            check_updates("");
        }
        public void check_updates(string passed_console_save_Location)
        {
            try
            {
                Update_Form update_codebase = new Update_Form(internal_version, "http://programs.buildingtents.com/6to4CardCleaner/", "http://programs2.buildingtents.com/6to4CardCleaner/", "6to4 Card Cleaner.exe");
                string private_aversion = update_codebase.public_find_new_version()[0];
                string[] AVersion = private_aversion.Split(new Char [] {'.', ' '}); //[0] is major, [1] is minor
                string[] CVersion = internal_version.Split(new Char [] {'.', ' '});

                if (AVersion.Length >= 2 && CVersion.Length >= 2) //properly formated version numbers
                {
                    if (Convert.ToInt32(AVersion[0]) > Convert.ToInt32(CVersion[0]) || Convert.ToInt32(AVersion[1]) > Convert.ToInt32(CVersion[1]))
                    {
                         MessageBox.Show("This is a out-of-date version, " + internal_version + ", version " + private_aversion + " is available, please click update if you wish to get the latest version.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                update_codebase = null;
                if (update_codebase != null)
                    update_codebase.Dispose();
                private_aversion = null;
                AVersion = null;
                CVersion = null;
            }
            catch
            {
            }
            
        }
        private void ReenableIPv6_Click(object sender, EventArgs e)
        {
            //if(janky_thread_on == true)
            if (internal_threadcount == 0)
            {
                Process theProc = System.Diagnostics.Process.GetCurrentProcess();
                // Console.WriteLine(theProc.ProcessName);
                ProcessThreadCollection theThreads = theProc.Threads;
                internal_threadcount = theThreads.Count;
                ThreadStart job = new ThreadStart(ReenableIPv6_Click_wrapper);
                Thread thread = new Thread(job);
                thread.Name = "6 to 4 - IPv6 Settings Panel";
                thread.Start();
            }
            else
            {
                MessageBox.Show("Thread Already Running"); // Cancel thread
            }
        }
        private void ReenableIPv6_Click_wrapper()
        {
            SetIPv6_Form setform = new SetIPv6_Form();
            setform.StartPosition = FormStartPosition.CenterScreen;
            setform.ShowDialog();
            setform = null;
            if (setform != null)
                setform.Dispose();

            write_ipv6Installed(scan_IPv6());
            internal_threadcount = 0;
        }
        private void Remove_all_button_Click(object sender, EventArgs e)
        {
            if (internal_threadcount == 0)
            {
                Process theProc = System.Diagnostics.Process.GetCurrentProcess();
                // Console.WriteLine(theProc.ProcessName);
                ProcessThreadCollection theThreads = theProc.Threads;
                internal_threadcount = theThreads.Count;
                ThreadStart job = new ThreadStart(Remove_Card_Steps_Wrapper);
                Thread thread = new Thread(job);
                thread.Name = "6 to 4 - Remove Card Thread";
                thread.Start();
            }
            else
            {
                MessageBox.Show("Thread Already Running"); // Cancel thread
            }
        }
        private void Remove_Card_Steps_Wrapper()
        {
            write_button_set(false, false, false);
            running = 3; // elevate running level
            write_statusbox("Disabling IPv6");
            write_textbox("\r\nDisabling IPv6: ");
            if (disable_IPv6() == 1)
            {
                write_progress("25", 100);
                write_textbox("\t\t\t\tComplete");
                remove_button_wrapper();
                write_progress("75", 100);
                if (internal_cpu_style != "")
                {
                    write_statusbox("Running MS KB Article 980486 Fix");
                    write_textbox("\r\nRunning Microsoft Knowledge Base Article 980486 Fix: ");
                    System.Diagnostics.Process.Start(PrepKB980486(internal_cpu_style), "/quiet /norestart");
                   /* Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.Verb = "runas";
                    p.StartInfo.FileName = PrepKB980486(internal_cpu_style);
                    p.StartInfo.Arguments = "/quiet /norestart";
                    p.Start();*/
                    Thread.Sleep(1000);
                    Process[] spawn_of_kb980486 = Process.GetProcessesByName("wusa");
                    //while (!p.HasExited && spawn_of_kb980486.Length != 0)
                    while (spawn_of_kb980486.Length != 0)
                    {
                        Thread.Sleep(500);
                        spawn_of_kb980486 = Process.GetProcessesByName("wusa");
                    }
                    write_textbox("\t\t\t\tComplete");
                    write_progress("100", 100);
                    write_statusbox("RESTART REQUIRED");
                    write_textbox("\r\nAll processes Complete, Restart now required to take effect");
                    spawn_of_kb980486 = null;
                    
                }
                else
                {
                    write_textbox("\t\t\t\tFAILED");
                    write_textbox("\r\nError, CPU style not found");
                    write_statusbox("Idle");
                }
                internal_threadcount = 0;
                GC.Collect();
            }
            running = 2;
            Activate_tray(false);
            write_button_set(true, false, false);
            something_has_changed++;
        }
        private int disable_IPv6()
        {
            something_has_changed++;
            int internal_IPv6_set = -1;
            RegistryKey OurKey = Registry.LocalMachine;
            OurKey = OurKey.OpenSubKey("SYSTEM"); OurKey = OurKey.OpenSubKey("CurrentControlSet"); OurKey = OurKey.OpenSubKey("services"); OurKey = OurKey.OpenSubKey("TCPIP6"); OurKey = OurKey.OpenSubKey("Parameters", true);
            try
            {
                OurKey.SetValue("DisabledComponents", internal_IPv6_set, RegistryValueKind.DWord);
                object temp_key = OurKey.GetValue("DisabledComponents");
                if (Convert.ToInt32(temp_key.ToString()) == internal_IPv6_set)
                {
                    OurKey = null;
                    temp_key = null;
                    return 1;
                }
                else
                {
                    OurKey.SetValue("DisabledComponents", internal_IPv6_set, RegistryValueKind.DWord);
                    object temp_key_2 = OurKey.GetValue("DisabledComponents");
                    if (Convert.ToInt32(temp_key_2.ToString()) == internal_IPv6_set) // second attemp
                    {
                        OurKey = null;
                        temp_key = null;
                        temp_key_2 = null;
                        return 1;
                    }
                    else
                    {
                        OurKey = null;
                        temp_key = null;
                        temp_key_2 = null;
                        return 0;
                    }
                }
            }
            catch // im admin only catches if key doesnt exist, which in most cases probley may
            {
                return 0;
            }
        }
        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            show_window(false);

            switch (running)
            {
                case 0:
                case 2:
                    if (something_has_changed > 0)
                    {
                        DialogResult last_question = MessageBox.Show("For these settings to take effect, this machine must restart, restart now? Make sure to save any work.", "Restart Now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (last_question == DialogResult.Yes) //call restart
                        {
                            Process.Start("shutdown", "/r /t 30 /c \"6to4 Card Cleaner has started a restart...\"");
                        }
                    }
                    running = 0;
                    Thread.Sleep(1500);//let things looking for running quit
                    break;
                case 1:
                case 3:
                    e.Cancel = true;
                    running = 1;
                    Activate_tray(true);
                    break;
                default:
                    if (something_has_changed > 0)
                    {
                        DialogResult last_question = MessageBox.Show("For these settings to take effect, this machine must restart, restart now? Make sure to save any work.", "Restart Now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (last_question == DialogResult.Yes) //call restart
                        {
                            Process.Start("shutdown", "/r /t 30 /c \"6to4 Card Cleaner has started a restart...\"");
                        }
                    }
                    running = 0;
                    Thread.Sleep(1500);//let things looking for running quit
                    break;

            }
        }
       /* private void button1_Click(object sender, EventArgs e)
        {
            verbose_wrapper();
        }*/
//        private void status_check_Wrapper() { }
        private void status_check()
        {
            while (running >= 1)
            {
                int cardNumber = Card_Number(internal_file_location, false);
                write_removed((internal_cards - cardNumber).ToString());
                write_progress((internal_cards - cardNumber).ToString(), 0);
                Thread.Sleep(1200);
            }
        }
        public void animation()
        {
           // Image gifImage = global::_6to4_Card_Cleaner.Properties.Resources.ChangeCon1;
           // FrameDimension dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
           // int frameCount = gifImage.GetFrameCount(dimension);
           // int index_IMG = 0;
           while (running >= 2)// always check
            {
                Process[] anim_process = Process.GetProcessesByName("devcon");
                if (anim_process.Length != 0)
                {
                    write_picture_ON(true);

                }
                else
                {
                    write_picture_ON(false);
                    GC.Collect();
                }
                //write_picture(global::_6to4_Card_Cleaner.Properties.Resources.ChangeCon0);
                anim_process = null;
                Thread.Sleep(1000);
                // Create message loop
            }
            
        }
     /* private void write_picture(Image writedata)
        {
            if (SetableStatus.InvokeRequired)
            {
                SetableStatus.BeginInvoke(new ImageParameterDelegate(write_picture), new object[] { writedata });
                return;
            }
            if (internal_console)
            {
            }
            else
            {
                try
                {
                    
                    pictureBox1.Image = writedata;

                }
                catch
                { }
            }
        }
      * */
        private void write_picture_ON(bool writedata)
        {
            if (SetableStatus.InvokeRequired)
            {
                SetableStatus.BeginInvoke(new boolParameterDelegate(write_picture_ON), new object[] { writedata });
                return;
            }
            try
            {

                pictureBox1.Enabled = writedata;

            }
            catch
            { }
        }

        private void already_active()
        {
            Process[] anim_process = Process.GetProcessesByName("devcon");
            if (anim_process.Length != 0)
            {
                DialogResult result = MessageBox.Show("It appears that components of the program, from another instance,  are currently running in the background, do you want to kill them?", "6to4 - Components", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Process[] Running = Process.GetProcessesByName("devcon");
                    foreach (Process run in Running)
                    {
                        run.Kill();
                        while (!run.HasExited)
                        {
                            write_textbox("Attempting to kill " + run.ProcessName + ", ID " + run.Id);
                            run.Kill();
                            run.Close();
                            Thread.Sleep(300);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("As long as the 6to4 animation is running, the process is running in the background");
                }

            }
        }
        #region Icon
        private void Activate_tray(bool passed_activation)
        {
            if (passed_activation && Tray_Icon == null)
            {
                running = 1;
                Tray_Icon = new NotifyIcon();

                Tray_Icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.restore_window);

                MenuItem[] mnuItems = new MenuItem[3];

                //create the menu items array

                mnuItems[0] = new MenuItem("Restore 6to4 Card Cleaner Window", new EventHandler(this.restore_window));
                mnuItems[0].DefaultItem = true;
                mnuItems[1] = new MenuItem("-");
                mnuItems[2] = new MenuItem("Abort and Restore Window", new EventHandler(this.abortAndrestore));

                //add the menu items to the context menu of the NotifyIcon
                ContextMenu notifyIconMenu = new ContextMenu(mnuItems);
                mnuItems = null;
                //notifyIconMenu.Dispose();
                Tray_Icon.ContextMenu = notifyIconMenu;
                System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainApp));
                Tray_Icon.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                resources = null;
                Tray_Icon.Text = "Right Click for Options";
                Tray_Icon.Visible = true;
                Tray_Icon.BalloonTipIcon = ToolTipIcon.Info;
                Tray_Icon.BalloonTipTitle = "Minimized";
                Tray_Icon.BalloonTipText = "Sent to Tray, until finished or clicked. Mouse Over for progress";
                Tray_Icon.ShowBalloonTip(7500);

            }
            else
            {
                if (Tray_Icon != null)
                {
                    Tray_Icon.Visible = false;
                    show_window(true);
                    Tray_Icon = null;
                }
            }
        }
        private void show_window(bool passed_bool)
        { // it basses so i can resuse delegate
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new boolParameterDelegate(show_window), new object[] { passed_bool });
                return;
            }
            if (passed_bool)
                this.Show();
            else
                this.Hide();
        }
        
        private void restore_window(object sender, EventArgs e)
        {
            Activate_tray(false);
        }

        private void abortAndrestore(object sender, EventArgs e)
        {
            if (k != null)
                k.Kill();
            Activate_tray(false);
        }

        #endregion Icon
    }

}
