namespace CheekyServices.Configuration;

public sealed class SerilogOptionsConfigurations
{
    public bool UseConsole { get; set; }
    public bool UseApplicationInsight { get; set; }
    public string SeqUrl { get; set; }
    public string LogTemplate { get; set; }
}