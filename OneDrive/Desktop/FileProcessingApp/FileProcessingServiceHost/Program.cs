using System;
using System.ServiceProcess;
using System.IO;
using log4net;
using log4net.Config;


namespace FileProcessingApp
{
    static class Program
    {
        static void Main()
        {
            XmlConfigurator.Configure();

            ILog log = LogManager.GetLogger(typeof(Program));
            try
            {
                File.AppendAllText(@"C:\Logs\service_log.txt", "Main method started...\n");
                log.Info("Service starting...");

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new FileProcessingServiceHost()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Logs\service_error.txt", "Fatal error: " + ex.ToString());
                log.Error("Service failed", ex);
            }                                                      
            
        }
    }
}
