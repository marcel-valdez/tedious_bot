using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace FishEyeService
{
    public partial class FishEyeService : ServiceBase
    {
        public FishEyeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Call start.bat
            Task.Factory.StartNew(() =>
            {
                CommandLine.Run(
                    @"D:\atlassian\fisheye\fecru-2.9.2\bin\start.bat");
                    //AppDomain.CurrentDomain.BaseDirectory + @"\bin\start.bat");

            });
        }

        protected override void OnStop()
        {
            Task.Factory.StartNew(() =>
            {
                CommandLine.Run(
                    @"d:\atlassian\fisheye\fecru-2.9.2\bin\stop.bat");
            });
        }
    }
}
