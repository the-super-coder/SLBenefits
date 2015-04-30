using System.IO;
using System.Linq;
using System.Reflection;

namespace SLBenefits.Core.Helper
{
    public static class EmbeddedResources
    {
        public static string GetResourceAsString(string resourceName)
        {
            Assembly sourceAssembly = Assembly.GetCallingAssembly();
            string resourceFullName = sourceAssembly.GetManifestResourceNames()
                                                    .FirstOrDefault(f => f.EndsWith(resourceName));

            string result = null;
            using (StreamReader sr = new StreamReader(sourceAssembly.GetManifestResourceStream(resourceFullName)))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }

        public static Stream GetResourceAsStream(string resourceName)
        {
            Assembly sourceAssembly = Assembly.GetCallingAssembly();
            string resourceFullName = sourceAssembly.GetManifestResourceNames()
                                                    .FirstOrDefault(f => f.EndsWith(resourceName));

            return sourceAssembly.GetManifestResourceStream(resourceFullName);
        }

    }
}
