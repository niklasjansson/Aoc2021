
using System.Drawing;

int minX = 150;
int maxX = 193;

int minY = -136;
int maxY = -86;

//var highestHeight = getHighestHeight(minX, maxX, minY, maxY);
var count = getHeightCount(minX, maxX, minY, maxY);

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


int getHighestHeight(int minX, int maxX, int minY, int maxY)
{
    return getHeights(minX, maxX, minY, maxY).Max();
}

int getHeightCount(int minX, int maxX, int minY, int maxY)
{
    return getHeights(minX, maxX, minY, maxY).Count();
}

IEnumerable<int> getHeights(int minX, int maxX, int minY, int maxY)
{
    for (int xv = 0; xv <= maxX; xv++)
    {
        for (int yv = minY; yv < 2000; yv++)
        {
            var points = getSimulatedPoints(xv, yv, minX, maxX, minY, maxY).ToList();
            if (points.Any(p => p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY))
            {
                yield return points.Max(p => p.Y);
            }
        }
    }
}

IEnumerable<Point> getSimulatedPoints(int xv, int yv, int minX, int maxX, int minY, int maxY)
{
    Point p = new Point(0, 0);
    Point v = new Point(xv, yv);
    while (p.X <= maxX && p.Y >= minY)
    {
        (p, v) = nextStep(p, v);
        yield return p;
    }
}

(Point p, Point v) nextStep(Point p0, Point v0)
{
    var p = p0 + new Size(v0.X, v0.Y);
    var v = new Point(v0.X > 1 ? v0.X - 1 : 0, v0.Y - 1);
    return (p, v);
}