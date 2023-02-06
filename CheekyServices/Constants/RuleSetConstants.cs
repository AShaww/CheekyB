namespace CheekyServices.Constants;

/// <summary>
/// Class for dealing with constants used in rules 
/// </summary>
public static class RuleSetConstants
{
    public const string PutUserRuleSetName = "PutUserValidator";
    
    public static List<string> PutUserRuleSets => new() { PutUserRuleSetName };
}
