using System;
using System.Collections.Generic;
using System.Text;

namespace ApiModels
{
    public class AddJobRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Type { get; set; }
    }
    public class GetJobResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public bool IsDone { get; set; }
    }
    public class JobDoneRequest
    {
        public string JobId { get; set; }
    }
    public class JobFilterRequest
    {
        public int State { get; set; }
        public int Type { get; set; }
    }

}
