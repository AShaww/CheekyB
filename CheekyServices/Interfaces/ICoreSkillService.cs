using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface ICoreSkillService
{
    Task<IEnumerable<CoreSkillDto>> GetAllCoreSkills();
    Task<CoreSkillDto> GetCoreSkillById(Guid coreSkillId);
    Task<CoreSkillDto> InsertCoreSkill(CoreSkillDto coreSkillToAdd);
    Task<CoreSkillDto> UpdateCoreSkill(CoreSkillDto coreSkillToUpdate);
    Task<CoreSkillDto> DeleteCoreSkill(Guid coreSkillId);
}