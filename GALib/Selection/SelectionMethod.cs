using System;
using System.Collections.Generic;
using System.Linq;

namespace GALib.Selection
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class SelectionMethod
  {
    #region [ Members ]

    private double truncationPercent = 0.25;
    
    #endregion

    #region [ Constructor ]

    /// <summary>
    /// Initializes the selection process
    /// </summary>
    /// <param name="population">The population</param>
    public virtual void Initialize(List<IGenotype> population)
    {
      if (TruncateBeforeSelect)
      {
        int count = (int)(population.Count * TruncationPercent);

        if (SelectionCount > count && !AllowDuplicates)
          throw new ArgumentException("Selection count is greater than truncated population size and duplicate selection is not allowed");

        // Truncate the population to the desired size, prioritizing the highest fitness individuals
        Population = population.OrderByDescending(f => f.Fitness).Take(count).ToList();
      }
      else
      {
        if (SelectionCount > population.Count && !AllowDuplicates)
          throw new ArgumentException("Selection count is greater than population size and duplicate selection is not allowed");

        Population = population;
      }
    }

    #endregion 

    #region [ Properties ]

    protected List<IGenotype> Population { get; private set; } = null;
    public bool AllowDuplicates { get; set; } = false;
    public int MaxRetriesForDuplicates { get; set; } = 100; // This is used to prevent infinite loops
    public int SelectionCount { get; set; } = 2;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="SelectionMethod"/> should truncate the population before performing the selection.
    /// </summary>
    /// <value>
    ///   <c>true</c> if truncate; otherwise, <c>false</c>.
    /// </value>
    public bool TruncateBeforeSelect { get; set; } = false;

    /// <summary>
    /// Gets or sets the truncation percent.
    /// </summary>
    /// <value>
    /// The truncation percent.
    /// </value>
    /// <exception cref="ArgumentException">Value must be greater than zero and less than 1</exception>
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

    #endregion

    /// <summary>
    /// Performs selection
    /// </summary>
    /// <returns>A list of the selected individuals</returns>
    public abstract List<IGenotype> DoSelection();

  }
}
