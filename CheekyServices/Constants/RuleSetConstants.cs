namespace CheekyServices.Constants;

/// <summary>
/// Class for dealing with constants used in rules 
/// </summary>
public static class RuleSetConstants
{
    public const string PutToDoRuleSetName = "PutToDoValidator";
    public const string PutSkillRuleSetName = "PutSkillValidator";
    public const string PutUserRuleSetName = "PutUserValidator";
    public const string PutUserPortfolioSetName = "PutUserPortfolioValidator";
    public const string PutUserSkillRuleSetName = "PutUserSkillValidator";
    public static List<string> PutToDoRuleSets => new() { PutToDoRuleSetName };
    public static List<string> PutSkillRuleSets => new() { PutSkillRuleSetName };
    public static List<string> PutUserRuleSets => new() { PutUserRuleSetName };
    public static List<string> PutUserPortfolioRuleSets => new() { PutUserPortfolioSetName };
    public static List<string> PutUserSkillRuleSets => new() { PutUserSkillRuleSetName };
}