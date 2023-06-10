using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetAllSkills();
    Task<IEnumerable<SkillDto>> GetAllSkillsBySkillTypeId(int skillTypeId);
    Task<SkillDto> GetSkillById(Guid skillId);
    Task<SkillDto> InsertSkill(SkillModificationDto skill);
    Task<SkillDto> UpdateSkill(SkillModificationDto skillToUpdate);
    Task<SkillDto> DeleteSkill(Guid skillId);
}
