//var inputText =
//@"#############
//#...........#
//###B#C#B#D###
//  #A#D#C#A#
//  #########";

//var inputText =
//@"#############
//#...........#
//###B#C#B#D###
//  #D#C#B#A#
//  #D#B#A#C#
//  #A#D#C#A#
//  #########";

//var inputText =
//@"#############
//#...........#
//###B#B#D#D###
//  #C#C#A#A#
//  #########";

var inputText =
@"#############
#...........#
###B#B#D#D###
  #D#C#B#A#
  #D#B#A#C#
  #C#C#A#A#
  #########";

(var compressed, var hallwayLength, var rooms, var roomSize) = compressInput(inputText);


var start = compressed;
var goal = createGoal(compressed, hallwayLength, rooms, roomSize);
var moveCost = new Dictionary<char, int>{
     { 'A', 1 },
     { 'B', 10 },
     { 'C', 100 },
     { 'D', 1000 }
};

var openSet = new HashSet<string>() { start };
var gScore = new Dictionary<string, int>();
var hScore = new Dictionary<string, int>();
var fScore = new Dictionary<string, int>();
var prev = new Dictionary<string, string>();

var g = 0;
var h = getHScore(start, hScore, hallwayLength, rooms, moveCost, roomSize);
var f = g + h;

gScore[start] = g;
hScore[start] = h;
fScore[start] = f;

var q = new PriorityQueue<string, int>();
q.Enqueue(start, f);

int i = 0;
while (q.Count > 0)
{
    i++;

    if (i == 34)
    {
        var y = 23;
    }
    var node = q.Dequeue();
    if (i % 10000 == 0)
    {
        Console.WriteLine(i);
        Console.WriteLine(Decompress(node, hallwayLength, rooms, roomSize));
    }

    if (!openSet.Contains(node))
    {
        continue;
    }
    if (node == goal)
    {
        break;
    }
    openSet.Remove(node);
    foreach (var (neighbor, cost) in getNeighbors(node, hallwayLength, rooms, moveCost, roomSize))
    {
        if (neighbor == "      CB   BA D CDA")
        {
            var x = 4;
        }
        var tentativeG = gScore[node] + cost;
        if (tentativeG < getGScore(gScore, neighbor))
        {
            gScore[neighbor] = tentativeG;
            fScore[neighbor] = tentativeG + getHScore(neighbor, hScore, hallwayLength, rooms, moveCost, roomSize);
            prev[neighbor] = node;
            q.Enqueue(neighbor, fScore[neighbor]);
            openSet.Add(neighbor);
        }
    }
}
Console.WriteLine("---");
var path = getPath(prev, goal, start).ToList();
path.Reverse();

foreach (var p in path)
{
    Console.WriteLine($"{gScore[p]}:");
    Console.WriteLine(Decompress(p, hallwayLength, rooms, roomSize));
}

var score = getGScore(gScore, goal);
Console.WriteLine(score);

int getGScore(Dictionary<string, int> gScore, string neighbor)
{
    if (gScore.ContainsKey(neighbor))
    {
        return gScore[neighbor];
    }
    return int.MaxValue;
}




