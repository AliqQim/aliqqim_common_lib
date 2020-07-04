using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommonUtils
{
    public class TestHelpers
    {

        //TODO unittest
        public static string SimplifyWhitespaces(string input, bool getRidOfEmptyLines, bool trimStrings)
        {
            IEnumerable<string> linesForOutput = input.Replace("\r", "").Split('\n');
            if (getRidOfEmptyLines)
                linesForOutput = linesForOutput.Where(s => !string.IsNullOrWhiteSpace(s));

            if (trimStrings)
                linesForOutput = linesForOutput.Select(s => s.Trim());

            return string.Join("\n", linesForOutput);
        }

    }
}
