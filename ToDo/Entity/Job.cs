using System;

namespace Entity
{
    public class Job : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string IsDone { get; set; }
        public DateTime StartDate { get; set; }
        public JobType JobType { get; set; }
    }
}
