using System.Text.Json;
using System.Text.Json.Serialization;

namespace RegistrationSystem.Controllers.DTOs
{
    public class ErrorDto
    {
        [JsonPropertyName("StatusCode")]
        public int StatusCode { get; set; } 
        [JsonPropertyName("Error")]
        public string Message { get; set; } = null!;
        public override string ToString ( )
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
