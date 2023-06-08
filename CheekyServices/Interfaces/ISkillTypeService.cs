using CheekyModels.Dtos;
using CheekyModels.Entities;

namespace CheekyServices.Interfaces;
public interface ISkillTypeService
{
    Task<IEnumerable<SkillTypeDto>> GetAllSkillTypes();
    Task<SkillTypeDto> GetSkillTypeById(int skillTypeId);
    Task<SkillTypeDto> InsertSkillType(SkillTypeDto skillTypeToAdd);
    Task<SkillTypeDto> UpdateSkillType(SkillTypeDto skillTypeToUpdate);
    Task<SkillTypeDto> DeleteSkillType(int skillTypeId);
}

