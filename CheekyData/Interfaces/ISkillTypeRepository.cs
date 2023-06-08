using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface ISkillTypeRepository : IRepository<SkillType>
{
    Task<IEnumerable<SkillType>> GetAllSkillTypeAsync();
}