IEnumerable<string> getPath(Dictionary<string, string> prev, string goal, string start)
{
    var current = goal;
    while (current != start)
    {
        yield return current;
        current = prev[current];
    }
    yield return current;
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

int getHScore(string node, Dictionary<string, int> hScore, int hallwayLength, SortedSet<int> rooms, Dictionary<char, int> moveCost, int roomSize)
{
    if (hScore.ContainsKey(node))
    {
        return hScore[node];
    }
    var score = 0;
    for (int i = 0; i < hallwayLength; i++)
    {
        if (node[i] != ' ')
        {
            int roomIndex = node[i] - 'A';
            score += moveCost[node[i]] * (Math.Abs(rooms.ElementAt(roomIndex) - i) + 1);
        }
    }
    for (int i = 0; i < rooms.Count(); i++)
    {
        var roomStartIndex = hallwayLength + roomSize * i;
        var thisType = (char)('A' + i);
        for (int j = 0; j < roomSize; j++)
        {
            var c = node[roomStartIndex + j];
            if (c != ' ' && c != thisType)
            {
                var otherIndex = c - 'A';
                score += moveCost[c] * (Math.Abs(rooms.ElementAt(otherIndex) - rooms.ElementAt(i)) + 1); //at least as many steps as it takes to get over there and in 
            }
        }
    }
    hScore.Add(node, score);
    return hScore[node];
}


(string, int, SortedSet<int>, int) compressInput(string inputText)
{
    var lines = inputText.Split("\n");
    List<int> roomIndices = getRoomIndices(lines[lines.Length - 2]).ToList();
    var hallwayLength = lines.First().Length - 2;
    var hallway = new string(' ', hallwayLength);
    var roomRows = lines.Skip(2).SkipLast(1).ToList();
    var rooms = roomIndices.Select(i => string.Join("", roomRows.Select(r => r[i + 1]))); //+1 since we have an extra #
    var roomIndexSet = new SortedSet<int>(roomIndices);
    return (hallway + string.Join("", rooms), hallwayLength, roomIndexSet, lines.Count() - 3);
}

string Decompress(string node, int hallwayLength, SortedSet<int> rooms, int roomSize)
{
    var roomsAsString = "";
    for (int outputRow = 0; outputRow < roomSize; outputRow++)
    {
        var roomAsString = "";
        var currentIndex = -1;
        for (var roomIndex = 0; roomIndex < rooms.Count; roomIndex++)
        {
            var index = rooms.ElementAt(roomIndex);
            var distanceFromLast = index - currentIndex - 1;
            currentIndex = index;
            roomAsString += new string(' ', distanceFromLast) + node[hallwayLength + roomIndex * roomSize + outputRow];
        }
        roomsAsString += roomAsString + "\n";
    }
    var ret = node[..hallwayLength].Replace(' ', '.') + "\n" + roomsAsString;
    return ret;
}

string createGoal(string compressed, int hallwayLength, SortedSet<int> rooms, int roomSize)
{
    var ret = new string(' ', hallwayLength);
    for (int i = 0; i < rooms.Count(); i++)
    {
        var thisType = (char)('A' + i);
        var count = compressed.Where(c => c == thisType).Count();
        var remainingCount = roomSize - count;
        ret += new string(' ', remainingCount) + new string(thisType, count);
    }
    return ret;
}


IEnumerable<int> getRoomIndices(string v)
{
    for (int i = 0; i < v.Length; i++)
    {
        var c = v[i];
        if (c != ' ' && c != '.' && c != '#')
        {
            yield return i - 1; //-1 == we have an initial #
        }
    }
}

IEnumerable<(string, int)> getNeighbors(string node, int hallwayLength, SortedSet<int> rooms, Dictionary<char, int> moveCost, int roomSize)
{
    for (int i = 0; i < hallwayLength; i++)
    {
        //(var ret, var cost) = moveRight(node, hallwayLength, i, rooms, moveCost);
        //if (ret != null)
        //{
        //    yield return (ret, cost);
        //}
        //(ret, cost) = moveLeft(node, i, rooms, moveCost);
        //if (ret != null)
        //{
        //    yield return (ret, cost);
        //}
        (var ret, var cost) = moveDown(node, hallwayLength, i, rooms, moveCost, roomSize);
        if (ret != null)
        {
            yield return (ret, cost);
        }
    }
    for (int roomNumber = 0; roomNumber < 4; roomNumber++)
    {
        for (int i = 0; i < hallwayLength; i++)
        {
            char thisType = (char)('A' + roomNumber);
            var roomStartIndex = hallwayLength + roomSize * roomNumber;
            var roomHallwayIndex = rooms.ElementAt(roomNumber);
            (var ret, var cost) = moveUp(node, i, roomNumber, hallwayLength, rooms, moveCost, roomSize, roomStartIndex, thisType, roomHallwayIndex);
            if (ret != null)
            {
                yield return (ret, cost);
            }
            //(ret, cost) = moveUpRight(node, roomNumber, hallwayLength, rooms, moveCost, roomSize);
            //if (ret != null)
            //{
            //    yield return (ret, cost);
            //}
        }
    }
}


//(string?, int) moveRight(string node, int hallwayLength, int i, ISet<int> rooms, Dictionary<char, int> moveCost)
//{
//    if (node[i] == ' ') //nothing here
//    {
//        return (null, -1);
//    }
//    var next = i + 1;
//    while (rooms.Contains(next)) //no one can stay outside a room, and we can't move there
//    {
//        next++;
//    }
//    if (next >= hallwayLength) //we're at the far end
//    {
//        return (null, -1);
//    }

//    //`next` is a position we can move to
//    if (node[next] != ' ') //but it is occupied
//    {
//        return (null, -1);
//    }

//    var cost = moveCost[node[i]] * (next - i);
//    var result = node[..i] + ' ' + node[(i + 1)..next] + node[i] + node[(next + 1)..];
//    if (result.Length != node.Length)
//    {
//        var x = "cx";
//    }
//    return (result, cost);
//}


//(string?, int) moveLeft(string node, int i, ISet<int> rooms, Dictionary<char, int> moveCost)
//{
//    if (node[i] == ' ') //nothing here
//    {
//        return (null, -1);
//    }
//    var next = i - 1;
//    while (rooms.Contains(next)) //no one can stay outside a room, and we can't move there
//    {
//        next--;
//    }
//    if (next < 0) //we're at the far end
//    {
//        return (null, -1);
//    }

//    //`next` is a position we can move to
//    if (node[next] != ' ') //but it is occupied
//    {
//        return (null, -1);
//    }

//    var cost = moveCost[node[i]] * (i - next);
//    var result = node[..next] + node[i] + node[(next + 1)..i] + ' ' + node[(i + 1)..];
//    if (result.Length != node.Length)
//    {
//        var x = "cx";
//    }
//    return (result, cost);
//}


(string?, int) moveDown(string node, int hallwayLength, int i, ICollection<int> rooms, Dictionary<char, int> moveCost, int roomSize)
{
    if (node[i] == ' ') //nothing here
    {
        return (null, -1);
    }
    var myRoom = node[i] - 'A';
    var myRoomPos = rooms.ElementAt(myRoom);
    var next = i;
    while (myRoomPos < next)
    {
        next--;
        if (node[next] != ' ') //can't pass them
        {
            return (null, -1);
        }
    }
    while (myRoomPos > next)
    {
        next++;
        if (node[next] != ' ') //can't pass them
        {
            return (null, -1);
        }
    }
    //we have a free path to the room
    var roomStartIndex = hallwayLength + roomSize * myRoom;
    for (int j = roomSize - 1; j >= 0; j--)
    {
        if (node[roomStartIndex + j] == ' ') //vacant so we can enter
        {
            var cost = moveCost[node[i]] * (Math.Abs(next - i) + j + 1); //sideways next-i, down j
            var result =
                //   i < next ?
                node[..i] + ' ' + node[(i + 1)..(roomStartIndex + j)] + node[i] + node[((roomStartIndex + j) + 1)..];
            // node[..i] + ' ' + node[(i + 1) + node[((roomStartIndex + j) + 1)..i] + ' ' + node[(i + 1)..];            
            if (result.Length != node.Length)
            {
                var x = "cx";
            }
            return (result, cost);
        }
        else if (node[roomStartIndex + j] != node[i]) // we still have others in the room, quit
        {
            return (null, -1);
        }
    }
    throw new Exception("this should not happen"); //this means that we had no space in the room  
}


//(string?, int) moveUpLeft(string node, int roomNumber, int hallwayLength, SortedSet<int> rooms, Dictionary<char, int> moveCost, int roomSize)
//{
//    char thisType = (char)('A' + roomNumber);
//    var roomStartIndex = hallwayLength + roomSize * roomNumber;
//    var roomHallwayIndex = rooms.ElementAt(roomNumber);
//    (var retOut, var costOut) = moveUp(node, roomNumber, hallwayLength, rooms, moveCost, roomSize, roomStartIndex, thisType, roomHallwayIndex);
//    if (retOut == null)
//    {
//        return (null, -1);
//    }

//    (var ret, var cost) = moveLeft(retOut, roomHallwayIndex, rooms, moveCost);
//    if (ret == null)
//    {
//        return (null, -1);
//    }
//    return (ret, cost + costOut);
//}

//(string?, int) moveUpRight(string node, int roomNumber, int hallwayLength, SortedSet<int> rooms, Dictionary<char, int> moveCost, int roomSize)
//{
//    char thisType = (char)('A' + roomNumber);
//    var roomStartIndex = hallwayLength + roomSize * roomNumber;
//    var roomHallwayIndex = rooms.ElementAt(roomNumber);
//    (var retOut, var costOut) = moveUp(node, roomNumber, hallwayLength, rooms, moveCost, roomSize, roomStartIndex, thisType, roomHallwayIndex);
//    if (retOut == null)
//    {
//        return (null, -1);
//    }
//    //return (retOut, 0);
//    (var ret, var cost) = moveRight(retOut, hallwayLength, roomHallwayIndex, rooms, moveCost);
//    if (ret == null)
//    {
//        return (null, -1);
//    }
//    return (ret, cost + costOut);
//}

(string?, int) moveUp(string node, int toPos, int roomNumber, int hallwayLength, ICollection<int> rooms, Dictionary<char, int> moveCost, int roomSize, int roomStartIndex, char thisType, int roomHallwayIndex)
{
    bool foundOther = false;
    //can't move to a room pos:
    if (rooms.Contains(toPos))
    {
        return (null, -1);
    }
    for (int j = 0; j < roomSize; j++)
    {
        if (node[roomStartIndex + j] != ' ' &&
            node[roomStartIndex + j] != thisType) //there is at least on incorrect in this room
        {
            foundOther = true;
            break;
        }
    }
    if (!foundOther) //couldn't find any incorrect in this room, we can't move out of it
    {
        return (null, -1);
    }    
    //this room is ok to move from, but can we move to the intended spot?
    if(toPos< roomHallwayIndex)
    {
        if(node[toPos..roomHallwayIndex].Any(c=>c != ' ')) //an obstacle
        {
            return (null, -1);
        }
    }
    else
    {
        if (node[roomHallwayIndex..(toPos+1)].Any(c => c != ' ')) //an obstacle
        {
            return (null, -1);
        }
    }
    
    //ok we can move it, let's find the top one
    for (int j = 0; j < roomSize; j++)
    {
        var c = node[roomStartIndex + j];
        if (c != ' ')
        {
            var cost = moveCost[c] * (j + 1 + Math.Abs(roomHallwayIndex-toPos));
            var result = node[..toPos] + c + node[(toPos + 1)..(roomStartIndex + j)] + ' ' + node[(roomStartIndex + j + 1)..];
            return (result, cost);
        }
    }
    //there was nothing left in this room
    return (null, -1);
}

//(string?, int) moveUp(string node, int roomNumber, int hallwayLength, ICollection<int> rooms, Dictionary<char, int> moveCost, int roomSize, int roomStartIndex, char thisType, int roomHallwayIndex)
//{
//    bool foundOther = false;
//    for (int j = 0; j < roomSize; j++)
//    {
//        if (node[roomStartIndex + j] != ' ' &&
//            node[roomStartIndex + j] != thisType) //there is at least on incorrect in this room
//        {
//            foundOther = true;
//            break;
//        }
//    }
//    if (!foundOther) //couldn't find any incorrect in this room, we can't move out of it
//    {
//        return (null, -1);
//    }

//    for (int j = 0; j < roomSize; j++)
//    {
//        if (node[roomStartIndex + j] != ' ')
//        {
//            var cost = moveCost[node[roomStartIndex + j]] * (j + 1);

//            var result = node[..roomHallwayIndex] + node[roomStartIndex + j] + node[(roomHallwayIndex + 1)..(roomStartIndex + j)] + ' ' + node[(roomStartIndex + j + 1)..];
//            if (result.Length != node.Length)
//            {
//                var x = "cx";
//            }
//            return (result, cost);
//        }
//    }
//    //there was none in this room
//    return (null, -1);
//}