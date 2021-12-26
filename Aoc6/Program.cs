//var lines = File.ReadAllLines("input6A.txt").ToList();
//var initialStates = lines.SelectMany(line => line.Split(",")).Select(x => int.Parse(x));
var initialStates = "3,5,3,5,1,3,1,1,5,5,1,1,1,2,2,2,3,1,1,5,1,1,5,5,3,2,2,5,4,4,1,5,1,4,4,5,2,4,1,1,5,3,1,1,4,1,1,1,1,4,1,1,1,1,2,1,1,4,1,1,1,2,3,5,5,1,1,3,1,4,1,3,4,5,1,4,5,1,1,4,1,3,1,5,1,2,1,1,2,1,4,1,1,1,4,4,3,1,1,1,1,1,4,1,4,5,2,1,4,5,4,1,1,1,2,2,1,4,4,1,1,4,1,1,1,2,3,4,2,4,1,1,5,4,2,1,5,1,1,5,1,2,1,1,1,5,5,2,1,4,3,1,2,2,4,1,2,1,1,5,1,3,2,4,3,1,4,3,1,2,1,1,1,1,1,4,3,3,1,3,1,1,5,1,1,1,1,3,3,1,3,5,1,5,5,2,1,2,1,4,2,3,4,1,4,2,4,2,5,3,4,3,5,1,2,1,1,4,1,3,5,1,4,1,2,4,3,1,5,1,1,2,2,4,2,3,1,1,1,5,2,1,4,1,1,1,4,1,3,3,2,4,1,4,2,5,1,5,2,1,4,1,3,1,2,5,5,4,1,2,3,3,2,2,1,3,3,1,4,4,1,1,4,1,1,5,1,2,4,2,1,4,1,1,4,3,5,1,2,1"
    .Split(",").Select(x => long.Parse(x));

var tmp = initialStates.GroupBy(s => s).ToDictionary(x => x.Key, x => x.Count());
var state = Enumerable.Range(0, 9).Select(i => tmp.ContainsKey(i) ? tmp[i] : (long)0).ToList();

for (var iteration = 0; iteration < 256; iteration++)
{
    var newState = Enumerable.Range(0, 9).Select(i => (long)0).ToList();
    for (var i = 0; i < 9; i++)
    {
        if (i == 0)
        {
            newState[8] += state[0];
            newState[6] += state[0];
        }
        else
        {
            newState[i - 1] += state[i];
        }
    }
    state = newState;
}

var total = state.Sum(x => x);
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
