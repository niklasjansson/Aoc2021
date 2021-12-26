using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc16
{
    internal class PacketParser
    {
        public BitSequence BitSequence { get; private set; }
        public int Index { get; private set; }

        public Packet ParseBitSequence(BitSequence bitSequence)
        {
            this.BitSequence = bitSequence;
            this.Index = 0;
            return ParsePacket();
        }

        private Packet ParsePacket()
        {
            var version = ParseInteger(3);
            var typeId = ParseInteger(3);
            if (typeId == 4)
            {
                var literal = ParseLiteral();
                return new LiteralPacket(version, typeId, literal);
            }
            else
            {
                var subPackets = ParseSubPackets();
                return new OperatorPacket(version, typeId, subPackets);
            }
        }

        private List<Packet> ParseSubPackets()
        {
            var lengthTypeId = this.ParseBool();
            if (lengthTypeId == false)
            {
                var length = ParseInteger(15);
                return ParseLengthBasedSubPackets(length).ToList();
            }
            else
            {
                var count = ParseInteger(11);
                return ParseCountBasedSubPackets(count).ToList();
            }
        }

        private IEnumerable<Packet> ParseCountBasedSubPackets(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return ParsePacket();
            }
        }

        private IEnumerable<Packet> ParseLengthBasedSubPackets(int length)
        {
            int finalIndex = this.Index + length;
            while (this.Index < finalIndex)
            {
                yield return ParsePacket();
            }
        }

        private long ParseLiteral()
        {
            long ret = 0;

            var (b, i) = ParseBoolAndInteger(4);
            while (b)
            {
                ret *= 16;
                ret += i;
                (b, i) = ParseBoolAndInteger(4);
            }
            ret *= 16;
            ret += i;
            return ret;
        }

        private bool ParseBool()
        {
            var b = BitSequence.GetBool(this.Index);
            this.Index++;
            return b;
        }

        private int ParseInteger(int bitCount)
        {
            var ret = BitSequence.GetInteger(this.Index, bitCount);
            this.Index += bitCount;
            return ret;
        }

        private (bool, int) ParseBoolAndInteger(int bitCount)
        {
            var b = ParseBool();
            var i = ParseInteger(bitCount);
            return (b, i);
        }

    }
}
