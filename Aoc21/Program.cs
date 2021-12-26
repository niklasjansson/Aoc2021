int scoreA = 0;
int scoreB = 0;
int posA = 6;
int posB = 7;

Dictionary<(int, int, int, int, bool), long> scoreCounts = new Dictionary<(int, int, int, int, bool), long>();
List<(int, int, int, int, bool)> toDistribute = new List<(int, int, int, int, bool)>();

toDistribute.Add((scoreA, scoreB, posA, posB, false));
scoreCounts.Add((scoreA, scoreB, posA, posB, false), 1);
const int maxCount = 21;
while (toDistribute.Any())
{
    toDistribute = toDistribute.OrderBy(c => c.Item1).ThenBy(c => c.Item2).ToList();
    var thisRow = toDistribute.First();
    long count = scoreCounts[thisRow];

    (int, int, int, int, bool) r3 = getNext(thisRow, 3);
    (int, int, int, int, bool) r4 = getNext(thisRow, 4);
    (int, int, int, int, bool) r5 = getNext(thisRow, 5);
    (int, int, int, int, bool) r6 = getNext(thisRow, 6);
    (int, int, int, int, bool) r7 = getNext(thisRow, 7);
    (int, int, int, int, bool) r8 = getNext(thisRow, 8);
    (int, int, int, int, bool) r9 = getNext(thisRow, 9);


    AddToScoreCount(scoreCounts, r3, count * 1);
    AddToScoreCount(scoreCounts, r4, count * 3);
    AddToScoreCount(scoreCounts, r5, count * 6);
    AddToScoreCount(scoreCounts, r6, count * 7);
    AddToScoreCount(scoreCounts, r7, count * 6);
    AddToScoreCount(scoreCounts, r8, count * 3);
    AddToScoreCount(scoreCounts, r9, count * 1);

    toDistribute = toDistribute.Skip(1).ToList();

    AddIfNotAlreadyThere(toDistribute, r3, maxCount);
    AddIfNotAlreadyThere(toDistribute, r4, maxCount);
    AddIfNotAlreadyThere(toDistribute, r5, maxCount);
    AddIfNotAlreadyThere(toDistribute, r6, maxCount);
    AddIfNotAlreadyThere(toDistribute, r7, maxCount);
    AddIfNotAlreadyThere(toDistribute, r8, maxCount);
    AddIfNotAlreadyThere(toDistribute, r9, maxCount);

    Console.WriteLine($"remaining: {toDistribute.Count()}");
}

var winnerA = scoreCounts.Where(x => x.Key.Item1 >= maxCount);
var winnerB = scoreCounts.Where(x => x.Key.Item2 >= maxCount);

var totalCountA = winnerA.Sum(x => x.Value);
var totalCountB = winnerB.Sum(x => x.Value);

Console.WriteLine(totalCountA);
Console.WriteLine(totalCountB);

//21*21*10*10 = 44100
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");



(int, int, int, int, bool) getNext((int, int, int, int, bool) thisRow, int offset)
{
    if (thisRow.Item5)
    {
        var c1 = (thisRow.Item4 + offset - 1) % 10 + 1;        
        return (thisRow.Item1, thisRow.Item2 + c1, thisRow.Item3, c1, false);
    }
    else
    {
        var c1 = (thisRow.Item3 + offset - 1) % 10 + 1;        
        return (thisRow.Item1 + c1, thisRow.Item2, c1, thisRow.Item4, true);
    }
}

void AddToScoreCount(Dictionary<(int, int, int, int, bool), long> scoreCounts, (int, int, int, int, bool) r, long count)
{
    if (scoreCounts.ContainsKey(r))
    {
        scoreCounts[r] += count;
    }
    else
    {
        scoreCounts.Add(r, count);
    }
}

void AddIfNotAlreadyThere(List<(int, int, int, int, bool)> toDistribute, (int, int, int, int, bool) r, int maxCount)
{
    if (r.Item1 < maxCount && r.Item2 < maxCount && !toDistribute.Contains(r))
    {
        toDistribute.Add(r);
    }
}
