using System;
using System.Collections.Generic;

namespace GALib
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="Gene">The type of the ene.</typeparam>
  /// <seealso cref="GALib.Genotype{Gene}" />
  /// <remarks>
  /// TODO this[int index] and ToList() need to return a deep copy of the underlying data if the Gene type is immutable
  /// </remarks>
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
    /// Implementation taken from https://stackoverflow.com/questions/8094867/good-gethashcode-override-for-list-of-foo-objects-respecting-the-order
    /// TODO look in to implementing SpookyHash from link
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
    /// Converts to the chrosome to a list.
    /// </summary>
    /// <returns>A list containing the individual genes of the chromosome.</returns>
    public override List<Gene> ToList()
    {
      return new List<Gene>(Chromosome);
    }

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
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
