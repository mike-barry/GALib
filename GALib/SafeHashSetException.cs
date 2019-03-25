using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public class SafeHashSetException : Exception
  {
    public SafeHashSetException(int maxAddRetries) : 
      base("Too many consecutive duplicates added to SafeHashSet (" + maxAddRetries + ")")
    {
    }
  }
}
