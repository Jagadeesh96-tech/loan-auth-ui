// FileProcessingApp.FileProcessingService.svc.cs (Implementation)
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace FileProcessingApp
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FileProcessingService : IFileProcessingService
    {
        public string GetFileData(FileRequest request)
        {
            try
            {
                // Add CORS headers for every response
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type");

                // Validate API Name
                if (request.ApiName != "ValidAPI")
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return "Invalid API Name";
                }

                // Validate File Path
                if (!File.Exists(request.FilePath))
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return "File not found";
                }

                // Read and return file content
                return File.ReadAllText(request.FilePath);
            }
            catch (Exception ex)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return $"Error: {ex.Message}";
            }
        }

        public void HandleOptionsRequest()
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        }
    }

}
