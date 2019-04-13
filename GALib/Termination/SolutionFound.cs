using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Termination
{
  public class SolutionFound : TerminationMethod
  {
    public override bool CheckTermination(IGeneticAlgorithm ga)
    {
      return ga.SolutionFound;
    }

    public override string ToString()
    {
      return "Solution Found";
    }
  }
}
