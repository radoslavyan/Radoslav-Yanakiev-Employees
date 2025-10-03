using MediatR;
using PairOfEmployees.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PairOfEmployees.Application.PairOfEmployeesData.Commands.LoadCsvDataCommand;



namespace PairOfEmployees.Application.PairOfEmployeesData.Commands
{
    public class LoadCsvDataHandler : MediatR.IRequestHandler<LoadCsvDataCommand, int>
    {
        private readonly IPairOfEmployeesLoader _pairOfEmployeesLoader;
        private readonly IPairOfEmployeesRepository _pairOfEmployeesRepository;

        public LoadCsvDataHandler(IPairOfEmployeesLoader pairOfEmployeesLoader, IPairOfEmployeesRepository pairOfEmployeesRepository)
            => (_pairOfEmployeesLoader, _pairOfEmployeesRepository) = (pairOfEmployeesLoader, pairOfEmployeesRepository);

        public async Task<int> Handle(LoadCsvDataCommand request, CancellationToken cancellation) 
        {
            var items = await _pairOfEmployeesLoader.ImportAsync(request.filePath , cancellation);
            await _pairOfEmployeesRepository.ClearAsync(cancellation);
            await _pairOfEmployeesRepository.AddRangeAsync(items, cancellation);
            return items.Count;
        }
    }
}
