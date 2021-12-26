internal class Interval
{
    public Interval(bool on, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)
    {
        this.On = on;
        this.Xmin = xmin;
        this.Xmax = xmax;
        this.Ymin = ymin;
        this.Ymax = ymax;
        this.Zmin = zmin;
        this.Zmax = zmax;
    }

    public bool On { get; }
    public int Xmin { get; }
    public int Xmax { get; }
    public int Ymin { get; }
    public int Ymax { get; }
    public int Zmin { get; }
    public int Zmax { get; }

    internal IEnumerable<(int x, int y, int z)> GetAllLitPoints()
    {
        for (int x = Xmin; x <= Xmax; x++)
        {
            for (int y = Ymin; y <= Ymax; y++)
            {
                for (int z = Zmin; z <= Zmax; z++)
                {
                    yield return (x, y, z);
                }
            }
        }
    }

    internal int GetLitPointsInInterval(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
    {
        var xmin0 = Math.Max(xMin, this.Xmin);
        var xmax0 = Math.Min(xMax, this.Xmax);

        var ymin0 = Math.Max(yMin, this.Ymin);
        var ymax0 = Math.Min(yMax, this.Ymax);

        var zmin0 = Math.Max(zMin, this.Zmin);
        var zmax0 = Math.Min(zMax, this.Zmax);
        if (xmax0 < xmin0 || ymax0 < ymin0 || zmax0 < zmin0)
        {
            return 0;
        }

        return (xmax0 - xmin0+1) * (ymax0 - ymin0+1) * (zmax0 - zmin0+1);
    }

    internal long GetLitPoints()
    {
        return ((long)Xmax - Xmin + 1) * ((long)Ymax - Ymin + 1) * ((long)Zmax - Zmin + 1);
    }

    private (Interval, Interval) SplitX(int x)
    {
        return (
            new Interval(this.On, this.Xmin, x - 1, this.Ymin, this.Ymax, this.Zmin, this.Zmax),
            new Interval(this.On, x, this.Xmax, this.Ymin, this.Ymax, this.Zmin, this.Zmax));
    }

    private (Interval, Interval, Interval) SplitX(int x1, int x2)
    {
        return (
            new Interval(this.On, this.Xmin, x1 - 1, this.Ymin, this.Ymax, this.Zmin, this.Zmax),
            new Interval(this.On, x1, x2, this.Ymin, this.Ymax, this.Zmin, this.Zmax),
            new Interval(this.On, x2 + 1, this.Xmax, this.Ymin, this.Ymax, this.Zmin, this.Zmax));
    }
    private (Interval, Interval) SplitY(int y)
    {
        return (
            new Interval(this.On, this.Xmin, this.Xmax, this.Ymin, y - 1, this.Zmin, this.Zmax),
            new Interval(this.On, this.Xmin, this.Xmax, y, this.Ymax, this.Zmin, this.Zmax));
    }

    private (Interval, Interval, Interval) SplitY(int y1, int y2)
    {
        return (
            new Interval(this.On, this.Xmin, this.Xmax, this.Ymin, y1 - 1, this.Zmin, this.Zmax),
            new Interval(this.On, this.Xmin, this.Xmax, y1, y2, this.Zmin, this.Zmax),
            new Interval(this.On, this.Xmin, this.Xmax, y2 + 1, this.Ymax, this.Zmin, this.Zmax));
    }
    private (Interval, Interval) SplitZ(int z)
    {
        return (
            new Interval(this.On, this.Xmin, this.Xmax, this.Ymin, this.Ymax, this.Zmin, z - 1),
            new Interval(this.On, this.Xmin, this.Xmax, this.Ymin, this.Ymax, z, this.Zmax));
    }

    private (Interval, Interval, Interval) SplitZ(int z1, int z2)
    {
        return (
            new Interval(this.On, this.Xmin, this.Xmax, this.Ymin, this.Ymax, this.Zmin, z1 - 1),
            new Interval(this.On, this.Xmin, this.Xmax, this.Ymin, this.Ymax, z1, z2),
            new Interval(this.On, this.Xmin, this.Xmax, this.Ymin, this.Ymax, z2 + 1, this.Zmax));
    }

    internal (IEnumerable<Interval> existingSplitted, IEnumerable<Interval> newSplitted)? Intersects(Interval existing)
    {
        List<Interval> retExisting = new List<Interval>();
        List<Interval> retNew = new List<Interval>();
        Interval? remainingExisting = existing;
        Interval? remainingNew = this;
        if (!HasIntersection(existing))
        {
            return null;
        }
        var xSplit = intersectX(remainingNew, remainingExisting);
        {
            retExisting.AddRange(xSplit.Value.existingSplitted);
            retNew.AddRange(xSplit.Value.newSplitted);
            remainingExisting = xSplit.Value.remainingExisting;
            remainingNew = xSplit.Value.remainingNew;
        }
        var ySplit = intersectY(remainingNew, remainingExisting);
        if (ySplit != null)
        {
            retExisting.AddRange(ySplit.Value.existingSplitted);
            retNew.AddRange(ySplit.Value.newSplitted);
            remainingExisting = ySplit.Value.remainingExisting;
            remainingNew = ySplit.Value.remainingNew;
        }
        var zSplit = intersectZ(remainingNew, remainingExisting);
        if (zSplit != null)
        {
            retExisting.AddRange(zSplit.Value.existingSplitted);
            retNew.AddRange(zSplit.Value.newSplitted);
            remainingExisting = zSplit.Value.remainingExisting;
            remainingNew = zSplit.Value.remainingNew;
        }
        if (remainingExisting != null && this.On)
        {
            retExisting.Add(remainingExisting);
        }
        return (existingSplitted: retExisting, newSplitted: retNew);
    }

    

    private bool HasIntersection(Interval existing)
    {
        return
            existing.Xmin <= this.Xmax &&
            existing.Xmax >= this.Xmin &&
            existing.Ymin <= this.Ymax &&
            existing.Ymax >= this.Ymin &&
            existing.Zmin <= this.Zmax &&
            existing.Zmax >= this.Zmin;
    }

    private (IEnumerable<Interval> existingSplitted, IEnumerable<Interval> newSplitted, Interval remainingExisting, Interval remainingNew)? intersectX(Interval remainingNew, Interval remainingExisting)
    {
        if (remainingNew == null || remainingExisting == null)
        {
            return null;
        }
        if (remainingExisting.Xmin < remainingNew.Xmin)
        {
            if (remainingExisting.Xmax < remainingNew.Xmin)
            {
                return null; //no intersection
            }
            else if (remainingExisting.Xmax < remainingNew.Xmax)//remaining starts to the left and ends in the middle
            {
                var (r0, r1) = remainingExisting.SplitX(remainingNew.Xmin);
                var (n0, n1) = remainingNew.SplitX(remainingExisting.Xmax + 1);
                return (new List<Interval> { r0 }, new List<Interval> { n1 }, r1, n0);
            }
            else if (remainingExisting.Xmax == remainingNew.Xmax) //remaining starts to the left and ends at the end
            {
                var (r0, r1) = remainingExisting.SplitX(remainingNew.Xmin);
                return (new List<Interval> { r0 }, new List<Interval> { }, r1, remainingNew);
            }
            else  //new is fully contained
            {
                var (r0, r1, r2) = remainingExisting.SplitX(remainingNew.Xmin, remainingNew.Xmax);
                return (new List<Interval> { r0, r2 }, new List<Interval> { }, r1, remainingNew);
            }
        }
        else if (remainingExisting.Xmin == remainingNew.Xmin)
        {
            if (remainingExisting.Xmax < remainingNew.Xmax) //remaining ends in the middle
            {
                var (n0, n1) = remainingNew.SplitX(remainingExisting.Xmax + 1);
                return (new List<Interval> { }, new List<Interval> { n1 }, remainingExisting, n0);
            }
            else if (remainingExisting.Xmax == remainingNew.Xmax) //identical
            {
                return (new List<Interval> { }, new List<Interval> { }, remainingExisting, remainingNew);
            }
            else  //remaining ends right of
            {
                var (r0, r1) = remainingExisting.SplitX(remainingNew.Xmax + 1);
                return (new List<Interval> { r1 }, new List<Interval> { }, r0, remainingNew);
            }
        }
        else if (remainingExisting.Xmin < remainingNew.Xmax)
        {
            if (remainingExisting.Xmax < remainingNew.Xmax) //remaining is fully contained
            {
                var (n0, n1, n2) = remainingNew.SplitX(remainingExisting.Xmin, remainingExisting.Xmax);
                return (new List<Interval> { }, new List<Interval> { n0, n2 }, remainingExisting, n1);
            }
            else if (remainingExisting.Xmax == remainingNew.Xmax) //stops at the end
            {
                var (n0, n1) = remainingNew.SplitX(remainingExisting.Xmin);
                return (new List<Interval> { }, new List<Interval> { n0 }, remainingExisting, n1);
            }
            else  //remaining starts in middle and ends right of
            {
                var (r0, r1) = remainingExisting.SplitX(remainingNew.Xmax + 1);
                var (n0, n1) = remainingNew.SplitX(remainingExisting.Xmin);
                return (new List<Interval> { r1 }, new List<Interval> { n0 }, r0, n1);
            }
        }
        else if (remainingExisting.Xmin == remainingNew.Xmax) //stops right at the end
        {
            if (remainingExisting.Xmax < remainingNew.Xmax) //remaining is fully contained
            {
                var (n0, n1, n2) = remainingNew.SplitX(remainingExisting.Xmin, remainingExisting.Xmax);
                return (new List<Interval> { }, new List<Interval> { n0, n2 }, remainingExisting, n1);
            }
            else if (remainingExisting.Xmax == remainingNew.Xmax) //stops at the end
            {
                var (n0, n1) = remainingNew.SplitX(remainingExisting.Xmin);
                return (new List<Interval> { }, new List<Interval> { n0 }, remainingExisting, n1);
            }
            else  //remaining starts in middle and ends right of
            {
                var (r0, r1) = remainingExisting.SplitX(remainingNew.Xmax + 1);
                var (n0, n1) = remainingNew.SplitX(remainingExisting.Xmin);
                return (new List<Interval> { r1 }, new List<Interval> { n0 }, r0, n1);
            }
        }
        else //existing completely to the right
        {
            return null;
        }
    }

    private (IEnumerable<Interval> existingSplitted, IEnumerable<Interval> newSplitted, Interval remainingExisting, Interval remainingNew)? intersectY(Interval remainingNew, Interval remainingExisting)
    {
        if (remainingNew == null || remainingExisting == null)
        {
            return null;
        }
        if (remainingExisting.Ymin < remainingNew.Ymin)
        {
            if (remainingExisting.Ymax < remainingNew.Ymin)
            {
                return null; //no intersection
            }
            else if (remainingExisting.Ymax < remainingNew.Ymax)//remaining starts to the left and ends in the middle
            {
                var (r0, r1) = remainingExisting.SplitY(remainingNew.Ymin);
                var (n0, n1) = remainingNew.SplitY(remainingExisting.Ymax + 1);
                return (new List<Interval> { r0 }, new List<Interval> { n1 }, r1, n0);
            }
            else if (remainingExisting.Ymax == remainingNew.Ymax) //remaining starts to the left and ends at the end
            {
                var (r0, r1) = remainingExisting.SplitY(remainingNew.Ymin);
                return (new List<Interval> { r0 }, new List<Interval> { }, r1, remainingNew);
            }
            else  //new is fully contained
            {
                var (r0, r1, r2) = remainingExisting.SplitY(remainingNew.Ymin, remainingNew.Ymax);
                return (new List<Interval> { r0, r2 }, new List<Interval> { }, r1, remainingNew);
            }
        }
        else if (remainingExisting.Ymin == remainingNew.Ymin)
        {
            if (remainingExisting.Ymax < remainingNew.Ymax) //remaining ends in the middle
            {
                var (n0, n1) = remainingNew.SplitY(remainingExisting.Ymax + 1);
                return (new List<Interval> { }, new List<Interval> { n1 }, remainingExisting, n0);
            }
            else if (remainingExisting.Ymax == remainingNew.Ymax) //identical
            {
                return (new List<Interval> { }, new List<Interval> { }, remainingExisting, remainingNew);
            }
            else  //remaining ends right of
            {
                var (r0, r1) = remainingExisting.SplitY(remainingNew.Ymax + 1);
                return (new List<Interval> { r1 }, new List<Interval> { }, r0, remainingNew);
            }
        }
        else if (remainingExisting.Ymin < remainingNew.Ymax)
        {
            if (remainingExisting.Ymax < remainingNew.Ymax) //remaining is fully contained
            {
                var (n0, n1, n2) = remainingNew.SplitY(remainingExisting.Ymin, remainingExisting.Ymax);
                return (new List<Interval> { }, new List<Interval> { n0, n2 }, remainingExisting, n1);
            }
            else if (remainingExisting.Ymax == remainingNew.Ymax) //stops at the end
            {
                var (n0, n1) = remainingNew.SplitY(remainingExisting.Ymin);
                return (new List<Interval> { }, new List<Interval> { n0 }, remainingExisting, n1);
            }
            else  //remaining starts in middle and ends right of
            {
                var (r0, r1) = remainingExisting.SplitY(remainingNew.Ymax + 1);
                var (n0, n1) = remainingNew.SplitY(remainingExisting.Ymin);
                return (new List<Interval> { r1 }, new List<Interval> { n0 }, r0, n1);
            }
        }
        else if (remainingExisting.Ymin == remainingNew.Ymax) //stops right at the end
        {
            if (remainingExisting.Ymax < remainingNew.Ymax) //remaining is fully contained
            {
                var (n0, n1, n2) = remainingNew.SplitY(remainingExisting.Ymin, remainingExisting.Ymax);
                return (new List<Interval> { }, new List<Interval> { n0, n2 }, remainingExisting, n1);
            }
            else if (remainingExisting.Ymax == remainingNew.Ymax) //stops at the end
            {
                var (n0, n1) = remainingNew.SplitY(remainingExisting.Ymin);
                return (new List<Interval> { }, new List<Interval> { n0 }, remainingExisting, n1);
            }
            else  //remaining starts in middle and ends right of
            {
                var (r0, r1) = remainingExisting.SplitY(remainingNew.Ymax + 1);
                var (n0, n1) = remainingNew.SplitY(remainingExisting.Ymin);
                return (new List<Interval> { r1 }, new List<Interval> { n0 }, r0, n1);
            }
        }
        else //existing completely to the right
        {
            return null;
        }
    }

    private (IEnumerable<Interval> existingSplitted, IEnumerable<Interval> newSplitted, Interval remainingExisting, Interval remainingNew)? intersectZ(Interval remainingNew, Interval remainingExisting)
    {
        if (remainingNew == null || remainingExisting == null)
        {
            return null;
        }
        if (remainingExisting.Zmin < remainingNew.Zmin)
        {
            if (remainingExisting.Zmax < remainingNew.Zmin)
            {
                return null; //no intersection
            }
            else if (remainingExisting.Zmax < remainingNew.Zmax)//remaining starts to the left and ends in the middle
            {
                var (r0, r1) = remainingExisting.SplitZ(remainingNew.Zmin);
                var (n0, n1) = remainingNew.SplitZ(remainingExisting.Zmax + 1);
                return (new List<Interval> { r0 }, new List<Interval> { n1 }, r1, n0);
            }
            else if (remainingExisting.Zmax == remainingNew.Zmax) //remaining starts to the left and ends at the end
            {
                var (r0, r1) = remainingExisting.SplitZ(remainingNew.Zmin);
                return (new List<Interval> { r0 }, new List<Interval> { }, r1, remainingNew);
            }
            else  //new is fully contained
            {
                var (r0, r1, r2) = remainingExisting.SplitZ(remainingNew.Zmin, remainingNew.Zmax);
                return (new List<Interval> { r0, r2 }, new List<Interval> { }, r1, remainingNew);
            }
        }
        else if (remainingExisting.Zmin == remainingNew.Zmin)
        {
            if (remainingExisting.Zmax < remainingNew.Zmax) //remaining ends in the middle
            {
                var (n0, n1) = remainingNew.SplitZ(remainingExisting.Zmax + 1);
                return (new List<Interval> { }, new List<Interval> { n1 }, remainingExisting, n0);
            }
            else if (remainingExisting.Zmax == remainingNew.Zmax) //identical
            {
                return (new List<Interval> { }, new List<Interval> { }, remainingExisting, remainingNew);
            }
            else  //remaining ends right of
            {
                var (r0, r1) = remainingExisting.SplitZ(remainingNew.Zmax + 1);
                return (new List<Interval> { r1 }, new List<Interval> { }, r0, remainingNew);
            }
        }
        else if (remainingExisting.Zmin < remainingNew.Zmax)
        {
            if (remainingExisting.Zmax < remainingNew.Zmax) //remaining is fully contained
            {
                var (n0, n1, n2) = remainingNew.SplitZ(remainingExisting.Zmin, remainingExisting.Zmax);
                return (new List<Interval> { }, new List<Interval> { n0, n2 }, remainingExisting, n1);
            }
            else if (remainingExisting.Zmax == remainingNew.Zmax) //stops at the end
            {
                var (n0, n1) = remainingNew.SplitZ(remainingExisting.Zmin);
                return (new List<Interval> { }, new List<Interval> { n0 }, remainingExisting, n1);
            }
            else  //remaining starts in middle and ends right of
            {
                var (r0, r1) = remainingExisting.SplitZ(remainingNew.Zmax + 1);
                var (n0, n1) = remainingNew.SplitZ(remainingExisting.Zmin);
                return (new List<Interval> { r1 }, new List<Interval> { n0 }, r0, n1);
            }
        }
        else if (remainingExisting.Zmin == remainingNew.Zmax) //stops right at the end
        {
            if (remainingExisting.Zmax < remainingNew.Zmax) //remaining is fully contained
            {
                var (n0, n1, n2) = remainingNew.SplitZ(remainingExisting.Zmin, remainingExisting.Zmax);
                return (new List<Interval> { }, new List<Interval> { n0, n2 }, remainingExisting, n1);
            }
            else if (remainingExisting.Zmax == remainingNew.Zmax) //stops at the end
            {
                var (n0, n1) = remainingNew.SplitZ(remainingExisting.Zmin);
                return (new List<Interval> { }, new List<Interval> { n0 }, remainingExisting, n1);
            }
            else  //remaining starts in middle and ends right of
            {
                var (r0, r1) = remainingExisting.SplitZ(remainingNew.Zmax + 1);
                var (n0, n1) = remainingNew.SplitZ(remainingExisting.Zmin);
                return (new List<Interval> { r1 }, new List<Interval> { n0 }, r0, n1);
            }
        }
        else //existing completely to the right
        {
            return null;
        }
    }
}