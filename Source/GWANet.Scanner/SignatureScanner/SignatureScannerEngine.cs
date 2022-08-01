using System;
using System.Runtime.CompilerServices;
using GWANet.Scanner.Definitions;
using GWANet.Scanner.SignatureScanner.Definitions;

namespace GWANet.Scanner.SignatureScanner
{
    internal abstract unsafe class SignatureScannerEngine
    {
        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected static ReadOnlySpan<ushort> BuildFullMatchTable(in BytePattern pattern)
        {
            var maskLength = pattern.Mask.Length;
            var fullMatchTable = GC.AllocateUninitializedArray<ushort>(maskLength, false);

            var matchCount = 0;
            for (ushort x = 1; x < maskLength; x++)
            {
                if (pattern.Mask[x] != 1)
                    continue;

                fullMatchTable[matchCount] = (ushort) (x - 1);
                matchCount++;
            }

            var matchTable = new ReadOnlySpan<ushort>(fullMatchTable)[..matchCount];
            return matchTable;
        }

        public abstract PatternScanResult FindPattern(byte* data, int dataLength, BytePattern pattern);

        protected static PatternScanResult FindPatternSimple(byte* data, int dataLength, BytePattern pattern)
        {
            var patternData = pattern.Pattern;
            var patternMask = pattern.Mask;

            var lastIndex = (dataLength - patternMask.Length) + 1;

            fixed (byte* patternDataPtr = patternData)
            {
                for (var x = 0; x < lastIndex; x++)
                {
                    var patternDataOffset = 0;
                    var currentIndex = x;

                    var y = 0;
                    do
                    {
                        if (patternMask[y] == 0x0)
                        {
                            currentIndex += 1;
                            y++;
                            continue;
                        }

                        if (data[currentIndex] != patternDataPtr[patternDataOffset])
                            goto loopexit;

                        currentIndex += 1;
                        patternDataOffset += 1;
                        y++;
                    } while (y < patternMask.Length);

                    return new PatternScanResult(x);
                    loopexit: ;
                }

                return new PatternScanResult(-1);
            }
        }
    }
}
