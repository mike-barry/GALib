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
  public abstract class GeneticAlgorithm<Gene>
    where Gene : IComparable
  {
    public int PopulationSize { get; set; } = 100;
    public List<IGenotype> Population { get; private set; } = null;

    public bool AllowDuplicates { get; private set; }
    public int MaxRetriesForDuplicates { get; private set; }

    public double PreserveElitePercent { get; set; } = 0; // TODO limit to between 0 and 1

    public GenotypeFactory.CreateGenotype<Gene> CreateMethod { get; private set; } = null;
    public Selection.SelectionMethod SelectionMethod { get; set; } = null;
    public Crossover.CrossoverMethod CrossoverMethod { get; set; } = null;
    public Mutation.MutationMethod MutationMethod { get; set; } = null;

    public abstract IGenotype GenerateRandomMember();
    public abstract double FitnessFunction(Gene[] geneSequence, out bool solutionFound);

    /// <summary>
    /// Constructor
    /// </summary>
    public GeneticAlgorithm(bool allowDuplicates, int maxRetriesForDuplicates)
    {
      AllowDuplicates = allowDuplicates;
      MaxRetriesForDuplicates = maxRetriesForDuplicates;
    }

    /// <summary>
    /// Runs the genetic algorithm for the specified number of generations
    /// </summary>
    /// <param name="numGenerations">The number of generations</param>
    /// <returns></returns>
    public IGenotype Run(uint numGenerations)
    {
      ICollection<IGenotype> nextPopulation;
      List<IGenotype> parents;
      Gene[][] children;
      IGenotype child;
      double fitness;
      bool solutionFound, mutated;
      int eliteSkip;

      ParameterCheck();

      if (Population == null)
        InitializePopulation();

      if (AllowDuplicates)
        nextPopulation = new List<IGenotype>(PopulationSize);
      else
        nextPopulation = new SafeHashSet<IGenotype>(MaxRetriesForDuplicates);

      eliteSkip = PopulationSize - (int)(PreserveElitePercent * PopulationSize);
      solutionFound = false;

      for (int generation = 0; generation < numGenerations; generation++)
      {
        nextPopulation.Clear();

        if( PreserveElitePercent > 0)
          foreach (IGenotype individual in Population.OrderBy(x => x.Fitness).Skip(eliteSkip).ToList())
            nextPopulation.Add(individual);

        SelectionMethod.Initialize(Population);

        while (nextPopulation.Count < PopulationSize)
        {
          parents = SelectionMethod.DoSelection();

          // Perform crossover
          children = CrossoverMethod.DoCrossover<Gene>(parents);

          // Iterate through each child produced by crossover
          for ( int i = 0; i < children.Length; i++ )
          {
            // Calculate fitness of child
            fitness = FitnessFunction(children[i], out solutionFound);

            if (solutionFound)
              break;

            // Perform mutation (but skip if solution has been found)
            mutated = solutionFound ? false : MutationMethod.DoMutation<Gene>(ref children[i]);

            // Recalculate fitness if mutation occurred
            if (mutated) 
              fitness = FitnessFunction(children[i], out solutionFound);

            // Instantiate child
            child = CreateMethod(children[i], fitness);

            try
            {
              nextPopulation.Add(child);
            }
            catch (SafeHashSetException)
            {
              Console.WriteLine("Duplicate max retry exception occurred");
              continue; // TODO need to do something more intelligent here
            }
          }
          
          if (solutionFound)
            break;
        }

        Console.WriteLine("Gen " + generation + ": " + Population.OrderBy(x => x.Fitness).Last().Fitness);

        Population = nextPopulation.ToList();

        if (solutionFound)
          break;
      }

      // Return the individual with the highest fitness
      return (Genotype<Gene>)Population.OrderBy(x => x.Fitness).Last();
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

      if (PopulationSize < 1)
        throw new Exception("Invalid population size");
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

      if (AllowDuplicates)
        Population = (List<IGenotype>)population;
      else
        Population = population.ToList();

      CreateMethod = GenotypeFactory.GetCreateMethod<Gene>(Population[0].GetType());
    }
  }
}
