using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public class Population//<T>
  {
    private const int MAX_RETRY_ATTEMPTS = 100;

    private List<Genotype> Members { get; set; } = new List<Genotype>();

    public Population(BasicGA/*<T>*/ ga)
    {
      GenerateRandomPopulation(ga);
    }

    private void GenerateRandomPopulation(BasicGA<T> ga)
    {
      int popSize, attemptNum;
      bool allowDuplicates;
      List<string> unique;
      Genotype member;

      popSize = ga.PopulationSize;
      allowDuplicates = ga.AllowInitializationDuplicates;
      unique = new List<string>(popSize);
      attemptNum = 0;

      while (Members.Count < popSize)
      {
        member = ga.GenerateRandomMember();

        if (!allowDuplicates && unique.Contains(member.ToString()))
          if (++attemptNum > MAX_RETRY_ATTEMPTS) 
            throw new Exception("Reached the maximum number of retry attempts to generate a unique random population member");
          else
            continue;

        unique.Add(member.ToString());
        Members.Add(member);
        attemptNum = 0;
      }
    }

    public void Sort()
    {
      Members.Sort();
    }
  }
}
