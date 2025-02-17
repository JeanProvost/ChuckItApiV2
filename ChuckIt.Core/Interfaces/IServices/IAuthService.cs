using Amazon.CognitoIdentityProvider.Model;
using ChuckIt.Core.Entities.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterDto registerDto);
        Task<ResendConfirmationCodeResponse> ResendConfirmationCodeAsync(string email);
        Task<ConfirmSignUpResponse> VerifyEmailAsync(VerifyEmailDto verifyRequest);
        Task<LoginResponseDto> LoginAsync(LoginDto login);
    }
}
