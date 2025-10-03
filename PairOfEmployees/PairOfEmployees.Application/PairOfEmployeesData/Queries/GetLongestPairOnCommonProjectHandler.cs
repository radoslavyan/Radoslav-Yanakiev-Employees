using MediatR;
using PairOfEmployees.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PairOfEmployees.Application.PairOfEmployeesData.Queries.GetLongestPairOnCommonProjectQuery;

namespace PairOfEmployees.Application.PairOfEmployeesData.Queries
{
    public class GetLongestPairOnCommonProjectHandler : IRequestHandler<GetLongestPairOnCommonProjectQuery, LongestPairDto?>
    {
        private readonly IPairOfEmployeesRepository _pairOfEmployeesRepository;
        public GetLongestPairOnCommonProjectHandler(IPairOfEmployeesRepository pairOfEmployeesRepository) 
            => _pairOfEmployeesRepository = pairOfEmployeesRepository;

        public Task<LongestPairDto?> Handle(GetLongestPairOnCommonProjectQuery request, CancellationToken cancellation) 
        {
            var hits = _pairOfEmployeesRepository.Query().ToList();
            if(hits.Count() == 0) return Task.FromResult<LongestPairDto?>(null);

            LongestPairDto? longest = null;

            foreach(var projectPair in hits.GroupBy(x => x.ProjectID))
            {
                var byEmp = projectPair.GroupBy(x => x.EmpID).ToDictionary(p => p.Key,
                    p => p.Select(f => (f.DateFrom,  f.DateTo)).OrderBy(t => t.DateFrom).ToList());

                var empIds = byEmp.Keys.OrderBy(x => x).ToArray();

                for(int i = 0; i < empIds.Length; i++)
                {
                    for (int j = 0; j < empIds.Length; j++)
                    {
                        var abc = byEmp[empIds[i]];
                        var cba = byEmp[empIds[j]];

                        int timeIndays = 0;
                        foreach (var (ax, ay) in abc)
                        foreach (var (bx, by) in cba) 
                        {
                            var start = ax > bx ? ax : bx;
                            var end = ay < by ? ay : by;
                            if (end >= start)
                                timeIndays += end.DayNumber - start.DayNumber + 1;
                        }

                        if(timeIndays > 0)
                        {
                            var contestant = new LongestPairDto(empIds[i], empIds[j], projectPair.Key, timeIndays);
                            if(longest is null || contestant.TimeCoop > longest.TimeCoop)
                               longest = contestant;
                        }
                    }
                }
            
            }

            return Task.FromResult(longest);
        }
    }
}
