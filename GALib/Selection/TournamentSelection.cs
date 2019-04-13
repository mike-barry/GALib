using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GALib.Selection
{
  /// <summary>
  /// Implementation of tournament selection
  /// </summary>
  /// <remarks>
  /// https://en.wikipedia.org/wiki/Tournament_selection
  /// </remarks>
  public class TournamentSelection : SelectionMethod
  {
    private int tournamentSize = 5;

    /// <summary>
    /// Gets or sets the size of the tournament.
    /// </summary>
    /// <value>
    /// The size of the tournament.
    /// </value>
    [Category("Parameters"), DisplayName("Tournament Size")]
    public int TournamentSize
    {
      get
      {
        return tournamentSize;
      }
      set
      {
        if (value < 1)
          tournamentSize = 1;
        else
          tournamentSize = value;
      }
    }

    /// <summary>
    /// Performs tournament selection
    /// </summary>
    /// <returns></returns>
    public override List<IGenotype> DoSelection()
    {
      int index;
      IGenotype best;
      ICollection<IGenotype> selection;

      if (AllowDuplicates)
        selection = new List<IGenotype>(SelectionCount);
      else
        selection = new SafeHashSet<IGenotype>(MaxRetriesForDuplicates);

      while (selection.Count < SelectionCount)
      {
        index = Tools.StaticRandom.Next(0, Population.Count);
        best = Population[index];

        for (int i = 1; i < TournamentSize; i++)
        {
          index = Tools.StaticRandom.Next(0, Population.Count);

          if (best.Fitness < Population[index].Fitness)
            best = Population[index];
        }

        selection.Add(best);
      }

      if (AllowDuplicates)
        return (List<IGenotype>)selection;
      else
        return selection.ToList();
    }


    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "Tournament Selection";
    }
  }
}
