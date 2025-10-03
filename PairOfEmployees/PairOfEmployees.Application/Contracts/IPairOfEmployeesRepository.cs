using PairOfEmployees.Domain.Models;

namespace PairOfEmployees.Application.Contracts
{
    public interface IPairOfEmployeesRepository
    {
        Task ClearAsync(CancellationToken cancellation = default);
        Task AddRange(IEnumerable<Domain.Models.PairOfEmployeesData> pairs, CancellationToken cancellation);
        IQueryable<Domain.Models.PairOfEmployeesData> Query();
        Task AddRangeAsync(IReadOnlyList<Domain.Models.PairOfEmployeesData> items, CancellationToken cancellation);
    }
}
