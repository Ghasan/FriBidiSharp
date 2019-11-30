using FriBidiSharp;
using System;
using System.Linq;
using Xunit;

namespace FriBidiSharpTest
{
    public class MainTests
    {
        [Fact]
        public void Main_Log2vis()
        {
            uint[] str = "سلام".ToUtf32Array();
            var pbase_dir = new uint[str.Length];
            var visual_str = new uint[str.Length];
            var lv = new int[str.Length];
            var vl = new int[str.Length];
            var e = new sbyte[str.Length];
            FriBidiSharpMain.Log2vis(str, str.Length, pbase_dir, visual_str, lv, vl, e);

            Assert.True(new int[] { 3, 2, 1, 0 }.SequenceEqual(vl));
            Assert.True(new int[] { 3, 2, 1, 0 }.SequenceEqual(lv));
        }
    }
}
