using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IUserSkillService
{
    /// <summary>
    /// Gets all user skills
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="skillNames"></param>
    /// <returns></returns>
    Task<UserSkillViewTableDto> GetAllUserSkills(int pageNumber, int pageSize,string[] skillNames);

    /// <summary>
    /// Gets all user skills based on a User Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<UserSkillDto>> GetAllUserSkillsByUserId(Guid userId);

    /// <summary>
    /// Gets all user skills based on a User Id & Skill Type Id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="skillTypeId"></param>
    /// <returns></returns>
    Task<IEnumerable<UserSkillDto>> GetAllUserSkillsByUserIdAndSkillTypeId(Guid userId, int skillTypeId);

    /// <summary>
    /// Inserts a new  User Skills
    /// </summary>
    /// <param name="userSkill"></param>
    /// <returns></returns>
    Task<UserSkillDto> InsertUserSkill(UserSkillModificationDto userSkill);

    /// <summary>
    /// Updates a User Skill based on a User Id
    /// </summary>
    /// <param name="userSkill"></param>
    /// <returns></returns>
    Task<UserSkillDto> UpdateUserSkill(UserSkillModificationDto userSkill);

    /// <summary>
    /// Deletes a User Skills based on a User Id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="skillId"></param>
    /// <returns></returns>
    Task<UserSkillDto> DeleteUserSkill(Guid userId, Guid skillId);
}
