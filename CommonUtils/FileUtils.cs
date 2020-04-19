using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonUtils
{
    public class FileUtils
    {
        public static string ClosestParentFolderOrNull(string path, string folderToFind)
        {
            IEnumerable<string> getAvailableParts()
            {
                var parts = path.Split('/', '\\');
                bool found = false;
                foreach (var part in parts.Reverse())
                {
                    if (part == folderToFind)
                        found = true;
                    if (found)
                        yield return part;
                }
            }

            var availableParts = getAvailableParts().ToArray();
            if (availableParts.Count() == 0)
                return null;

            return Path.Combine(availableParts.Reverse().ToArray());
        }

        /// <summary>
        /// возвращает путь до папки проекта (если быть точным, до папки, в которой ~Debug\Bin\*сборка*.dll)
        /// 
        /// за основу взято https://sau001.wordpress.com/2019/02/24/net-core-unit-tests-how-to-deploy-files-without-using-deploymentitem/
        /// </summary>
        /// <returns></returns>
        public static string GetPathToCurrentAssemblyProjectFolder()
        {
            return GetPathToAssemblyProjectFolder(Assembly.GetExecutingAssembly());
        }

        public static string GetPathToAssemblyProjectFolder(Assembly assembly)
        {
            string pathAssembly = assembly.Location;
            string folderAssembly = Path.GetDirectoryName(pathAssembly);
            string binFolder = ClosestParentFolderOrNull(folderAssembly, "bin");
            if (binFolder == null)
                throw new Exception("Не нашлась папка 'bin'");
            string folderProjectLevel =
                Path.GetFullPath(Path.Combine(binFolder, "..", ".."));

            return folderProjectLevel;
        }
    }
}
