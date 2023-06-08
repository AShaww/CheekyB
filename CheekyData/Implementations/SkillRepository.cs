using System.Linq.Expressions;
using CheekyData;
using CheekyData.Implementations;
using CheekyData.Interfaces;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace NavyPottleData.Implementation;

public class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(CheekyContext cheekyContext)
        : base(cheekyContext) { }

    public async Task<IEnumerable<Skill>> GetAllSkills()
    {
        return await _cheekyContext.Skills
            .Include(x => x.SkillType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Skill>> GetSkillsByPredicate(Expression<Func<Skill, bool>> predicate)
        => await _cheekyContext.Set<Skill>().Include(x => x.SkillType).Where(predicate).ToListAsync().ConfigureAwait(false);

}
