using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}
