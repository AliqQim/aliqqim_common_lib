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
        [InlineData(@"StripTagsTestFiles\StandardCase\input.xml", @"StripTagsTestFiles\StandardCase\output.txt")]
        [InlineData(@"StripTagsTestFiles\SingleLine\input.xml", @"StripTagsTestFiles\SingleLine\output.txt")]
        [InlineData(@"StripTagsTestFiles\TagSpletedByLinebreak\input.xml", @"StripTagsTestFiles\TagSpletedByLinebreak\output.txt")]
        public void StripTagsTests(string input, string output)
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