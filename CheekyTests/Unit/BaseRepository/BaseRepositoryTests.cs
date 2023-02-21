using CheekyData.Interfaces;
using CheekyData.Models;
using CheekyModels.Dtos;
using CheekyTests.Unit.Common;
using CheekyTests.Unit.Helper;
using Moq;
using Xunit;

namespace CheekyTests.Unit.BaseRepository;

public class BaseRepositoryTests
{
    private readonly Mock<IRepository<User>> _mockRepository;
    private IRepository<User> _repository;

    public BaseRepositoryTests()
    {

        var mock = new Mock<IRepository<User>>();
        // Setup User Repository mock
        _mockRepository = new MockedAsyncRepository<User>(UsersList.GetUsersList()).GetRepository();
        _repository = _mockRepository.Object;
    }

    #region GetAllAsync

    // Positive use case
    [Fact]
    public async Task GetAllAsync_ReturnsListOfUsers()
    {
        // Arrange, Act
        var result = await _repository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.True(result.Count() > 0);
    }

    // Negative use case - return null from repository
    [Fact]
    public async Task GetAllAsync_WhenPassedNullArgument_ReturnsNullReferenceException()
    {
        //Arrange
        _mockRepository.Setup(s => s.GetAllAsync()).Returns<IRepository<UserDto>>(null);
        _repository = _mockRepository.Object;

        // Act, Assert
        await Assert.ThrowsAsync<NullReferenceException>(async () => await _repository.GetAllAsync());
    }

    // Use case with exception
    [Fact]
    public async Task GetAllAsync_WhenPassedIndexOfOutOfRangeItem_ThrowsOutOfRangeException()
    {
        // Act, Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _repository.GetAllAsync().Result.ToList()[2]);
    }

    #endregion

    #region AddAsync cases

    // Positive use case
    [Fact]
    public async Task AddAsync_WhenPassedValidUser_ReturnsNotNullResult()
    {
        // Arrange
        var user = UsersList.GetUsersList()[0];
        _mockRepository.Setup(s => s.AddAsync(user)).Returns(Task.FromResult(user));
        _repository = _mockRepository.Object;

        // Act
        var result = await _repository.AddAsync(user);

        //Assert
        Assert.NotNull(result);
    }

    // Negative use case 
    [Fact]
    public async Task AddAsync_WhenPassedNullArgument_ReturnsNull()
    {
        // Arrange
        var user = UsersList.GetUsersList()[0];
        _mockRepository.Setup(s => s.AddAsync(user)).Returns(Task.FromResult(user));
        _repository = _mockRepository.Object;
        user = null;

        // Act, Assert
        Assert.Null(await _repository.AddAsync(user));
    }

    #endregion

    #region UpdateAsync cases

    // Positive use case
    [Fact]
    public async Task UpdateAsync_WhenPassedValidUser_ReturnsNotNullResponse()
    {
        // Arrange
        var user = UsersList.GetUsersList()[0];
        _mockRepository.Setup(s => s.UpdateAsync(user)).Returns(Task.FromResult(user));
        _repository = _mockRepository.Object;

        /// Act
        var result = await _repository.UpdateAsync(user);

        //Assert
        Assert.NotNull(result);
    }

    // Negative use case 
    [Fact]
    public async Task UpdateAsync_WhenPassedNullArgument_ReturnsNull()
    {
        // Act
        var result = await _repository.UpdateAsync(null);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region DeleteAsync cases

    // Positive use case
    [Fact]
    public async Task DeleteAsync_WhenPassedValidUser_ReturnsCorrectUserResponse()
    {
        // Arrange
        var user = UsersList.GetUsersList()[0];
        _mockRepository.Setup(s => s.DeleteAsync(user)).Returns(Task.FromResult(user));
        _repository = _mockRepository.Object;

        /// Act
        var result = await _repository.DeleteAsync(user);

        //Assert
        Assert.NotNull(result);
    }

    // Negative use case 
    [Fact]
    public async Task DeleteAsync_WhenPassedNullArgument_ReturnsNull()
    {
        // Act
        var result = await _repository.UpdateAsync(null);

        //Assert
        Assert.Null(result);
    }

    #endregion

    #region GetFirstOrDefault

    // Positive use case
    [Fact]
    public async Task GetFirstOrDefault_WhenPassedValidId_ReturnsUser()
    {
        // Arrange, Act
        var result = await _repository.GetFirstOrDefault(a => a.UserId == UsersList.GetUsersList()[0].UserId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.UserId, UsersList.GetUsersList()[0].UserId);
    }

    // Negative use case
    [Fact]
    public async Task GetFirstOrDefault_WhenPassedInvalidGuid_ReturnsNull()
    {
        //Act
        var result = await _repository.GetFirstOrDefault(a => a.UserId == Guid.NewGuid());

        //Assert
        Assert.Null(result);
    }

    // Negative use case
    [Fact]
    public async Task GetFirstOrDefault_WhenPassedNullArgument_ReturnsNull()
    {
        // Arrange
        User? user = null;
        _mockRepository.Setup(s => s.GetFirstOrDefault(a => a.UserId == UsersList.GetUsersList()[0].UserId)).Returns(Task.FromResult(user));
        _repository = _mockRepository.Object;
        //Act
        var result = await _repository.GetFirstOrDefault(a => a.UserId == UsersList.GetUsersList()[0].UserId);

        //Assert
        Assert.Null(result);
    }

    #endregion
}