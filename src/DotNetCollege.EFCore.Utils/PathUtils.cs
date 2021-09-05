using System;
using System.IO;
using System.Reflection;

namespace DotNetCollege.EFCore.Utils
{
    public class PathUtils
    {
        public static string GetApplicationPathByAssembly(Assembly assemblyName)
        {
            var appData = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(appData);
            var assemmblyName = assemblyName.GetName().Name;
            var rootAppDirectory = Path.Combine(path, assemmblyName);

            if (!Directory.Exists(rootAppDirectory))
            {
                Directory.CreateDirectory(rootAppDirectory);
            }

            return rootAppDirectory;
        }
    }
}
