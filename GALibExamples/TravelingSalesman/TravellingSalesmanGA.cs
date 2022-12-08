using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;

using GALib;
using System.Drawing.Imaging;

namespace GALibExamples.TravelingSalesman
{
  public class TravellingSalesmanGA : GeneticAlgorithm<int>
  {

    #region [ Location ]

    /// <summary>
    /// 
    /// </summary>
    public struct Location
    {
      public string Name;
      public double X;
      public double Y;

      public Location(string name, double x, double y)
      {
        Name = name;
        X = x;
        Y = y;
      }

      public Point ToPoint(double xScale, double xOffset, double yScale, double yOffset, int pad)
      {
        return new Point((int)((X + xOffset) * xScale) + pad, (int)((Y + yOffset) * yScale) + pad);
      }

    }

    #endregion

    #region [ LocationPair ]

    /// <summary>
    /// 
    /// </summary>
    public class LocationPair
    {
      public Location A;
      public Location B;

      public LocationPair(Location a, Location b)
      {
        A = a;
        B = b;
      }

      public override bool Equals(object obj)
      {
        if (obj is LocationPair castObj)
          return A.X.Equals(castObj.A.X) && A.Y.Equals(castObj.A.Y) && B.X.Equals(castObj.B.X) && B.Y.Equals(castObj.B.Y);
        else
          return false;
      }

      public override int GetHashCode()
      {
        return A.X.GetHashCode() ^ B.X.GetHashCode() ^ A.Y.GetHashCode() ^ B.Y.GetHashCode();
      }

      public override string ToString()
      {
        return A.Name.ToString() + " -> " + B.Name.ToString();
      }
    }

    #endregion 

    #region [ Members ]

    private TravelingSalesmanDataset dataset = null;
    private List<Location> locations = null;
    private List<int> geneDomain = null;
    private int genotypeLength;

    private readonly Pen blackPen = new Pen(Color.Black, 2);
    private readonly Pen greenPen = new Pen(Color.LightGreen, 2);

    #endregion

    #region [ Constructor ]

    public TravellingSalesmanGA(TravellingSalesmanParams p) :
      base(p)
    {
      dataset = p.GetDataset();
      locations = dataset.Locations;
      genotypeLength = locations.Count - 2;
      geneDomain = Enumerable.Range(1, genotypeLength).ToList();

      LocationCount = locations.Count;

      //TODO see performance of using pre-calculated distance
      //distances = new Dictionary<LocationPair, double>(locations.Count);
      //for (int i = 0; i < locations.Count; i++)
      //  for (int j = i; j < locations.Count; j++)
      //  {
      //    Location a = locations[i];
      //    Location b = locations[j];
      //    double distance = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
      //    distances.Add(new LocationPair(a, b), distance);
      //  }
    }

    #endregion

    #region [ Properties ]

    [Category("Setup")]
    public int LocationCount { get; private set; }

    #endregion

    #region [ Methods ]

    /// <summary>
    /// Returns the fitnesses of an individual
    /// </summary>
    /// <param name="geneSequence">The gene sequence.</param>
    /// <param name="solutionFound">if set to <c>true</c> [solution found].</param>
    /// <returns></returns>
    public override double FitnessFunction(int[] geneSequence, out bool solutionFound)
    {
      double cost;
      Location a, b;

      // From start location to first location
      a = locations[0];
      b = locations[geneSequence[0]];
      cost = Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

      for (int i = 0; i < geneSequence.Length - 1; i++)
      {
        a = locations[geneSequence[i]];
        b = locations[geneSequence[i + 1]];
        cost += Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
      }

      // From last location to start location
      a = locations[geneSequence[geneSequence.Length - 1]];
      b = locations[0];
      cost += Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2)); ;

      solutionFound = false;
      return 1 / cost;
    }

    /// <summary>
    /// Generates a random member.
    /// </summary>
    /// <returns></returns>
    public override IGenotype GenerateRandomMember()
    {
      int[] geneSequence;
      double fitness;

      geneSequence = new int[genotypeLength];
      geneDomain.CopyTo(geneSequence);
      Tools.Shuffle(geneSequence);
      fitness = FitnessFunction(geneSequence, out bool junk);

      return new GenotypeGenericList<int>(geneSequence, fitness);
    }

    /// <summary>
    /// Draws the individual.
    /// </summary>
    /// <param name="individual">The individual.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns></returns>
    public override Bitmap DrawIndividual(IGenotype individual, int width, int height)
    {
      return DrawIndividual((Genotype<int>)individual, width, height);
    }

    /// <summary>
    /// Draws the individual.
    /// </summary>
    /// <param name="individual">The individual.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns></returns>
    private Bitmap DrawIndividual(Genotype<int> individual, int width, int height)
    {
      const int DOT_SIZE = 10;
      const int HALF_DOT_SIZE = DOT_SIZE / 2;
      const int PAD = 20;
      const int DOUBLE_PAD = PAD * 2;

      double xOffset, yOffset, xScale, yScale;
      Bitmap img;
      Graphics g;
      Point a, b;


      xOffset = -dataset.MinX;
      yOffset = -dataset.MinY;
      xScale = (width - DOUBLE_PAD) / (dataset.MaxX - dataset.MinX);
      yScale = (height - DOUBLE_PAD) / (dataset.MaxY - dataset.MinY);

      img = new Bitmap(width, height);
      g = Graphics.FromImage(img);
      g.FillRectangle(Brushes.White, 0, 0, width, height);

      a = locations[0].ToPoint(xScale, xOffset, yScale, yOffset, PAD);
      b = locations[individual[0]].ToPoint(xScale, xOffset, yScale, yOffset, PAD);
      g.DrawLine(Pens.LightGreen, a, b);
      g.FillEllipse(Brushes.Black, new RectangleF(b.X - HALF_DOT_SIZE, b.Y - HALF_DOT_SIZE, DOT_SIZE, DOT_SIZE));

      for (int i = 0; i < individual.Length - 1; i++)
      {
        a = locations[individual[i]].ToPoint(xScale, xOffset, yScale, yOffset, PAD);
        b = locations[individual[i + 1]].ToPoint(xScale, xOffset, yScale, yOffset, PAD);
        g.DrawLine(Pens.Black, a, b);
        g.FillEllipse(Brushes.Black, new RectangleF(b.X - HALF_DOT_SIZE, b.Y - HALF_DOT_SIZE, DOT_SIZE, DOT_SIZE));
      }

      a = locations[individual[individual.Length - 1]].ToPoint(xScale, xOffset, yScale, yOffset, PAD);
      b = locations[0].ToPoint(xScale, xOffset, yScale, yOffset, PAD);
      g.DrawLine(Pens.Black, a, b);
      g.FillEllipse(Brushes.LightGreen, new RectangleF(b.X - HALF_DOT_SIZE, b.Y - HALF_DOT_SIZE, DOT_SIZE, DOT_SIZE));

      return img;
    }

    #endregion

  }
}
