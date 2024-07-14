using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Users.Register
{
    public record RegistrationResponse(bool Flag, string Message = null!);
}
