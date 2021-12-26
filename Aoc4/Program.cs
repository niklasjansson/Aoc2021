var lines = File.ReadAllLines("input4A.txt").ToList();

(var drawSequence, var boards) = ExtractData(lines);
var score = boards.Select(board => board.CalculateScore(drawSequence)).OrderByDescending(x => x.NumberOfSteps).ThenBy(x => x.Score).First().Score;

// See https://aka.ms/new-console-template for more information
Console.WriteLine(score);

(IEnumerable<string> drawSequence, IEnumerable<Board> boards) ExtractData(List<string> lines)
{
    var drawSequence = lines[0].Split(",");
    var boards = lines.Skip(2).Chunk(6).Select(x => new Board(x));
    return (drawSequence, boards);
}
