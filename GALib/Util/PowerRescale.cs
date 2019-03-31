using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Util
{
  public class PowerRescale : IRescale
  {
    private double power;

    public PowerRescale(double power)
    {
      this.power = power;
    }

    public double Rescale(double value)
    {
      return Math.Pow(value, power);
    }
  }
}
