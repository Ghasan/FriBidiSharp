using System;
using System.Collections.Generic;
using System.Text;

namespace FriBidiSharp
{
    unsafe public static class Api
    {
        //public static sbyte LogicalToVisual(uint[] str, int length, uint[] pbaseDir, uint[] visualStr, int[] logicalToVisualPositions, int[] visualToLogicalPositions, sbyte[] embeddingLevels)
        //{
        //    fixed (uint* strP = &str[0])
        //    {
        //        fixed (uint* pbaseDirP = &pbaseDir[0])
        //        {
        //            fixed (uint* visualStrP = &visualStr[0])
        //            {
        //                fixed (int* logicalToVisualPositionsP = &logicalToVisualPositions[0])
        //                {
        //                    fixed (int* visualToLogicalPositionsP = &visualToLogicalPositions[0])
        //                    {
        //                        fixed (sbyte* embeddingLevelsP = &embeddingLevels[0])
        //                        {
        //                            return fribidi_begindecls.Log2vis(strP, length, pbaseDirP, visualStrP, logicalToVisualPositionsP, visualToLogicalPositionsP, embeddingLevelsP);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        // Extensions

        public static string ToUtf16String(this int[] utf32String)
        {
            var bldr = new StringBuilder();

            foreach (int utf32Char in utf32String)
                bldr.Append(Char.ConvertFromUtf32(utf32Char));

            return bldr.ToString();
        }

        public static int[] FromUtf16String(this string str)
        {
            var list = new List<int>();

            char? highSurrogate = null;

            foreach (char chr in str)
            {
                if (highSurrogate != null)
                {
                    list.Add(Char.ConvertToUtf32(highSurrogate.Value, chr));
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
