namespace CheekyServices.Interfaces;

public interface IAuthService
{
    Task<string> Login(string googleToken);
}
