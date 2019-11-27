using System;
using Xunit;
using FriBidiSharp;

namespace FriBidiSharpTest
{
    public class ApiTests
    {
        [Fact]
        public void Api_LogicalToVisual()
        {
            uint[] str = { 0x0628, 0x0633, 0x0645 };
            var pbase_dir = new uint[3];
            var visual_str = new uint[3];
            var lv = new int[3];
            var vl = new int[3];
            var e = new sbyte[100];
            //var x = Api.LogicalToVisual(str, 3, pbase_dir, visual_str, lv, vl, e);
            var x = FriBidiSharp.Main.Log2vis(str, 3, pbase_dir, visual_str, lv, vl, e);
        }
    }
}
