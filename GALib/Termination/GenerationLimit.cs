using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Termination
{
  public class GenerationLimit : TerminationMethod
  {
    private int maxGenerations = 100;

    public int MaxGenerations
    {
      get
      {
        return maxGenerations;
      }
      set
      {
        if (value < 1)
          maxGenerations = value;
        else
          maxGenerations = value;
      }
    }

    public override bool CheckTermination(IGeneticAlgorithm ga)
    {
      if (ga.GenerationNumber >= maxGenerations)
        return true;
      else
        return false;
    }
  }
}
