using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Mutation
{
  public class NoMutation : MutationMethod
  {
    /// <summary>
    /// Performs no mutation
    /// </summary>
    /// <typeparam name="Gene">The gene type</typeparam>
    /// <param name="chromosome">The chromosome</param>
    /// <returns>False</returns>
    public override bool DoMutation<Gene>(ref Gene[] chromosome)
    {
      return false;
    }
  }
}
