using System;
using System.Collections.Generic;

namespace GALib
{
  public class GenotypeGenericList<Gene> : Genotype<Gene>
    where Gene : IComparable
  {
    private Gene[] Chromosome { get; set; }
    private int CachedHashCode { get; set; }

    public override Gene this[int index] { get { return Chromosome[index]; } }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="chromosome">The chromosome of the individual</param>
    /// <param name="fitness">The fitness value of the individual</param>
    public GenotypeGenericList(Gene[] chromosome, double fitness)//, bool useStringRepresentationForComparison = false) //TODO implement this???
    {
      Chromosome = chromosome;
      Fitness = fitness;
      Length = chromosome.Length;

      CalculateHashCode();
    }

    /// <summary>
    /// Calculates the hash code for Chromosome
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/8094867/good-gethashcode-override-for-list-of-foo-objects-respecting-the-order
    /// TODO look in to implementing SpookyHash (see link)
    /// </remarks>
    private void CalculateHashCode()
    {
      int hash = 0x2D2816FE;

      unchecked
      {
        foreach (Gene gene in Chromosome)
          hash = hash * 31 + gene.GetHashCode();
      }

      CachedHashCode = hash;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chromosome"></param>
    /// <param name="fitness"></param>
    /// <returns></returns>
    public override Genotype<Gene> GenericConstructor(Gene[] chromosome, double fitness)
    {
      return new GenotypeGenericList<Gene>(chromosome, fitness);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      if (obj is GenotypeGenericList<Gene>)
      {
        GenotypeGenericList<Gene> otherRecast;

        otherRecast = obj as GenotypeGenericList<Gene>;

        if (Length != otherRecast.Length)
          return false;

        for (int i = 0; i < Length; i++)
          if (!Chromosome[i].Equals(otherRecast.Chromosome[i]))
            return false;

        return true;
      }
      else
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
      return CachedHashCode;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      //return string.Join(" ", Chromosome);
      return Fitness + ": " + string.Join(" ", Chromosome);
    }
  }
}
