using System;
using System.Collections.Generic;
using System.Linq;

namespace GALib.Selection
{
  /// <summary>
  /// Implementation of fitness proportionate selection
  /// </summary>
  /// <remarks>
  /// Derived from examples at https://en.wikipedia.org/w/index.php?title=Fitness_proportionate_selection&oldid=848806286
  /// </remarks>
  public class FitnessProportionateSelection : SelectionMethod
  {
    private double fitnessMetric = double.NaN;

    public bool UseStochasticAcceptance { get; set; }
    public int MaxRetriesForStochasticAcceptance { get; set; } = 100;

    /// <summary>
    /// Initializes the selection process
    /// </summary>
    /// <param name="population">The population</param>
    public override void Initialize(List<IGenotype> population)
    {
      base.Initialize(population);

      if (CheckNegativeFitness(Population))
        throw new ArgumentException("Population contains an individual with negative fitness");

      if (UseStochasticAcceptance)
      {
        // Find the maximum fitness of the population
        fitnessMetric = double.NegativeInfinity;
        foreach (IGenotype individual in Population)
          fitnessMetric = Math.Max(fitnessMetric, individual.Fitness);
      }
      else
      {
        // Calculate the sum of all the fitnesses in the population
        fitnessMetric = 0;
        foreach (IGenotype individual in Population)
          fitnessMetric += individual.Fitness;
      }
    }

    /// <summary>
    /// Performs fitness proportionate selection
    /// </summary>
    /// <returns>A list of the selected individuals</returns>
    public override List<IGenotype> DoSelection()
    {
      IGenotype selected;
      ICollection<IGenotype> selection;

      if (AllowDuplicates)
        selection = new List<IGenotype>(SelectionCount);
      else
        selection = new SafeHashSet<IGenotype>(MaxRetriesForDuplicates);

      while (selection.Count < SelectionCount)
      {
        if (UseStochasticAcceptance)
          selected = StochasticAcceptanceSelect(Population, fitnessMetric);
        else
          selected = SimpleSelect(Population, fitnessMetric);

        selection.Add(selected);
      }

      if (AllowDuplicates)
        return (List<IGenotype>)selection;
      else
        return selection.ToList();
    }

    /// <summary>
    /// Selects a single individual from the population using the simple method
    /// </summary>
    /// <param name="population">The population</param>
    /// <param name="sumFitness">The sum of fitness for the population</param>
    /// <returns>An individual from the population</returns>
    private IGenotype SimpleSelect(List<IGenotype> population, double sumFitness)
    {
      double value;

      // Generate a random value that is between 0 and sumFitness
      value = Tools.StaticRandom.NextDouble() * sumFitness;

      foreach (IGenotype individual in population)
      {
        // Subtract this individual's fitness from the value
        value -= individual.Fitness;

        // Return the current individual if the value is now less than zero
        if (value < 0)
          return individual;
      }

      // Return the last individual
      return population.Last();
    }

    /// <summary>
    /// Selects an individual from the population using the stochastic acceptance method
    /// </summary>
    /// <param name="population">The population</param>
    /// <param name="maxFitness">The maximum fitness of the population</param>
    /// <returns>An individual from the population</returns>
    private IGenotype StochasticAcceptanceSelect(List<IGenotype> population, double maxFitness)
    {
      int retries, index;
      double randFitness;

      retries = 0;

      while (++retries < MaxRetriesForStochasticAcceptance)
      {
        // Randomly select an individual
        index = (int)(Tools.StaticRandom.NextDouble() * population.Count);

        // Generate a random fitness between 0 and maximum fitness
        randFitness = Tools.StaticRandom.NextDouble() * maxFitness;

        // Return the selected individual if its fitness is greater than the random fitness; otherwise, try again
        if (randFitness < population[index].Fitness)
          return population[index];
      }

      // Handle the situation where we've retried too many times
      throw new Exception("Too many retries attempted with stochastic acceptance");
    }

    /// <summary>
    /// Returns false if the population contains an individual with a negative fitness
    /// </summary>
    /// <param name="population">The population</param>
    /// <returns></returns>
    private bool CheckNegativeFitness(List<IGenotype> population)
    {
      foreach (IGenotype individual in population)
        if (individual.Fitness < 0)
          return true;

      return false;
    }

    /// <summary>
    /// 
    /// </summary>
    public static void Test()
    {
      const int SIZE = 4;//1000000;
      const bool DUPLICATES = false;//false;

      List<IGenotype> population, selected;
      SelectionMethod selectionMethod;
      Dictionary<string, int> results;

      selectionMethod = new FitnessProportionateSelection()
      {
        AllowDuplicates = DUPLICATES,
        MaxRetriesForDuplicates = 100,
        MaxRetriesForStochasticAcceptance = 100,
        SelectionCount = SIZE,
        UseStochasticAcceptance = true
      };

      population = new List<IGenotype>(4);
      population.Add(new GenotypeString("10", 0.1));
      //population.Add(new GenotypeString("08", 0.08));
      //population.Add(new GenotypeString("12", 0.12));
      population.Add(new GenotypeString("20", 0.2));
      population.Add(new GenotypeString("30", 0.3));
      population.Add(new GenotypeString("40", 0.4));

      selectionMethod.Initialize(population);
      selected = selectionMethod.DoSelection();

      results = new Dictionary<string, int>(SIZE);

      foreach (IGenotype individual in population)
        results.Add(individual.ToString(), 0);

      foreach (IGenotype individual in selected)
        results[individual.ToString()]++;

      foreach (string key in results.Keys)
        Console.WriteLine(key + "% = " + ((double)results[key]/SIZE));

      Console.WriteLine();
    }

  }
}
