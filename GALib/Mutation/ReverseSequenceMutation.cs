using System;
using System.Linq;

namespace GALib.Mutation
{
  /// <summary>
  /// Page 71 from https://www.researchgate.net/publication/221700559_Analyzing_the_Performance_of_Mutation_Operators_to_Solve_the_TravellingSalesman_Problem
  /// </summary>
  /// <seealso cref="GALib.Mutation.MutationMethod" />
  public class ReverseSequenceMutation : MutationMethod
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ReverseSequenceMutation"/> class.
    /// </summary>
    public ReverseSequenceMutation() : base(true) { }

    /// <summary>
    /// Handles the mutation.
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene.</typeparam>
    /// <param name="chromosome">The chromosome.</param>
    protected override void HandleMutation<Gene>(ref Gene[] chromosome)
    {
      int a, b, i;
      
      a = Tools.StaticRandom.Next(0, chromosome.Length - 1);
      b = Tools.StaticRandom.Next(2, chromosome.Length - a + 1);
      i = a;

      foreach (Gene g in chromosome.Skip(a).Take(b).Reverse())
        chromosome[i++] = g;
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "Reverse Sequence Mutation";
    }

    /// <summary>
    /// Tests this mutation method.
    /// </summary>
    public void Test()
    {
      int[] chromosome = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

      Console.WriteLine(string.Join(" ", chromosome));
      HandleMutation(ref chromosome);
      Console.WriteLine(string.Join(" ", chromosome));
    }
  }
}
