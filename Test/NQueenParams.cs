using System.ComponentModel;

using GALib;

namespace Test
{
  public class NQueenParams : GeneticAlgorithmParameters
  {
    [Category("Problem Parameters"), DisplayName("Number of Queens")]
    public int NumQueens { get; set; } = 100;


    public override string ToString()
    {
      return "N Queen Problem";
    }
  }
}
