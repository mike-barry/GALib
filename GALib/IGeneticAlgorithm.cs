using System;
using System.Collections.Generic;
using System.Drawing;

namespace GALib
{
  public interface IGeneticAlgorithm
  {
    Action FinishedGeneration { get; }
    Action TerminationReached { get; }

    Selection.SelectionMethod SelectionMethod { get; set; }
    Crossover.CrossoverMethod CrossoverMethod { get; set; }
    Mutation.MutationMethod MutationMethod { get; set; }
    List<Termination.TerminationMethod> TerminationMethods { get; }

    int GenerationNumber { get; }
    List<IGenotype> Population { get; }

    bool Converged { get; }
    bool Terminated { get; }
    bool SolutionFound { get; }

    IGenotype BestCurrent { get; }
    IGenotype BestInitial { get; }

    void Run();
    Bitmap DrawIndividual(IGenotype individual, int width, int height);
  }
}
