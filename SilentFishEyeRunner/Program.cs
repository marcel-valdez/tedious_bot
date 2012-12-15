using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace SilentFishEyeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            //var thread = new Thread(
            /*() =>*/
            Run(@"D:\programs\glassfish3\jdk7\bin\java.exe", 
                @"-Xmx1024m -XX:MaxNewSize=128m -XX:MaxPermSize=256m "+
                @" ""-Dfisheye.library.path="" "+
                @"""-Dfisheye.inst=D:\atlassian\fisheye\fecru-2.9.2\bin\.."""+
                @" -Djava.awt.headless=true "+
                @"""-Djava.endorsed.dirs=D:\atlassian\fisheye\fecru-2.9.2\bin\..\lib\endorsed"" "+
                @"-jar ""D:\atlassian\fisheye\fecru-2.9.2\bin\..\fisheyeboot.jar"" start");
            //{
            //    IsBackground = true
            //};

            //thread.Start();
        }

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
            {
                using (StreamReader sr = process.StandardOutput)
                {
                    while (!process.HasExited)
                    {
                        logWriter.Write(sr.ReadLine());
                        if (++lineCount > maxLines)
                        {
                            logWriter.BaseStream.SetLength(0);
                        }
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
                UseShellExecute = false,
                Arguments = args,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden                
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
