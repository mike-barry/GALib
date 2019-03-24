using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public interface IGenotype
  {
    double Fitness { get; }
    int Length { get; }
  }
}
