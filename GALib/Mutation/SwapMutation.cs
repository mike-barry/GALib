using System;
using System.ComponentModel;

namespace GALib.Mutation
{
  /// <summary>
  /// This is a mutation method I made up.
  /// </summary>
  /// <seealso cref="GALib.Mutation.MutationMethod" />
  public class SwapMutation : MutationMethod
  {
    private int maxSwaps = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="SwapMutation"/> class.
    /// </summary>
    public SwapMutation() : base(true) { }

    /// <summary>
    /// Gets or sets the maximum number of swaps.
    /// </summary>
    /// <value>
    /// The maximum number of swaps.
    /// </value>
    /// <exception cref="ArgumentException">Value cannot be less than 1</exception>
    [Category("Parameters"), DisplayName("Max Number of Swaps")]
    public int MaxNumberOfSwaps
    {
      get
      {
        return maxSwaps;
      }
      set
      {
        if (value < 1)
          throw new ArgumentException("Value cannot be less than 1");
        else
          maxSwaps = value;
      }
    }

    /// <summary>
    /// Handles the mutation.
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene.</typeparam>
    /// <param name="chromosome">The chromosome.</param>
    protected override void HandleMutation<Gene>(ref Gene[] chromosome)
    {
      int length, numSwaps, indexA, indexB;
      Gene hold;

      length = chromosome.Length;

      if (maxSwaps == 1)
        numSwaps = 1;
      else
        numSwaps = Tools.StaticRandom.Next(1, maxSwaps + 1);

      for (int i = 0; i < numSwaps; i++)
      {
        indexA = Tools.StaticRandom.Next(0, length);
        indexB = Tools.StaticRandom.Next(0, length);

        hold = chromosome[indexA];
        chromosome[indexA] = chromosome[indexB];
        chromosome[indexB] = hold;
      }
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "Swap Mutation";
    }

  }
}
