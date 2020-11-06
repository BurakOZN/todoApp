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
}
