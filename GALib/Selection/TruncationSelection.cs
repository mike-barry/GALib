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

      // Truncate the population to the desired size, prioritizing the highest fitness individuals
      count = population.Count - (int)(population.Count * TruncationPercent);
      truncatedPopulation = population.OrderBy(f => f.Fitness).Skip(count).ToList();

      // Initialize with the truncated population
      base.Initialize(truncatedPopulation);
    }

    /// <summary>
    /// Performs truncation selection
    /// </summary>
    /// <returns></returns>
    public override List<IGenotype> DoSelection()
    {
      ICollection<IGenotype> selection;
      int index;

      if (AllowDuplicates)
        selection = new List<IGenotype>(SelectionCount);
      else
        selection = new SafeHashSet<IGenotype>(MaxRetriesForDuplicates);

      while (selection.Count < SelectionCount)
      {
        index = Tools.StaticRandom.Next(0, Population.Count);
        selection.Add(Population[index]);
      }

      if (AllowDuplicates)
        return (List<IGenotype>)selection;
      else
        return selection.ToList();
    }



  }
}
