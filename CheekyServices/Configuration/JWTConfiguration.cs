namespace CheekyServices.Configuration;

/// <summary>
/// Class for dealing with JWT Configuration
/// </summary>
public class JWTConfiguration
{
    public string Key { get; set; }
    
    public string Issuer { get; set; }
    
    public string Audience { get; set; }
}
