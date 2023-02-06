using CheekyModels.Dtos;

namespace CheekyServices.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Gets all users ,filtered by pageSize and pageNumber 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<UserDto>> GetAllUsers(int pageNumber, int pageSize);

    /// <summary>
    /// Gets a User by UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<UserDto> GetUserById(Guid userId);

    /// <summary>
    /// Inserts a User
    /// </summary>
    /// <param name="userToAdd"></param>
    /// <returns></returns>
    Task<UserDto> InsertUser(UserDto userToAdd);

    /// <summary>
    /// Update a User based on a UserId
    /// </summary>
    /// <param name="userToUpdate"></param>
    /// <returns></returns>
    Task<UserDto> UpdateUser(UserDto userToUpdate);

    /// <summary>
    /// Delete a User based on a UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<UserDto> SoftDeleteUser(Guid userId);

    /// <summary>
    /// Get a list of never LoggedUsers
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<UserDto>> GetNeverLoggedUsers();

    /// <summary>
    /// Get a list of Users logedInByGoogle
    /// </summary>
    /// <param name="googleUser"></param>
    /// <returns></returns>
    Task<UserDto> LoginByGoogleUser(GoogleUserDto googleUser);
}