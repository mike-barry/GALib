using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

using GALib;
using System.Drawing.Imaging;

namespace Test.TravelingSalesman
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

      public Point ToPoint(double xScale, int xOffset, double yScale, int yOffset)
      {
        return new Point((int)(X * xScale) + xOffset, (int)(Y * yScale) + yOffset);
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

    private List<Location> locations = null;
    private List<int> geneDomain = null;
    private int genotypeLength;

    #endregion

    #region [ Constructor ]

    public TravellingSalesmanGA(TravellingSalesmanParams p) :
      base(p)
    {
      locations = p.GetDataset().Locations;

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
    /// <returns></returns>
    public Bitmap DrawIndividual(Genotype<int> individual)
    {
      int pad, scale, dotSize;
      Bitmap img;
      Graphics g;
      Font font;
      Pen blackPen, greenPen;
      Location a, b;

      font = new Font(FontFamily.GenericSansSerif, 20);
      blackPen = new Pen(Color.Black, 2);
      greenPen = new Pen(Color.LightGreen, 2);

      scale = 5;
      pad = 5;
      dotSize = 5;
      img = new Bitmap(100 * scale + pad * 2, 100 * scale + pad * 2);
      g = Graphics.FromImage(img);

      g.FillRectangle(Brushes.White, 0, 0, 100 * scale + pad * 2, 100 * scale + pad * 2);
      //g.DrawString("#" + GenerationNumber + " " + (1 / individual.Fitness).ToString("0.0000"), font, Brushes.Black, 0, 0);

      a = locations[0];
      b = locations[individual[0]];
      g.DrawLine(greenPen, (float)a.X * scale + pad, (float)a.Y * scale + pad, (float)b.X * scale + pad, (float)b.Y * scale + pad);
      g.FillEllipse(Brushes.Black, new RectangleF((float)b.X * scale + pad - dotSize, (float)b.Y * scale + pad - dotSize, dotSize * 2, dotSize * 2));

      for (int i = 0; i < individual.Length - 1; i++)
      {
        a = locations[individual[i]];
        b = locations[individual[i + 1]];
        g.DrawLine(blackPen, (float)a.X * scale + pad, (float)a.Y * scale + pad, (float)b.X * scale + pad, (float)b.Y * scale + pad);
        g.FillEllipse(Brushes.Black, new RectangleF((float)b.X * scale + pad - dotSize, (float)b.Y * scale + pad - dotSize, dotSize * 2, dotSize * 2));
      }

      a = locations[individual[individual.Length - 1]];
      b = locations[0];
      g.DrawLine(blackPen, (float)a.X * scale + pad, (float)a.Y * scale + pad, (float)b.X * scale + pad, (float)b.Y * scale + pad);
      g.FillEllipse(Brushes.LightGreen, new RectangleF((float)b.X * scale + pad - dotSize, (float)b.Y * scale + pad - dotSize, dotSize * 2, dotSize * 2));

      return img;
    }

    #endregion

  }
}
