namespace RegistrationSystem.BusinessLogic.DTOs
{
    public interface IUserInfoDto
    {
        string? FirstName { get; }
        string? LastName { get; }
        string? PersonalCode { get; }
        string? Phone { get; }
        string? Email { get; }
        byte[ ]? Photo { get; }
        string? City { get; }
        string? Street { get; }
        string? HouseNumber { get; }
        string? AppartmentNumber { get; }
        bool IsAllPropertiesNotEmpty ( );
    }
}
