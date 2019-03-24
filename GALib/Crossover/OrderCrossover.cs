using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Crossover
{
  public class OrderCrossover : CrossoverMethod
  {
    public bool ProduceSingleChild { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Gene"></typeparam>
    /// <param name="parents"></param>
    /// <returns></returns>
    public override Gene[][] DoCrossover<Gene>(List<IGenotype> parents)
    {
      // See http://creationwiki.org/Genetic_algorithm#Order_crossover_.28OX.29
      throw new NotImplementedException();
    }
  }
}
