internal abstract class SnailNumber
{

    internal static SnailNumber Sum(SnailNumber s1, SnailNumber s2)
    {
        var afterAddition = new PairSnailNumber(s1, s2);
        afterAddition.Reduce();
        return afterAddition;
    }
    internal static SnailNumber ParseSnailNumber(string v)
    {
        return Parse(v).Item1;
    }

    private static (SnailNumber, int) Parse(string v)
    {
        if (v.StartsWith("["))
        {
            (var item1, var count1) = Parse(v[1..]);
            (var item2, var count2) = Parse(v[(count1 + 2)..]);
            return (new PairSnailNumber(item1, item2), count1 + count2 + 3);
        }
        else
        {
            var n0 = v.IndexOf(',');
            var n1 = v.IndexOf(']');
            if (n0 < 0) n0 = int.MaxValue;
            if (n1 < 0) n1 = int.MaxValue;
            var n = Math.Min(n0, n1);
            int ret = int.Parse(v[..n]);
            return (new PlainSnailNumber(ret), n);
        }
    }

    internal abstract int GetMagnitude();

    internal abstract void Reduce();
    internal abstract void ApplyLeftMost(int value);
    internal abstract void ApplyRightMost(int value);

    internal abstract SnailNumber Clone();
}

internal class PlainSnailNumber : SnailNumber
{
    public int Value { get; set; }

    public PlainSnailNumber(int value)
    {
        this.Value = value;
    }


    internal PairSnailNumber? DoSplit()
    {
        if (this.Value < 10)
        {
            return null;
        }
        var val = this.Value / 2;
        return new PairSnailNumber(val, this.Value - val);
    }

    internal override void Reduce()
    {
    }

    internal override void ApplyLeftMost(int value)
    {
        this.Value += value;
    }

    internal override void ApplyRightMost(int value)
    {
        this.Value += value;
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }

    internal override int GetMagnitude()
    {
        return this.Value;
    }

    internal override SnailNumber Clone()
    {
        return new PlainSnailNumber(this.Value);    
    }
}

internal class PairSnailNumber : SnailNumber
{
    public SnailNumber Item1 { get; set; }
    public SnailNumber Item2 { get; set; }

    public PairSnailNumber(int item1, int item2) : this(new PlainSnailNumber(item1), new PlainSnailNumber(item2))
    {
    }

    public PairSnailNumber(SnailNumber item1, SnailNumber item2)
    {
        this.Item1 = item1;
        this.Item2 = item2;
    }

    public override string ToString()
    {
        return $"[{Item1},{Item2}]";
    }

    internal override void Reduce()
    {
        var keepGoing = true;
        while (keepGoing)
        {
            keepGoing = false;
            keepGoing = this.Explode();
            if (!keepGoing) { keepGoing = this.Split(); }
        }
    }

    internal bool Explode()
    {
        var ret = DoExplode(0);
        return ret.Item4;
    }

    internal (PlainSnailNumber?, int?, int?, bool) DoExplode(int depth)
    {
        if (depth == 4)
        {
            if (Item1 is PlainSnailNumber plain1 &&
                Item2 is PlainSnailNumber plain2)
            {
                return (new PlainSnailNumber(0), plain1.Value, plain2.Value, true);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        else if (depth > 4)
        {
            throw new NotImplementedException();
        }
        else
        {
            if (Item1 is PairSnailNumber pair1)
            {
                (var toReplace, var toUseLeft, var toUseRight, var exploded) = pair1.DoExplode(depth + 1);
                if (exploded)
                {
                    if (toReplace != null) Item1 = toReplace;
                    if (toUseRight != null) Item2.ApplyLeftMost(toUseRight.Value);
                    return (null, toUseLeft, null, true);
                }
            }
            if (Item2 is PairSnailNumber pair2)
            {
                (var toReplace, var toUseLeft, var toUseRight, var exploded) = pair2.DoExplode(depth + 1);
                if (exploded)
                {
                    if (toReplace != null) Item2 = toReplace;
                    if (toUseLeft != null) Item1.ApplyRightMost(toUseLeft.Value);
                    return (null, null, toUseRight, true);
                }
            }
            return (null, null, null, false);
        }
    }

    internal bool Split()
    {
        var ret = false;
        if (Item1 is PlainSnailNumber plain1)
        {
            var newNumber = plain1.DoSplit();
            if (newNumber != null)
            {
                this.Item1 = newNumber;
                ret = true;
            }
        }
        else if (Item1 is PairSnailNumber pair1)
        {
            ret = pair1.Split();
        }
        if (ret) return true;

        if (Item2 is PlainSnailNumber plain2)
        {
            var newNumber = plain2.DoSplit();
            if (newNumber != null)
            {
                this.Item2 = newNumber;
                ret = true;
            }
        }
        else if (Item2 is PairSnailNumber pair2)
        {
            ret = pair2.Split();
        }
        return ret;
    }

    internal override void ApplyLeftMost(int value)
    {
        this.Item1.ApplyLeftMost(value);
    }

    internal override void ApplyRightMost(int value)
    {
        this.Item2.ApplyRightMost(value);
    }

    internal override int GetMagnitude()
    {
        return 3 * this.Item1.GetMagnitude() + 2 * this.Item2.GetMagnitude();
    }

    internal override SnailNumber Clone()
    {
        return new PairSnailNumber(this.Item1.Clone(), this.Item2.Clone());
    }
}