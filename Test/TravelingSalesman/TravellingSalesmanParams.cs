using System;
using System.ComponentModel;

using GALib;

namespace Test.TravelingSalesman
{
  public class TravellingSalesmanParams : GeneticAlgorithmParameters
  {
    public enum DatasetTypeEnum
    {
      Random,
      Airports,
      Circle
    }

    [Category("Problem Parameters"), DisplayName("Number of Locations")]
    public int NumLocations { get; set; } = 100;

    [Category("Problem Parameters"), DisplayName("Dataset")]
    public DatasetTypeEnum DatasetType { get; set; } = DatasetTypeEnum.Random;

    public TravelingSalesmanDataset GetDataset()
    {
      switch(DatasetType)
      {
        case DatasetTypeEnum.Airports:
          return TravelingSalesmanDataset.LoadAirports(NumLocations);
        case DatasetTypeEnum.Circle:
          throw new NotImplementedException();
        case DatasetTypeEnum.Random:
        default:
          return TravelingSalesmanDataset.GenerateRandom(NumLocations);
      }
    }

    public override string ToString()
    {
      return "Traveling Salesman Problem";
    }
  }
}
