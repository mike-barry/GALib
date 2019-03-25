using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public class SafeHashSet : ICollection<IGenotype>
  {
    private HashSet<IGenotype> hashSet;

    public int MaxAddRetries { get; set; } = 100;
    public int RetryCount { get; private set; } = 0;

    public int Count { get { return hashSet.Count; } }
    public bool IsReadOnly { get { return false; } }

    public SafeHashSet(int MaxAddRetries)
    {
      hashSet = new HashSet<IGenotype>();
    }

    public void Add(IGenotype item)
    {
      if (hashSet.Add(item) == false && ++RetryCount > MaxAddRetries)
        throw new Exception("Too many consecutive duplicates added to CustomHashSet");

      RetryCount = 0;
    }

    public void Clear()
    {
      hashSet.Clear();
    }

    public bool Contains(IGenotype item)
    {
      return hashSet.Contains(item);
    }

    public void CopyTo(IGenotype[] array, int arrayIndex)
    {
      hashSet.CopyTo(array, arrayIndex);
    }

    public bool Remove(IGenotype item)
    {
      return hashSet.Remove(item);
    }

    public IEnumerator<IGenotype> GetEnumerator()
    {
      return hashSet.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return hashSet.GetEnumerator();
    }
  }
}
