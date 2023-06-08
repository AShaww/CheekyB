using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface ITrainedSkillRepository : IRepository<TrainedSkill>
{
    Task<IEnumerable<TrainedSkill>> GetAllTrainedSkillAsync();
}