using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GALib;

namespace Test
{
  class Program
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      //HashSetTest();
      //GALib.Selection.FitnessProportionateSelection.Test();
      NQueen();
    }

    /// <summary>
    /// 
    /// </summary>
    static void NQueen()
    {
      DateTime startTime, stopTime;
      NQueenGA nQueen;
      int numQueens;

      startTime = DateTime.Now;

      numQueens = 500;

      nQueen = new NQueenGA(numQueens, true, numQueens * 2)
      {
        PopulationSize = numQueens,// * 10,
        PreserveElitePercent = 0.50,
        SelectionMethod = new GALib.Selection.TruncationSelection()
        {
          TruncationPercent = 0.10
        },
        //SelectionMethod = new GALib.Selection.FitnessProportionateSelection()
        //{
        //  UseStochasticAcceptance = false
        //},
        CrossoverMethod = new GALib.Crossover.PartiallyMappedCrossover()
        {
          ProduceTwoChildren = true
        },
        //MutationMethod = new GALib.Mutation.NoMutation()
        MutationMethod = new GALib.Mutation.SwapMutation()
        {
          MutationChance = 0.90,
          MaxNumberOfSwaps = 1//numQueens / 10
        }
      };

      Console.WriteLine("Optimal = " + nQueen.BestFitness);

      IGenotype result = nQueen.Run(int.MaxValue);

      stopTime = DateTime.Now;

      Console.WriteLine("Optimal = " + nQueen.BestFitness);
      Console.WriteLine("Result  = " + result.Fitness);

      Console.WriteLine();
      //Console.WriteLine("Solution found for " + nQueen.NumQueens + "-queen problem");
      //Console.WriteLine(nQueen.Population[0]);
      Console.WriteLine("Duration = " + (stopTime - startTime).TotalSeconds + " seconds");
      Console.WriteLine();
      Console.WriteLine("Press any key to quit...");
      Console.ReadKey();
    }

    /// <summary>
    /// 
    /// </summary>
    static void HashSetTest()
    {
      HashSet<IGenotype> hashSet;
      GenotypeGenericList<int> a, b;

      hashSet = new HashSet<IGenotype>();

      a = new GenotypeGenericList<int>(new int[] { 1, 2, 3 }, 1.0);
      b = new GenotypeGenericList<int>(new int[] { 1, 2, 3 }, 1.0);

      hashSet.Add(a);
      hashSet.Add(b);
    }

    /// <summary>
    /// 
    /// </summary>
    static void Test()
    {
    }

  }
}
