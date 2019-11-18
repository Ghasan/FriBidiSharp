using FriBidi;

namespace FriBidiSharp
{
    unsafe public static class Main
    {
        public static sbyte LogicalToVisual(uint[] str, int length, uint[] pbaseDir, uint[] visualStr, int[] logicalToVisualPositions, int[] visualToLogicalPositions, sbyte[] embeddingLevels)
        {
            fixed (uint* strP = &str[0])
            {
                fixed (uint* pbaseDirP = &pbaseDir[0])
                {
                    fixed (uint* visualStrP = &visualStr[0])
                    {
                        fixed (int* logicalToVisualPositionsP = &logicalToVisualPositions[0])
                        {
                            fixed (int* visualToLogicalPositionsP = &visualToLogicalPositions[0])
                            {
                                fixed (sbyte* embeddingLevelsP = &embeddingLevels[0])
                                {
                                    return fribidi_begindecls.Log2vis(strP, length, pbaseDirP, visualStrP, logicalToVisualPositionsP, visualToLogicalPositionsP, embeddingLevelsP);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
