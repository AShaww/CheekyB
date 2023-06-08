using System.Linq.Expressions;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheekyData.Implementations;

public class UserSkillRepository : Repository<UserSkill>, IUserSkillRepository
{
    public UserSkillRepository(CheekyContext cheekyContext)
        : base(cheekyContext)
    {
    }

    public async Task<UserSkillViewTableDto> GetAllUserSkills(
        Expression<Func<UserSkill, bool>> predicate, int pageNumber, int pageSize, string[] skillNames)
    {

        var pagedData = await _cheekyContext.UserSkills
            .Include(x => x.Rating)
            .Include(x => x.Skill)
            .Include(x => x.User)
            .Where(predicate)
            .GroupBy(c => new { Name = string.Concat(c.User.FirstName, " ", c.User.Surname), Id = c.UserId })
            .Select(g => new UserSkillViewDto
                {
                    LastEvaluated = g.OrderByDescending(le => le.LastEvaluated).FirstOrDefault().LastEvaluated,
                    Username = g.Key.Name,
                    UserId = g.Key.Id,
                    Skills = g.Select(s => s.Skill.SkillName).ToList()
                }
            )
            .ToListAsync()
            .ConfigureAwait(false);
        
        var data = new UserSkillViewTableDto(pagedData, pagedData.Count,(pagedData.Count + pageSize - 1) / pageSize, pageNumber);
        
        if (skillNames.Length is 0)
        {
            data.UserSkillData = data.UserSkillData
                .OrderBy(x => x.Username)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return data;
        }

        var filteredSkills = data.UserSkillData
            .Where(x => x.Skills.Any(skillNames.Contains))
            .OrderBy(x => x.Username)
            .ToList();

        data = new UserSkillViewTableDto(filteredSkills.Skip((pageNumber - 1) * pageSize).Take(pageSize), filteredSkills.Count,(filteredSkills.Count + pageSize - 1) / pageSize, pageNumber);

        return data;
    }

    public async Task<IEnumerable<UserSkill>> GetUserSkillsByPredicate(Expression<Func<UserSkill, bool>> predicate)
        => await _cheekyContext
            .Set<UserSkill>()
            .Include(x => x.User)
            .Include(x => x.Skill)
            .Include(x => x.Rating)
            .Where(predicate)
            .ToListAsync().ConfigureAwait(false);
}
