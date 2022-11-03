using RegistrationSystem.BusinessLogic.DTOs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RegistrationSystem.Controllers.DTOs
{
    public class SuccessResponse<T> : ErrorResponse
    {
        [JsonPropertyName("data")]
        public T? Payload { get; set; }

        public SuccessResponse ( ) { }

        public SuccessResponse (IServiceResponseDto<T> responseDto)
        {
            IsSuccess = responseDto.IsSuccess;
            Message = responseDto.Message;
            StatusCode = responseDto.StatusCode;
            Payload = responseDto.Object;
        }

        public override string ToString ( )
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
