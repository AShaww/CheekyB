using System.Linq.Expressions;
using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface ISkillRepository : IRepository<Skill>
{
    Task<IEnumerable<Skill>> GetAllSkills();
    Task<IEnumerable<Skill>> GetSkillsByPredicate(Expression<Func<Skill, bool>> predicate);

}
