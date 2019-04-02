using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public interface IConstructorParams
  {
    //TODO implement -- will be used in GUI for specifying parameters for GA
    bool AllowDuplicates { get; }
    int MaxRetriesForDuplicates { get; }
  }
}
