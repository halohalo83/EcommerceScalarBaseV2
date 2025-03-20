using Ardalis.Specification;
using Domain.Interfaces;

namespace Application.Common.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
