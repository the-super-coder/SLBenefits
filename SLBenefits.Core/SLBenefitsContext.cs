using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace SLBenefits.Core
{
    public class SLBenefitsContext : DbContext
    {
        public SLBenefitsContext()
        {
            Database.SetInitializer<SLBenefitsContext>(null);
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.
                Conventions.
                Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            
        }

    }
}
