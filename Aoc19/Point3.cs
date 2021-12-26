internal struct Point3
{
    public Point3(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public int X { get; }
    public int Y { get; }
    public int Z { get; }

    public static Point3 operator +(Point3 first, Point3 second)
    {
        return new Point3(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
    }

    public static Point3 operator -(Point3 first, Point3 second)
    {
        return new Point3(first.X - second.X, first.Y - second.Y, first.Z - second.Z);
    }

    public static Point3 operator -(Point3 point)
    {
        return new Point3(-point.X, -point.Y, -point.Z);
    }

    public override string ToString()
    {
        return $"({X},{Y},{Z})";
    }

    internal long Manhattan(Point3 p2)
    {
        var p = p2 - this;
        return Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z);
    }
}