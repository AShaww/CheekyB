using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Constants.ExceptionMessageConstants;
using CheekyServices.Exceptions;
using CheekyServices.Exceptions.TrainedSkillExceptions;
using CheekyServices.Interfaces;

namespace CheekyServices.Implementations;

public class TrainedSkillService : ITrainedSkillService
{
    private readonly IMapper _mapper;
    private readonly ITrainedSkillRepository _trainedSkillRepository;

    public TrainedSkillService(ITrainedSkillRepository trainedSkillRepository, IMapper mapper)
    {
        _trainedSkillRepository = trainedSkillRepository ?? throw new ArgumentNullException(nameof(trainedSkillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<TrainedSkillDto>> GetAllTrainedSkills()
    {
        var trainedSkills = await _trainedSkillRepository.GetAllTrainedSkillAsync();
        return _mapper.Map<IEnumerable<TrainedSkillDto>>(trainedSkills);
    }

    public async Task<TrainedSkillDto> GetTrainedSkillById(Guid trainedSkillId)
    {
        var response = await _trainedSkillRepository.GetAllAsync(a => a.TrainedSkillId == trainedSkillId);
       
        if (response == null)
        {
            throw new CheekyExceptions<TrainedSkillNotFoundException>("The ToDo with the specified ID does not exist.");
        }

        return _mapper.Map<TrainedSkillDto>(response);
    }

    public async Task<TrainedSkillDto> InsertTrainedSkill(TrainedSkillDto trainedSkillToAdd)
    {
        var trainedSkill = _mapper.Map<TrainedSkill>(trainedSkillToAdd);
        var addedTrainedSkill = await _trainedSkillRepository.AddAsync(trainedSkill);
        return _mapper.Map<TrainedSkillDto>(addedTrainedSkill);
    }

    public async Task<TrainedSkillDto> UpdateTrainedSkill(TrainedSkillDto trainedSkillToUpdate)
    {
        var existingTrainedSkill = await _trainedSkillRepository.GetFirstOrDefault(a => a.TrainedSkillId == trainedSkillToUpdate.TrainedSkillId);
       
        if (trainedSkillToUpdate == null)
        {
            throw new CheekyExceptions<TrainedSkillNotFoundException>(TrainedSkillExceptionMessages.TrainedSkillNotFoundExceptionMessage);
        }

        existingTrainedSkill = _mapper.Map(trainedSkillToUpdate, existingTrainedSkill);
        await _trainedSkillRepository.UpdateAsync(existingTrainedSkill);

        return trainedSkillToUpdate;
    }

    public async Task<TrainedSkillDto> DeleteTrainedSkill(Guid trainedSkillId)
    {
        var existingTrainedSkill = await _trainedSkillRepository.GetFirstOrDefault(a => a.TrainedSkillId == trainedSkillId);
        if (existingTrainedSkill == null)
        {
            throw new CheekyExceptions<TrainedSkillNotFoundException>(TrainedSkillExceptionMessages.TrainedSkillNotFoundExceptionMessage);
        }

        await _trainedSkillRepository.DeleteAsync(existingTrainedSkill);
        return _mapper.Map<TrainedSkillDto>(existingTrainedSkill);
    }
}
