using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;

using GALib;
using System.Drawing.Imaging;

namespace Test
{
  public class TravelingSalesmanGA : GeneticAlgorithm<int>
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
    //private Dictionary<LocationPair,double> distances = null;
    private List<int> geneDomain = null;
    private int genotypeLength;

    #endregion

    #region [ Constructor ]

    public TravelingSalesmanGA(List<Location> locations, bool allowDuplicates, int maxRetriesForDuplicates) :
      base(allowDuplicates, maxRetriesForDuplicates)
    {
      this.locations = locations;
      genotypeLength = locations.Count - 2;
      geneDomain = Enumerable.Range(1, genotypeLength).ToList();

      NewGeneration = HandleNewGeneration;


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

    private Genotype<int> tempLastBest = null; // TEMP
    private int tempNumber = 0; // TEMP

    /// <summary>
    /// Handles a new generation.
    /// </summary>
    /// <param name="ga">The genetic algorithm instance.</param>
    private void HandleNewGeneration(IGeneticAlgorithm ga)
    {
      int pad, scale, dotSize;
      Bitmap img;
      Graphics g;
      Font font;
      Pen blackPen, greenPen;
      Genotype<int> individual;
      Location a, b;

      individual = (Genotype<int>)Population.OrderBy(x => x.Fitness).Last();

      Console.WriteLine("Gen " + GenerationNumber + ": " + 1 / individual.Fitness);

      if (individual == tempLastBest)
        return;
      else
        tempLastBest = individual;

      font = new Font(FontFamily.GenericSansSerif, 20);
      blackPen = new Pen(Color.Black, 2);
      greenPen = new Pen(Color.LightGreen, 2);

      scale = 10;
      pad = 5;
      dotSize = 8;
      img = new Bitmap(100 * scale + pad * 2, 100 * scale + pad * 2);
      g = Graphics.FromImage(img);

      g.FillRectangle(Brushes.White, 0, 0, 100 * scale + pad * 2, 100 * scale + pad * 2);
      g.DrawString("#" + ga.GenerationNumber + " " + (1 / individual.Fitness).ToString("0.0000"), font, Brushes.Black, 0, 0);

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
      g.FillEllipse(Brushes.Green, new RectangleF((float)b.X * scale + pad - dotSize, (float)b.Y * scale + pad - dotSize, dotSize * 2, dotSize * 2));

      img.Save("C:\\Temp\\" + tempNumber++ + ".png", ImageFormat.Png);

    }

  }
}
