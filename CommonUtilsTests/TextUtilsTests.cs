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
            
            string PrepareForCompare(string s)
                => TestHelpers.SimplifyWhitespaces(s, true, true);
        }

        [Theory]
        [InlineData(@"StripTagsTestFiles\WarAndPeaceTrickyFootnoteCase\input.xml", 
            @"StripTagsTestFiles\WarAndPeaceTrickyFootnoteCase\outputLinebreaksInserted.txt", 
            true)]
        [InlineData(@"StripTagsTestFiles\WarAndPeaceTrickyFootnoteCase\input.xml", 
            @"StripTagsTestFiles\WarAndPeaceTrickyFootnoteCase\outputLinebreaksNotInserted.txt", 
            false)]
        public void StripTagsTestExactLines(string input, string output, bool insertLinebreaks)
        {
            string basePath = FileUtils.GetPathToCurrentAssemblyCsprojFolder();

            string res = TextUtils.StripTags(File.ReadAllText(Path.Combine(basePath, input)), insertLinebreaks);
            string expectedOutput = File.ReadAllText(Path.Combine(basePath, output));

            Assert.Equal(PrepareForCompare(expectedOutput),
                PrepareForCompare(res));
            
            string PrepareForCompare(string s)
                => TestHelpers.SimplifyWhitespaces(s, false, true);
        }

        [Theory]
        [InlineData("asd <bR > ura", "asd \n ura", true)]
        [InlineData("asd <bR > ura", "asd  ura", false)]
        [InlineData("asd <br /> ura", "asd \n ura", true)]
        [InlineData("asd <brt > ura", "asd  ura", true)]
        [InlineData("asd <p>hoi!</P> ura", "asd \nhoi!\n ura", true)]
        public void StripTagsDifferentTagFormsTest(string input, string expectedOutput, bool insertLinebreaks)
        {
            string res = TextUtils.StripTags(input, insertLinebreaks);
            
            Assert.Equal(res, expectedOutput);
        }

        
            
    }
}