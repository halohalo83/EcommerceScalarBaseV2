using Ardalis.Specification;
using Domain.Interfaces;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
