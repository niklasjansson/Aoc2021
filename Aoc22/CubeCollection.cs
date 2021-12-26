internal class CubeCollection
{
    public CubeCollection()
    {
        this.ExistingCubes = new List<Interval>();
    }

    public List<Interval> ExistingCubes { get; private set; }


    internal void Add(Interval startCube)
    {
        this.ExistingCubes = Add(this.ExistingCubes, startCube).ToList();
    }

    internal static IEnumerable<Interval> Add(IEnumerable<Interval> collection, Interval startCube)
    {
        var newCollection = collection;
        var allToAdd = new Stack<Interval>();
        allToAdd.Push(startCube);

        while (allToAdd.Any())
        {
            var cube = allToAdd.Pop();
            var x = AddCubeToCollection(newCollection, cube);
            foreach (var c in x.moreToAdd)
            {
                allToAdd.Push(c);
            }
            newCollection = x.newCollection;
        }
        return newCollection;
    }

    private static (IEnumerable<Interval> newCollection, IEnumerable<Interval> moreToAdd) AddCubeToCollection(IEnumerable<Interval> collection, Interval startCube)
    {
        var newCollection = collection.ToList();
        foreach (var cube in collection)
        {
            var intersectionResult = startCube.Intersects(cube);
            if (intersectionResult != null)
            {
                newCollection.Remove(cube);
                newCollection.AddRange(intersectionResult.Value.existingSplitted);
                return (newCollection, intersectionResult.Value.newSplitted);
            }
        }
        if (startCube.On)
        {
            newCollection.Add(startCube);
        }
        return (newCollection, Enumerable.Empty<Interval>());
    }

    //internal IEnumerable<Interval> GetNewCubes(Interval startCube, List<Interval> existingCubes)
    //{
    //    List<Interval> existingToCheck = existingCubes.Select(c => c).ToList();
    //    var newToCheck = new Stack<Interval>();
    //    newToCheck.Push(startCube);

    //    while (newToCheck.Any())
    //    {
    //        var cube = newToCheck.Pop();

    //        IntersectEverything(existingToCheck);

    //        if (finished != null)
    //        {
    //            ret.Add(finished);
    //        }
    //    }
    //    if (newToCheck.Any())
    //    {
    //        ret.AddRange(newToCheck);
    //    }
    //    return ret;
    //}

    //private Interval? CheckAgainstFirst(List<Interval> existingToCheck, Stack<Interval> newToCheck)
    //{
    //    var toCheck = newToCheck.Pop();
    //    var existing = existingToCheck.Pop();
    //    var toAdd = new List<Interval>();
    //    foreach (var cube in newToCheck)
    //    {
    //        var intersectionResult = cube.Intersects(existing);
    //        if (intersectionResult != null)
    //        {
    //            foreach (var c in intersectionResult.Value.existingSplitted)
    //            {
    //                existingToCheck.Push(c);
    //            }
    //            newToCheck.AddRange(intersectionResult.Value.newSplitted);
    //            return null;
    //        }
    //    }
    //    return existing;
    //}

    //internal void Add(Interval startCube)
    //{
    //    var newToCheck = new List<Interval> { startCube };
    //    var ret = new List<Interval>();
    //    foreach (var existing in ExistingCubes)
    //    {
    //        var toAdd = new List<Interval>();
    //        foreach (var cube in newToCheck)
    //        {
    //            var intersectionResult = cube.Intersects(existing);
    //            if (intersectionResult != null)
    //            {
    //                ret.AddRange(intersectionResult.Value.existingSplitted);
    //                toAdd.AddRange(intersectionResult.Value.newSplitted);
    //            }
    //            else
    //            {
    //                ret.Add(existing);
    //                toAdd.Add(cube);
    //            }
    //        }
    //        newToCheck = toAdd;
    //    }
    //    if (newToCheck.Count > 0 && startCube.On)
    //    {
    //        ret.AddRange(newToCheck);
    //    }
    //    this.ExistingCubes = ret;
    //}

    internal IEnumerable<(int x, int y, int z)> GetAllLitPoints()
    {
        return this.ExistingCubes.SelectMany(c => c.GetAllLitPoints());//.OrderBy(c => c.x).ThenBy(c => c.y).ThenBy(c => c.z);
    }

    internal int GetLitPointsInInterval(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
    {
        return this.ExistingCubes.Sum(c => c.GetLitPointsInInterval(xMin, xMax, yMin, yMax, zMin, zMax));
    }

    internal long GetLitPoints()
    {
        return this.ExistingCubes.Sum(c => c.GetLitPoints());
    }
}