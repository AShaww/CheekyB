using CheekyB.Endpoints;
using CheekyModels.Dtos;
using CheekyServices.Exceptions;
using CheekyServices.Interfaces;
using CheekyTests.Unit.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Xunit;

namespace CheekyTests.Unit.Endpoints
{
    [Trait("User Endpoint", "Unit")]
    public class UserEndpointsTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly DefaultHttpContext _httpContext;
        private readonly List<UserDto> _userListDtos;

        public UserEndpointsTests()
        {
            _userListDtos = UsersDtoList.GetUsersDtoList();

            _userServiceMock = new Mock<IUserService>();

            _httpContext = new DefaultHttpContext
            {
                Request = { Path = "/User", Scheme = "http", Host = HostString.FromUriComponent("localhost") }
            };
        }

        //Positive use case
        [Fact]
        public async Task GetAllUsersAsync_Returns200Ok()
        {
            // Arrange
            _userServiceMock.Setup(a => a.GetAllUsers(1, 10)).ReturnsAsync(new List<UserDto>());

            //Act
            var result = await UserEndpoints.GetAllUsers(_userServiceMock.Object, 1, 10);

            //assert
            var typedResult = Assert.IsType<Ok<IEnumerable<UserDto>>>(result);
            Assert.Equal(StatusCodes.Status200OK, typedResult.StatusCode);
        }

