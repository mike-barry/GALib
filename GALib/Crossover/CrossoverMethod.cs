using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Crossover
{
  public abstract class CrossoverMethod
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CrossoverMethod"/> class.
    /// </summary>
    /// <param name="distinctRequired">if set to <c>true</c> distinct chrosome required.</param>
    protected CrossoverMethod(bool distinctChromosomeRequired)
    {
      DistinctChromosomeRequired = distinctChromosomeRequired;
    }

    /// <summary>
    /// Gets a value indicating whether the crossover method requires a distinct chromosome.
    /// </summary>
    /// <value>
    ///   <c>true</c> if crossover method requires a distinct chromosome; otherwise, <c>false</c>.
    /// </value>
    public bool DistinctChromosomeRequired { get; private set; } // TODO need to implement this

    /// <summary>
    /// Does the crossover.
    /// </summary>
    /// <typeparam name="Gene">The type of the gene.</typeparam>
    /// <param name="parents">The parents.</param>
    /// <returns></returns>
    public abstract Gene[][] DoCrossover<Gene>(List<IGenotype> parents) where Gene : IComparable;
  }
}
