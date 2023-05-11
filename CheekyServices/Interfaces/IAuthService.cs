namespace CheekyServices.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Allows user to log in 
    /// </summary>
    /// <param name="googleToken"></param>
    /// <returns></returns>
    /// <exception cref="SecurityTokenExpiredException"></exception>
    Task<string> Login(string token);
}