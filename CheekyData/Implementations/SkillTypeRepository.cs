using CheekyData.Interfaces;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheekyData.Implementations;

public class SkillTypeRepository: Repository<SkillType>, ISkillTypeRepository
{
    public SkillTypeRepository(CheekyContext cheekyContext) : base(cheekyContext) { } 
    
    public async Task<IEnumerable<SkillType>> GetAllSkillTypeAsync()
    {
        return await _cheekyContext.SkillTypes.ToListAsync();
    }
}