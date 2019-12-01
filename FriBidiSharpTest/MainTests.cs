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
            uint[] str = "سلام Hi".ToUtf32Array();
            var pbase_dir = new uint[str.Length];
            var visual_str = new uint[str.Length];
            var lv = new int[str.Length];
            var vl = new int[str.Length];
            var e = new sbyte[str.Length];

            pbase_dir[0] = (uint)FriBidiSharp.ParagraphType.RTL;
            FriBidiSharpMain.Log2vis(str, str.Length, pbase_dir, visual_str, lv, vl, e);
            Assert.True(new int[] { 6, 5, 4, 3, 2, 0, 1 }.SequenceEqual(lv));
            Assert.True(new int[] { 5, 6, 4, 3, 2, 1, 0 }.SequenceEqual(vl));

            pbase_dir[0] = (uint)FriBidiSharp.ParagraphType.LTR;
            FriBidiSharpMain.Log2vis(str, str.Length, pbase_dir, visual_str, lv, vl, e);
            Assert.True(new int[] { 3, 2, 1, 0, 4, 5, 6 }.SequenceEqual(lv));
            Assert.True(new int[] { 3, 2, 1, 0, 4, 5, 6 }.SequenceEqual(vl));
        }
    }
}
