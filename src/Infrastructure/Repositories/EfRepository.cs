using Application.Common.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public EfRepository(ECommerceDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }

        // We override the default behavior when mapping to a dto.
        // We're using Mapster's ProjectToType here to immediately map the result from the database.
        // This is only done when no Selector is defined, so regular specifications with a selector also still work.
        protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
             specification.Selector is not null
                ? base.ApplySpecification(specification)
                : ApplySpecification(specification, false)
                    .ProjectToType<TResult>();
    }
}
