using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Crossover
{
  public abstract class CrossoverMethod
  {
    // TODO implement more from http://www.rubicite.com/Tutorials/GeneticAlgorithms.aspx
    public abstract Gene[][] DoCrossover<Gene>(List<IGenotype> parents) where Gene : IComparable;
  }
}
