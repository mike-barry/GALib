using System.Collections.Generic;
using System.ComponentModel;

namespace GALib.Crossover
{
  public class PartiallyMappedCrossover : CrossoverMethod
  {
    public PartiallyMappedCrossover() : base(true) { }

    //public int MinCrossoverWidth { get; set; } // TODO implement
    //public int MaxCrossoverWidth { get; set; } // TODO implement

    [Category("Parameters"), DisplayName("Produce Two Children")]
    public bool ProduceTwoChildren { get; set; } = true;

    /// <summary>
    /// Performs partially mapped crossover
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="parents">The parents</param>
    /// <returns>One or two chromosomes depending on the value of <see cref="ProduceTwoChildren"/></returns>
    public override Gene[][] DoCrossover<Gene>(List<IGenotype> parents)
    {
      Genotype<Gene> parentA, parentB;
      int length, start, stop;
      Dictionary<Gene, Gene> mapA, mapB;
      Gene[] childA, childB;
      Gene[][] children;

      parentA = (Genotype<Gene>)parents[0];
      parentB = (Genotype<Gene>)parents[1];

      length = parentA.Length;
      start = Tools.StaticRandom.Next(0, length);
      stop = Tools.StaticRandom.Next(start, length) + 1;

      if (!ProduceTwoChildren)
      {
        childA = new Gene[length];

        mapA = new Dictionary<Gene,Gene>(stop - start);

        for (int i = start; i < stop; i++)
        {
          childA[i] = parentB[i];
          mapA.Add(parentB[i], parentA[i]);
        }

        for (int i = 0; i < start; i++)
        {
          childA[i] = parentA[i];
          while (mapA.ContainsKey(childA[i]))
            childA[i] = mapA[childA[i]];
        }

        for (int i = stop; i < length; i++)
        {
          childA[i] = parentA[i];
          while (mapA.ContainsKey(childA[i]))
            childA[i] = mapA[childA[i]];
        }

        children = new Gene[][] { childA };
      }
      else
      {
        childA = new Gene[length];
        childB = new Gene[length];

        mapA = new Dictionary<Gene,Gene>(stop - start);
        mapB = new Dictionary<Gene,Gene>(stop - start);

        for (int i = start; i < stop; i++)
        {
          childA[i] = parentB[i];
          childB[i] = parentA[i];

          mapA.Add(parentB[i], parentA[i]);
          mapB.Add(parentA[i], parentB[i]);
        }

        for (int i = 0; i < start; i++)
        {
          childA[i] = parentA[i];
          while (mapA.ContainsKey(childA[i]))
            childA[i] = mapA[childA[i]];

          childB[i] = parentB[i];
          while (mapB.ContainsKey(childB[i]))
            childB[i] = mapB[childB[i]];
        }

        for (int i = stop; i < length; i++)
        {
          childA[i] = parentA[i];
          while (mapA.ContainsKey(childA[i]))
            childA[i] = mapA[childA[i]];

          childB[i] = parentB[i];
          while (mapB.ContainsKey(childB[i]))
            childB[i] = mapB[childB[i]];
        }

        children = new Gene[][] { childA, childB };
      }

      return children;
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "Partially Mapped Crossover (PMX)";
    }
  }
}
