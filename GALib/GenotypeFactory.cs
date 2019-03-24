using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public static class GenotypeFactory
  {
    /// <summary>
    /// TODO documentation
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="chromosome"></param>
    /// <param name="fitness"></param>
    /// <returns></returns>
    public delegate IGenotype CreateGenotype<Gene>(Gene[] chromosome, double fitness) where Gene : IComparable;

    /// <summary>
    /// TODO documentation
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="chromosome"></param>
    /// <param name="fitness"></param>
    /// <returns></returns>
    public static Genotype<Gene> CreateGenotypeGenericList<Gene>(Gene[] chromosome, double fitness)
      where Gene : IComparable
    {
      return new GenotypeGenericList<Gene>(chromosome, fitness);
    }

    /// <summary>
    /// TODO documentation
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="chromosome"></param>
    /// <param name="fitness"></param>
    /// <returns></returns>
    public static Genotype<Gene> CreateGenotypeString<Gene>(Gene[] chromosome, double fitness)
      where Gene : IComparable
    {
      return new GenotypeGenericList<Gene>(chromosome, fitness);
    }

    /// <summary>
    /// TODO documentation
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="typeOfGenotype"></param>
    /// <returns></returns>
    public static CreateGenotype<Gene> GetCreateMethod<Gene>(Type typeOfGenotype)
      where Gene : IComparable
    {
      if (typeOfGenotype == typeof(GenotypeGenericList<Gene>))
        return CreateGenotypeGenericList<Gene>;

      if (typeOfGenotype == typeof(GenotypeString))
        return CreateGenotypeString<Gene>;

      throw new NotImplementedException("GenotypeFactory missing implementation for " + typeOfGenotype.FullName);
    }


  }
}
