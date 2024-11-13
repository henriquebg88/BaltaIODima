using System.Text.Json.Serialization;

namespace Dima.Core.Responses
{
    public class Response<TData>
    {
        private readonly int _statusCode;

        //Evitar erro na hora da serialização
        [JsonConstructor]
        public Response()
        {
            _statusCode = Configurations.DefaultStatusCode;
        }

        public Response(TData? data, int statusCode = Configurations.DefaultStatusCode, string? message = null )
        {
            this.data = data;
            this.message = message;
            _statusCode = statusCode;
        }

        public TData? data { get; set; }
        public string? message { get; set; }
        [JsonIgnore]
        public bool isSuccess => _statusCode is >= 200 and <= 299;

    }
}