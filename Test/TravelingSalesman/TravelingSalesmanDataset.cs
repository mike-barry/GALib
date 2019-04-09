using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Test.TravelingSalesman
{
  public class TravelingSalesmanDataset
  {
    public List<TravellingSalesmanGA.Location> Locations = new List<TravellingSalesmanGA.Location>();
    public double MinX = double.MaxValue;
    public double MinY = double.MaxValue;
    public double MaxX = double.MinValue;
    public double MaxY = double.MinValue;

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

    /// <summary>
    /// Loads random locations.
    /// </summary>
    /// <param name="numLocations">The number locations.</param>
    public static TravelingSalesmanDataset GenerateRandom(int numLocations)
    {
      TravelingSalesmanDataset d;
      Random rand;

      d = new TravelingSalesmanDataset();
      rand = new Random(421784);

      for (int i = 0; i < numLocations; i++)
        d.AddLocation(new TravellingSalesmanGA.Location("Location #" + i, rand.NextDouble() * 100, rand.NextDouble() * 100));

      return d;
    }

    /// <summary>
    /// Loads airport locations.
    /// </summary>
    /// <param name="numLocations">The number locations.</param>
    /// <exception cref="Exception">Error parsing file</exception>
    public static TravelingSalesmanDataset LoadAirports(int numLocations)
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

  }
}
