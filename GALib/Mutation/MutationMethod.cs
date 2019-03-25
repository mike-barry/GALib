using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Mutation
{
  public abstract class MutationMethod
  {
    private double mutationChance = 0.10;

    /// <summary>
    /// Gets or sets the mutation chance.
    /// </summary>
    /// <value>
    /// The mutation chance.
    /// </value>
    /// <exception cref="ArgumentException">Value must be between 0 and 1</exception>
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
      }
    }

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
        HandleMutation<Gene>(ref chromosome);
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
