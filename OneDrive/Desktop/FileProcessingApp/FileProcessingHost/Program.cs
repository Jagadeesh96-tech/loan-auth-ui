using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using FileProcessingApp;
using System.ServiceModel.Web;



namespace FileProcessingApp
{
    class FileProcessingHost
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8500/FileProcessingApp/");
            using (ServiceHost host = new ServiceHost(typeof(FileProcessingService), baseAddress))
            {
                try
                {
                    ServiceEndpoint endpoint = host.AddServiceEndpoint(
                        typeof(IFileProcessingService), new WebHttpBinding(), "");

                    endpoint.Behaviors.Add(new WebHttpBehavior());

                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior { HttpGetEnabled = true };
                    host.Description.Behaviors.Add(smb);

                    host.Open();
                    Console.WriteLine("Service is running at: " + baseAddress);
                    Console.WriteLine("Press Enter to exit...");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
