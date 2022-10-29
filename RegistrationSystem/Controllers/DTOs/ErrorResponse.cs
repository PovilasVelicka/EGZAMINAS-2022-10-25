using System.Text.Json;
using System.Text.Json.Serialization;

namespace RegistrationSystem.Controllers.DTOs
{
    public class ErrorResponse
    {
        [JsonPropertyName("status")]
        public int StatusCode { get; set; }
        [JsonPropertyName("error")]
        public string Message { get; set; } = null!;
        public override string ToString ( )
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
