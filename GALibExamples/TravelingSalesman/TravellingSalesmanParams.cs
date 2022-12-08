using System;
using System.ComponentModel;

using GALib;

namespace GALibExamples.TravelingSalesman
{
  public class TravellingSalesmanParams : GeneticAlgorithmParams
  {

    [Category("Problem Parameters"), DisplayName("Number of Locations")]
    public int NumLocations { get; set; } = 30;

    [Category("Problem Parameters"), DisplayName("Dataset")]
    public TravelingSalesmanDataset.DatasetTypeEnum DatasetType { get; set; } = TravelingSalesmanDataset.DatasetTypeEnum.Circle;

    public TravelingSalesmanDataset GetDataset()
    {
      return TravelingSalesmanDataset.Generate(DatasetType, NumLocations);
    }

    public override string ToString()
    {
      return "Traveling Salesman Problem";
    }
  }
}
