using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IUserSkillService
{
    Task<UserSkillViewTableDto> GetAllUserSkills(int pageNumber, int pageSize,string[] skillNames);
    Task<IEnumerable<UserSkillDto>> GetAllUserSkillsByUserId(Guid userId);
    Task<IEnumerable<UserSkillDto>> GetAllUserSkillsByUserIdAndSkillTypeId(Guid userId, int skillTypeId);
    Task<UserSkillDto> InsertUserSkill(UserSkillModificationDto userSkill);
    Task<UserSkillDto> UpdateUserSkill(UserSkillModificationDto userSkill);
    Task<UserSkillDto> DeleteUserSkill(Guid userId, Guid skillId);
}
