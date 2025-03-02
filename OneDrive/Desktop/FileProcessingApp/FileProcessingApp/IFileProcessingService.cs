// FileProcessingApp.IFileProcessingService.cs (Service Contract)
using System.ServiceModel;
using System.ServiceModel.Web;

namespace FileProcessingApp
{
    [ServiceContract]
    public interface IFileProcessingService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetFileData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetFileData(FileRequest request);

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "/GetFileData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void HandleOptionsRequest();
    }

}