internal class Board
{
    public Board(string[] x)
    {
        this.Lines = x.Take(5).Select(y => SplitBoardLine(y).ToList()).ToList();
    }

    private IEnumerable<string> SplitBoardLine(string y)
    {
        for (int i = 0; i < 5; i++)
        {
            yield return y.Substring(i * 3, 2).Trim();
        }
    }

    public IList<List<string>> Lines { get; }

    internal (int NumberOfSteps, int Score) CalculateScore(IEnumerable<string> drawSequence)
    {
        var isUsed = Enumerable.Range(0, 5).Select(i => Enumerable.Range(0, 5).Select(j => false).ToList()).ToList();

        foreach (var (num, numberOfSteps) in drawSequence.Select((x, i) => (x, i)))
        {
            for (int i = 0; i < 5; i++)
            {
                var pos = Lines[i].IndexOf(num);
                if (pos >= 0)
                {
                    isUsed[i][pos] = true;
                    if (isDone(isUsed, i, pos))
                    {
                        return (numberOfSteps, getScore(Lines, isUsed) * int.Parse(num));
                    }
                }
            }
        }
        return (int.MaxValue, 0);
    }

    private int getScore(IList<List<string>> lines, List<List<bool>> isUsed)
    {
        var sum = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (!isUsed[i][j])
                {
                    sum += int.Parse(lines[i][j]);
                }
            }
        }
        return sum;
    }

    private bool isDone(List<List<bool>> isUsed, int i, int pos)
    {
        return isUsed[i].All(x => x) || isUsed.All(u => u[pos]);
    }
}