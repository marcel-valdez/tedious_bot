namespace FishEyeService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fishEyeServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.fishEyeServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // fishEyeServiceProcessInstaller
            // 
            this.fishEyeServiceProcessInstaller.Password = null;
            this.fishEyeServiceProcessInstaller.Username = null;
            // 
            // fishEyeServiceInstaller
            // 
            this.fishEyeServiceInstaller.Description = "Fisheye + Crucible 64-bit used as a service.";
            this.fishEyeServiceInstaller.DisplayName = "Fisheye + Crucible";
            this.fishEyeServiceInstaller.ServiceName = "Fisheye";
            this.fishEyeServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.fishEyeServiceProcessInstaller,
            this.fishEyeServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller fishEyeServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller fishEyeServiceInstaller;
    }
}