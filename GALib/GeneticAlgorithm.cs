using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="Gene">The type of the Gene.</typeparam>
  public abstract class GeneticAlgorithm<Gene> : IGeneticAlgorithm
    where Gene : IComparable
  {
    #region [ Members ]

    private double preserveElitePercent = 0.1;

    #endregion

    #region [ Constructor ]

    /// <summary>
    /// Constructor
    /// </summary>
    public GeneticAlgorithm(GeneticAlgorithmParameters p)
    {
      AllowDuplicates = p.AllowDuplicates;
      MaxRetriesForDuplicates = p.MaxRetriesForDuplicates;
      PopulationSize = p.PopulationSize;
      PreserveElitePercent = p.PreserveElitePercent;
    }

    #endregion

    #region [ Properties ]

    public Action FinishedGeneration { get; set; } = null;

    public Action TerminationReached { get; set; } = null;

    public int PopulationSize { get; set; } = 100;

    public List<IGenotype> Population { get; private set; } = null;

    public int GenerationNumber { get; private set; } = 0;

    public bool Converged { get; private set; } = false;

    public bool Terminated { get; private set; } = false;

    public bool SolutionFound { get; private set; } = false;

    public bool AllowDuplicates { get; private set; }

    public int MaxRetriesForDuplicates { get; private set; }

    public double PreserveElitePercent
    {
      get
      {
        return preserveElitePercent;
      }
      set
      {
        if (value < 0)
          preserveElitePercent = 0;
        else if (value > 1)
          preserveElitePercent = 1;
        else
          preserveElitePercent = value;
      }
    }

    public IGenotype BestCurrent { get; private set; } = null;

    public IGenotype BestInitial { get; private set; } = null;

    public GenotypeFactory.CreateGenotype<Gene> CreateMethod { get; private set; } = null;

    public Selection.SelectionMethod SelectionMethod { get; set; } = null;

    public Crossover.CrossoverMethod CrossoverMethod { get; set; } = null;

    public Mutation.MutationMethod MutationMethod { get; set; } = null;

    public List<Termination.TerminationMethod> TerminationMethods { get; private set; } = new List<Termination.TerminationMethod>();

    #endregion

    #region [ Abstract Methods ]

    public abstract IGenotype GenerateRandomMember();
    public abstract double FitnessFunction(Gene[] geneSequence, out bool solutionFound);

    #endregion

    /// <summary>
    /// Runs the genetic algorithm for the specified number of generations
    /// </summary>
    /// <param name="numGenerations">The number of generations</param>
    /// <returns></returns>
    public void Run()
    {
      ICollection<IGenotype> nextPopulation;
      List<IGenotype> parents;
      Gene[][] children;
      IGenotype child;
      double fitness;
      bool mutated, solutionFound;

      ParameterCheck();

      if (Population == null)
        InitializePopulation();

      if (AllowDuplicates)
        nextPopulation = new List<IGenotype>(PopulationSize);
      else
        nextPopulation = new SafeHashSet<IGenotype>(MaxRetriesForDuplicates);

      solutionFound = false;

      if( PreserveElitePercent > 0)
        foreach (IGenotype individual in Population.Take((int)(PreserveElitePercent * PopulationSize)).ToList())
          nextPopulation.Add(individual);

      SelectionMethod.Initialize(Population);

      while (nextPopulation.Count < PopulationSize)
      {
        try
        {
          parents = SelectionMethod.DoSelection();
        }
        catch (SafeHashSetException)
        {
          Converged = true;
          break;
        }

        // Perform crossover
        children = CrossoverMethod.DoCrossover<Gene>(parents);

        // Iterate through each child produced by crossover
        for ( int i = 0; i < children.Length; i++ )
        {
          // Calculate fitness of child
          fitness = FitnessFunction(children[i], out solutionFound);

          // Perform mutation (but skip if solution has been found)
          mutated = solutionFound ? false : MutationMethod.DoMutation<Gene>(ref children[i]); // TODO return mutation and use only if better fitness??? Make a parameter to turn on/off

          // Recalculate fitness if mutation occurred
          if (mutated) 
            fitness = FitnessFunction(children[i], out solutionFound);

          // Instantiate child
          child = CreateMethod(children[i], fitness);

          try
          {
            nextPopulation.Add(child);

            if (solutionFound)
              break;
          }
          catch (SafeHashSetException)
          {
            Converged = true;
            break;
          }
        }
          
        if (solutionFound || Converged)
          break;
      }

      GenerationNumber++;
      SolutionFound = solutionFound;
      Population = nextPopulation.OrderByDescending(x => x.Fitness).ToList();
      BestCurrent = Population.First();

      FinishedGeneration?.Invoke();

      foreach ( Termination.TerminationMethod t in TerminationMethods )
        if( t.CheckTermination(this) )
        {
          Terminated = true;
          TerminationReached?.Invoke();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ParameterCheck()
    {
      if (SelectionMethod is null)
        throw new Exception("No selection method specified");

      if (CrossoverMethod is null)
        throw new Exception("No crossover method specified");

      if (MutationMethod is null)
        throw new Exception("No mutation method specified");

      if (TerminationMethods.Count == 0)
        throw new Exception("No termination method(s) specified");

      if (PopulationSize < 1)
        throw new Exception("Invalid population size");

      if (SolutionFound)
        throw new Exception("Solution already found");

      if (Converged)
        throw new Exception("GA has already converged");
    }

    /// <summary>
    /// Creates an initial population
    /// </summary>
    private void InitializePopulation()
    {
      ICollection<IGenotype> population;

      if (AllowDuplicates)
        population = new List<IGenotype>(PopulationSize);
      else
        population = new SafeHashSet<IGenotype>(MaxRetriesForDuplicates);

      while (population.Count < PopulationSize)
        population.Add(GenerateRandomMember());

      Population = population.OrderByDescending(x => x.Fitness).ToList();
      BestInitial = Population.First();

      CreateMethod = GenotypeFactory.GetCreateMethod<Gene>(BestInitial.GetType());
    }
  }
}
