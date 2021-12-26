var inputText =
@"OFSVVSFOCBNONHKFHNPK

HN -> C
VB -> K
PF -> C
BO -> F
PB -> F
OH -> H
OB -> N
PN -> O
KO -> V
CK -> V
FP -> H
PC -> V
PP -> N
FN -> N
CC -> F
FC -> N
BP -> N
SH -> F
NS -> V
KK -> B
HS -> C
NV -> N
FO -> B
VO -> S
KN -> F
SC -> V
NB -> H
CH -> B
SF -> V
NP -> V
FB -> P
CV -> B
PO -> P
SV -> P
OO -> V
PS -> C
CO -> N
SP -> B
KP -> H
KH -> S
KS -> S
NH -> K
SS -> P
PV -> P
KV -> V
ON -> N
BS -> C
HP -> K
SB -> P
VC -> B
HB -> N
FS -> V
VP -> K
BB -> N
FK -> S
CS -> P
SO -> F
HF -> F
VV -> C
BC -> S
SN -> K
KB -> H
BN -> H
HO -> S
KC -> F
CP -> S
HC -> S
OS -> K
NK -> N
BF -> S
VN -> B
SK -> K
HV -> B
KF -> H
FV -> B
VF -> H
BH -> S
NN -> O
HH -> K
CN -> H
PH -> V
NF -> S
OV -> P
OC -> V
OK -> H
OF -> H
HK -> N
FH -> P
BK -> N
VS -> H
NO -> V
VK -> K
CF -> N
CB -> N
NC -> K
PK -> B
VH -> F
FF -> C
BV -> P
OP -> K";
//var inputText =
//@"NNCB

//CH -> B
//HH -> N
//CB -> H
//NH -> C
//HB -> C
//HC -> B
//HN -> C
//NN -> C
//BH -> H
//NC -> B
//NB -> B
//BN -> B
//BB -> N
//BC -> B
//CC -> N
//CN -> C";

var inputLines = inputText.Split("\n");
var template = inputLines.Take(1).Single();
var rawRules = inputLines.Skip(2).Select(line => line.Split(" -> ")).ToDictionary(x => x[0], x => x[1]);
var rules = rawRules.ToDictionary(rule => rule.Key, rule =>
    new List<string>{
        rule.Key.Substring(0,1) + rule.Value,
        rule.Value + rule.Key.Substring(1,1)
    }
);

var originalPairs = Enumerable.Range(0, template.Length - 1).Select(i => template.Substring(i, 2)).ToList();
var newPairs1 = applyRules(originalPairs, rules).ToList();
var newPairs2 = applyRules(newPairs1, rules).ToList();
var newPairs3 = applyRules(newPairs2, rules).ToList();
var newPairs4 = applyRules(newPairs3, rules).ToList();

var output1 = makeOutput(newPairs1);
var output2 = makeOutput(newPairs2);
var output3 = makeOutput(newPairs3);
var output4 = makeOutput(newPairs4);


Console.WriteLine(output1);
Console.WriteLine(output2);
Console.WriteLine(output3);
Console.WriteLine(output4);

//var result = Enumerable.Range(0, 40).Aggregate(originalPairs, (pairs, _) => applyRules(pairs, rules).ToList());

const int iterationCount = 40;
var onlyLastResult = Enumerable.Range(0, iterationCount).Aggregate(originalPairs.Last(), (pair, _) => applyRuleLast(pair, rules));
var lastChar = onlyLastResult.Substring(1);
var asCounts = originalPairs.GroupBy(p => p).ToDictionary(p => p.Key, p => (long)p.Count());

var newCounts = Enumerable.Range(0, iterationCount).Aggregate(asCounts, (pair, _) => applyCountRules(pair, rules));

var individualCounts = ExtractCharCounts(newCounts, lastChar);



var mostCommon = individualCounts.OrderByDescending(c => c.Value).First();
var leastCommon = individualCounts.OrderBy(c => c.Value).First();
var diff = mostCommon.Value - leastCommon.Value;

//var asString = makeOutput(result);
//var counts = asString.ToCharArray().GroupBy(c => c).Select(g => (c: g.Key, count: g.Count()));
//var mostCommon = counts.OrderByDescending(c => c.count).First();
//var leastCommon = counts.OrderBy(c => c.count).First();
//var diff = mostCommon.count - leastCommon.count;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

IEnumerable<string> applyRules(IEnumerable<string> originalPairs, Dictionary<string, List<string>> rules)
{
    foreach (var pair in originalPairs)
    {
        var extra = rules[pair];
        foreach (var item in extra)
        {
            yield return item;
        }
    }
}

string applyRuleLast(string originalPair, Dictionary<string, List<string>> rules)
{
    var extra = rules[originalPair];
    return extra.Last();
}

Dictionary<string, long> applyCountRules(Dictionary<string, long> asCounts, Dictionary<string, List<string>> rules)
{
    var ret = new Dictionary<string, long>();
    foreach (var count in asCounts)
    {
        var extra = rules[count.Key];
        foreach (var item in extra)
        {
            AddOrUpdate(ref ret, item, count.Value);
        }
    }
    return ret;
}

void AddOrUpdate(ref Dictionary<string, long> ret, string item, long value)
{
    if (ret.ContainsKey(item))
    {
        ret[item] += value;
    }
    else
    {
        ret.Add(item, value);
    }
}

string makeOutput(List<string> pairs)
{
    return string.Join("", pairs.Select(p => p.Substring(0, 1))) + pairs.Last().Substring(1);
}

Dictionary<string, long> ExtractCharCounts(Dictionary<string, long> newCounts, string lastChar)
{
    var ret = new Dictionary<string, long>();
    foreach (var x in newCounts)
    {
        AddOrUpdate(ref ret, x.Key.Substring(0, 1), x.Value);
    }
    AddOrUpdate(ref ret, lastChar, 1);
    return ret;
}