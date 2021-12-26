using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc16
{
    internal class BitSequence
    {
        private readonly bool[] bits;

        public BitSequence(bool[] bits)
        {
            this.bits = bits;
        }

        public override string ToString()
        {
            return string.Join("", bits.Select(b => b ? "1" : "0"));
        }

        internal static BitSequence FromLower4Bits(List<byte> asNumbers)
        {
            return new BitSequence(GetLower4Bits(asNumbers).ToArray());
        }

        

        internal static IEnumerable<bool> GetLower4Bits(IEnumerable<byte> bytes)
        {
            foreach (var b in bytes)
            {
                yield return (b & (1 << 3)) > 0;
                yield return (b & (1 << 2)) > 0;
                yield return (b & (1 << 1)) > 0;
                yield return (b & (1 << 0)) > 0;
            }
        }

        internal bool GetBool(int index)
        {
            return this.bits[index];
        }

        internal int GetInteger(int index, int bitCount)
        {
            var tmp = this.bits[index..(index + bitCount)];
            int ret = 0;
            foreach(var b in tmp)
            {
                ret *= 2;
                if (b) ret++;
            }
            return ret;
        }
    }
}
