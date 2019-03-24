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

      startTime = DateTime.Now;

      nQueen = new NQueenGA(8)
      {
        PopulationSize = 100,
        AllowDuplicates = false,
        //SelectionMethod = new GALib.Selection.TruncationSelection(),
        SelectionMethod = new GALib.Selection.FitnessProportionateSelection()
        {
          AllowDuplicates = false,
          UseStochasticAcceptance = true
        },
        CrossoverMethod = new GALib.Crossover.PartiallyMappedCrossover()
        {
          ProduceTwoChildren = false
        },
        MutationMethod = new GALib.Mutation.NoMutation()
      };

      nQueen.Run(1);

      stopTime = DateTime.Now;

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



  }
}
