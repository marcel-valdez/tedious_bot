using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FishEyeService
{
    public sealed class Program
    {
        private const string SVC_NAME = "Fisheye";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.Write("Running with args: ");
            foreach (string arg in args)
            {
                Console.Write("<" + arg + "> ");
            }

            Console.WriteLine();

            string serviceName = SVC_NAME;

            if (args.Length == 0)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new FishEyeService() };
                ServiceBase.Run(ServicesToRun);
            }
            else if (args.Length >= 1)
            {
                if (args.Length == 2)
                {
                    serviceName = args[1];
                }

                switch (args[0])
                {
                    case "-install":
                        InstallService(serviceName);
                        StartService(serviceName);
                        break;
                    case "-uninstall":
                        StopService(serviceName);
                        UninstallService(serviceName);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private static bool IsInstalled(string serviceName)
        {
            using (ServiceController controller =
                new ServiceController(serviceName))
            {
                try
                {
                    ServiceControllerStatus status = controller.Status;
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        private static bool IsRunning(string serviceName)
        {
            using (ServiceController controller =
                new ServiceController(serviceName))
            {
                if (!IsInstalled(serviceName))
                {
                    return false;
                }

                return (controller.Status == ServiceControllerStatus.Running);
            }
        }

        private static AssemblyInstaller GetInstaller()
        {
            var installer = new AssemblyInstaller(
                typeof(FishEyeService).Assembly,
                null)
            {
                UseNewContext = true
            };

            return installer;
        }

        private static void InstallService(string serviceName)
        {
            Console.WriteLine("Attempting to install the service.");

            if (IsInstalled(serviceName))
            {
                Console.WriteLine("Service is already installed.");
                return;
            }

            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Install(state);
                        installer.Commit(state);
                    }
                    catch
                    {
                        try
                        {
                            installer.Rollback(state);
                        }
                        catch
                        {
                        }

                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void UninstallService(string fisheye)
        {
            Console.WriteLine("Attempting to uninstall the service.");

            if (!IsInstalled(fisheye))
            {
                Console.WriteLine("Service is already uninstalled.");
                return;
            }
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Uninstall(state);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private static void StartService(string serviceName)
        {
            if (!IsInstalled(serviceName))
            {
                return;
            }

            using (ServiceController controller =
                new ServiceController(serviceName))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                        controller.WaitForStatus(
                            ServiceControllerStatus.Running,
                            TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private static void StopService(string serviceName)
        {
            if (!IsInstalled(serviceName))
            {
                return;
            }

            using (var controller = new ServiceController(serviceName))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Stopped)
                    {
                        controller.Stop();
                        controller.WaitForStatus(
                            ServiceControllerStatus.Stopped,
                            TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

    }
}
