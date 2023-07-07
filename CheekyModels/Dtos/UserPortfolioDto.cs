namespace CheekyModels.Dtos
{
    public record UserPortfolioDto
    {
        public Guid UserPortfolioId { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? DateJoined { get; set; }
        public string IntroductionMessage { get; set; }
        public string CV { get; set; }
        public decimal? Capacity { get; set; }
    }
}