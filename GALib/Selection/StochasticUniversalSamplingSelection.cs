using System;
using System.Collections.Generic;
using System.Linq;

namespace GALib.Selection
{
  /// <summary>
  /// Implementation of stochastic universal sampling selection
  /// </summary>
  /// <remarks>
  /// https://en.wikipedia.org/wiki/Stochastic_universal_sampling
  /// https://watchmaker.uncommons.org/manual/ch03s02.html#d0e749
  /// </remarks>
  public class StochasticUniversalSamplingSelection : SelectionMethod
  {
    /// <summary>
    /// Performs stochastic universal sampling selection
    /// </summary>
    /// <returns></returns>
    public override List<IGenotype> DoSelection()
    {
      throw new NotImplementedException();
    }
  }
}
