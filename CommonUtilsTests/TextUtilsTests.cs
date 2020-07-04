using Xunit;
using CommonUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace CommonUtils.Tests
{
    public class TextUtilsTests
    {
        [Theory]
        [InlineData(@"StripTagsTestFiles\input.xml", @"StripTagsTestFiles\output.txt")]
        public void StripTags_StandardCase(string input, string output)
        {
            string basePath = FileUtils.GetPathToCurrentAssemblyCsprojFolder();

            string res = TextUtils.StripTags(File.ReadAllText(Path.Combine(basePath, input)));
            string expectedOutput = File.ReadAllText(Path.Combine(basePath, output));

            Assert.Equal(PrepareForCompare(expectedOutput),
                PrepareForCompare(res));
        }

        private string PrepareForCompare(string s)
            => TestHelpers.SimplifyWhitespaces(s, true, true);
            
    }
}