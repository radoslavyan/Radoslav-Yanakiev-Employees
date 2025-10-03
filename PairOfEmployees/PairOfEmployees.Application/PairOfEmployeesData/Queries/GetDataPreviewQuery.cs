using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairOfEmployees.Application.PairOfEmployeesData.Queries
{
    public record GetDataPreviewQuery() : MediatR.IRequest<List<DataPreviewDto>>;

    public record DataPreviewDto(int EmpId, int ProjectId, string DateFrom, string DateTo);
}
