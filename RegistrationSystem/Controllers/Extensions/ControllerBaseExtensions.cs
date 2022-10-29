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

        public static ObjectResult MapServiceDto<K, T> (this ControllerBase controller, IServiceResponseDto<K> responseDto, T? responseObj)
        {
            ObjectResult objectResult;

            if (responseDto.IsSuccess)
            {
                objectResult = new(
                    new SuccessResponse<T>
                    {
                        IsSuccess = responseDto.IsSuccess,
                        StatusCode = responseDto.StatuCode,
                        Message = responseDto.Message,
                        Payload = responseObj
                    });
            }
            else
            {
                objectResult = new(
                  new ErrorResponse
                  {
                      IsSuccess = responseDto.IsSuccess,
                      StatusCode = responseDto.StatuCode,
                      Message = responseDto.Message,
                  });
            }
           
            objectResult.StatusCode = responseDto.StatuCode;
            return objectResult;
        }
    }
}
