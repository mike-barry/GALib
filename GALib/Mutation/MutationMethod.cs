using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Mutation
{
  public abstract class MutationMethod
  {
    /// <summary>
    /// Performs mutation
    /// </summary>
    /// <typeparam name="Gene">The gene type</typeparam>
    /// <param name="chromosome">The chromosome</param>
    /// <returns>True if a mutation occurred; otherwise, false</returns>
    public abstract bool DoMutation<Gene>(ref Gene[] chromosome);
  }
}
