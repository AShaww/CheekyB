using System.Linq.Expressions;
using CheekyData.Interfaces;
using Moq;

namespace CheekyTests.Unit.Common;

    public class MockedAsyncRepository<T> where T : class, new()
    {
        private readonly Mock<IRepository<T>> _repository;
        private readonly List<T> _entities;

        /// <summary>
        /// Initialises a new mock repository with no entities
        /// </summary>
        public MockedAsyncRepository()
        {
            _repository = new Mock<IRepository<T>>();
            _entities = new List<T>();
            SetUpMockCalls();
        }

        ///// <summary>
        ///// Initialises a new mock repository
        ///// </summary>
        ///// <param name="entities"></param>
        public MockedAsyncRepository(List<T> entities)
        {
            _repository = new Mock<IRepository<T>>();
            _entities = entities;
            SetUpMockCalls();
        }

        /// <summary>
        /// Returns the mocked repository
        /// </summary>
        /// <returns></returns>
        public Mock<IRepository<T>> GetRepository()
        {
            return _repository;
        }

        /// <summary>
        /// Set up the mocked respository calls
        /// </summary>
        private void SetUpMockCalls()
        {
            _repository.Setup(s => s.GetAllAsync()).Returns(Task.FromResult(_entities.AsEnumerable()));

            _repository.Setup(x => x.AddAsync(It.IsAny<T>())).Callback<T>(x => _entities.Add(x));

            _repository.Setup(x => x.UpdateAsync(It.IsAny<T>())).Callback<T>(x => _entities.Where(y => y.Equals(x)));

            _repository.Setup(x => x.DeleteAsync(It.IsAny<T>())).Callback<T>(x => _entities.Remove(x));

            _repository.Setup(x => x.UpdateAsync(It.IsAny<T>())).Callback<T>(x => _entities.Where(y => y.Equals(x)));

            _repository.Setup(s => s.GetFirstOrDefault(It.IsAny<Expression<Func<T, bool>>>())).Returns<Expression<Func<T, bool>>>(x =>
            {
                Predicate<T> pred = new Predicate<T>(x.Compile());
                return Task.FromResult(_entities.FindAll(pred).FirstOrDefault()) as Task<T>;
            });
        }
}