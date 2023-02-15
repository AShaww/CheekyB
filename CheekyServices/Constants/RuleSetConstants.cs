namespace CheekyServices.Constants;

/// <summary>
/// Class for dealing with constants used in rules 
/// </summary>
public static class RuleSetConstants
{
    public const string PutUserRuleSetName = "PutUserValidator";
    public const string PutToDoRuleSetName = "PutToDoValidator";
    
    public static List<string> PutUserRuleSets => new() { PutUserRuleSetName };
    public static List<string> PutToDoRuleSets => new() { PutToDoRuleSetName };

}
