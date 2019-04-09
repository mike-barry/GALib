using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public abstract class Genotype<Gene> : IGenotype
    where Gene : IComparable
  {
    public double Fitness { get; protected set; }
    public int Length { get; protected set; }

    public abstract List<Gene> ToList(); 
    public abstract Gene this[int index] { get; }
    public abstract override bool Equals(object obj);
    public abstract override int GetHashCode();
    public abstract override string ToString();
  }
}
