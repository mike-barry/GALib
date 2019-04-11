using System.ComponentModel;

using GALib;

namespace Test.NQueen
{
  public class NQueenParams : GeneticAlgorithmParams
  {
    /// <summary>
    /// Gets or sets the number queens.
    /// </summary>
    /// <value>
    /// The number queens.
    /// </value>
    [Category("Problem Parameters"), DisplayName("Number of Queens")]
    public int NumQueens { get; set; } = 10;

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "N Queen Problem";
    }
  }
}
