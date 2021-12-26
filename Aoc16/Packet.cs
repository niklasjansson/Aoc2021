namespace Aoc16
{
    internal abstract class Packet
    {
        protected Packet(int version, int typeId)
        {
            Version = version;
            TypeId = typeId;
        }

        public int Version { get; set; }
        public int TypeId { get; set; }

        internal abstract long GetTotalVersion();
        internal abstract long Evaluate();
    }

    internal class LiteralPacket : Packet
    {
        public LiteralPacket(int version, int typeId, long literal) : base(version, typeId)
        {
            this.Value = literal;
        }

        public long Value { get; set; }

        internal override long Evaluate()
        {
            return this.Value;
        }

        internal override long GetTotalVersion()
        {
            return Version;
        }
    }

    internal class OperatorPacket : Packet
    {
        public OperatorPacket(int version, int typeId, List<Packet> subPackets) : base(version, typeId)
        {
            SubPackets = subPackets;
        }

        public int Operation { get; set; }
        List<Packet> SubPackets { get; set; }

        internal override long Evaluate()
        {
            return TypeId switch
            {
                0 => SubPackets.Sum(p => p.Evaluate()),
                1 => SubPackets.Aggregate(1L, (p1, p2) => p1 * p2.Evaluate()),
                2 => SubPackets.Min(p => p.Evaluate()),
                3 => SubPackets.Max(p => p.Evaluate()),
                5 => SubPackets.First().Evaluate() > SubPackets.Last().Evaluate() ? 1 : 0,
                6 => SubPackets.First().Evaluate() < SubPackets.Last().Evaluate() ? 1 : 0,
                7 => SubPackets.First().Evaluate() == SubPackets.Last().Evaluate() ? 1 : 0,
                _ => throw new NotImplementedException()
            };
        }

        internal override long GetTotalVersion()
        {
            return this.Version + SubPackets.Sum(p => p.GetTotalVersion());
        }
    }
}