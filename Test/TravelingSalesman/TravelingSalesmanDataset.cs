using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Test.TravelingSalesman
{
  public class TravelingSalesmanDataset
  {

    #region [ DatasetTypeEnum ]

    public enum DatasetTypeEnum
    {
      Airports,
      Circle,
      Donut,
      Random,
      Star,
      Test
    }

    #endregion

    #region [ Members ]

    public List<TravellingSalesmanGA.Location> Locations = new List<TravellingSalesmanGA.Location>();
    public double MinX = double.MaxValue;
    public double MinY = double.MaxValue;
    public double MaxX = double.MinValue;
    public double MaxY = double.MinValue;

    #endregion

    #region [ Constructor ]

    private TravelingSalesmanDataset() { } // Intentionally declared private so it can't be instantiated outside this class

    #endregion

    #region [ Methods ]

    /// <summary>
    /// Adds a location.
    /// </summary>
    /// <param name="location">The location.</param>
    private void AddLocation(TravellingSalesmanGA.Location location)
    {
      MinX = Math.Min(MinX, location.X);
      MaxX = Math.Max(MaxX, location.X);
      MinY = Math.Min(MinY, location.Y);
      MaxY = Math.Max(MaxY, location.Y);

      Locations.Add(location);
    }

    /// <summary>
    /// Normalizes the locations.
    /// </summary>
    private void Normalize(double newMaxX, double newMaxY)
    {
      double xScale, yScale;
      TravellingSalesmanGA.Location location;

      xScale = newMaxX / (MaxX - MinX);
      yScale = newMaxY / (MaxY - MinY);

      for (int i = 0; i < Locations.Count; i++)
      {
        location = Locations[i];
        location.X = (location.X - MinX) * xScale;
        location.Y = (location.Y - MinY) * yScale;
        Locations[i] = location;
      }

      MinX = 0;
      MinY = 0;
      MaxX = newMaxX;
      MaxY = newMaxY;
    }

    #endregion

    #region [ Static Methods ]

    public static TravelingSalesmanDataset Generate(DatasetTypeEnum datasetType, int numLocations)
    {
      switch (datasetType)
      {
        case DatasetTypeEnum.Airports:
          return LoadAirports(numLocations);
        case DatasetTypeEnum.Circle:
          return GenerateCircle(numLocations, 1);
        case DatasetTypeEnum.Donut:
          return GenerateCircle(numLocations, 2);
        case DatasetTypeEnum.Random:
          return GenerateRandom(numLocations);
        case DatasetTypeEnum.Star:
          return GenerateCircle(numLocations, 1.10);
        case DatasetTypeEnum.Test:
        default:
          return GenerateRandom(numLocations, 421784);
      }
    }

    /// <summary>
    /// Generates random locations.
    /// </summary>
    /// <param name="numLocations">The number locations.</param>
    private static TravelingSalesmanDataset GenerateRandom(int numLocations, int? seed = null)
    {
      TravelingSalesmanDataset d;
      Random rand;

      d = new TravelingSalesmanDataset();

      if (seed != null)
        rand = new Random((int)seed);
      else
        rand = new Random();

      for (int i = 0; i < numLocations; i++)
        d.AddLocation(new TravellingSalesmanGA.Location("Location #" + i, rand.NextDouble() * 100, rand.NextDouble() * 100));

      return d;
    }

    /// <summary>
    /// Generates locations in a circle pattern.
    /// </summary>
    /// <param name="numLocations">The number locations.</param>
    /// <param name="altScale">The alt scale.</param>
    /// <returns></returns>
    private static TravelingSalesmanDataset GenerateCircle(int numLocations, double altScale = 1)
    {
      TravelingSalesmanDataset d;
      double step;

      d = new TravelingSalesmanDataset();
      step = Math.PI / ((numLocations - 1) / 2);

      for (int i = 1; i <= numLocations; i++)
      {
        if (i % 2 == 0)
          d.AddLocation(new TravellingSalesmanGA.Location(i.ToString(), Math.Cos(step * i), Math.Sin(step * i)));
        else
          d.AddLocation(new TravellingSalesmanGA.Location(i.ToString(), Math.Cos(step * i) * altScale, Math.Sin(step * i) * altScale));
      }

      d.Normalize(100, 100);

      return d;
    }

    /// <summary>
    /// Loads airport locations.
    /// </summary>
    /// <param name="numLocations">The number locations.</param>
    /// <exception cref="Exception">Error parsing file</exception>
    private static TravelingSalesmanDataset LoadAirports(int numLocations)
    {
      TravelingSalesmanDataset d;
      Random rand;
      List<string> lines;
      int index;
      string[] airport;

      d = new TravelingSalesmanDataset();
      rand = new Random(421784);

      lines = File.ReadAllLines(@"./Resources/US Airports.txt").ToList();

      if (numLocations > lines.Count)
        numLocations = lines.Count;

      try
      {
        for (int i = 0; i < numLocations; i++)
        {
          index = rand.Next(0, lines.Count);
          airport = lines[index].Split('\t');
          d.AddLocation(new TravellingSalesmanGA.Location(airport[0], double.Parse(airport[1]), double.Parse(airport[2])));
          lines.RemoveAt(index);
        }
      }
      catch
      {
        // TODO handle different exceptions and provide better description of what went wrong
        throw new Exception("Error parsing file");
      }

      d.Normalize(100, 100);
      return d;
    }

    #endregion

  }
}
