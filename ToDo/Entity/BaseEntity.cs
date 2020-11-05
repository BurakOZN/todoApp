using System;
using System.Text;

namespace Entity
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            IsActive = true;
        }
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
