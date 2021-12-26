var lines = File.ReadAllLines(@"N:\knowledgebase\aoc\input2").Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

var (oxygen, co2) = getOxygenAndCo2(lines);

Console.WriteLine($"(oxygen, co2) = ({oxygen}, {co2}) = {oxygen * co2}");

Console.WriteLine("Hello, World!");


(long oxygen, long co2) getOxygenAndCo2(IEnumerable<string> lines)
{
    return (getByBitCriteria(true, lines, ""), getByBitCriteria(false, lines, ""));
}

long getByBitCriteria(bool mostCommon, IEnumerable<string> lines, string prefix)
{
    if (lines.Count() == 1)
    {
        return Convert.ToInt64(prefix+lines.First(), 2);
    }
    var firstCol = lines.Select(l => l[0]);
    var oneIsMostCommonNum = firstCol.Where(c => c == '1').Count() * 2 >= lines.Count();
    var mostCommonNumber = oneIsMostCommonNum ? 1 : 0;
    var toFilter = (mostCommon ? mostCommonNumber : 1 - mostCommonNumber).ToString()[0];
    var nextLines = lines.Where(l => l[0] == toFilter).Select(l => l.Substring(1)).ToList();
    return getByBitCriteria(mostCommon, nextLines, prefix + toFilter);
}