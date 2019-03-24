using System.Collections.Generic;

using GALib;

namespace Test
{
  public class NQueenGA : GeneticAlgorithm<int>
  {
    public int NumQueens { get; private set; }
    public List<int> GeneDomain { get; private set; } = new List<int>();
    public int BestFitness { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="numQueens"></param>
    public NQueenGA(int numQueens) :
      base()
    {
      NumQueens = numQueens;
      BestFitness = 0;

      for (int i = 0; i < numQueens; i++)
      {
        GeneDomain.Add(i);
        BestFitness += i + 1;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override IGenotype GenerateRandomMember()
    {
      int[] geneSequence;
      double fitness;

      geneSequence = new int[NumQueens];
      GeneDomain.CopyTo(geneSequence);
      Tools.Shuffle(geneSequence);
      fitness = FitnessFunction(geneSequence, out bool junk);

      return new GenotypeGenericList<int>(geneSequence, fitness);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geneSequence"></param>
    /// <returns></returns>
    public override double FitnessFunction(int[] geneSequence, out bool solutionFound)
    {
      int conflicts = 0;

      for (int i = 0; i < NumQueens; i++)
        for (int j = i + 1; j < NumQueens; j++)
          if (geneSequence[i] + (j - i) == geneSequence[j])
            conflicts++;
          else if (geneSequence[i] - (j - i) == geneSequence[j])
            conflicts++;

      solutionFound = (conflicts == 0);

      return BestFitness - conflicts;
    }
  }
}
