using System;
using System.Collections.Generic;
using System.Globalization;
using GWANet.Scanner.Extensions;

namespace GWANet.Scanner.Definitions
{
    public class BytePattern
    {
        public byte[] Pattern { get; init; }
        public byte[] Mask { get; }
        public long Offset { get; }
        
        private static readonly object CtorLock = new();
        private static readonly char[] MaskIgnore = { '?', '?' };

        /// <summary>
        /// Constructs byte patterns and mask for suitable <see cref="ISignatureScannerEngine"/> usage
        /// </summary>
        /// <param name="pattern">AoB pattern in a '00 6B ?? 91 F6 B?' format, where ? is a mask byte</param>
        /// <param name="offset">Offset from AoB scan result.</param>
        public BytePattern(string pattern, long offset)
        {
            Offset = offset;
            var bytes = new List<byte>(256);
            var maskBuilder = new List<byte>(256);
        
            var enumerator = new SpanSplitEnumerator<char>(pattern, ' ');
            var questionMarkFlag = new ReadOnlySpan<char>( MaskIgnore);

            lock (CtorLock)
            {
                maskBuilder.Clear();
                bytes.Clear();

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Equals(questionMarkFlag, StringComparison.Ordinal))
                    {
                        maskBuilder.Add(0x0);
                        bytes.Add(0x0);
                    }
                    else
                    {
                        bytes.Add(byte.Parse(enumerator.Current, NumberStyles.AllowHexSpecifier));
                        maskBuilder.Add(0x1);
                    }

                }

                Mask = maskBuilder.ToArray();
                Pattern = bytes.ToArray();
            }
        }
    }
}