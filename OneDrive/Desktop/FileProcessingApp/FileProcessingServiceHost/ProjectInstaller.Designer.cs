using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FileProcessingServiceHost
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        private void InitializeComponent()
        {
            this.serviceProcessInstaller = new ServiceProcessInstaller();
            this.serviceInstaller = new ServiceInstaller();

            // Set the account type the service runs under
            this.serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

            // Configure the service installer
            this.serviceInstaller.ServiceName = "FileProcessingServiceHost";
            this.serviceInstaller.DisplayName = "File Processing Service Host";
            this.serviceInstaller.Description = "A Windows service to host the File Processing WCF service.";
            this.serviceInstaller.StartType = ServiceStartMode.Automatic;

            // Add both installers to the Installers collection
            this.Installers.AddRange(new Installer[] { this.serviceProcessInstaller, this.serviceInstaller });
        }
    }
}
