using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Constants.ExceptionMessageConstants;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.SkillExceptions;
using CheekyServices.Interfaces;
using Serilog;

namespace CheekyServices.Implementations;

/// <summary>
/// service for dealing with skills
/// </summary>
public class SkillService : ISkillService
{
   
    private readonly ISkillRepository _skillRepository;
    private readonly IMapper _mapper;

    public SkillService(ISkillRepository skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    #region GetAllSkills
    /// <inheritdoc/>
    public async Task<IEnumerable<SkillDto>> GetAllSkills()
    {
        var skills = await _skillRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SkillDto>>(skills);
    }
    #endregion

    #region GetAllSkillsBySkillTypeId
    /// <inheritdoc/>
    public async Task<IEnumerable<SkillDto>> GetAllSkillsBySkillTypeId(int skillTypeId)
    {
        var skills = await _skillRepository.GetSkillsByPredicate(x => x.SkillTypeId == skillTypeId);
        
        if (skills == null)
        {
            Log.Error($"{SkillExceptionMessages.SkillNotFoundExceptionMessage} {skillTypeId}");
            throw new CheekyExceptions<SkillNotFoundException>(SkillExceptionMessages.SkillNotFoundExceptionMessage);
        }

        return _mapper.Map<IEnumerable<SkillDto>>(skills);

    }
    #endregion

    #region GetSkillBySkillId
    /// <inheritdoc/>
    public async Task<SkillDto> GetSkillById(Guid skillId)
    {
        var skill = await _skillRepository.GetSkillsByPredicate(x => x.SkillId == skillId);
        
        if (skill == null || !skill.Any())
        {
            Log.Error($"{SkillExceptionMessages.SkillNotFoundExceptionMessage} {skillId}");
            throw new CheekyExceptions<SkillNotFoundException>(SkillExceptionMessages.SkillNotFoundExceptionMessage);
        }

        return _mapper.Map<SkillDto>(skill.FirstOrDefault());
    }
    #endregion

    #region InsertSkill
    /// <inheritdoc/>
    public async Task<SkillDto> InsertSkill(SkillModificationDto skill)
    {
        ArgumentException.ThrowIfNullOrEmpty(skill.SkillName);

        //does skill already exist
        if (await _skillRepository.DoesExistInDb(x => x.SkillName == skill.SkillName))
        {
            Log.Error($"{SkillExceptionMessages.SkillDuplicateExceptionMessage} {skill.SkillName}");
            throw new CheekyExceptions<SkillConflictException>(SkillExceptionMessages.SkillDuplicateExceptionMessage);
        }
        
        var skillToAdd = _mapper.Map<Skill>(skill);
        var addedSkill = await _skillRepository.AddAsync(skillToAdd);
        var skillIncludingSkillType = await _skillRepository.GetSkillsByPredicate(x => x.SkillId == addedSkill.SkillId);
        
        return _mapper.Map<SkillDto>(skillIncludingSkillType.FirstOrDefault());

    }
    #endregion

    #region UpdateSkill
    /// <inheritdoc/>
    public async Task<SkillDto> UpdateSkill(SkillModificationDto skill)
    {
        ArgumentException.ThrowIfNullOrEmpty(skill.SkillName);

        var skillToUpdate = await _skillRepository.GetFirstOrDefault(a => a.SkillId == skill.SkillId);
        //As we are doing checks on the insert to ensure we don't insert skills with the same name we need to have a check for the ID and a check for the skill name here
        if (skillToUpdate == null)
        {
            Log.Error($"{SkillExceptionMessages.SkillNotFoundExceptionMessage} {skill.SkillId}");
            throw new CheekyExceptions<SkillNotFoundException>(SkillExceptionMessages.SkillNotFoundExceptionMessage);
        }

        if (await _skillRepository.DoesExistInDb(x => x.SkillName == skill.SkillName))
        {
            Log.Error($"{SkillExceptionMessages.SkillDuplicateExceptionMessage} {skill.SkillName}");
            throw new CheekyExceptions<SkillConflictException>(SkillExceptionMessages.SkillDuplicateExceptionMessage);
        }

        skillToUpdate = _mapper.Map(skill, skillToUpdate);

        await _skillRepository.UpdateAsync(skillToUpdate);

        var skillIncludingSkillType = await _skillRepository.GetSkillsByPredicate(x => x.SkillId == skillToUpdate.SkillId);
        return _mapper.Map<SkillDto>(skillIncludingSkillType.FirstOrDefault());

    }
    #endregion

    #region DeleteSkill
    /// <inheritdoc/>
    public async Task<SkillDto> DeleteSkill(Guid skillId)
    {
        var skillToDelete = await _skillRepository.GetFirstOrDefault(a => a.SkillId == skillId);
        if (skillToDelete == null)
        {
            Log.Error($"{SkillExceptionMessages.SkillNotFoundExceptionMessage} {skillId}");
            throw new CheekyExceptions<SkillNotFoundException>(SkillExceptionMessages.SkillNotFoundExceptionMessage);
        }
        await _skillRepository.DeleteAsync(skillToDelete);
        return _mapper.Map<SkillDto>(skillToDelete);
    } 
    #endregion
}
