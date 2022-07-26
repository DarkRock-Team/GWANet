using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using GWANet.Scanner.Definitions;
using GWANet.Scanner.SignatureScanner.Definitions;

namespace GWANet.Scanner.SignatureScanner;

internal unsafe class SignatureScannerAvxEngine : SignatureScannerEngine
{
    private const int AvxRegisterLength = 32;

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public new PatternScanResult FindPattern(byte* data, int dataLength, BytePattern pattern)
    {
        var dataPtr = data;
        var matchTable = BuildFullMatchTable(pattern);
        var vectors = GenerateVectorPadding(pattern);

        var matchTableLength = matchTable.Length;

        ref var pVec = ref vectors[0];
        var vectorLength = vectors.Length;

        var firstByteVec = Vector256.Create(pattern.Pattern[0]);
        ref var pFirstVec = ref firstByteVec;

        var simdJump = AvxRegisterLength - 1;
        var searchLength = dataLength - Math.Max(pattern.Pattern.Length, AvxRegisterLength);
        var position = 0;
        for (; position < searchLength; position++, dataPtr += 1)
        {
            // Problem: If pattern starts with unknown, will never match.
            var rhs = Avx.LoadVector256(dataPtr);
            var equal = Avx2.CompareEqual(pFirstVec, rhs);
            var findFirstByte = Avx2.MoveMask(equal);

            if (findFirstByte == 0)
            {
                position += simdJump;
                dataPtr += simdJump;
                continue;
            }

            // Shift up until first byte found.
            var offset = BitOperations.TrailingZeroCount((uint) findFirstByte);
            position += offset;
            dataPtr += offset;

            // Match with remaining vectors.
            var iMatchTableIndex = 0;
            var isFound = true;
            for (var i = 0; i < vectorLength; i++)
            {
                var nextByte = dataPtr + (1 + i * AvxRegisterLength);
                var rhsNo2 = Avx.LoadVector256(nextByte);
                var curPatternVector = Unsafe.Add(ref pVec, i);

                var compareResult = Avx2.MoveMask(Avx2.CompareEqual(curPatternVector, rhsNo2));

                for (; iMatchTableIndex < matchTableLength; iMatchTableIndex++)
                {
                    int matchIndex = matchTable[iMatchTableIndex];

                    if (i > 0)
                        matchIndex -= i * AvxRegisterLength;

                    if (matchIndex >= AvxRegisterLength)
                        break;

                    if (((compareResult >> matchIndex) & 1) == 1)
                        continue;

                    isFound = false;
                    break;
                }

                if (!isFound)
                    break;
            }

            if (isFound) return new PatternScanResult(position);
        }

        // Check last few bytes in cases pattern was not found and long overflows into possibly unallocated memory.
        return base.FindPattern(data + position, dataLength - position, pattern).AddOffset(position);
    }

    private static Vector256<byte>[] GenerateVectorPadding(in BytePattern pattern)
    {
        var patternLen = pattern.Mask.Length;
        var vectorCount = (int) Math.Ceiling((patternLen - 1) / (float) AvxRegisterLength);
        var patternVectors = new Vector256<byte>[vectorCount];

        ref var pPattern = ref pattern.Pattern[1];
        patternLen--;
        for (var i = 0; i < vectorCount; i++)
            if (i < vectorCount - 1)
            {
                patternVectors[i] =
                    Unsafe.As<byte, Vector256<byte>>(ref Unsafe.Add(ref pPattern, i * AvxRegisterLength));
            }
            else
            {
                var o = i * AvxRegisterLength;
                patternVectors[i] = Vector256.Create(
                    Unsafe.Add(ref pPattern, o + 0),
                    o + 1 < patternLen ? Unsafe.Add(ref pPattern, o + 1) : (byte) 0,
                    o + 2 < patternLen ? Unsafe.Add(ref pPattern, o + 2) : (byte) 0,
                    o + 3 < patternLen ? Unsafe.Add(ref pPattern, o + 3) : (byte) 0,
                    o + 4 < patternLen ? Unsafe.Add(ref pPattern, o + 4) : (byte) 0,
                    o + 5 < patternLen ? Unsafe.Add(ref pPattern, o + 5) : (byte) 0,
                    o + 6 < patternLen ? Unsafe.Add(ref pPattern, o + 6) : (byte) 0,
                    o + 7 < patternLen ? Unsafe.Add(ref pPattern, o + 7) : (byte) 0,
                    o + 8 < patternLen ? Unsafe.Add(ref pPattern, o + 8) : (byte) 0,
                    o + 9 < patternLen ? Unsafe.Add(ref pPattern, o + 9) : (byte) 0,
                    o + 10 < patternLen ? Unsafe.Add(ref pPattern, o + 10) : (byte) 0,
                    o + 11 < patternLen ? Unsafe.Add(ref pPattern, o + 11) : (byte) 0,
                    o + 12 < patternLen ? Unsafe.Add(ref pPattern, o + 12) : (byte) 0,
                    o + 13 < patternLen ? Unsafe.Add(ref pPattern, o + 13) : (byte) 0,
                    o + 14 < patternLen ? Unsafe.Add(ref pPattern, o + 14) : (byte) 0,
                    o + 15 < patternLen ? Unsafe.Add(ref pPattern, o + 15) : (byte) 0,
                    o + 16 < patternLen ? Unsafe.Add(ref pPattern, o + 16) : (byte) 0,
                    o + 17 < patternLen ? Unsafe.Add(ref pPattern, o + 17) : (byte) 0,
                    o + 18 < patternLen ? Unsafe.Add(ref pPattern, o + 18) : (byte) 0,
                    o + 19 < patternLen ? Unsafe.Add(ref pPattern, o + 19) : (byte) 0,
                    o + 20 < patternLen ? Unsafe.Add(ref pPattern, o + 20) : (byte) 0,
                    o + 21 < patternLen ? Unsafe.Add(ref pPattern, o + 21) : (byte) 0,
                    o + 22 < patternLen ? Unsafe.Add(ref pPattern, o + 22) : (byte) 0,
                    o + 23 < patternLen ? Unsafe.Add(ref pPattern, o + 23) : (byte) 0,
                    o + 24 < patternLen ? Unsafe.Add(ref pPattern, o + 24) : (byte) 0,
                    o + 25 < patternLen ? Unsafe.Add(ref pPattern, o + 25) : (byte) 0,
                    o + 26 < patternLen ? Unsafe.Add(ref pPattern, o + 26) : (byte) 0,
                    o + 27 < patternLen ? Unsafe.Add(ref pPattern, o + 27) : (byte) 0,
                    o + 28 < patternLen ? Unsafe.Add(ref pPattern, o + 28) : (byte) 0,
                    o + 29 < patternLen ? Unsafe.Add(ref pPattern, o + 29) : (byte) 0,
                    o + 30 < patternLen ? Unsafe.Add(ref pPattern, o + 30) : (byte) 0,
                    o + 31 < patternLen ? Unsafe.Add(ref pPattern, o + 31) : (byte) 0
                );
            }

        return patternVectors;
    }
}