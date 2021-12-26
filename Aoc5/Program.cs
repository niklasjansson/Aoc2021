using System.Collections.Generic;
using System.Drawing;

var lines = File.ReadAllLines("input5A.txt").ToList().Select(l => new Line(l));

//var theseLines = lines.Where(line => line.IsHorizontal || line.IsVertical);

var board = ApplyAllLines(lines);

var badSpots = board.Where(x => x.Value >= 2).Count();



// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Dictionary<Point, int> ApplyAllLines(IEnumerable<Line> theseLines)
{
    var ret = new Dictionary<Point, int>();
    foreach (var line in theseLines)
    {
        ApplyLine(ret, line);
    }
    return ret;
}

void ApplyLine(Dictionary<Point, int> ret, Line line)
{
    if (line.IsVertical)
    {
        for (int i = line.MinY; i <= line.MaxY; i++)
        {
            var p = new Point(line.StartPoint.X, i);
            if (!ret.ContainsKey(p))
            {
                ret.Add(p, 0);
            }
            ret[p]++;
        }
    }
    else if (line.IsHorizontal)
    {
        for (int i = line.MinX; i <= line.MaxX; i++)
        {
            var p = new Point(i, line.StartPoint.Y);
            if (!ret.ContainsKey(p))
            {
                ret.Add(p, 0);
            }
            ret[p]++;
        }
    }
    else if (line.IsTLBR)
    {
        for (int i = 0; i <= line.MaxX - line.MinX; i++)
        {
            var p = new Point(line.MinX + i, line.MinY + i);
            if (!ret.ContainsKey(p))
            {
                ret.Add(p, 0);
            }
            ret[p]++;
        }
    }
    else
    {
        for (int i = 0; i <= line.MaxX - line.MinX; i++)
        {
            var p = new Point(line.MinX + i, line.MaxY - i);
            if (!ret.ContainsKey(p))
            {
                ret.Add(p, 0);
            }
            ret[p]++;
        }
    }
}