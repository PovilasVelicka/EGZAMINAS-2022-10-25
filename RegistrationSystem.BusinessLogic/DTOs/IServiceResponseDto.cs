namespace RegistrationSystem.BusinessLogic.DTOs
{
    public interface IServiceResponseDto<T>
    {
        bool IsSuccess { get; }
        int StatuCode { get; }
        string Message { get; }
        T? Object { get; }
    }
}
