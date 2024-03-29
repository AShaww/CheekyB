﻿using System.Linq.Expressions;
using CheekyModels.Entities;

namespace CheekyData.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetNotLoggedUsersAsync(Expression<Func<User, bool>> predicate);
    Task<IEnumerable<User>> GetAllUsersAsync(Expression<Func<User, bool>> predicate, int pageNumber, int pageSize);
}
