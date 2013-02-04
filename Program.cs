using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Reflection;
//using System.IO;
using System.Diagnostics;

namespace _6to4_Card_Cleaner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool debug_mode = false;
            string Real_Version = "1.64";

            //make sure we arent already running
            Process[] ProcessesScan = Process.GetProcessesByName("devcon");
            if (ProcessesScan.Length != 0)
            {//it is running
                DialogResult result = MessageBox.Show("Devcon currently running, please close then continue or restart.", "Already Running", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result != DialogResult.OK)
                    Application.Exit();
            }//not running

            if (args.Length < 1)
            {
                Application.Run(new MainApp(debug_mode, Real_Version, false)); // verbose mode is last one
            }
            else//Check varibles, has to have a at least 1 arg in
            {
                MainApp consoleversion = new MainApp(debug_mode, Real_Version, false);
                switch (args[0].ToLower())
                {
                    case "/auto":
                        consoleversion.easy_set(true, false);
                        consoleversion.scan_me();
                        consoleversion.remove_cards();
                        break;
                    case "/findcards":
                        consoleversion.easy_set(true, false);
                        consoleversion.scan_me();
                        break;
                    case "/verbose":
                        Application.Run(new MainApp(debug_mode, Real_Version, true));
                        break;

                    default:
                        Console.WriteLine("The program removes all of the IPv6 to IPv4 virtual converter cards.\r\n");
                        Console.WriteLine("The cards are automaticly made and if they are removed, Windows will just make a new one.\r\n");
                        Console.WriteLine("");
                        Console.WriteLine("\"6to4 Card Cleaner.exe\" /auto");
                        Console.WriteLine("\tWill attempt to automaticly remove cards");
                        Console.WriteLine("\"6to4 Card Cleaner.exe\" /findcards");
                        Console.WriteLine("\tWill just find cards, not remove them");
                        Console.WriteLine("\"6to4 Card Cleaner.exe\" /verbose");
                        Console.WriteLine("\tPretty card removal");
                        Console.WriteLine("Warning: All command line systems are experimental right now, not recommended");
                        Console.WriteLine("Ver " + Real_Version);
                        break;
                }
                consoleversion = null;
                if (consoleversion != null)
                    consoleversion.Dispose();
            }
        }
    }
}
