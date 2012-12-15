using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace FishEyeService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            EventLogInstaller installer = FindInstaller(this.Installers);
            if (installer != null)
            {
                // enter your event log name here
                installer.Log = "FishEyeServiceLog";
            }
        }

        private static EventLogInstaller FindInstaller(InstallerCollection installers)
        {
            foreach (Installer installer in installers)
            {
                if (installer is EventLogInstaller)
                {
                    return (EventLogInstaller)installer;
                }

                EventLogInstaller eventLogInstaller = 
                    FindInstaller(installer.Installers);
                if (eventLogInstaller != null)
                {
                    return eventLogInstaller;
                }
            }

            return null;
        }
    }
}
