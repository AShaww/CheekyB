namespace CheekyB.Metadata;

public class RuleSetMetadata
{
    public List<string> RuleSets { get; }

    public RuleSetMetadata(List<string> ruleSets)
    {
        RuleSets = ruleSets ?? throw new ArgumentNullException(nameof(ruleSets));
    }
}