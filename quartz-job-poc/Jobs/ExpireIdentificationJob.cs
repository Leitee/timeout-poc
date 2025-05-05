using Quartz;
using quartz_job_poc.Services;
using System.Threading.Tasks;

namespace quartz_job_poc.Jobs
{
    public class ExpireIdentificationJob : IJob
    {
        private readonly InMemoryIdentificationRepository _repository;

        public ExpireIdentificationJob(InMemoryIdentificationRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var id = context.MergedJobDataMap.GetString("Id");
            await _repository.UpdateAsync(id, true);
        }
    }
} 