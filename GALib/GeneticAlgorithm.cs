using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public abstract class GeneticAlgorithm<Gene>
    where Gene : IComparable
  {
    public int PopulationSize { get; set; } = 100;
    public List<IGenotype> Population { get; private set; } = null;

    public bool AllowDuplicates { get; set; } = false;
    public int MaxRetriesForDuplicates { get; set; } = 100; // This is used to prevent infinite loops
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
    public GeneticAlgorithm()
    {
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
      int duplicateRetryCount;

      ParameterCheck();

      if (Population == null)
        InitializePopulation();

      if (AllowDuplicates)
        nextPopulation = new List<IGenotype>(PopulationSize);
      else
        nextPopulation = new HashSet<IGenotype>();

      if (PreserveParents && AllowDuplicates)
        preservedParents = new HashSet<IGenotype>();
      else
        preservedParents = null;

      solutionFound = false;
      duplicateRetryCount = 0;

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

            if (AllowDuplicates)
              nextPopulation.Add(child);
            else if (((HashSet<IGenotype>)nextPopulation).Add(child) == false)
              if (++duplicateRetryCount > MaxRetriesForDuplicates)
                throw new Exception("Reached the maximum number of retry attempts to generate a unique child");
              else
                continue;

            duplicateRetryCount = 0;
          }
          
          if (solutionFound)
            break;
        }

        if (AllowDuplicates)
          Population = (List<IGenotype>)nextPopulation;
        else
          Population = nextPopulation.ToList();
      }

      // Sort the population based on fitness (higher fitness will be at end of list)
      Population.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));

      // Return the individual with the highest score
      return (Genotype<Gene>)Population[Population.Count];
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
      int retryCount;
      ICollection<IGenotype> population;
      IGenotype member;

      if (AllowDuplicates)
        population = new List<IGenotype>(PopulationSize);
      else
        population = new HashSet<IGenotype>();

      retryCount = 0;

      while (population.Count < PopulationSize)
      {
        member = GenerateRandomMember();

        if(AllowDuplicates)
          population.Add(member);
        else if (((HashSet<IGenotype>)population).Add(member) == false)
          if (++retryCount > MaxRetriesForDuplicates)
            throw new Exception("Reached the maximum number of retry attempts to generate a unique random population member");
          else
            continue;

        retryCount = 0;
      }

      if (AllowDuplicates)
        Population = (List<IGenotype>)population;
      else
        Population = population.ToList();

      CreateMethod = GenotypeFactory.GetCreateMethod<Gene>(Population[0].GetType());
    }
  }
}
