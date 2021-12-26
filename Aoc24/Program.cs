
var inputText =
@"inp w
mul x 0
add x z
mod x 26
div z 1
add x 12
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 4
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 15
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 11
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 11
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 7
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -14
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 2
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 12
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 11
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -10
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 13
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 11
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 9
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 13
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 12
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -7
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 6
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 10
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 2
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -2
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 11
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -1
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 12
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -4
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 3
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -12
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 13
mul y x
add z y";
var instructionLines = inputText.Split("\n");

//var inputValue = "13579246899999";

var numberOfSets = 14;
var numberOfInstructions = numberOfSets * 18;

for(int i = 1; i <= 9; i++)
{
    for (int j = 1; j <= 9; j++)
    {
        var inputValue = $"91811211611981";
        var inputValues = inputValue.ToCharArray().Select(s => long.Parse(s.ToString()));
        var result = executeInstructions(instructionLines.Take(numberOfInstructions).ToList(), inputValues);

        var decoded = DecodeInt(result);
        Console.WriteLine($"{i},{j}:{result}");
        Console.WriteLine(string.Join(",", decoded));
        Console.WriteLine(string.Join("", decoded.Select(i => (char)('A' + i))));
        Console.WriteLine("");
    }
}



// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");



long executeInstructions(IList<string> instructionLines, IEnumerable<long> inputValues)
{
    var registers = new Dictionary<char, long>();
    registers.Add('w', 0);
    registers.Add('x', 0);
    registers.Add('y', 0);
    registers.Add('z', 0);

    var inputQueue = new Queue<long>(inputValues);

    for (int ip = 0; ip < instructionLines.Count; ip++)
    {
        //if (ip % 18 == 0 && ip > 0)
        //{
        //    var decoded = DecodeInt(registers['z']);
        //    Console.WriteLine($"{ip}:{registers['z']}");
        //    Console.WriteLine(string.Join(",", decoded));
        //    Console.WriteLine(string.Join("", decoded.Select(i => (char)('A' + i))));
        //    Console.WriteLine("");
        //}
        var s = instructionLines[ip].Split(" ");
        var op = s[0];
        var reg = s[1][0];
        if (s.Length == 2)
        {
            var input = inputQueue.Dequeue();
            registers[reg] = input;
        }
        else
        {
            long number = 0;
            if (!long.TryParse(s[2], out number))
            {
                number = registers[s[2][0]];
            }
            executeInstruction(op, reg, number, registers);
        }
    }
    return registers['z'];
}

List<long> DecodeInt(long v)
{
    var ret = new List<long>();
    while (v > 0)
    {
        ret.Add(v % 26);
        v = v / 26;
    }
    ret.Reverse();
    return ret;
}

void executeInstruction(string op, char reg, long number, Dictionary<char, long> registers)
{
    if (op == "add")
    {
        registers[reg] += number;
    }
    else if (op == "mul")
    {
        registers[reg] *= number;
    }
    else if (op == "div")
    {
        registers[reg] /= number;
    }
    else if (op == "mod")
    {
        registers[reg] %= number;
    }
    else if (op == "eql")
    {
        registers[reg] = registers[reg] == number ? 1 : 0;
    }
}