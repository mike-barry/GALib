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
    public bool AllowInitializationDuplicates { get; set; } = false;
    public int MaxRetriesForInitializationDuplicates { get; set; } = 100;

    public bool PreserveParents { get; set; } = false; // aka elitism -- TODO
    public bool MutateParents { get; set; } = false; // TODO

    public List<IGenotype> Population { get; private set; } = null;

    public Selection.SelectionMethod SelectionMethod { get; set; } = null;
    public Crossover.CrossoverMethod CrossoverMethod { get; set; } = null;
    public Mutation.MutationMethod MutationMethod { get; set; } = null;

    public abstract Genotype<Gene> GenerateRandomMember();

    public abstract double FitnessFunction(Gene[] geneSequence);

    //public abstract double OptimalFitness { get; set; }

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
    public Genotype<Gene> Run(uint numGenerations)
    {
      List<IGenotype> parents, nextPopulation;
      Gene[][] children;
      Genotype<Gene> theStud;
      IGenotype child;

      ParameterCheck();

      if (Population == null)
        InitializePopulation();

      theStud = (Genotype<Gene>)Population[0];

      nextPopulation = new List<IGenotype>(PopulationSize);

      for (int generation = 0; generation < numGenerations; generation++)
      {
        nextPopulation.Clear();
        SelectionMethod.Initialize(Population);

        while (nextPopulation.Count < PopulationSize)
        {
          parents = SelectionMethod.DoSelection();
          children = CrossoverMethod.DoCrossover<Gene>(parents);

          for( int i = 0; i < children.Length; i++ )
          {
            //MutationMethod.DoMutation<Gene>(ref children[i]);
            child = theStud.GenericConstructor(children[i], FitnessFunction(children[i]));
            // TODO duplicate children check ???
            // TODO break if solution found
            nextPopulation.Add(child);
          }
        }

        Population = nextPopulation;
      }

      // Sort the population based on fitness (higher fitness will be at end of list)
      Population.Sort((a, b) => a.Fitness.CompareTo(b.Fitness));

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

      //if (MutationMethod is null)
      //  throw new Exception("No mutation method specified");
    }

    /// <summary>
    /// Creates an initial population
    /// </summary>
    private void InitializePopulation()
    {
      int attemptNum;
      HashSet<IGenotype> unique;
      Genotype<Gene> member;

      unique = new HashSet<IGenotype>();
      attemptNum = 0;

      while (unique.Count < PopulationSize)
      {
        member = GenerateRandomMember();

        if (!AllowInitializationDuplicates && unique.Contains(member))
          if (++attemptNum > MaxRetriesForInitializationDuplicates)
            throw new Exception("Reached the maximum number of retry attempts to generate a unique random population member");
          else
            continue;

        unique.Add(member);
        attemptNum = 0;
      }

      Population = new List<IGenotype>(unique);
    }
  }
}
