using AutoMapper;
using CheekyData.Implementations;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Constants.ExceptionMessageConstants;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.CoreSkillExceptions;
using CheekyServices.Interfaces;

namespace CheekyServices.Implementations;

public class CoreSkillService : ICoreSkillService
{
    private readonly IMapper _mapper;
    private readonly ICoreSkillRepository _coreSkillRepository;

    public CoreSkillService(ICoreSkillRepository coreSkillRepository, IMapper mapper)
    {
        _coreSkillRepository = coreSkillRepository ?? throw new ArgumentNullException(nameof(coreSkillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<CoreSkillDto>> GetAllCoreSkills()
    {
        var coreSkills = await _coreSkillRepository.GetAllCoreSkillsAsync();
        return _mapper.Map<IEnumerable<CoreSkillDto>>(coreSkills);
    }

    public async Task<CoreSkillDto> GetCoreSkillById(Guid coreSkillId)
    {
        var response = await _coreSkillRepository.GetAllAsync(a => a.CoreSkillId == coreSkillId);

        if (response == null)
        {
            throw new CheekyExceptions<CoreSkillNotFoundException>();
        }

        return _mapper.Map<CoreSkillDto>(response);
    }

    public async Task<CoreSkillDto> InsertCoreSkill(CoreSkillDto coreSkillToAdd)
    {
        ArgumentException.ThrowIfNullOrEmpty(coreSkillToAdd.SkillName);
        
        if (await _coreSkillRepository.DoesExistInDb(x => x.CoreSkillId == coreSkillToAdd.CoreSkillId))
        {
            throw new CheekyExceptions<CoreSkillNotFoundException>(CoreSkillExceptionMessages.CoreSkillDuplicateExceptionMessage);
        }

        try
        {
            var coreSkill = _mapper.Map<CoreSkill>(coreSkillToAdd);
            
            var addedCoreSkill = await _coreSkillRepository.AddAsync(coreSkill);

            return coreSkillToAdd;
        }
        catch (Exception ex)
        {
            var text = ex;
            
        }
        return default;
    }

    public async Task<CoreSkillDto> UpdateCoreSkill(CoreSkillDto coreSkillToUpdate)
    {
        var existingCoreSkill = await _coreSkillRepository.GetFirstOrDefault(a => a.CoreSkillId == coreSkillToUpdate.CoreSkillId);
        
        if (existingCoreSkill == null)
        {
            throw new CheekyExceptions<CoreSkillNotFoundException>(CoreSkillExceptionMessages.CoreSkillNotFoundExceptionMessage);
        }

        var updatedCoreSkill = _mapper.Map(coreSkillToUpdate, existingCoreSkill);
        await _coreSkillRepository.UpdateAsync(updatedCoreSkill);

        return coreSkillToUpdate;
    }

    public async Task<CoreSkillDto> DeleteCoreSkill(Guid coreSkillId)
    {
        var existingCoreSkill = await _coreSkillRepository.GetFirstOrDefault(a => a.CoreSkillId == coreSkillId);
        
        if (existingCoreSkill == null)
        {
            throw new CheekyExceptions<CoreSkillNotFoundException>(CoreSkillExceptionMessages.CoreSkillNotFoundExceptionMessage);
        }

        await _coreSkillRepository.DeleteAsync(existingCoreSkill);
        return _mapper.Map<CoreSkillDto>(existingCoreSkill);
    }
}
