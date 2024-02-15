using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ListenerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Persistence.Repositories
{
    internal sealed class ListenerCommandRepository : IListenerCommandRepository
    {
        private readonly ApplicationDbContext _Context;
        public ListenerCommandRepository(ApplicationDbContext context)
        {
            _Context = context;
        }
        public async Task DeleteAsync(Listener value)
        {
            await Task.Run(() => _Context.Remove(value));
        }

        public async Task InsertAsync(Listener value)
        {
            await _Context.AddAsync(value);
        }

        public async Task UpdateAsync(Listener value)
        {
            await Task.Run(() => _Context.Update(value));
        }
    }
}
