using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface ICoreSkillRepository: IRepository<CoreSkill>
{
    Task<IEnumerable<CoreSkill>> GetAllCoreSkillsAsync();
}