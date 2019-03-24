using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Mutation
{
  public class NoMutation : MutationMethod
  {
    public override bool DoMutation<Gene>(ref Gene[] chromosome)
    {
      return false;
    }
  }
}
