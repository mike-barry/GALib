using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GALib.Crossover
{
  /// <summary>
  /// Order Crossover (OX1)
  /// Page 140-141 http://dcs.gla.ac.uk/~pat/jchoco/gatsp/papers/aiRev99.pdf
  /// </summary>
  /// <seealso cref="GALib.Crossover.CrossoverMethod" />
  public class Order1Crossover : CrossoverMethod
  {
    public Order1Crossover() : base(true) { }

    [Category("Parameters"), DisplayName("Produce Two Children")]
    public bool ProduceTwoChildren { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="parents"></param>
    /// <returns></returns>
    public override Gene[][] DoCrossover<Gene>(List<IGenotype> parents)
    {
      List<Gene> parentA, parentB;
      int length, a, b;
      Gene[] childA, childB;

      parentA = ((Genotype<Gene>)parents[0]).ToList();
      parentB = ((Genotype<Gene>)parents[1]).ToList();
      length = parentA.Count;
      a = 3;// Tools.StaticRandom.Next(0, length);
      b = 4;// Tools.StaticRandom.Next(0, length - a + 1);

      if (ProduceTwoChildren)
      {
        childA = CreateChild(parentA, parentB, length, a, b);
        childB = CreateChild(parentB, parentA, length, a, b);
        return new Gene[][] { childA, childB };
      }
      else
      {
        childA = CreateChild(parentA, parentB, length, a, b);
        return new Gene[][] { childA };
      }
    }

    /// <summary>
    /// Creates the child.
    /// </summary>
    /// <typeparam name="Gene">The type of the ene.</typeparam>
    /// <param name="parentA">The parent a.</param>
    /// <param name="parentB">The parent b.</param>
    /// <param name="length">The length.</param>
    /// <param name="a">The start of the crossover.</param>
    /// <param name="b">The length of the crossover.</param>
    /// <returns></returns>
    private Gene[] CreateChild<Gene>(List<Gene> parentA, List<Gene> parentB, int length, int a, int b)
      where Gene : IComparable
    {
      var middle = parentA.Skip(a).Take(b);
      var unused = parentB.Skip(a + b).Concat(parentB.Take(a + b)).Except(middle);
      var left = unused.Skip(length - a - b);
      var right = unused.Take(length - a - b);
      return left.Concat(middle.Concat(right)).ToArray();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "Order Crossover (OX1)";
    }

    /// <summary>
    /// Tests this crossover method.
    /// </summary>
    public void Test()
    {
      GenotypeGenericList<int> parentA, parentB;
      int[][] children;

      parentA = new GenotypeGenericList<int>(new int[] { 1, 4, 2, 8, 5, 7, 3, 6, 9 }, 0);
      parentB = new GenotypeGenericList<int>(new int[] { 7, 5, 3, 1, 9, 8, 6, 4, 2 }, 0);
      children = DoCrossover<int>(new List<IGenotype>(2) { parentA, parentB });

      Console.WriteLine(string.Join(" ", parentA.ToList()));
      Console.WriteLine(string.Join(" ", parentB.ToList()));
      Console.WriteLine(string.Join(" ", children[0].ToList()));
      Console.WriteLine(string.Join(" ", children[1].ToList()));
    }
  }
}
