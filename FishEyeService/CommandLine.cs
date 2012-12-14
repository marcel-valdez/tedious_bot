using System.Diagnostics;
using System.IO;
using System.Text;

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
            ProcessStartInfo info = getProcessStartInfo(fileName, args);
            RunProcess(info);
        }

        private static void RunProcess(ProcessStartInfo info)
        {            
            string prefix = Path.GetFileNameWithoutExtension(info.FileName);
            FileStream serviceLog = CreateLogFile(prefix);

            using (Process process = Process.Start(info))
            using (StreamReader sr = process.StandardOutput)
            {
                while (!sr.EndOfStream)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(sr.ReadLine());
                    serviceLog.Write(bytes, 0, bytes.Length);
                    if (serviceLog.Length > 5000)
                    {
                        serviceLog.Close();
                        serviceLog = CreateLogFile(prefix);
                    }
                }
            }

            using (serviceLog) { }
        }

        private static ProcessStartInfo getProcessStartInfo(string fileName, string args)
        {
            return new ProcessStartInfo(fileName)
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            UseShellExecute = false,
                            Arguments = args,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        };
        }

        private static FileStream CreateLogFile(string prefix)
        {
            return File.Create(prefix + "service.log", 5000, FileOptions.DeleteOnClose);
        }
    }
}
