using CheekyData.Interfaces;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheekyData.Implementations;

public class TrainedSkillRepository : Repository<TrainedSkill>, ITrainedSkillRepository
{
    public TrainedSkillRepository(CheekyContext cheekyContext) : base(cheekyContext)
    {
    }

    public async Task<IEnumerable<TrainedSkill>> GetAllTrainedSkillAsync()
    {
        return await _cheekyContext.TrainedSkills.ToListAsync();
    }
}

