using System;
using System.Collections.Generic;
using System.Linq;

namespace GALib.Selection
{
  /// <summary>
  /// Implementation of truncation selection
  /// </summary>
  public class TruncationSelection : SelectionMethod
  {
    private double truncationPercent = 0.25;

    /// <summary>
    /// 
    /// </summary>
    public double TruncationPercent
    {
      get
      {
        return truncationPercent;
      }
      set
      {
        if (value > 1 || value <= 0)
          throw new ArgumentException("Value must be greater than zero and less than 1");
        truncationPercent = value;
      }
    }

    /// <summary>
    /// Initializes the selection process
    /// </summary>
    /// <param name="population">The population</param>
    public override void Initialize(List<IGenotype> population)
    {
      List<IGenotype> truncatedPopulation;
      int count;

      // Sort the population based on fitness (higher fitness will be at end of list)
      population.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));

      // Truncate the population to the desired size
      count = population.Count - (int)(population.Count * TruncationPercent);
      truncatedPopulation = population.Skip(count).ToList(); // TODO test

      // Initialize with the truncated population
      base.Initialize(truncatedPopulation);
    }

    /// <summary>
    /// Performs truncation selection
    /// </summary>
    /// <returns></returns>
    public override List<IGenotype> DoSelection()
    {
      List<IGenotype> selection;
      int index;

      selection = new List<IGenotype>(SelectionCount);
      ResetDuplicatesHandling();
      
      while (selection.Count < SelectionCount)
      {
        index = Tools.StaticRandom.Next(0, Population.Count);

        if (HandleDuplicates(Population[index])) 
          selection.Add(Population[index]);
      }

      return selection;
    }



  }
}
