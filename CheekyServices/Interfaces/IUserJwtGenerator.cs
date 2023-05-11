using CheekyModels.Dtos;

namespace CheekyServices.Interfaces
{/// <summary>
/// Interface for generating a JWT token, given a UserDto user
/// </summary>
    public interface IUserJwtGenerator
    {/// <summary>
     /// Generate a JWT for the provided UserDto user
     /// </summary>
     /// <param name="user">A UserDto user from which to take information and construct a JWT token</param>
     /// <returns></returns>
        string GenerateToken(UserDto user);
    }
}
