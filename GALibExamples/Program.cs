using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using GALib;
using GALib.Crossover;
using GALib.Mutation;

namespace GALibExamples
{
  class Program
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      //Test();
      //HashSetTest();
      //GALib.Selection.FitnessProportionateSelection.Test();
      //NQueen();

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

    /// <summary>
    /// 
    /// </summary>
    static void Test()
    {
      //new Order1Crossover().Test();
      //new ReverseSequenceMutation().Test();
      //new CenterInverseMutation().Test();
    }

    /// <summary>
    /// 
    /// </summary>
    static void NQueen()
    {
      //DateTime startTime, stopTime;
      //NQueenGA nQueen;
      //int numQueens, numRuns;
      //List<int> numGenerations;

      //numRuns = 1;
      //numGenerations = new List<int>(numRuns);

      //startTime = DateTime.Now;

      //numQueens = 100;

      //for (int run = 0; run < numRuns; run++)
      //{
      //  nQueen = new NQueenGA(numQueens, true, numQueens * 100)
      //  {
      //    PopulationSize = numQueens,// * 10,
      //    PreserveElitePercent = 0.25,
      //    //SelectionMethod = new GALib.Selection.RandomSelection()
      //    //{
      //    //  TruncateBeforeSelect = true,
      //    //  TruncationPercent = 0.25
      //    //},
      //    SelectionMethod = new GALib.Selection.FitnessProportionateSelection()
      //    {
      //      AllowDuplicates = true,
      //      MaxRetriesForDuplicates = numQueens * 100,
      //      UseStochasticAcceptance = false,
      //      TruncateBeforeSelect = true,
      //      TruncationPercent = 0.25
      //    },
      //    CrossoverMethod = new GALib.Crossover.PartiallyMappedCrossover()
      //    {
      //      ProduceTwoChildren = true
      //    },
      //    //MutationMethod = new GALib.Mutation.NoMutation()
      //    MutationMethod = new GALib.Mutation.SwapMutation()
      //    {
      //      MutationChance = 0.10,
      //      MaxNumberOfSwaps = 1//numQueens / 50
      //    }
      //  };

      //  //nQueen.RescaleMethod = new PowerRescale(1.3);
      //  //nQueen.RescaleMethod = new ExponentialRescale(0.5);

      //  if (nQueen.RescaleMethod == null)
      //    Console.WriteLine("Optimal = " + nQueen.BestFitness);
      //  else
      //    Console.WriteLine("Optimal = " + nQueen.RescaleMethod.Rescale(nQueen.BestFitness));

      //  for (int i = 0; i < int.MaxValue; i++)
      //    nQueen.Run();

      //  //Console.WriteLine("Optimal = " + nQueen.BestFitness);
      //  //Console.WriteLine("Result  = " + result.Fitness);

      //  if (nQueen.SolutionFound)
      //  {
      //    numGenerations.Add(nQueen.GenerationNumber);
      //    Console.WriteLine("# of generations = " + nQueen.GenerationNumber);
      //  }
      //  else
      //    Console.WriteLine("# of generations = " + nQueen.GenerationNumber + " PREMATURELY CONVERGED");
      //}

      //stopTime = DateTime.Now;

      //Console.WriteLine("Median = " + numGenerations.OrderBy(x => x).Skip(numGenerations.Count / 2).Take(1).First());

      //Console.WriteLine();
      ////Console.WriteLine("Solution found for " + nQueen.NumQueens + "-queen problem");
      ////Console.WriteLine(nQueen.Population[0]);
      //Console.WriteLine("Average Duration = " + (stopTime - startTime).TotalSeconds / numRuns + " seconds");
      //Console.WriteLine("Total Duration = " + (stopTime - startTime).TotalSeconds + " seconds");
      //Console.WriteLine();
      //Console.WriteLine("Press any key to quit...");
      //Console.ReadKey();
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
