using System.Collections.Specialized;

namespace SLBenefits.Core.Helper
{
    public interface IConfigurationHelper
    {
        NameValueCollection AppSettings { get; }
    }
    public class ConfigurationHelper : IConfigurationHelper
    {
        public NameValueCollection AppSettings
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings;
            }
        }

    }
}
