using System.Linq.Expressions;
using AutoMapper;
using CheekyData.Interfaces;
using CheekyData.Models;
using CheekyModels.Dtos;
using CheekyServices.Exceptions;
using CheekyServices.Implementations;
using CheekyServices.Mappers;
using CheekyTests.Unit.Helper;
using Moq;
using Xunit;

namespace CheekyTests.Unit.Service;

public class UserServiceTests
{
    private readonly IMapper _mapperMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private UserService _userService;

    //reuseable setup bits and pieces for the mock. Generally for the positive test cases. Custom test mocking goes into the tests directly.
    public UserServiceTests()
    {
        // Create example data
        var entities = UsersList.GetUsersList();

        // Setup automapper mock
        var autoMapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<UserProfile>(); });
        _mapperMock = new Mapper(autoMapperConfig);

        // Setup User Repository mock
        _userRepositoryMock = new Mock<IUserRepository>();
        _userRepositoryMock.Setup(s => s.GetAllAsync(x => !x.Archived)).ReturnsAsync(entities);
        _userRepositoryMock.Setup(s => s.GetAllUsersAsync(x => !x.Archived, 1, 10)).ReturnsAsync(entities);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);
        _userRepositoryMock.Setup(s => s.AddAsync(It.IsAny<User>())).ReturnsAsync(entities[0]);
        _userRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<User>())).ReturnsAsync(entities[0]);
        _userRepositoryMock.Setup(s => s.GetNotLoggedUsersAsync(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(entities);
        _userRepositoryMock.Setup(s => s.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(UsersList.GetUsersList()[0]);

        // Set User Service
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);
    }

    #region GetAllUsers cases

    // Positive use case
    [Fact]
    public async Task GetAllUsers_ReturnsListOfUsers()
    {
        // Arrange, Act
        var result = await _userService.GetAllUsers(1, 10);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    // Negative use case - return null from repository
    [Fact]
    public async Task GetAllUsers_WhenPassedNullArgument_ThrowsNullReferenceException()
    {
        //Arrange
        _userRepositoryMock.Setup(s => s.GetAllUsersAsync(x => !x.Archived, 1, 10)).Returns<IRepository<UserDto>>(null);
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Act, Assert

        await Assert.ThrowsAsync<NullReferenceException>(async () => await _userService.GetAllUsers(1, 10));
    }

    // Use case with exception
    [Fact]
    public async Task GetAllUsers_WhenPassedIndexOutOfRangeItem_ThrowsOutOfRangeException()
    {
        //Act Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _userService.GetAllUsers(1, 10).Result.ToList()[2]);
    }

    #endregion

    #region GetById cases

    // Positive use case
    [Fact]
    public async Task GetUserById_WhenPassedValidUserId_ReturnsValidUser()
    {
        // Arrange, Act
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);


        // Set User Service
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        var result = await _userService.GetUserById(user.UserId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.UserId, user.UserId);
    }

    // Negative use case
    [Fact]
    public async Task GetUserById_WhenUserConflictExceptionIsTriggeredInRepository_ThrowsUserNotFoundException()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
            .ThrowsAsync(new CheekyExceptions<UserNotFoundException>());

        // Act
        var exception =
            await Assert.ThrowsAsync<CheekyExceptions<UserNotFoundException>>(() =>
                _userService.GetUserById(user.UserId));

        // Assert
        Assert.IsType<CheekyExceptions<UserNotFoundException>>(exception);
    }

    #endregion

    #region AddAsync cases

    // Positive use case
    [Fact]
    public async Task InsertUser_WhenPassedValidUser_ReturnsValidAddedUser()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);


        // Act
        var result = await _userService.InsertUser(user);

        //Assert
        Assert.IsType<UserDto>(result);
        Assert.NotNull(result);
    }

    // Negative use case 
    [Fact]
    public async Task InsertUser_WhenExceptionIsThrown_ThrowsInternalServerErrorException()
    {
        // Arrange/Act
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.AddAsync(It.IsAny<User>())).ThrowsAsync(new CheekyExceptions<Exception>());

        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Assert
        var exception = await Assert.ThrowsAsync<CheekyExceptions<Exception>>(() => _userService.InsertUser(user));
        Assert.IsType<CheekyExceptions<Exception>>(exception);
    }



    [Fact]
    public async Task InsertAsync_WhenUserConflictExceptionIsTriggeredInRepository_ThrowsUserConflictException()
    {
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);

        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);
        // Act

        var exception =
            await Assert.ThrowsAsync<CheekyExceptions<UserConflictException>>(() => _userService.InsertUser(user));
        // Assert
        Assert.Equal("The user with the specified email already exists.", exception.Message);
    }

    #endregion

    #region UpdateUser cases

    // Positive use case
    [Fact]
    public async Task UpdateUser_WhenPassedValidUser_ReturnsValidUpdatedUser()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>()))
            .Returns(Task.FromResult(true));
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Act
        var result = await _userService.UpdateUser(user);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<UserDto>(result);
    }

    // Negative use case
    [Fact]
    public async Task UpdateUser_WhenUserNotFoundExceptionIsTriggeredInRepository_ThrowsSkillNotFoundException()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
            .ThrowsAsync(new CheekyExceptions<UserNotFoundException>());

        // Act
        var exception =
            await Assert.ThrowsAsync<CheekyExceptions<UserNotFoundException>>(() => _userService.UpdateUser(user));

        // Assert
        Assert.IsType<CheekyExceptions<UserNotFoundException>>(exception);
    }

    [Fact]
    public async Task UpdateUser_WhenExceptionIsTriggeredInRepository_ThrowsInternalServerError()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>()))
            .Returns(Task.FromResult(true));
        _userRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<User>()))
            .ThrowsAsync(new CheekyExceptions<Exception>());
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Act
        var exception = await Assert.ThrowsAsync<CheekyExceptions<Exception>>(() => _userService.UpdateUser(user));
        Assert.IsType<CheekyExceptions<Exception>>(exception);
    }

    #endregion

    #region SoftDeleteUser cases

    // Positive use case
    [Fact]
    public async Task SoftDeleteUser_WhenPassedValidUserId_ThrowsValidDeletedSkill()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Act
        var result = await _userService.SoftDeleteUser(user.UserId);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<UserDto>(result);
    }

    // Negative use case 


    [Fact]
    public async Task SoftDeleteUser_WhenSkillNotFoundExceptionIsTriggeredInRepository_ThrowsUserNotFoundException()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>()))
            .ThrowsAsync(new CheekyExceptions<UserNotFoundException>());

        // Act
        var exception =
            await Assert.ThrowsAsync<CheekyExceptions<UserNotFoundException>>(() =>
                _userService.SoftDeleteUser(user.UserId));

        // Assert
        Assert.IsType<CheekyExceptions<UserNotFoundException>>(exception);
    }

    #endregion

    #region GetNeverLoggedUsers cases

    [Fact]
    public async Task GetNeverLoggedUsers_ReturnsValidList()
    {
        // Act
        var result = await _userService.GetNeverLoggedUsers();

        // Assert
        var users = result as UserDto[] ?? result.ToArray();
        Assert.NotEmpty(users);
        Assert.Equal(2, users.Count());
    }

    #endregion
}