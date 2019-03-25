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
  /// <typeparam name="Gene">The type of the ene.</typeparam>
  /// <remarks>
  /// TODO change Population to be an ICollection -- it will simplify code but will require code changes in SelectionMethod classes
  /// </remarks>
  public abstract class GeneticAlgorithm<Gene>
    where Gene : IComparable
  {
    public int PopulationSize { get; set; } = 100;
    public List<IGenotype> Population { get; private set; } = null;

    public bool AllowDuplicates { get; private set; }
    public int MaxRetriesForDuplicates { get; private set; }

    public bool PreserveParents { get; set; } = false; // aka elitism

    public GenotypeFactory.CreateGenotype<Gene> CreateMethod { get; private set; } = null;
    public Selection.SelectionMethod SelectionMethod { get; set; } = null;
    public Crossover.CrossoverMethod CrossoverMethod { get; set; } = null;
    public Mutation.MutationMethod MutationMethod { get; set; } = null;

    public abstract IGenotype GenerateRandomMember();
    public abstract double FitnessFunction(Gene[] geneSequence, out bool solutionFound);

    /// <summary>
    /// Constructor
    /// </summary>
    public GeneticAlgorithm(bool allowDuplicates, int maxRetriesForDuplicates = 100)
    {
      AllowDuplicates = allowDuplicates;
      MaxRetriesForDuplicates = maxRetriesForDuplicates;

      //if (allowDuplicates)
      //{
      //  Population = new List<IGenotype>();
      //  MaxRetriesForDuplicates = -1;
      //}
      //else
      //{
      //  Population = new SmartHashSet(maxRetriesForDuplicates);
      //  MaxRetriesForDuplicates = maxRetriesForDuplicates;
      //}
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
      HashSet<IGenotype> preservedParents;
      Gene[][] children;
      IGenotype child;
      double fitness;
      bool solutionFound, mutated;

      ParameterCheck();

      if (Population == null)
        InitializePopulation();

      if (AllowDuplicates)
        nextPopulation = new List<IGenotype>(PopulationSize);
      else
        nextPopulation = new SafeHashSet(MaxRetriesForDuplicates);

      if (PreserveParents && AllowDuplicates)
        preservedParents = new HashSet<IGenotype>();
      else
        preservedParents = null;

      solutionFound = false;

      for (int generation = 0; generation < numGenerations; generation++)
      {
        nextPopulation.Clear();

        if (PreserveParents)
          preservedParents.Clear();

        SelectionMethod.Initialize(Population);

        while (nextPopulation.Count < PopulationSize)
        {
          parents = SelectionMethod.DoSelection();

          if (PreserveParents)
            if (AllowDuplicates)
            {
              // nextPopulation is a List so we need to make sure parents are added only once
              foreach (IGenotype parent in parents)
                if (preservedParents.Add(parent))
                  nextPopulation.Add(parent);
            }
            else
            {
              // nextPopulation is a HashSet so we can just keep adding the parents
              foreach (IGenotype parent in parents)
                nextPopulation.Add(parent);
            }

          // Perform crossover
          children = CrossoverMethod.DoCrossover<Gene>(parents);

          // Iterate through each child produced by crossover
          for ( int i = 0; i < children.Length; i++ )
          {
            // Calculate fitness of child
            fitness = FitnessFunction(children[i], out solutionFound);

            // Perform mutation (but skip if solution has been found)
            mutated = solutionFound ? false : MutationMethod.DoMutation<Gene>(ref children[i]);

            // Recalculate fitness if mutation occurred
            if (mutated) 
              fitness = FitnessFunction(children[i], out solutionFound);

            // Instantiate child
            child = CreateMethod(children[i], fitness);

            nextPopulation.Add(child);
          }
          
          if (solutionFound)
            break;
        }

        if (AllowDuplicates)
          Population = (List<IGenotype>)nextPopulation;
        else
          Population = nextPopulation.ToList();
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
        population = new SafeHashSet(MaxRetriesForDuplicates);

      while (population.Count < PopulationSize)
        population.Add(GenerateRandomMember());

      // TODO this can be removed if Population is converted to ICollection<IGenotype>
      if (AllowDuplicates)
        Population = (List<IGenotype>)population;
      else
        Population = population.ToList();

      CreateMethod = GenotypeFactory.GetCreateMethod<Gene>(Population[0].GetType());
    }
  }
}
