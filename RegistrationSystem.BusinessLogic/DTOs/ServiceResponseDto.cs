using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("RegistrationSystemTests")]

namespace RegistrationSystem.BusinessLogic.DTOs
{
    internal class ServiceResponseDto<T> : IServiceResponseDto<T>
    {

        public bool IsSuccess { get; }
        public int StatusCode { get; }
        public string Message { get; }
        public T? Object { get; }

        public ServiceResponseDto (string errorMessage)
        {
            IsSuccess = false;
            Message = errorMessage;
            StatusCode = 400;
        }

        public ServiceResponseDto (string message, bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = isSuccess ? 200 : 400;
        }

        public ServiceResponseDto (T? obj)
        {
            IsSuccess = obj != null;
            Message = IsSuccess ? "" : "Not found";
            Object = obj;
            StatusCode = IsSuccess ? 200 : 404;
        }

        public ServiceResponseDto (T? obj, string message, int statusCode)
        {
            IsSuccess = obj != null;
            Message = message;
            Object = obj;
            StatusCode = statusCode;
        }

    }
}
