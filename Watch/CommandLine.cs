using System.Diagnostics;
using System.IO;

namespace CodeGeneration
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
        public static string Run(string fileName, string args)
        {
            ProcessStartInfo info = GetProcessStartInfo(fileName, args);
            string returnvalue = string.Empty;

            using (Process process = Process.Start(info))
            {
                StreamReader sr = process.StandardOutput;
                returnvalue = sr.ReadToEnd();
            }

            return returnvalue;
        }

        /// <summary>
        /// Starts the command line interpreter and executes the commands. 
        /// The return value of the command will be returned. 
        /// </summary>
        /// <param name="command">Command which should be executed, like nslookup www.code-in.net</param>
        /// <returns></returns>
        public static string RunCmd(string command)
        {
            ProcessStartInfo info = GetProcessStartInfo("cmd", string.Empty);
            //info.UseShellExecute = false;            
            string returnvalue = string.Empty;
            
            using (Process process = Process.Start(info))
            {
                StreamWriter sw = process.StandardInput;
                StreamReader sr = process.StandardOutput;
                sw.WriteLine(command);
                sw.Close();
                returnvalue = sr.ReadToEnd();
            }

            return returnvalue;
        }

        private static ProcessStartInfo GetProcessStartInfo(string fileName, string args)
        {
            var info = new ProcessStartInfo(fileName)
                        {
                            UseShellExecute = false,
                            Arguments = args,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            CreateNoWindow = false
                        };

            return info;
        }
    }
}
