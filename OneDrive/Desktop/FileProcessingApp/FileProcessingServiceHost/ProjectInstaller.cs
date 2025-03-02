using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
    private ServiceProcessInstaller processInstaller;
    private ServiceInstaller serviceInstaller;

    public ProjectInstaller()
    {
        processInstaller = new ServiceProcessInstaller();
        processInstaller.Account = ServiceAccount.LocalSystem;

        serviceInstaller = new ServiceInstaller();
        serviceInstaller.ServiceName = "FileProcessingServiceHost";
        serviceInstaller.DisplayName = "File Processing Service";
        serviceInstaller.Description = "Processes file requests via WCF REST API.";
        serviceInstaller.StartType = ServiceStartMode.Automatic;

        Installers.Add(processInstaller);
        Installers.Add(serviceInstaller);
    }
}
