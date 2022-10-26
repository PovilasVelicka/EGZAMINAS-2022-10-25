using RegistrationSystem.BusinessLogic.DTOs;

namespace RegistrationSystem.Controllers.DTOs
{
    public class LoginResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = null!;
        public T? Payload { get; set; } 
        public LoginResponse (IServiceResponseDto<T> responseDto )
        {
            IsSuccess = responseDto.IsSuccess;
            Message = responseDto.Message;
            Payload = responseDto.Object;
        }
    }
}
