
// FileProcessingApp.DataContracts.cs (Data Contracts)
using System.Runtime.Serialization;

namespace FileProcessingApp
{
    [DataContract]
    public class FileRequest
    {
        [DataMember]
        public string ApiName { get; set; }

        [DataMember]
        public string FilePath { get; set; }
    }
}