namespace TrafficMonitorService
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
            this.TrafficServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.TrafficMonitorServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // TrafficServiceProcessInstaller
            // 
            this.TrafficServiceProcessInstaller.Password = null;
            this.TrafficServiceProcessInstaller.Username = null;
            // 
            // TrafficMonitorServiceInstaller
            // 
            this.TrafficMonitorServiceInstaller.ServiceName = "TrafficMonitorService";
            this.TrafficMonitorServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TrafficServiceProcessInstaller,
            this.TrafficMonitorServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller TrafficServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller TrafficMonitorServiceInstaller;
    }
}