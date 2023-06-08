using CheekyData.Interfaces;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheekyData.Implementations;

public class CoreSkillRepository : Repository<CoreSkill>, ICoreSkillRepository
{

    public CoreSkillRepository(CheekyContext cheekyContext) : base(cheekyContext)
    {
    }

    public async Task<IEnumerable<CoreSkill>> GetAllCoreSkillsAsync()
    {
        return await _cheekyContext.CoreSkills.ToListAsync();
    }
}