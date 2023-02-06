using AutoMapper;
using CheekyData.Interfaces;
using CheekyData.Models;
using CheekyModels.Dtos;
using CheekyServices.Constants;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;

namespace CheekyServices.Implementations;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    #region GetAllUsers

    /// <inheritdoc/>
    public async Task<IEnumerable<UserDto>> GetAllUsers(int pageNumber, int pageSize)
    {
        var users = await _userRepository.GetAllUsersAsync(x => !x.Archived, pageNumber, pageSize);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    #endregion

    #region GetUserById

    /// <inheritdoc/>
    public async Task<UserDto> GetUserById(Guid userId)
    {
        var user = await _userRepository.GetFirstOrDefault(x => x.UserId == userId);
        if (user == null)
        {
            throw new CheekyExceptions<UserNotFoundException>(UserExceptionMessages.UserNotFoundExceptionMessage);
        }

        return _mapper.Map<UserDto>(user);
    }

    #endregion

    #region InsertUser

    /// <inheritdoc/>
    public async Task<UserDto> InsertUser(UserDto user)
    {
        ArgumentException.ThrowIfNullOrEmpty(user.FirstName);
        ArgumentException.ThrowIfNullOrEmpty(user.Surname);
        ArgumentException.ThrowIfNullOrEmpty(user.Email);
        //does user already exist
        if (await _userRepository.DoesExistInDb(x => x.Email == user.Email))
        {
            throw new CheekyExceptions<UserConflictException>(UserExceptionMessages.UserDuplicateExceptionMessage);
        }

        var userToAdd = _mapper.Map<User>(user);
        userToAdd.CreatedOn = DateTime.UtcNow;
        userToAdd.ModifiedOn = DateTime.UtcNow;
        userToAdd.LoginDate = user.LoginDate ?? default;
        var addedUser = await _userRepository.AddAsync(userToAdd);
        user = _mapper.Map<UserDto>(addedUser);
        user.UserId = userToAdd.UserId;
        return user;
    }

    #endregion

    #region UpdateUser

    /// <inheritdoc/>
    public async Task<UserDto> UpdateUser(UserDto user)
    {
        ArgumentException.ThrowIfNullOrEmpty(user.FirstName);
        ArgumentException.ThrowIfNullOrEmpty(user.Surname);
        ArgumentException.ThrowIfNullOrEmpty(user.Email);

        var userToUpdate = await _userRepository.GetFirstOrDefault(a => a.UserId == user.UserId);
        if (userToUpdate == null)
        {
            throw new CheekyExceptions<UserNotFoundException>(UserExceptionMessages.UserNotFoundExceptionMessage);
        }

        userToUpdate.ModifiedOn = DateTime.UtcNow;

        userToUpdate = _mapper.Map(user, userToUpdate);

        await _userRepository.UpdateAsync(userToUpdate);

        return user;
    }

    #endregion

    #region SoftDeleteUser

    /// <inheritdoc/>
    public async Task<UserDto> SoftDeleteUser(Guid userId)
    {
        var userToSoftDelete = await _userRepository.GetFirstOrDefault(a => a.UserId == userId);
        if (userToSoftDelete == null)
        {
            throw new CheekyExceptions<UserNotFoundException>(UserExceptionMessages.UserNotFoundExceptionMessage);
        }

        userToSoftDelete.ModifiedOn = DateTime.UtcNow;
        userToSoftDelete.ArchivedOn = DateTime.UtcNow;
        userToSoftDelete.Archived = true;

        await _userRepository.UpdateAsync(userToSoftDelete);
        return _mapper.Map<UserDto>(userToSoftDelete);
    }

    #endregion

    #region GetNeverLoggedUsers

    /// <inheritdoc/>
    public async Task<IEnumerable<UserDto>> GetNeverLoggedUsers()
    {
        var users = await _userRepository.GetNotLoggedUsersAsync(x => x.LoginDate == default && !x.Archived);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    #endregion

    #region LoginByGoogleUser

    /// <inheritdoc/>
    public async Task<UserDto> LoginByGoogleUser(GoogleUserDto googleUser)
    {
        var user = await _userRepository.GetFirstOrDefault(x => x.Email == googleUser.Email);
        if (user == null)
        {
            var userToInsert = _mapper.Map<UserDto>(googleUser);
            userToInsert.LoginDate = DateTime.UtcNow;
            return await InsertUser(userToInsert);
        }

        user.LoginDate = DateTime.UtcNow;
        user.GoogleUserId = googleUser.GoogleUserId;

        await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserDto>(user);
    }

    #endregion
}