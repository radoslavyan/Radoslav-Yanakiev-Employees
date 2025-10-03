using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairOfEmployees.Application.PairOfEmployeesData.Queries
{
    public record GetLongestPairOnCommonProjectQuery : IRequest<LongestPairDto?>;

    public sealed record LongestPairDto(int EmpId1, int EmpId2, int ProjectId, int TimeCoop);
}
