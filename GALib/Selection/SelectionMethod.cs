using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    private int maxRetriesForDuplicates = 100;
    private int selectionCount = 2;

    #endregion

    #region [ Properties ]

    /// <summary>
    /// Gets the population.
    /// </summary>
    /// <value>
    /// The population.
    /// </value>
    protected List<IGenotype> Population { get; private set; } = null;

    /// <summary>
    /// Gets or sets the selection count.
    /// </summary>
    /// <value>
    /// The selection count.
    /// </value>
    [Category("General"), DisplayName("Selection Count")]
    protected int SelectionCount
    {
      get
      {
        return selectionCount;
      }
      set
      {
        if (value < 1)
          selectionCount = 1;
        else
          selectionCount = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow duplicates during selection.
    /// </summary>
    /// <value>
    ///   <c>true</c> if [allow duplicates]; otherwise, <c>false</c>.
    /// </value>
    [Category("General"), DisplayName("Allow Duplicate Selection")]
    public bool AllowDuplicates { get; set; } = false;

    /// <summary>
    /// Gets or sets the maximum retries for duplicate collisions.
    /// </summary>
    /// <value>
    /// The maximum retries for duplicates.
    /// </value>
    /// <remarks>This is used to prevent infinite loops</remarks>
    [Category("General"), DisplayName("Max Duplicate Collisions")]
    public int MaxRetriesForDuplicates
    {
      get
      {
        return maxRetriesForDuplicates;
      }
      set
      {
        if (value < 1)
          maxRetriesForDuplicates = 1;
        else
          maxRetriesForDuplicates = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="SelectionMethod"/> should truncate the population before performing the selection.
    /// </summary>
    /// <value>
    ///   <c>true</c> if truncate; otherwise, <c>false</c>.
    /// </value>
    [Category("General"), DisplayName("Truncate Population Before Selection")]
    public bool TruncateBeforeSelect { get; set; } = false;

    /// <summary>
    /// Gets or sets the truncation percent.
    /// </summary>
    /// <value>
    /// The truncation percent.
    /// </value>
    [Category("General"), DisplayName("Truncation Percent")]
    public double TruncationPercent
    {
      get
      {
        return truncationPercent;
      }
      set
      {
        if (value > 1)
          truncationPercent = 1;
        else if (value < 0)
          truncationPercent = 0;
        else
          truncationPercent = value;
      }
    }

    #endregion

    #region [ Abstract Method ]

    /// <summary>
    /// Performs selection
    /// </summary>
    /// <returns>A list of the selected individuals</returns>
    public abstract List<IGenotype> DoSelection();

    #endregion

    #region [ Methods ]

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

    /// <summary>
    /// Checks the if the population contains an individual with negative fitness.
    /// </summary>
    /// <param name="population">The population.</param>
    /// <returns><c>true</c> if the population contains an individual with negative fitness; otherwise, <c>false</c></returns>
    public bool CheckNegativeFitness()
    {
      foreach (IGenotype individual in Population)
        if (individual.Fitness < 0)
          return true;

      return false;
    }

    #endregion 

  }
}
