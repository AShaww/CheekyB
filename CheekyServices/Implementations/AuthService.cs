using AutoMapper;
using CheekyModels.Dtos;
using CheekyServices.Configuration;
using CheekyServices.Interfaces;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;

namespace CheekyServices.Implementations;

    public class AuthService : IAuthService
    {
        private readonly GoogleAuthConfiguration _googleAuthConfig;
        private readonly IUserJwtGenerator _userJwtGenerator;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthService(GoogleAuthConfiguration configuration, IUserJwtGenerator userJwtGenerator, IUserService userService, IMapper mapper)
        {
            _googleAuthConfig = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _userJwtGenerator = userJwtGenerator ?? throw new ArgumentNullException(nameof(userJwtGenerator));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        
        #region Login
        /// <inheritdoc/>
        public async Task<string> Login(string googleToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleToken,
                    new GoogleJsonWebSignature.ValidationSettings { Audience = new[] { _googleAuthConfig.ClientId } });

                var userDto = await _userService.LoginByGoogleUser(_mapper.Map<GoogleUserDto>(payload));

                var jwtToken = _userJwtGenerator.GenerateToken(userDto);

                // check if the token is expired
                if (_userJwtGenerator.IsTokenExpired(jwtToken))
                {
                    // handle expired token
                    throw new Exception("Token is expired.");
                }
                
                return jwtToken;
            }
            catch (Exception e)
            {
                throw new SecurityTokenExpiredException(e.Message);
            }
        }
        #endregion
        
    }