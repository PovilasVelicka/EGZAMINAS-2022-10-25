using System.Text.Json;
using System.Text.Json.Serialization;

namespace RegistrationSystem.Controllers.DTOs
{
    public class ErrorResponse
    {
        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }
        [JsonPropertyName("status")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;
        public override string ToString ( )
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
