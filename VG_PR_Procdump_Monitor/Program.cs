using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace VG_PR_Procdump_Monitor
{
    /// <summary>
    /// A helper app responsible for monitoring the PRBF2 server process.
    /// It launches ProCdump to capture crash dumps when the server process crashes.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method initializes the monitoring process, launches ProCdump, and continually checks for the PRBF2 server process.
        /// If the server process is not found, it assumes a crash has occurred and restarts ProCdump after a countdown.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in this implementation... yet?)</param>
        static void Main(string[] args)
        {
            const int initialSleepTime = 30;
            const int regularSleepTime = 60;
            const string processName = "PRBF2_w32ded";

            CountdownAndRestart(initialSleepTime);

            Console.WriteLine($"{DateTime.Now} - Launching ProCdump, Monitoring PR Server for unresolved crashes...");
            Thread.Sleep(2000);
            LaunchProcdump();

            while (true)
            {
                Thread.Sleep(1000);
                if (!Process.GetProcessesByName(processName).Any())
                {
                    Console.WriteLine($"{DateTime.Now} - PR Server has crashed and a dump log has been created.");
                    CountdownAndRestart(regularSleepTime);
                    Console.WriteLine($"{DateTime.Now} - Launching ProCdump, Monitoring PR Server for unresolved crashes...");
                    Thread.Sleep(2000);
                    LaunchProcdump();
                }
            }
        }

        private static void CountdownAndRestart(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"Restarting ProCdump in {i} seconds...\r");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        private static void LaunchProcdump()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "procdump.exe",
                Arguments = "-ma -b -e 1 -g prbf2_w32ded.exe",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process.Start(startInfo);
        }
    }
}
