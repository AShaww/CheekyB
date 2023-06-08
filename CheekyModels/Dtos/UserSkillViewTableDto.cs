namespace CheekyModels.Dtos;

public class UserSkillViewTableDto
{
    public UserSkillViewTableDto(IEnumerable<UserSkillViewDto> userSkillData,int totalRecords,int totalPages,int currentPage)
    {
        UserSkillData = userSkillData;
        TotalRecords = totalRecords;
        TotalPages = totalPages;
        CurrentPage = currentPage;
    }

    public IEnumerable<UserSkillViewDto> UserSkillData { get; set; }

    public int TotalRecords { get; private set; }
    
    public int TotalPages { get;  private set; }
    
    public int CurrentPage { get; private set; }
}
