using System.Diagnostics;
using System.IO;
using System;

namespace FishEyeService
{
    /// <summary>
    /// Allows to execute console applications direct or via command prompt.
    /// Autor: Mattia Baldinger, www.code-in.net
    /// </summary>
    public class CommandLine
    {
        /// <summary>
        /// Executes the specified commandl line appliation direclty with the arguments. 
        /// The return value of the called application will be returned. 
        /// </summary>
        /// <param name="fileName">Name of the application e.g. ipconfig</param>
        /// <param name="args">Parameters should be passed to the application e.g. /renew</param>
        /// <returns>The text which is returned by the application, when the called application is closed or finished</returns>
        public static void Run(string fileName, string args = "")
        {
            ProcessStartInfo info = GetProcessStartInfo(fileName, args);
            RunProcess(info);
        }

        private static void RunProcess(ProcessStartInfo info)
        {
            int maxLines = 1000, lineCount = 0;
            string prefix = Path.GetFileNameWithoutExtension(info.FileName);
            StreamWriter logWriter = CreateLogFile(prefix);
            using (Process process = Process.Start(info))
            using (StreamReader sr = process.StandardOutput)
            {
                while (!sr.EndOfStream)
                {
                    logWriter.Write(sr.ReadLine());
                    logWriter.Flush();
                    if (++lineCount > maxLines)
                    {
                        logWriter.BaseStream.SetLength(0);
                    }
                }
            }

            using (logWriter)
            {
            }
        }

        private static ProcessStartInfo GetProcessStartInfo(
            string fileName,
            string args)
        {
            return new ProcessStartInfo(fileName)
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            UseShellExecute = true,
                            Arguments = args,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        };
        }

        private static StreamWriter CreateLogFile(string prefix)
        {
            return File.CreateText(
                String.Format(@"d:\atlassian\fisheye\fecru-2.9.2\{0}service.log",
                              prefix));
        }
    }
}
