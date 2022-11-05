using Microsoft.AspNetCore.Mvc;
using RegistrationSystem.BusinessLogic.DTOs;
using RegistrationSystem.Controllers.DTOs;
using System.Security.Claims;

namespace RegistrationSystem.Controllers.Extensions
{
    internal static class ControllerBaseExeption
    {
        public static Guid GetUserGuid (this ControllerBase controller)
        {
            var userIdClaim = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) { return Guid.Empty; }

            if (Guid.TryParse(userIdClaim.Value, out Guid UserId))
            {
                return UserId;
            }
            else
            {
                throw new KeyNotFoundException($"ControllerBaseExeption, no GuidId found in Claim NameIdentifier");
            }
        }

        public static ObjectResult MapServiceDto<K, T> (this ControllerBase controller, IServiceResponseDto<K> serviceResponseDto, T? controllerResponseData)
        {
            ObjectResult objectResult;

            if (serviceResponseDto.IsSuccess)
            {
                objectResult = new(
                    new SuccessResponse<T>
                    {
                        IsSuccess = serviceResponseDto.IsSuccess,
                        StatusCode = serviceResponseDto.StatusCode,
                        Message = serviceResponseDto.Message,
                        Payload = controllerResponseData
                    });
            }
            else
            {
                objectResult = new(
                  new ErrorResponse
                  {
                      IsSuccess = serviceResponseDto.IsSuccess,
                      StatusCode = serviceResponseDto.StatusCode,
                      Message = serviceResponseDto.Message,
                  });
            }

            objectResult.StatusCode = serviceResponseDto.StatusCode;
            return objectResult;
        }
    }
}
