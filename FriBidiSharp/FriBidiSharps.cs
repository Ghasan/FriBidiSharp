using System;
using System.Collections.Generic;
using System.Text;

namespace FriBidiSharp
{
    public static class FriBidiSharpExtensions
    {
        public static string ToUtf16String(this uint[] utf32Array)
        {
            var bldr = new StringBuilder();

            foreach (int utf32Char in utf32Array)
                bldr.Append(Char.ConvertFromUtf32(utf32Char));

            return bldr.ToString();
        }

        public static uint[] ToUtf32Array(this string str)
        {
            var list = new List<uint>();

            char? highSurrogate = null;

            foreach (char chr in str)
            {
                if (highSurrogate != null)
                {
                    list.Add((uint)Char.ConvertToUtf32(highSurrogate.Value, chr));
                    highSurrogate = null;
                }
                else if (Char.IsHighSurrogate(chr))
                    highSurrogate = chr;
                else
                    list.Add(chr);
            }

            return list.ToArray();
        }
    }
}
