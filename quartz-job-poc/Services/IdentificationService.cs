using Quartz;
using quartz_job_poc.Jobs;
using quartz_job_poc.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace quartz_job_poc.Services
{
    public class IdentificationService
    {
        private readonly InMemoryIdentificationRepository _repository;
        private readonly ISchedulerFactory _schedulerFactory;

        public IdentificationService(InMemoryIdentificationRepository repository, ISchedulerFactory schedulerFactory)
        {
            _repository = repository;
            _schedulerFactory = schedulerFactory;
        }

        public async Task<List<Identification>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Identification> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Identification> CreateAsync(string name)
        {
            var identification = new Identification
            {
                Name = name,
                CreatedAt = DateTime.UtcNow,
                IsExpired = false
            };

            await _repository.CreateAsync(identification);
            
            // Schedule expiration job
            await ScheduleExpirationJob(identification.Id);
            
            return identification;
        }

        private async Task ScheduleExpirationJob(string id)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            
            // Create job
            var job = JobBuilder.Create<ExpireIdentificationJob>()
                .WithIdentity($"expire-{id}", "identifications")
                .UsingJobData("Id", id)
                .Build();

            // Create trigger for 10 seconds in the future
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger-{id}", "identifications")
                .StartAt(DateBuilder.FutureDate(10, IntervalUnit.Second))
                .Build();

            // Schedule the job
            await scheduler.ScheduleJob(job, trigger);
        }
    }
} 