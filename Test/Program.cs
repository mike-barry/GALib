using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GALib;
using GALib.Util;

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
      //NQueen();
      TravelingSalesman();
    }

    static void TravelingSalesman()
    {
      DateTime startTime, stopTime;
      int numLocations, numRuns;
      Random rand;
      List<TravelingSalesmanGA.Location> locations;

      numRuns = 1;
      numLocations = 100;

      locations = new List<TravelingSalesmanGA.Location>(numLocations);
      rand = new Random(421784);

      for (int i = 0; i < numLocations; i++)
        locations.Add(new TravelingSalesmanGA.Location() { Name = "Location #" + i, X = rand.NextDouble() * 100, Y = rand.NextDouble() * 100 });

      startTime = DateTime.Now;

      for (int run = 0; run < numRuns; run++)
      {
        TravelingSalesmanGA travel = new TravelingSalesmanGA(locations, false, numLocations * 100)
        {
          ConsoleOutput = true,
          PopulationSize = 100, //numLocations,
          PreserveElitePercent = 0.10,
          SelectionMethod = new GALib.Selection.FitnessProportionateSelection()
          {
            AllowDuplicates = false,
            MaxRetriesForDuplicates = numLocations * 100,
            UseStochasticAcceptance = false,
            TruncateBeforeSelect = true,
            TruncationPercent = 0.25
          },
          CrossoverMethod = new GALib.Crossover.PartiallyMappedCrossover()
          {
            ProduceTwoChildren = true
          },
          MutationMethod = new GALib.Mutation.SwapMutation()
          {
            MutationChance = 0.1,
            MaxNumberOfSwaps = 1
          }
        };

        travel.Run(10000);
      }

      stopTime = DateTime.Now;

      Console.WriteLine();
      Console.WriteLine("Average Duration = " + (stopTime - startTime).TotalSeconds / numRuns + " seconds");
      Console.WriteLine("Total Duration = " + (stopTime - startTime).TotalSeconds + " seconds");
      Console.WriteLine();
      Console.WriteLine("Press any key to quit...");
      Console.ReadKey();
    }

    /// <summary>
    /// 
    /// </summary>
    static void NQueen()
    {
      DateTime startTime, stopTime;
      NQueenGA nQueen;
      int numQueens, numRuns;
      List<int> numGenerations;

      numRuns = 1;
      numGenerations = new List<int>(numRuns);

      startTime = DateTime.Now;

      numQueens = 100;

      for (int run = 0; run < numRuns; run++)
      {
        nQueen = new NQueenGA(numQueens, true, numQueens * 100)
        {
          ConsoleOutput = true,
          PopulationSize = numQueens,// * 10,
          PreserveElitePercent = 0.25,
          //SelectionMethod = new GALib.Selection.RandomSelection()
          //{
          //  TruncateBeforeSelect = true,
          //  TruncationPercent = 0.25
          //},
          SelectionMethod = new GALib.Selection.FitnessProportionateSelection()
          {
            AllowDuplicates = true,
            MaxRetriesForDuplicates = numQueens * 100,
            UseStochasticAcceptance = false,
            TruncateBeforeSelect = true,
            TruncationPercent = 0.25
          },
          CrossoverMethod = new GALib.Crossover.PartiallyMappedCrossover()
          {
            ProduceTwoChildren = true
          },
          //MutationMethod = new GALib.Mutation.NoMutation()
          MutationMethod = new GALib.Mutation.SwapMutation()
          {
            MutationChance = 0.10,
            MaxNumberOfSwaps = 1//numQueens / 50
          }
        };

        //nQueen.RescaleMethod = new PowerRescale(1.3);
        //nQueen.RescaleMethod = new ExponentialRescale(0.5);

        if (nQueen.RescaleMethod == null)
          Console.WriteLine("Optimal = " + nQueen.BestFitness);
        else
          Console.WriteLine("Optimal = " + nQueen.RescaleMethod.Rescale(nQueen.BestFitness));

        IGenotype result = nQueen.Run(int.MaxValue);

        //Console.WriteLine("Optimal = " + nQueen.BestFitness);
        //Console.WriteLine("Result  = " + result.Fitness);

        if (nQueen.SolutionFound)
        {
          numGenerations.Add(nQueen.GenerationNumber);
          Console.WriteLine("# of generations = " + nQueen.GenerationNumber);
        }
        else
          Console.WriteLine("# of generations = " + nQueen.GenerationNumber + " PREMATURELY CONVERGED");
      }

      stopTime = DateTime.Now;

      Console.WriteLine("Median = " + numGenerations.OrderBy(x => x).Skip(numGenerations.Count / 2).Take(1).First());

      Console.WriteLine();
      //Console.WriteLine("Solution found for " + nQueen.NumQueens + "-queen problem");
      //Console.WriteLine(nQueen.Population[0]);
      Console.WriteLine("Average Duration = " + (stopTime - startTime).TotalSeconds / numRuns + " seconds");
      Console.WriteLine("Total Duration = " + (stopTime - startTime).TotalSeconds + " seconds");
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
