using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FoxxyShutdownCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            bool hibernate = false;
            bool isProcessRunning = false;
            bool forceShutdown = false;
            int timeInterval = 0;
            string processFilePath = "";
            string test = AppDomain.CurrentDomain.BaseDirectory;
            if (args.Contains("-h")) //hibernate option
            {
                hibernate = true;
            }
            if(args.Contains("-force")) //force shutdown
            {
                forceShutdown = true;
            }
            if (args.Contains("-t")) //check if a time interval is given
            {
                string searchString = "-t";
                int tIndex = Array.IndexOf(args, searchString);
                if (int.Parse(args[tIndex + 1]) > 0) //the time interval is to be located in the index after "-t"
                {
                    timeInterval = int.Parse(args[tIndex + 1]);
                }
                else if (int.Parse(args[tIndex + 1]) <= 0) //time interval has to be positive
                {
                    throw new Exception("Time interval must be a ppositive, nonzero number");
                }

            }
            try
            {
                if (args.Contains("-f"))
                {
                    string searchString = "-f";
                    int fIndex = Array.IndexOf(args, searchString);
                    if (fIndex + 1 < args.Length && args[fIndex + 1] != " ")
                    {
                        processFilePath = args[fIndex + 1];
                    }
                    else
                    {
                        processFilePath = AppDomain.CurrentDomain.BaseDirectory + "processes.txt";
                    }
                    isProcessRunning = CheckIfProcessRunning(processFilePath);
                }
            }
            catch
            {
                throw new Exception("Process list file not found in " + processFilePath);
            }
            if (!isProcessRunning)
            {
                Shutdown(hibernate, forceShutdown, timeInterval);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">location of txt file with process names</param>
        /// <returns></returns>
        public static bool CheckIfProcessRunning(string filePath)
        {
            List<string> processNames = new List<string>();

            string[] lines = File.ReadAllLines(filePath);
            processNames = lines.ToList();

            if (processNames != null)
            {
                foreach (string process in processNames)
                {
                    Process[] pName = Process.GetProcessesByName(process);
                    if(pName.Length == 0)//process not running
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void Shutdown (bool hibernate, bool forceShutdown, int timeInterval)
        {
            string shutdownCommand;
            if(hibernate) //if hibernate option is set
            {
                shutdownCommand = "/h /t " + timeInterval.ToString(); //if hibernate in Windows is not set, nothing will happen, else sets the computer to hibernate
            }
            else
            {
                shutdownCommand = "/s /t " + timeInterval.ToString(); //if hibernate option here is not set with "-h", shut down instead;
            }
            if(forceShutdown)
            {
                shutdownCommand += " /f"; //forces shutdown regardless of programs running
            }
            if (timeInterval > 0)
            {
                Process.Start("shutdown", shutdownCommand); //with time interval will show a window
            }
            else //start without window
            {
                ProcessStartInfo psi = new ProcessStartInfo("shutdown", shutdownCommand);
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }
        }
    }
}
