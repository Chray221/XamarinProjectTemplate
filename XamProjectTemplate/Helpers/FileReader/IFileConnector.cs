using Newtonsoft.Json.Linq;

namespace XamProjectTemplate.Helpers.FileReader
{
    public interface IFileConnector
    {
        void ReceiveJSONData(JObject jsonData, int wsType);
        void ReceiveError(string title, string error, int wsType);
    }
}
