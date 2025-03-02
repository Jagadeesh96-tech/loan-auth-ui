using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using FileProcessingApp;
using System.IO;
using System.ServiceModel.Web;


namespace FileProcessingApp
{
    public partial class FileProcessingServiceHost : ServiceBase
    {
       // private ServiceHost serviceHost = null;
        private ServiceHost _host;

        public FileProcessingServiceHost()
        {
            this.ServiceName = "FileProcessingServiceHost";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                File.AppendAllText(@"C:\Logs\service_log.txt", "Service starting...\n");

                Uri baseAddress = new Uri("http://localhost:8500/FileProcessingApp/");
                _host = new ServiceHost(typeof(FileProcessingService));

                if (_host.Description.Endpoints.Count == 0)
                {
                    ServiceEndpoint endpoint = _host.AddServiceEndpoint(
                        typeof(IFileProcessingService), new WebHttpBinding(), "");

                    endpoint.Behaviors.Add(new WebHttpBehavior());
                }

                _host.Open();
                File.AppendAllText(@"C:\Logs\service_log.txt", "Service started successfully.\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Logs\service_error.txt", ex.ToString());
            }
        }


        protected override void OnStop()
        {
            try
            {
                if (_host != null)
                {
                    _host.Close();
                    _host = null;
                }
                File.AppendAllText(@"C:\Logs\service_log.txt", "Service stopped.\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\Logs\service_error.txt", "Error stopping service: " + ex.ToString());
            }
        }
    }
}
