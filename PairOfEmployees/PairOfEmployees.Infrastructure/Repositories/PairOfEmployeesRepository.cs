using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PairOfEmployees.Application.Contracts;
using PairOfEmployees.Domain.Models;
using Microsoft.EntityFrameworkCore;
using PairOfEmployees.Infrastructure.DatabaseContext;

namespace PairOfEmployees.Persistence.Repositories
{
    public class PairOfEmployeesRepository : IPairOfEmployeesRepository
    {
        private readonly PairDbContext _context;
        public PairOfEmployeesRepository(PairDbContext context)
        {
            _context = context;
        }
        public async Task ClearAsync(CancellationToken cancellation = default)
        {
            _context.PairsOfEmployeesData.RemoveRange(_context.PairsOfEmployeesData);
            await _context.SaveChangesAsync(cancellation);
        }
        public async Task AddRangeAsync(IEnumerable<PairOfEmployeesData> pairs, CancellationToken cancellation)
        {
            await _context.PairsOfEmployeesData.AddRangeAsync(pairs, cancellation);
            await _context.SaveChangesAsync(cancellation);
        }
        public IQueryable<PairOfEmployeesData> Query()
        {
            return _context.PairsOfEmployeesData.AsQueryable();
        }

        public Task AddRange(IEnumerable<PairOfEmployeesData> pairs, CancellationToken cancellation)
        {
            
            _context.PairsOfEmployeesData.AddRange(pairs);
            _context.SaveChangesAsync(cancellation);
            return Task.CompletedTask;
        }

        public Task AddRangeAsync(IReadOnlyList<PairOfEmployeesData> items, CancellationToken cancellation)
        {
           _context.PairsOfEmployeesData.AddRangeAsync(items, cancellation);
           _context.SaveChangesAsync(cancellation);
            return Task.CompletedTask;
        }
    }
    
    
}
