using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Constants.ExceptionMessageConstants;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;
using NavyPottleServices.Exceptions;
using Serilog;

namespace CheekyServices.Implementations;
/// <summary>
/// Service for dealing with users 
/// </summary>
public class UserSkillService : IUserSkillService
{
    private readonly IMapper _mapper;
    private readonly IUserSkillRepository _userSkillRepository;
    
    public UserSkillService(IUserSkillRepository userSkillRepository, IMapper mapper)
    {
        _userSkillRepository = userSkillRepository ?? throw new ArgumentNullException(nameof(userSkillRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #region GetAllUserSkillsByUserId

    public async Task<UserSkillViewTableDto> GetAllUserSkills(int pageNumber, int pageSize, string[] skillNames)
    {
        var userSkill = await _userSkillRepository.GetAllUserSkills(x=>!x.User.Archived , pageNumber, pageSize, skillNames);

        return _mapper.Map<UserSkillViewTableDto>(userSkill);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<UserSkillDto>> GetAllUserSkillsByUserId(Guid userId)
    {
        var userSkills = await _userSkillRepository.GetAllAsync(x => x.UserId == userId);
        return _mapper.Map<IEnumerable<UserSkillDto>>(userSkills);
    }
    
    #endregion

    #region GetAllUserSkillsByUserIdAndSkillTypeId
    /// <inheritdoc/>
    public async Task<IEnumerable<UserSkillDto>> GetAllUserSkillsByUserIdAndSkillTypeId(Guid userId, int skillTypeId)
    {
        var userSkills = await _userSkillRepository.GetAllAsync(x => x.UserId == userId && x.Skill.SkillTypeId == skillTypeId);
        return _mapper.Map<IEnumerable<UserSkillDto>>(userSkills);
    }
    #endregion

    #region InsertUserSkill
    /// <inheritdoc/>
    public async Task<UserSkillDto> InsertUserSkill(UserSkillModificationDto userSkill)
    {
        var userSkillToAdd = _mapper.Map<UserSkill>(userSkill);

        //does user skill already exist
        if (await _userSkillRepository.DoesExistInDb(x => x.UserId == userSkill.UserId && x.SkillId == userSkill.SkillId))
        {
            Log.Error($"{UserSkillExceptionMessages.UserSkillDuplicateExceptionMessage} {userSkill.UserId}");
            throw new CheekyExceptions<UserSkillConflictException>(UserSkillExceptionMessages.UserSkillDuplicateExceptionMessage);
        }

        var addedUserSkill = await _userSkillRepository.AddAsync(userSkillToAdd);
        var userSkillIncludingRating = await _userSkillRepository.GetUserSkillsByPredicate(c => c.UserId == addedUserSkill.UserId && c.SkillId == addedUserSkill.SkillId && c.RatingId == addedUserSkill.RatingId);

        return _mapper.Map<UserSkillDto>(userSkillIncludingRating.FirstOrDefault());

    }
    #endregion

    #region UpdateUserSkill
    /// <inheritdoc/>
    public async Task<UserSkillDto> UpdateUserSkill(UserSkillModificationDto userSkill)
    {
        var userSkillToUpdate = await _userSkillRepository.GetFirstOrDefault(c => c.UserId == userSkill.UserId && c.SkillId == userSkill.SkillId);

        if (userSkillToUpdate is null)
        {
            Log.Error($"{UserExceptionMessages.UserNotFoundExceptionMessage} or {SkillExceptionMessages.SkillNotFoundExceptionMessage} {userSkill.UserId}");
            throw new CheekyExceptions<UserSkillConflictException>($"{UserExceptionMessages.UserNotFoundExceptionMessage} or {SkillExceptionMessages.SkillNotFoundExceptionMessage}");
        }

        userSkillToUpdate.RatingId = userSkill.RatingId;
        userSkillToUpdate.LastEvaluated = DateTime.UtcNow;
        userSkillToUpdate = _mapper.Map(userSkill, userSkillToUpdate);
        await _userSkillRepository.UpdateAsync(userSkillToUpdate);

        var userSkillIncludingRating = await _userSkillRepository.GetUserSkillsByPredicate(c => c.UserId == userSkill.UserId && c.SkillId == userSkill.SkillId && c.RatingId == userSkill.RatingId);
        return _mapper.Map<UserSkillDto>(userSkillIncludingRating.FirstOrDefault());
    }
    #endregion

    #region DeleteUserSkill
    /// <inheritdoc/>
    public async Task<UserSkillDto> DeleteUserSkill(Guid userId, Guid skillId)
    {
        var userSkillToDelete = await _userSkillRepository.GetFirstOrDefault(c => c.UserId == userId && c.SkillId == skillId);
        
        if (userSkillToDelete is null)
        {
            Log.Error($"{UserExceptionMessages.UserNotFoundExceptionMessage} or {SkillExceptionMessages.SkillNotFoundExceptionMessage} {skillId}");
            throw new CheekyExceptions<UserSkillConflictException>($"{UserExceptionMessages.UserNotFoundExceptionMessage} or {SkillExceptionMessages.SkillNotFoundExceptionMessage}");
        }
        
        await _userSkillRepository.DeleteAsync(userSkillToDelete);
        return _mapper.Map<UserSkillDto>(userSkillToDelete);
    } 
    #endregion

}
