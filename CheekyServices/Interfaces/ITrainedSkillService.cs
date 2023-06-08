using AutoMapper;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CheekyServices.Interfaces
{
    public interface ITrainedSkillService
    {
        Task<IEnumerable<TrainedSkillDto>> GetAllTrainedSkills();
        Task<TrainedSkillDto> GetTrainedSkillById(Guid trainedSkillId);
        Task<TrainedSkillDto> InsertTrainedSkill(TrainedSkillDto trainedSkillToAdd);
        Task<TrainedSkillDto> UpdateTrainedSkill(TrainedSkillDto trainedSkillToUpdate);
        Task<TrainedSkillDto> DeleteTrainedSkill(Guid trainedSkillId);
    }
}