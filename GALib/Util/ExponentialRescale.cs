using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Util
{
  public class ExponentialRescale : IRescale
  {
    private double expScaleFactor;

    public ExponentialRescale(double expScaleFactor)
    {
      this.expScaleFactor = expScaleFactor;
    }

    public double Rescale(double value)
    {
      return Math.Exp(expScaleFactor * value);
    }
  }
}
