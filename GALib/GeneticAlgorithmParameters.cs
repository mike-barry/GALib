using System.ComponentModel;

namespace GALib
{
  public abstract class GeneticAlgorithmParameters
  {
    [Category("GA Parameters"), DisplayName("Allow Duplicate Individuals")]
    public bool AllowDuplicates { get; set; } = false;

    [Category("GA Parameters"), DisplayName("Max Duplicate Collisions")]
    public int MaxRetriesForDuplicates { get; set; } = 100;

    [Category("GA Parameters"), DisplayName("Population Size")]
    public int PopulationSize { get; set; } = 100;

    [Category("GA Parameters"), DisplayName("Elitism Percent"), Description("The percent of elites to preserve between generations")]
    public double PreserveElitePercent { get; set; } = 0.1;

  }
}
