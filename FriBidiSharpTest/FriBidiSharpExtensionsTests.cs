using FriBidiSharp;
using System;
using System.Linq;
using Xunit;

namespace FriBidiSharpTest
{
    public class FriBidiSharpExtensionsTests
    {
        [Fact]
        public void FriBidiSharpExtensions_ToUtf16String()
        {
            uint[] musicalNote = { 0x1D160, 0x1D161 };
            string utf16 = musicalNote.ToUtf16String();

            Assert.Equal(4, utf16.Length);

            Assert.Equal(utf16.Substring(0, 2), Char.ConvertFromUtf32((int)musicalNote[0]));
            Assert.Equal(utf16.Substring(2, 2), Char.ConvertFromUtf32((int)musicalNote[1]));
        }

        [Fact]
        public void FriBidiSharpExtensions_ToUtf32Array()
        {
            uint[] musicalNote = { 0x1D160, 0x1D161 };
            string utf16 = musicalNote.ToUtf16String();

            uint[] musicalNoteResult = utf16.ToUtf32Array();
            Assert.True(musicalNote.SequenceEqual(musicalNoteResult));
        }
    }
}
