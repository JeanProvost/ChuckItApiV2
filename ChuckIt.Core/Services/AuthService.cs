// Ignore Spelling: Auth Dto

using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using ChuckIt.Core.Entities.Users.Dtos;
using ChuckIt.Core.Interfaces.IRepositories;
using ChuckIt.Core.Interfaces.IServices;
using ChuckItApiV2.Core.Entities.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChuckIt.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoProvider;
        private readonly string? _clientId;
        private readonly string? _userPoolId;
        private readonly string? _cognitoClientSecret;
        private readonly IUserRepository _userRepository;


        public AuthService(IConfiguration configuration, IAmazonCognitoIdentityProvider cognitoProvider, IUserRepository userRepository)
        {
            _cognitoProvider = cognitoProvider;
            _clientId = Environment.GetEnvironmentVariable("COGNITO_CLIENT_ID");
            _userPoolId = Environment.GetEnvironmentVariable("COGNITO_USER_POOL_ID");
            _cognitoClientSecret = Environment.GetEnvironmentVariable("COGNITO_CLIENT_SECRET");
            _userRepository = userRepository;
        }

        public async Task<string> RegisterUserAsync(RegisterDto registerDto)
        {
            var userExists = await _userRepository.GetUserByEmail(registerDto.Email);

            if (userExists != null)
            {
                throw new InvalidOperationException($"User already registered with email {registerDto.Email}");
            }

            //Cognito user sign up
            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Username = registerDto.Email,
                Password = registerDto.Password,
                SecretHash = GenerateSecretHash(registerDto.Email, _clientId, _cognitoClientSecret),
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = registerDto.Email},
                    new AttributeType { Name = "given_name", Value = registerDto.FirstName},
                    new AttributeType { Name = "family_name", Value = registerDto.LastName},
                    new AttributeType { Name = "name", Value = $"{registerDto.FirstName} {registerDto.LastName}"}
                }
            };

            var signUpResponse = await _cognitoProvider.SignUpAsync(signUpRequest);

            if (signUpResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Failed to register user {registerDto.Email}");
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email
            };

            await _userRepository.Create(user);

            return "User registered successfully";
        }

        public async Task<ConfirmSignUpResponse> VerifyEmailAsync(VerifyEmailDto verifyRequest)
        {
            var request = new ConfirmSignUpRequest
            {
                ClientId = _clientId,
                Username = verifyRequest.Email,
                ConfirmationCode = verifyRequest.ConfirmationCode,
                SecretHash = GenerateSecretHash(verifyRequest.Email, _clientId, _cognitoClientSecret),
            };

            var response = await _cognitoProvider.ConfirmSignUpAsync(request);

            return response;
        }

        public async Task<ResendConfirmationCodeResponse> ResendConfirmationCodeAsync(string email)
        {
            var request = new ResendConfirmationCodeRequest
            {
                ClientId = _clientId,
                Username = email,
                SecretHash = GenerateSecretHash(email, _clientId, _cognitoClientSecret),
            };

            var response = await _cognitoProvider.ResendConfirmationCodeAsync(request);

            return response;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto login)
        {
            var cognitoProvider = new AmazonCognitoIdentityProviderClient();

            var authRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _clientId,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", login.Email },
                    { "PASSWORD", login.Password },
                    { "SECRET_HASH", GenerateSecretHash(login.Email, _clientId, _cognitoClientSecret) }
                }
            };

            try
            {
                var authResponse = await _cognitoProvider.InitiateAuthAsync(authRequest);

                return new LoginResponseDto
                {
                    AccessToken = authResponse.AuthenticationResult.AccessToken,
                    RefreshToken = authResponse.AuthenticationResult.RefreshToken,
                    ExpiresIn = authResponse.AuthenticationResult.ExpiresIn
                };
            }
            catch (NotAuthorizedException ex)
            {
                throw new Exception($"Invalid login credentials: {ex.Message}");
            }
        }

        private string GenerateSecretHash(string username, string clientId, string clientSecret)
        {
            var dataString = username + clientId;
            var keyBytes = Encoding.UTF8.GetBytes(clientSecret);
            var dataBytes = Encoding.UTF8.GetBytes(dataString);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(dataBytes);
                return Convert.ToBase64String(hashBytes);
            }
        } 

      public Guid GetUserId(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedException("User ID not found in claims");
            var userGuid = Guid.Parse(userId);

            return userGuid;
        }
    }
}
