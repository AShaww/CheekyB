using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface ISkillService
{
    /// <summary>
    /// Return all Skills in a list 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<SkillDto>> GetAllSkills();
    /// <summary>
    /// Return all Skills based on a SkillType in a list
    /// </summary>
    /// <param name="skillTypeId"></param>
    /// <returns></returns>
    Task<IEnumerable<SkillDto>> GetAllSkillsBySkillTypeId(int skillTypeId);
    // <summary>
    /// Returns a Skill based on a specific SkillId 
    /// </summary>
    /// <param name="skillId"></param>
    /// <returns></returns>
    /// <exception cref="NavyPottleException{SkillNotFoundException}"></exception>
    Task<SkillDto> GetSkillById(Guid skillId);
    /// <summary>
    /// Inserts a Skill into the db 
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    /// <exception cref="NavyPottleException{SkillConflictException}"></exception>
    Task<SkillDto> InsertSkill(SkillModificationDto skill);
    /// <summary>
    /// Updates an already existing Skill based on Id
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    /// <exception cref="NavyPottleException{SkillNotFoundException}"></exception>
    /// <exception cref="NavyPottleException{SkillConflictException}"></exception>
    Task<SkillDto> UpdateSkill(SkillModificationDto skillToUpdate);
    /// <summary>
    /// Delete Skill based on SkillId 
    /// </summary>
    /// <param name="skillId"></param>
    /// <returns></returns>
    /// <exception cref="NavyPottleException{SkillNotFoundException}"></exception>
    Task<SkillDto> DeleteSkill(Guid skillId);
}
