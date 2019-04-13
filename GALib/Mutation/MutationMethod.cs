using System;
using System.ComponentModel;

namespace GALib.Mutation
{
  public abstract class MutationMethod
  {
    private double mutationChance = 0.10;

    /// <summary>
    /// Initializes a new instance of the <see cref="MutationMethod"/> class.
    /// </summary>
    /// <param name="distinctChromosomeRequired">if set to <c>true</c> distinct chromosome required.</param>
    public MutationMethod(bool distinctChromosomeRequired)
    {
      DistinctChromosomeRequired = distinctChromosomeRequired;
    }

    /// <summary>
    /// Gets or sets the mutation chance.
    /// </summary>
    /// <value>
    /// The mutation chance.
    /// </value>
    /// <exception cref="ArgumentException">Value must be between 0 and 1</exception>
    [Category("Parameters"), DisplayName("Mutation Probability")]
    public double MutationChance
    {
      get
      {
        return mutationChance;
      }
      set
      {
        if (mutationChance < 0 || mutationChance > 1)
          throw new ArgumentException("Value must be between 0 and 1");
        mutationChance = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the mutation method requires a distinct chromosome.
    /// </summary>
    /// <value>
    ///   <c>true</c> if mutation method requires a distinct chromosome; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool DistinctChromosomeRequired { get; private set; } // TODO need to implement this

    /// <summary>
    /// Performs mutation of the chromosome.
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene.</typeparam>
    /// <param name="chromosome">The chromosome.</param>
    /// <returns></returns>
    public virtual bool DoMutation<Gene>(ref Gene[] chromosome)
    {
      if (MutationChance < Tools.StaticRandom.NextDouble())
      {
        HandleMutation(ref chromosome);
        return true;
      }
      else
        return false;
    }

    /// <summary>
    /// Handles the mutation.
    /// </summary>
    /// <typeparam name="Gene">The type of the ene.</typeparam>
    /// <param name="chromosome">The chromosome.</param>
    protected abstract void HandleMutation<Gene>(ref Gene[] chromosome);
  }
}
