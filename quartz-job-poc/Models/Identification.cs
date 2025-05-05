using System;

namespace quartz_job_poc.Models
{
    public class Identification
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsExpired { get; set; } = false;
    }
} 