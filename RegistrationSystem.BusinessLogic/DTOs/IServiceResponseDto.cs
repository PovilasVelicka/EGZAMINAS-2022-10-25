using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
