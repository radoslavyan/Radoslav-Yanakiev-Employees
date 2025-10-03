using MediatR;
using PairOfEmployees.Application.Contracts;
using PairOfEmployees.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairOfEmployees.Application.PairOfEmployeesData.Queries
{
    public class GetDataPreviewQueryHandler : IRequestHandler<GetDataPreviewQuery, List<DataPreviewDto>>
    {
        private readonly IPairOfEmployeesRepository _pairOfEmployeesRepository;
        public GetDataPreviewQueryHandler(IPairOfEmployeesRepository pairOfEmployeesRepository)
            => _pairOfEmployeesRepository = pairOfEmployeesRepository;

        public async Task<List<DataPreviewDto>> Handle(GetDataPreviewQuery query, CancellationToken cancellation = default)
        {
            var data = await Task.FromResult(_pairOfEmployeesRepository.Query()
                .OrderBy(f => f.ProjectID)
                .ThenBy(f => f.EmpID)
                .ToList());

            return data.Select(d => new DataPreviewDto(
                d.EmpID,
                d.ProjectID,
                d.DateFrom.ToString("yyyy-MM-dd"),
                d.DateTo.ToString("yyyy-MM-dd")
            )).ToList();
        }
    }
}
