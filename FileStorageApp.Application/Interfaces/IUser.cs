using FileStorageApp.Application.Users.Login;
using FileStorageApp.Application.Users.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorageApp.Application.Interfaces
{
    public interface IUser
    {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registrationDTO);
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}
