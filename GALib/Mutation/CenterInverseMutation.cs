using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Mutation
{
  /// <summary>
  /// Page 71 from https://www.researchgate.net/publication/221700559_Analyzing_the_Performance_of_Mutation_Operators_to_Solve_the_TravellingSalesman_Problem
  /// </summary>
  /// <seealso cref="GALib.Mutation.MutationMethod" />
  public class CenterInverseMutation : MutationMethod
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    /// <returns></returns>
    public CenterInverseMutation() : base(true) { }

    /// <summary>
    /// Handles the mutation.
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene.</typeparam>
    /// <param name="chromosome">The chromosome.</param>
    protected override void HandleMutation<Gene>(ref Gene[] chromosome)
    {
      int length, center, index;

      length = chromosome.Length;
      center = Tools.StaticRandom.Next(0, length + 1);
      index = 0;

      foreach (Gene g in chromosome.Take(center).Reverse())
        chromosome[index++] = g;

      foreach (Gene g in chromosome.Skip(center).Reverse())
        chromosome[index++] = g;
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "Center Inverse Mutation";
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
