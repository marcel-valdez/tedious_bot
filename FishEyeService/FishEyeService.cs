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
                CommandLine.Run("bin/start.bat");
            });
        }

        protected override void OnStop()
        {
            Task.Factory.StartNew(() =>
            {
                CommandLine.Run("bin/stop.bat");
            });
        }
    }
}
