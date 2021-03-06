﻿using Xunit;
using CommonUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUtils.Tests
{
    public class FileUtilsTests
    {
        [Theory]
        [InlineData(@"c:\aaa\bbb\ccc", "aaa", @"c:\aaa")]
        [InlineData(@"c:\aaa\bbb\ccc", "zzz", null)]
        [InlineData(@"c:\aaa\bbb\ccc\", "aaa", @"c:\aaa")]
        [InlineData(@"c:\aaa\bbb\aaa\ccc\", "aaa", @"c:\aaa\bbb\aaa")]
        [InlineData(@"c:\AAA\bbb\ccc", "aAa", @"c:\aaa")]
        public void ClosestParentFolderOrNullTest(string inputPath, 
            string folderToFind, 
            string expectedOutput)
        {
            string? res = FileUtils.ClosestParentFolderOrNull(inputPath, folderToFind);
            Assert.Equal(expectedOutput, res);
        }
    }
}