using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PairOfEmployees.Domain.Models;

namespace PairOfEmployees.Application.Contracts
{
    public interface IPairOfEmployeesLoader
    {
        Task<IReadOnlyList<Domain.Models.PairOfEmployeesData>> ImportAsync(string filePath, CancellationToken cancellation = default);
    }
}
