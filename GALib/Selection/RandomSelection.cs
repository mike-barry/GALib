using System;
using System.Collections.Generic;
using System.Linq;

namespace GALib.Selection
{
  /// <summary>
  /// Implementation of random selection
  /// </summary>
  public class RandomSelection : SelectionMethod
  {
    /// <summary>
    /// Performs truncation selection
    /// </summary>
    /// <returns></returns>
    public override List<IGenotype> DoSelection()
    {
      ICollection<IGenotype> selection;
      int index;

      if (AllowDuplicates)
        selection = new List<IGenotype>(SelectionCount);
      else
        selection = new SafeHashSet<IGenotype>(MaxRetriesForDuplicates);

      while (selection.Count < SelectionCount)
      {
        index = Tools.StaticRandom.Next(0, Population.Count);
        selection.Add(Population[index]);
      }

      if (AllowDuplicates)
        return (List<IGenotype>)selection;
      else
        return selection.ToList();
    }



  }
}
