//var inputText =
//@"start-A
//start-b
//A-c
//A-b
//b-d
//A-end
//b-end";
//var inputText =
//@"dc-end
//HN-start
//start-kj
//dc-start
//dc-HN
//LN-dc
//HN-end
//kj-sa
//kj-HN
//kj-dc";

var inputText =
@"mj-TZ
start-LY
TX-ez
uw-ez
ez-TZ
TH-vn
sb-uw
uw-LY
LY-mj
sb-TX
TH-end
end-LY
mj-start
TZ-sb
uw-RR
start-TZ
mj-TH
ez-TH
sb-end
LY-ez
TX-mt
vn-sb
uw-vn
uw-TZ";
var pairs = inputText.Split("\n").Select(pair =>
{
    var x = pair.Split("-");
    return (x[0], x[1]);
}).ToList();

var paths = getAllPaths(pairs, "start", "start", "end", false, new List<string> { "start" }).ToList();


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


IEnumerable<IEnumerable<string>> getAllPaths(IEnumerable<(string, string)> pairs, string current, string start, string end, bool twiceUsed, IEnumerable<string> currentPath)
{
    if (!pairs.Any())
    {
        yield break;
    }
    foreach (var pair in pairs.Where(p => p.Item1 == current || p.Item2 == current))
    {
        var other = pair.Item1 == current ? pair.Item2 : pair.Item1;
        var newPath = currentPath.Concat(new List<string> { other });
        if (other == end)
        {
            yield return newPath;
        }
        else if (other.ToLower() != other)
        {
            var restPaths = getAllPaths(pairs, other, start, end, twiceUsed, newPath);
            foreach (var path in restPaths)
            {
                yield return path;
            }
        }
        else if (other != start && other != end)
        {
            if (currentPath.Contains(other))
            {
                if (!twiceUsed)
                {
                    var restPaths = getAllPaths(pairs, other, start, end, true, newPath);
                    foreach (var path in restPaths)
                    {
                        yield return path;
                    }
                }
            }
            else
            {
                var restPaths = getAllPaths(pairs, other, start, end, twiceUsed, newPath);
                foreach (var path in restPaths)
                {
                    yield return path;
                }
            }
        }
    }
}
