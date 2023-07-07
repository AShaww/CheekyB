namespace CheekyModels.Entities;

public class UserPortfolio
{
    public Guid UserPortfolioId { get; set; }

    public Guid UserId { get; set; }
    
    public string JobTitle { get; set; }

    public DateTime? DateJoined { get; set; }

    public string IntroductionMessage { get; set; }

    public string CV { get; set; }

    public decimal? Capacity { get; set; }

    public virtual User User { get; set; }
}

