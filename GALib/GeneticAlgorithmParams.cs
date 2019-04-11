using System.ComponentModel;

namespace GALib
{
  public abstract class GeneticAlgorithmParams
  {
    private int maxRetriesForDuplicates = 100;
    private int populationSize = 100;
    private double preserveElitePercent = 0.1;

    [Category("GA Parameters"), DisplayName("Allow Duplicate Individuals")]
    public bool AllowDuplicates { get; set; } = false;

    [Category("GA Parameters"), DisplayName("Max Duplicate Collisions")]
    public int MaxRetriesForDuplicates
    {
      get
      {
        return maxRetriesForDuplicates;
      }
      set
      {
        if (value < 0)
          maxRetriesForDuplicates = 0;
        else
          maxRetriesForDuplicates = value;
      }
    }

    [Category("GA Parameters"), DisplayName("Population Size")]
    public int PopulationSize
    {
      get
      {
        return populationSize;
      }
      set
      {
        if (value < 0)
          populationSize = 0;
        else
          populationSize = value;
      }
    }

    [Category("GA Parameters"), DisplayName("Elitism Percent"), Description("The percent of elites to preserve between generations")]
    public double PreserveElitePercent
    {
      get
      {
        return preserveElitePercent;
      }
      set
      {
        if (value < 0)
          preserveElitePercent = 0;
        else if (value > 1)
          preserveElitePercent = 1;
        else
          preserveElitePercent = value;
      }
    }

  }
}
