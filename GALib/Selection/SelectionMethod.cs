using System;
using System.Collections.Generic;

namespace GALib.Selection
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class SelectionMethod
  {
    private HashSet<IGenotype> duplicateHash = new HashSet<IGenotype>();
    private int duplicateCount = 0;

    protected List<IGenotype> Population { get; private set; } = null;

    public int SelectionCount { get; set; } = 2;
    public bool AllowDuplicates { get; set; } = false;
    public int MaxRetriesForDuplicates { get; set; } = 100; // This is used to prevent infinite loops

    /// <summary>
    /// Initializes the selection process
    /// </summary>
    /// <param name="population">The population</param>
    public virtual void Initialize(List<IGenotype> population)
    {
      if (SelectionCount > population.Count && !AllowDuplicates)
        throw new ArgumentException("Selection count is greater than population size and duplicate selection is not allowed");

      ResetDuplicatesHandling();

      Population = population;
    }

    /// <summary>
    /// Performs selection
    /// </summary>
    /// <returns>A list of the selected individuals</returns>
    public abstract List<IGenotype> DoSelection();

    /// <summary>
    /// Resets the handling for the selection of duplicates
    /// </summary>
    protected void ResetDuplicatesHandling()
    {
      if (AllowDuplicates)
      {
        duplicateHash.Clear();
        duplicateCount = 0;
      }
    }

    /// <summary>
    /// Handles the selection of duplicates
    /// </summary>
    /// <param name="individual"></param>
    /// <returns>True if there is no duplicate issue, otherwise false</returns>
    protected bool HandleDuplicates(IGenotype individual)
    {
      if (!AllowDuplicates)
        if (duplicateHash.Add(individual) == false)
        {
          if (++duplicateCount > MaxRetriesForDuplicates)
            throw new Exception("Too many duplicates encountered during selection");
          return false;
        }

      duplicateCount = 0;
      return true;
    }
  }
}
