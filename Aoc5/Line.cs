using System.Drawing;

internal class Line
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }

    public Line(string l)
    {
        var ps = l.Split(" -> ");
        this.StartPoint = parsePoint(ps[0]);
        this.EndPoint = parsePoint(ps[1]);
    }

    private Point parsePoint(string v)
    {
        var z = v.Split(",").Select(x => int.Parse(x));
        return new Point(z.First(), z.Last());
    }
    public bool IsHorizontal { get { return StartPoint.Y == EndPoint.Y; } }
    public bool IsVertical { get { return StartPoint.X == EndPoint.X; } }
    public bool IsTLBR { get {
            return
(StartPoint.X < EndPoint.X && StartPoint.Y < EndPoint.Y)
||
(StartPoint.X > EndPoint.X && StartPoint.Y > EndPoint.Y);
                } }


    public int MinX { get { return StartPoint.X < EndPoint.X ? StartPoint.X : EndPoint.X; } }
    public int MaxX { get { return StartPoint.X < EndPoint.X ? EndPoint.X : StartPoint.X; } }

    public int MinY { get { return StartPoint.Y < EndPoint.Y ? StartPoint.Y : EndPoint.Y; } }
    public int MaxY { get { return StartPoint.Y < EndPoint.Y ? EndPoint.Y : StartPoint.Y; } }    
}