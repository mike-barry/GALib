using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Termination
{
  public abstract class TerminationMethod
  {
    public abstract bool CheckTermination(IGeneticAlgorithm ga);
  }
}
