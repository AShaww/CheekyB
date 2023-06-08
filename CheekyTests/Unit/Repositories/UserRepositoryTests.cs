using System.Linq.Expressions;
using CheekyData.Interfaces;
using CheekyModels.Entities;
using CheekyTests.Unit.Helper;
using Moq;
using Xunit;

namespace CheekyTests.Unit.Repositories;

[Trait("User Repository", "Unit")]
public class UserRepositoryTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private IUserRepository _userRepository;

    public UserRepositoryTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);
        _userRepositoryMock.Setup(s => s.GetNotLoggedUsersAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(UsersList.GetUsersList());
        _userRepository = _userRepositoryMock.Object;
    }

    #region DoesUserExist cases

    // Positive use case 
    [Fact]
    public async Task DoesExistInDb_WhenPassedValidUser_ReturnsTrue()
    {
        //Arrange
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);
        _userRepository = _userRepositoryMock.Object;

        // Act
        var result = await _userRepository.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>());

        //Assert
        Assert.True(result);
    }


    #endregion

    #region GetNotLoggedUsersAsync cases

    // Positive use case
    [Fact]
    public async Task GetNotLoggedUsersAsync_WhenPassedPredicate_ReturnsUsersList()
    {
        // Act
        var result = await _userRepository.GetNotLoggedUsersAsync(x => x.LoginDate == default);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    // Negative use case
    [Fact]
    public async Task GetNotLoggedUsersAsync_WhenPassedNullArgument_ThrowsNullReferenceException()
    {
        //Arrange
        _userRepositoryMock.Setup(s => s.GetNotLoggedUsersAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(null as IEnumerable<User>);
        _userRepository = _userRepositoryMock.Object;


        // Act, Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
            (Task)_userRepository.GetNotLoggedUsersAsync(x => x.LoginDate == null).Result);
    }
}

#endregion