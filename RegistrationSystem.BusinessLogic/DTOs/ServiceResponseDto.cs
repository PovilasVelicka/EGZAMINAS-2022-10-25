namespace RegistrationSystem.BusinessLogic.DTOs
{
    internal class ServiceResponseDto<T> : IServiceResponseDto<T>
    {

        public bool IsSuccess { get; }
        public int StatuCode { get; }
        public string Message { get; }
        public T? Object { get; }

        public ServiceResponseDto (string errorMessage)
        {
            IsSuccess = false;
            Message = errorMessage;
            StatuCode = 400;
        }

        public ServiceResponseDto (string message, bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatuCode = isSuccess ? 200 : 400;
        }

        public ServiceResponseDto (T? obj)
        {
            IsSuccess = obj != null;
            Message = IsSuccess ? "" : "Not found";
            Object = obj;
            StatuCode = IsSuccess ? 200 : 404;
        }

        public ServiceResponseDto (T? obj, string message, int statusCode)
        {
            IsSuccess = obj != null;
            Message = message;
            Object = obj;
            StatuCode = statusCode;
        }

    }
}
