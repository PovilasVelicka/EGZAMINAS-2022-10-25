namespace RegistrationSystem.BusinessLogic.DTOs
{
    public interface IServiceResponseDto<T>
    {
        bool IsSuccess { get; }
        int StatusCode { get; }
        string Message { get; }
        T? Object { get; }
    }
}
