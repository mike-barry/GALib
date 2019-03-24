using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public class GenotypeString : Genotype<char>
  {
    private string Chromosome { get; set; }

    public GenotypeString(string chromosome, double fitness)
    {
      Fitness = fitness;
      Chromosome = chromosome;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public override char this[int index]
    {
      get { return Chromosome[index]; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return Chromosome;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      return Chromosome.Equals(obj.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
      return Chromosome.GetHashCode();
    }
  }
}
