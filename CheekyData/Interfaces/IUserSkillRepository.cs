using System.Linq.Expressions;
using CheekyModels.Dtos;
using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface IUserSkillRepository : IRepository<UserSkill>
{
    Task<UserSkillViewTableDto> GetAllUserSkills(Expression<Func<UserSkill, bool>> predicate, int pageNumber,
        int pageSize,string[] skillNames);
    Task<IEnumerable<UserSkill>> GetUserSkillsByPredicate(Expression<Func<UserSkill, bool>> predicate);
}
