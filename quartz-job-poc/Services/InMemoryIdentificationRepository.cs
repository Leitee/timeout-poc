using quartz_job_poc.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quartz_job_poc.Services
{
    public class InMemoryIdentificationRepository
    {
        private readonly ConcurrentDictionary<string, Identification> _identifications = new ConcurrentDictionary<string, Identification>();

        public Task<List<Identification>> GetAllAsync()
        {
            return Task.FromResult(_identifications.Values.ToList());
        }

        public Task<Identification> GetByIdAsync(string id)
        {
            if (_identifications.TryGetValue(id, out var identification))
            {
                return Task.FromResult(identification);
            }
            
            return Task.FromResult<Identification>(null);
        }

        public Task<Identification> CreateAsync(Identification identification)
        {
            _identifications[identification.Id] = identification;
            return Task.FromResult(identification);
        }

        public Task<bool> UpdateAsync(string id, bool isExpired)
        {
            if (_identifications.TryGetValue(id, out var identification))
            {
                identification.IsExpired = isExpired;
                return Task.FromResult(true);
            }
            
            return Task.FromResult(false);
        }
    }
} 