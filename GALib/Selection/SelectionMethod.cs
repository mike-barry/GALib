using System;
using System.Collections.Generic;

namespace GALib.Selection
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class SelectionMethod
  {
    /// <summary>
    /// Initializes the selection process
    /// </summary>
    /// <param name="population">The population</param>
    public virtual void Initialize(List<IGenotype> population)
    {
      if (SelectionCount > population.Count && !AllowDuplicates)
        throw new ArgumentException("Selection count is greater than population size and duplicate selection is not allowed");

      Population = population;
    }

    protected List<IGenotype> Population { get; private set; } = null;

    public bool AllowDuplicates { get; set; } = false;
    public int MaxRetriesForDuplicates { get; set; } = 100; // This is used to prevent infinite loops
    public int SelectionCount { get; set; } = 2;

    /// <summary>
    /// Performs selection
    /// </summary>
    /// <returns>A list of the selected individuals</returns>
    public abstract List<IGenotype> DoSelection();

  }
}
