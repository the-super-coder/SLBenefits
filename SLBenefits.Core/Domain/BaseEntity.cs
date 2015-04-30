using System;

namespace SLBenefits.Core.Domain
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatetionDate { get; set; }
    }
}