        ////Negative use case
        [Fact]
        public async Task
            GetAllUsersAsync_WhenExceptionIsThrownInUserService_Throws500InternalServerErrorException()
        {
            // Arrange
            _userServiceMock.Setup(a => a.GetAllUsers(1, 10)).ThrowsAsync(new Exception());

            //Act
            var result = await UserEndpoints.GetAllUsers(_userServiceMock.Object, 1, 10);


            var typedResult = Assert.IsType<ProblemHttpResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, typedResult.StatusCode);
        }

        // Positive use case
        [Fact]
        public async Task GetUserById_WhenPassedValidUserId_Returns200Ok()
        {
            // Arrange
            var userId = _userListDtos[0];
            _userServiceMock.Setup(a => a.GetUserById(It.IsAny<Guid>())).ReturnsAsync(userId);

            // Act
            var result = await UserEndpoints.GetUserById(_userServiceMock.Object, userId.UserId);


            // Assert
            var typedResult = Assert.IsType<Ok<UserDto>>(result);
            Assert.Equal(StatusCodes.Status200OK, typedResult.StatusCode);
        }

        //Negative use case
        [Fact]
        public async Task GetUserById_WhenUserNotFoundExceptionIsThrownInUserService_Throws404NotFoundException()
        {
            // Arrange
            _userServiceMock.Setup(a => a.GetUserById(It.IsAny<Guid>()))
                .ThrowsAsync(new CheekyExceptions<UserNotFoundException>());

            // Act
            var result = await UserEndpoints.GetUserById(_userServiceMock.Object, Guid.NewGuid());

            // Assert
            var typedResult = Assert.IsType<NotFound<string>>(result);
            Assert.Equal(StatusCodes.Status404NotFound, typedResult.StatusCode);
        }

        [Fact]
        public async Task GetUserById_Returns500_ServerErrorException()
        {
            // Arrange
            _userServiceMock.Setup(a => a.GetUserById(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            // Act
            var result = await UserEndpoints.GetUserById(_userServiceMock.Object, Guid.NewGuid());

            // Assert
            var typedResult = Assert.IsType<ProblemHttpResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, typedResult.StatusCode);
        }

        // Positive use case
        [Fact]
        public async Task InsertUser_WhenPassedValidUser_Returns201Created()
        {
            // Arrange
            var userToAdd = _userListDtos[0];
            _userServiceMock.Setup(a => a.InsertUser(It.IsAny<UserDto>())).ReturnsAsync(userToAdd);

            // Act

            var result = await UserEndpoints.InsertUser(_httpContext, _userServiceMock.Object, userToAdd);

            // Assert
            var typedResult = Assert.IsType<Created<UserDto>>(result);
            Assert.Equal(StatusCodes.Status201Created, typedResult.StatusCode);
            Assert.Equal("http://localhost/User/c6d48587-6f80-4b97-b7ac-7b73a3956ef1", typedResult.Location);
        }

        //Negative use case
        [Fact]
        public async Task InsertUser_WhenUserConflictExceptionIsThrownInUserService_Throws409ConflictException()
        {
            // Arrange
            _userServiceMock.Setup(a => a.InsertUser(It.IsAny<UserDto>()))
                .ThrowsAsync(new CheekyExceptions<UserConflictException>());

            // Act
            var userToAdd = new UserDto();
            var result = await UserEndpoints.InsertUser(_httpContext, _userServiceMock.Object, userToAdd);

            // Arrange
            var typedResult = Assert.IsType<Conflict<string>>(result);
            Assert.Equal(StatusCodes.Status409Conflict, typedResult.StatusCode);
        }

        [Fact]
        public async Task InsertUser_WhenExceptionIsThrownInUserService_Throws500InternalServerErrorException()
        {
            // Arrange
            _userServiceMock.Setup(a => a.InsertUser(It.IsAny<UserDto>())).ThrowsAsync(new Exception());

            // Act
            var userToAdd = new UserDto();
            var result = await UserEndpoints.InsertUser(_httpContext, _userServiceMock.Object, userToAdd);

            //Arrange
            var typedResult = Assert.IsType<ProblemHttpResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, typedResult.StatusCode);
        }

        // Positive use case
        [Fact]
        public async Task UpdateUser_WhenPassedValidUser_Returns200Ok()
        {
            // Arrange
            var userToUpdate = _userListDtos[0];
            _userServiceMock.Setup(a => a.UpdateUser(It.IsAny<UserDto>())).ReturnsAsync(userToUpdate);

            // Act

            var result = await UserEndpoints.UpdateUser(_userServiceMock.Object, userToUpdate);

            // Assert
            var typedResult = Assert.IsType<Ok<UserDto>>(result);
            Assert.Equal(StatusCodes.Status200OK, typedResult.StatusCode);
        }

        //Negative use case

        [Fact]
        public async Task UpdateUser_WhenUserNotFoundExceptionIsThrownInUserService_Throws404NotFoundException()
        {
            // Act
            _userServiceMock.Setup(a => a.UpdateUser(It.IsAny<UserDto>()))
                .ThrowsAsync(new CheekyExceptions<UserNotFoundException>());

            // Arrange
            var userToUpdate = new UserDto();
            var result = await UserEndpoints.UpdateUser(_userServiceMock.Object, userToUpdate);

            // Assert
            var typedResult = Assert.IsType<NotFound<string>>(result);
            Assert.Equal(StatusCodes.Status404NotFound, typedResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_WhenUserConflictExceptionIsThrownInUserService_Throws400ConflictException()
        {
            // Act
            _userServiceMock.Setup(a => a.UpdateUser(It.IsAny<UserDto>()))
                .ThrowsAsync(new CheekyExceptions<UserConflictException>());

            // Arrange
            var userToUpdate = new UserDto();
            var result = await UserEndpoints.UpdateUser(_userServiceMock.Object, userToUpdate);

            // Assert
            var typedResult = Assert.IsType<Conflict<string>>(result);
            Assert.Equal(StatusCodes.Status409Conflict, typedResult.StatusCode);
        }

        // Positive use case
        [Fact]
        public async Task SoftDeleteUser_WhenPassedValidUserId_Returns200Ok()
        {
            //Act
            var userToDelete = _userListDtos[0];
            _userServiceMock.Setup(a => a.SoftDeleteUser(It.IsAny<Guid>())).ReturnsAsync(userToDelete);

            // Arrange

            var result = await UserEndpoints.DeleteUser(_userServiceMock.Object, userToDelete.UserId);

            // Assert
            var typedResult = Assert.IsType<Ok<UserDto>>(result);
            Assert.Equal(StatusCodes.Status200OK, typedResult.StatusCode);
        }

        //Negative use case 
        [Fact]
        public async Task SoftDeleteUser_WhenUserNotFoundExceptionIsThrownInUserService_Throws404NotFoundException()
        {
            // Act
            _userServiceMock.Setup(a => a.SoftDeleteUser(It.IsAny<Guid>()))
                .ThrowsAsync(new CheekyExceptions<UserNotFoundException>());

            // Arrange
            var userToDelete = _userListDtos[0];
            var result = await UserEndpoints.DeleteUser(_userServiceMock.Object, userToDelete.UserId);

            // Assert
            var typedResult = Assert.IsType<NotFound<string>>(result);
            Assert.Equal(StatusCodes.Status404NotFound, typedResult.StatusCode);
        }

        [Fact]
        public async Task SoftDeleteUser_WhenExceptionIsThrownInUserService_Throws500InternalServerErrorException()
        {
            // Act
            _userServiceMock.Setup(a => a.SoftDeleteUser(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            // Arrange
            var userToDelete = _userListDtos[0];
            var result = await UserEndpoints.DeleteUser(_userServiceMock.Object, userToDelete.UserId);

            //Assert
            var typedResult = Assert.IsType<ProblemHttpResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, typedResult.StatusCode);
        }
    }
}