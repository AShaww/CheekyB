using CheekyModels.Entities;
using CheekyModels.Dtos;

namespace CheekyTests.Unit.Helper;

    internal static class UsersList
    {
        internal static List<User> GetUsersList()
        {
            var entities = new List<User>();
            entities.Add(new User
            {
                UserId = Guid.Parse("C6D48587-6F80-4B97-B7AC-7B73A3956EF1"),
                FirstName = "Test User 1",
                Surname = "Surname Test 1",
                Email = "test@gmail.com",
                Archived = false,
                LoginDate = null
            });
            entities.Add(new User
            {
                UserId = Guid.Parse("558A04F9-930F-4B1B-90E2-3F5650D79704"),
                FirstName = "Test User 2",
                Surname = "Surname Test 2",
                Email = "test2@gmail.com",
                Archived = false,
                LoginDate = default
            });
            return entities;
        }
    }

    internal static class UsersDtoList
    {
        internal static List<UserDto> GetUsersDtoList()
        {
            var entities = new List<UserDto>();
            entities.Add(new UserDto
            {
                UserId = Guid.Parse("C6D48587-6F80-4B97-B7AC-7B73A3956EF1"),
                FirstName = "Test User 1",
                Surname = "Surname Test 1",
                Email = "test@gmail.com",
                Archived = false,
                LoginDate = null
            });
            entities.Add(new UserDto
            {
                UserId = Guid.Parse("558A04F9-930F-4B1B-90E2-3F5650D79704"),
                FirstName = "Test User 2",
                Surname = "Surname Test 2",
                Email = "test2@gmail.com",
                Archived = false,
                LoginDate = null
            });
            entities.Add(new UserDto
            {
                UserId = Guid.Parse("f1d08142-0b9a-409e-90be-04260ad58de7"),
                FirstName = "Test User 3",
                Surname = "Surname Test 3",
                Email = "Email@gmail.com",
                Archived = false,
                LoginDate = null
            });
            return entities;
        }
}