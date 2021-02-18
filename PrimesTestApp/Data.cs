using System.Text.Json.Serialization;

namespace PrimesTestApp
{
    class Data
    {
        public Data()
        {
        }

        [JsonPropertyName("baseAddress")]
        public string BaseAddress{get;set;}
    }
}
