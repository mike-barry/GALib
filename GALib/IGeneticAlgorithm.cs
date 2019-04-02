using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public interface IGeneticAlgorithm
  {
    Action FinishedGeneration { get; }
    Action TerminationReached { get; }

    int GenerationNumber { get; }
    bool Converged { get; }
    bool SolutionFound { get; }
    List<IGenotype> Population { get; }

    void Run();
  }
}
