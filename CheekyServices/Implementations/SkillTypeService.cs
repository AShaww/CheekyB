using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Constants.ExceptionMessageConstants;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.SkillTypesExceptions;
using CheekyServices.Interfaces;

namespace CheekyServices.Implementations;

public class SkillTypeService : ISkillTypeService
{
    private readonly IMapper _mapper;
    private readonly ISkillTypeRepository _skillTypeRepository;

    public SkillTypeService(ISkillTypeRepository skillTypeRepository, IMapper mapper)
    {
        _skillTypeRepository = skillTypeRepository ?? throw new ArgumentNullException(nameof(skillTypeRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<SkillTypeDto>> GetAllSkillTypes()
    {
        var skillTypes = await _skillTypeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SkillTypeDto>>(skillTypes);
    }

    public async Task<SkillTypeDto> GetSkillTypeById(int skillTypeId)
    {
        var skillType = await _skillTypeRepository.GetAllAsync(a => a.SkillTypeId == skillTypeId);
       
        if (skillType == null)
        {
            throw new CheekyExceptions<SkillTypeNotFoundException>("The SkillType with the specified ID does not exist.");
        }

        return _mapper.Map<SkillTypeDto>(skillType);
    }

    public async Task<SkillTypeDto> InsertSkillType(SkillTypeDto skillTypeToAdd)
    {
        ArgumentException.ThrowIfNullOrEmpty(skillTypeToAdd.TypeName);

        if (await _skillTypeRepository.DoesExistInDb(x => x.SkillTypeId == skillTypeToAdd.SkillTypeId))
        {
            throw new CheekyExceptions<SkillTypeNotFoundException>(SkillTypeExceptionMessages.SkillTypeDuplicateExceptionMessage);
        }

        try
        {
            var skillType = _mapper.Map<SkillType>(skillTypeToAdd);
            skillType.SkillTypeId = skillTypeToAdd.SkillTypeId;
            
            var addedSkillType = await _skillTypeRepository.AddAsync(skillType);
            skillTypeToAdd = _mapper.Map<SkillTypeDto>(addedSkillType);
            return skillTypeToAdd;

        }
        catch (Exception ex)
        {
            var text = ex;
            
        }
        return default;
    }

    public async Task<SkillTypeDto> UpdateSkillType(SkillTypeDto skillTypeToUpdate)
    {
        var existingSkillType = await _skillTypeRepository.GetFirstOrDefault(a => a.SkillTypeId == skillTypeToUpdate.SkillTypeId);
        
        if (skillTypeToUpdate == null)
        {
            throw new CheekyExceptions<SkillTypeNotFoundException>(SkillTypeExceptionMessages.SkillTypeNotFoundExceptionMessage);;
        }

        existingSkillType = _mapper.Map( skillTypeToUpdate, existingSkillType );
        await _skillTypeRepository.UpdateAsync(existingSkillType);

        return skillTypeToUpdate;
    }

    public async Task<SkillTypeDto> DeleteSkillType(int skillTypeId)
    {
        var existingSkillType = await _skillTypeRepository.GetFirstOrDefault(a => a.SkillTypeId == skillTypeId);
        
        if (existingSkillType == null)
        {
            throw new CheekyExceptions<SkillTypeNotFoundException>(SkillTypeExceptionMessages.SkillTypeNotFoundExceptionMessage);
        }

        await _skillTypeRepository.DeleteAsync(existingSkillType);
        return _mapper.Map<SkillTypeDto>(existingSkillType);
    }
}
