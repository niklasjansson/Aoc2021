
var inputText = @"4438624262
6263251864
2618812434
2134264565
1815131247
2612457325
8585767584
7217134556
2825456563
8248473584";

var energyLevels = inputText.Split("\n").Select(s => s.ToCharArray().Select(c => int.Parse(c.ToString())).ToList()).ToList();

var steps = GetSteps(energyLevels, 10000);

var firstStepWithAllFlashes = steps.Select((step, i) => new { step, i }).Where(x => x.step.numberOfFlashes == 100).First();

var totalFlashes = steps.Sum(step => step.numberOfFlashes);

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


IEnumerable<(int numberOfFlashes, List<List<int>> result)> GetSteps(List<List<int>> energyLevels, int iterationCount)
{
    var currentEnergyLevels = energyLevels;
    for (int i = 0; i < iterationCount; i++)
    {
        var step = NextStep(currentEnergyLevels);
        yield return step;
        currentEnergyLevels = step.result;
    }
}

(int numberOfFlashes, List<List<int>> result) NextStep(List<List<int>> currentEnergyLevels)
{
    ISet<System.Drawing.Point> hadFlash = new HashSet<System.Drawing.Point>();
    var newEnergyLevels = currentEnergyLevels.Select(row => row.Select(col => col + 1).ToList()).ToList();

    while (newEnergyLevels.SelectMany(e => e).Any(e => e > 9))
    {
        for (int row = 0; row < newEnergyLevels.Count(); row++)
        {
            for (int col = 0; col < newEnergyLevels.First().Count(); col++)
            {
                var p = new System.Drawing.Point(col, row);
                if (newEnergyLevels[row][col] > 9 && !hadFlash.Contains(p))
                {
                    hadFlash.Add(p);
                    newEnergyLevels[row][col] = 0;
                    AddEnergyToNeighbors(newEnergyLevels, row, col, hadFlash);
                }
            }
        }
    }
    return (hadFlash.Count(), newEnergyLevels);
}

void AddEnergyToNeighbors(List<List<int>> newEnergyLevels, int row, int col, ISet<System.Drawing.Point> hadFlash)
{
    for (int rowDiff = -1; rowDiff <= 1; rowDiff++)
    {
        for (int colDiff = -1; colDiff <= 1; colDiff++)
        {
            var r = row + rowDiff;
            var c = col + colDiff;
            if (rowDiff == 0 && colDiff == 0)
            {

            }
            else if (r >= 0 && r < newEnergyLevels.Count && c >= 0 && c < newEnergyLevels.First().Count && !hadFlash.Contains(new System.Drawing.Point(c, r)))
            {
                newEnergyLevels[r][c]++;
            }
        }
    }
}