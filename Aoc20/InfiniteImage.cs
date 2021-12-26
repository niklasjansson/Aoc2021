using System.Drawing;

internal class InfiniteImage
{


    public InfiniteImage(IEnumerable<Point> points, int plannedIterations)
    {
        this.MinXExtra = points.Min(p => p.X) - plannedIterations * 2 - 1;
        this.MaxXExtra = points.Max(p => p.X) + plannedIterations * 2 + 1;
        this.MinYExtra = points.Min(p => p.Y) - plannedIterations * 2 - 1;
        this.MaxYExtra = points.Max(p => p.Y) + plannedIterations * 2 + 1;

        this.MinX = points.Min(p => p.X) - plannedIterations;
        this.MaxX = points.Max(p => p.X) + plannedIterations;
        this.MinY = points.Min(p => p.Y) - plannedIterations;
        this.MaxY = points.Max(p => p.Y) + plannedIterations;

        this.Points = new HashSet<Point>(points);
    }

    public InfiniteImage(int minX, int maxX, int minY, int maxY, HashSet<Point> points, int minXExtra, int maxXExtra, int minYExtra, int maxYExtra)
    {
        MinX = minX;
        MaxX = maxX;
        MinY = minY;
        MaxY = maxY;
        Points = points;
        MinXExtra = minXExtra;
        MaxXExtra = maxXExtra;
        MinYExtra = minYExtra;
        MaxYExtra = maxYExtra;
    }

    public int MinX { get; private set; }
    public int MaxX { get; private set; }
    public int MinY { get; private set; }
    public int MaxY { get; private set; }

    public HashSet<Point> Points { get; private set; }
    public int MinXExtra { get; private set; }
    public int MaxXExtra { get; private set; }
    public int MinYExtra { get; private set; }
    public int MaxYExtra { get; private set; }

    public override string ToString()
    {
        const bool extra = false;
        var ret = "";
        if (extra)
        {
            for (int y = MinYExtra; y <= MaxYExtra; y++)
            {
                for (int x = MinXExtra; x <= MaxXExtra; x++)
                {
                    var lit = Points.Contains(new Point(x, y));
                    ret = ret + (lit ? "#" : ".");
                }
                ret = ret + "\n";
            }
        }
        else
        {
            for (int y = MinY; y <= MaxY; y++)
            {
                for (int x = MinX; x <= MaxX; x++)
                {
                    var lit = Points.Contains(new Point(x, y));
                    ret = ret + (lit ? "#" : ".");
                }
                ret = ret + "\n";
            }
        }
        return ret;
    }

    public InfiniteImage applyAlgorithm(IList<bool> algorithm)
    {
        var ret = new HashSet<Point>();
        for (int y = MinYExtra; y <= MaxYExtra; y++)
        {
            for (int x = MinYExtra; x <= MaxXExtra; x++)
            {
                var partialImage = extractImage(x, y, Points);
                var lit = applyAlgorithmToImage(partialImage, algorithm);
                if (lit)
                {
                    ret.Add(new Point(x, y));
                }
            }
        }
        return new InfiniteImage(MinX, MaxX, MinY, MaxY, ret, MinXExtra, MaxXExtra, MinYExtra, MaxYExtra);
    }


    IEnumerable<bool> extractImage(int mx, int my, HashSet<Point> image)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                yield return image.Contains(new Point(x + mx, y + my));
            }
        }
    }

    bool applyAlgorithmToImage(IEnumerable<bool> partialImage, IList<bool> algorithm)
    {
        var number = toNumber(partialImage.ToList());
        return algorithm[number];
    }

    int toNumber(IList<bool> partialImage)
    {
        var ret = 0;
        var n = 1;
        for (int i = 8; i >= 0; i--)
        {
            if (partialImage[i])
            {
                ret += n;
            }
            n *= 2;
        }
        return ret;
    }

    internal IEnumerable<Point> GetPoints()
    {
        return this.Points.Where(p => p.X >= MinX && p.X <= MaxX && p.Y >= MinY && p.Y <= MaxY);
    }
}