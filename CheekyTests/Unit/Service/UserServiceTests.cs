using System;
using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using CheekyData.Interfaces;
using CheekyModels.Dtos;
using CheekyModels.Entities;
using CheekyServices.Exceptions;
using CheekyServices.Implementations;
using CheekyServices.Interfaces;
using CheekyServices.Mappers;
using CheekyTests.Unit.Helper;
using Moq;
using Xunit;

namespace CheekyTests.Unit.Service;

[Trait("User Service", "Unit")]
public class UserServiceTests
{
    private readonly IMapper _mapperMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private UserService _userService;
    private readonly Mock<IUserPortfolioService> _userPortfolioServiceMock;

    
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
        _userRepositoryMock.Setup(a => a.GetAllUsersAsync(x => !x.Archived, 1, 10));
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);
        _userRepositoryMock.Setup(s => s.AddAsync(It.IsAny<User>())).ReturnsAsync(entities[0]);
        _userRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<User>())).ReturnsAsync(entities[0]);
        _userRepositoryMock.Setup(s => s.GetNotLoggedUsersAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(entities);
        _userRepositoryMock.Setup(s => s.GetFirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(UsersList.GetUsersList()[0]);

        // Setup UserPortfolioService mock
        _userPortfolioServiceMock = new Mock<IUserPortfolioService>();
        
        // Set User Service
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);
    }

    #region GetAllAsync cases

    // Positive use case
    [Fact]
    public async Task GetAllAsync_ReturnsListOfUsers()
    {
        // Arrange, Act

        var result = await _userService.GetAllUsers(1, 10);
        
        //Assert
       Assert.NotNull(result);
       Assert.True(result.Any());
    }

    // Negative use case - return null from repository
    [Fact]
    public async Task GetAllAsync_ReturnsNullReferenceException()
    {
        //Arrange
        _userRepositoryMock.Setup(s => s.GetAllAsync(x => !x.Archived)).Returns<IRepository<UserDto>>(null);
        
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Act, Assert
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _userService.GetAllUsers(1, 10));
    }

    // Use case with exception
    [Fact]
    public async Task GetAllAsync_ThrowsOutOfRangeException()
    {
        //Act Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _userService.GetAllUsers(1,10).Result.ToList()[2]);
    }

    #endregion

    #region GetById cases

    // Positive use case
    [Fact]
    public async Task GetUserById_Returns_ValidUser()
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
    public async Task GetUserById_Returns_Not_Found_Message()
    {
        // Arrange, Act

        // Set User Service
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);

        //Assert

        var exception = await Assert.ThrowsAsync<CheekyExceptions<UserNotFoundException>>(() => _userService.GetUserById(user.UserId));

        Assert.Equal("The user with the specified ID does not exist.", exception.Message);
    }

    #endregion

    #region AddAsync cases

    // Positive use case
    [Fact]
    public async Task AddAsync_ReturnsValidUserDtoObject()
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
    public async Task AddAsync_ReturnsInternalServerError()
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
    public async Task InsertAsync_Returns_Conflict_Message()
    {
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);

        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);
        // Act

        var exception = await Assert.ThrowsAsync<CheekyExceptions<UserConflictException>>(() => _userService.InsertUser(user));
        // Assert
        Assert.Equal("The user with the specified email already exists.", exception.Message);
    }

    #endregion

    #region UpdateAsync cases

    // Positive use case
    [Fact]
    public async Task UpdateAsync_Returns_ValidUserDtoObject()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).Returns(Task.FromResult(true));
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Act
        var result = await _userService.UpdateUser(user);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<UserDto>(result);
    }

    // Negative use case
    [Fact]
    public async Task UpdateAsync_Returns_NotFound_Message()
    {
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);

        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);
        // Act
        var exception = await Assert.ThrowsAsync<CheekyExceptions<UserNotFoundException>>(() => _userService.UpdateUser(user));

        // Assert
        Assert.Equal("The user with the specified ID does not exist.", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsInternalServerError()
    {
        // Arrange
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);
        _userRepositoryMock.Setup(s => s.DoesExistInDb(It.IsAny<Expression<Func<User, bool>>>())).Returns(Task.FromResult(true));
        _userRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<User>())).ThrowsAsync(new CheekyExceptions<Exception>());
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        // Act
        var exception = await Assert.ThrowsAsync<CheekyExceptions<Exception>>(() => _userService.UpdateUser(user));
        Assert.IsType<CheekyExceptions<Exception>>(exception);
    }

    #endregion

    #region SoftDeleteUser cases

    // Positive use case
    [Fact]
    public async Task DeleteAsync_Returns_ValidUserDtoObject()
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
    public async Task SoftDeleteAsync_Returns_NotFound_Message()
    {
        var user = _mapperMock.Map<UserDto>(UsersList.GetUsersList()[0]);

        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);
        // Act
        var exception = await Assert.ThrowsAsync<CheekyExceptions<UserNotFoundException>>(() => _userService.SoftDeleteUser(user.UserId));

        // Assert
        Assert.Equal("The user with the specified ID does not exist.", exception.Message);
    }


    [Fact]
    public async Task DeleteAsync_Returns_NotFound_Message()
    {
        // Arrange
        var user = new User();

        // Act, Assert
        _userRepositoryMock.Setup(s => s.DeleteAsync(user)).Returns<IRepository<UserDto>>(null);
        _userService = new UserService(_userRepositoryMock.Object, _mapperMock);

        var exception = await Assert.ThrowsAsync<CheekyExceptions<UserNotFoundException>>(() => _userService.SoftDeleteUser(user.UserId));

        Assert.Equal("The user with the specified ID does not exist.", exception.Message);
    }

    #endregion

    #region GetNeverLoggedUsers cases

    [Fact]
    public async Task GetNeverLoggedUsers_Returns_Result()
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