using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLBenefits.Core.Domain
{
    public class Category : BaseEntity
    {
        public Category()
        {
            CreationDate = DateTime.Now;
            UpdatetionDate = DateTime.Now;
            IsActive = true;
        }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
