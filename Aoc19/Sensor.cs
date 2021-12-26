
internal class Sensor
{

    public IEnumerable<Point3> SensorData { get; set; }

    public Sensor(IEnumerable<Point3> sensorData, bool precalc)
    {
        this.SensorData = sensorData;
        if (!precalc)
        {
            return;
        }
        this.ModifiedSensorData = new List<IEnumerable<Point3>>();
        this.Modifier = new List<Func<Point3, Point3>>();
        Modifier.Add(p => new Point3(p.X, p.Y, p.Z));
        Modifier.Add(p => new Point3(p.X, p.Z, -p.Y));
        Modifier.Add(p => new Point3(p.X, -p.Y, -p.Z));
        Modifier.Add(p => new Point3(p.X, -p.Z, p.Y));
        Modifier.Add(p => new Point3(-p.X, p.Z, p.Y));
        Modifier.Add(p => new Point3(-p.X, p.Y, -p.Z));
        Modifier.Add(p => new Point3(-p.X, -p.Z, -p.Y));
        Modifier.Add(p => new Point3(-p.X, -p.Y, p.Z));

        Modifier.Add(p => new Point3(p.Y, p.Z, p.X));
        Modifier.Add(p => new Point3(p.Y, p.X, -p.Z));
        Modifier.Add(p => new Point3(p.Y, -p.Z, -p.X));
        Modifier.Add(p => new Point3(p.Y, -p.X, p.Z));
        Modifier.Add(p => new Point3(-p.Y, p.X, p.Z));
        Modifier.Add(p => new Point3(-p.Y, p.Z, -p.X));
        Modifier.Add(p => new Point3(-p.Y, -p.X, -p.Z));
        Modifier.Add(p => new Point3(-p.Y, -p.Z, p.X));

        Modifier.Add(p => new Point3(p.Z, p.X, p.Y));
        Modifier.Add(p => new Point3(p.Z, p.Y, -p.X));
        Modifier.Add(p => new Point3(p.Z, -p.X, -p.Y));
        Modifier.Add(p => new Point3(p.Z, -p.Y, p.X));
        Modifier.Add(p => new Point3(-p.Z, p.Y, p.X));
        Modifier.Add(p => new Point3(-p.Z, p.X, -p.Y));
        Modifier.Add(p => new Point3(-p.Z, -p.Y, -p.X));
        Modifier.Add(p => new Point3(-p.Z, -p.X, p.Y));

        ModifiedSensorData = Modifier.Select(m => sensorData.Select(m)).ToList();
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.X, p.Y, p.Z)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.X, p.Z, -p.Y)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.X, -p.Y, -p.Z)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.X, -p.Z, p.Y)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.X, p.Z, p.Y)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.X, p.Y, -p.Z)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.X, -p.Z, -p.Y)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.X, -p.Y, p.Z)));

        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Y, p.Z, p.X)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Y, p.X, -p.Z)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Y, -p.Z, -p.X)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Y, -p.X, p.Z)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Y, p.X, p.Z)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Y, p.Z, -p.X)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Y, -p.X, -p.Z)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Y, -p.Z, p.X)));

        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Z, p.X, p.Y)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Z, p.Y, -p.X)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Z, -p.X, -p.Y)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(p.Z, -p.Y, p.X)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Z, p.Y, p.X)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Z, p.X, -p.Y)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Z, -p.Y, -p.X)));
        //ModifiedSensorData.Add(sensorData.Select(p => new Point3(-p.Z, -p.X, p.Y)));
    }

    internal (Point3 offset, int transform)? Matches(Sensor sensor, int minCount)
    {
        for (int i = 0; i < 24; i++)
        {
            var ret = matchesTransform(sensor, minCount, ModifiedSensorData[i]);
            if (ret != null)
            {
                return (ret.Value, i);
            }
        }
        return null;
    }

    internal Sensor OffsetAndTransform(Point3 offset, int transform)
    {
        var points = this.ModifiedSensorData[transform].Select(point => point + offset).ToList();
        return new Sensor(points, false);
    }

    private Point3? matchesTransform(Sensor sensor, int minCount, IEnumerable<Point3> theseSensorPoints)
    {
        foreach (var point in theseSensorPoints)
        {
            foreach (var otherPoint in sensor.SensorData)
            {
                Point3 offset = otherPoint - point;
                if (matchesTransformAndOffset(offset, sensor, minCount, theseSensorPoints))
                {
                    return offset;
                }
            }
        }
        return null;
    }

    private bool matchesTransformAndOffset(Point3 offset, Sensor sensor, int minCount, IEnumerable<Point3> theseSensorPoints)
    {
        var matchingCount = countMatches(offset, sensor, theseSensorPoints);
        if (matchingCount > 1)
        {
            var x = 1 + 2;
        }
        return matchingCount >= minCount;
    }

    private int countMatches(Point3 offset, Sensor sensor, IEnumerable<Point3> theseSensorPoints)
    {
        var offseted = theseSensorPoints.Select(point => point + offset).ToList();
        var matched = sensor.SensorData.Where(other => offseted.Contains(other));
        return matched.Count();
    }

    public List<IEnumerable<Point3>> ModifiedSensorData { get; set; }
    public List<Func<Point3, Point3>> Modifier { get; set; }
}