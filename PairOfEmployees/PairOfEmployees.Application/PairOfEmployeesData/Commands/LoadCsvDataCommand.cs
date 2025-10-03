using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairOfEmployees.Application.PairOfEmployeesData.Commands
{
    public record LoadCsvDataCommand(string filePath) : MediatR.IRequest<int>;
}
