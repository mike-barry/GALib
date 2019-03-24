using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Crossover
{
  public class PartiallyMappedCrossover : CrossoverMethod
  {
    //public int MinCrossoverWidth { get; set; }
    //public int MaxCrossoverWidth { get; set; }

    public bool ProduceTwoChildren { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="parents"></param>
    /// <returns></returns>
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
      stop = Tools.StaticRandom.Next(start, length); // TODO this is not right

      if (!ProduceTwoChildren)
      {
        childA = new Gene[length];

        mapA = new Dictionary<Gene,Gene>(stop - start + 1);

        for (int i = start; i <= stop; i++)
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

        for (int i = stop + 1; i < length; i++)
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

        mapA = new Dictionary<Gene,Gene>(stop - start + 1);
        mapB = new Dictionary<Gene,Gene>(stop - start + 1);

        for (int i = start; i <= stop; i++)
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
            childB[i] = mapA[childB[i]];
        }

        for (int i = stop + 1; i < length; i++)
        {
          childA[i] = parentA[i];
          while (mapA.ContainsKey(childA[i]))
            childA[i] = mapA[childA[i]];

          childB[i] = parentB[i];
          while (mapB.ContainsKey(childB[i]))
            childB[i] = mapA[childB[i]];
        }

        children = new Gene[][] { childA, childB };
      }

      return children;
    }
  }
}
