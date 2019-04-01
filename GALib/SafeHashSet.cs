using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  /// <summary>
  /// A simple wrapper for HashSet that keeps track of the number of consecutive duplicates
  /// that have been attempted to be added.  If too many occur consecutively, an 
  /// <see cref="Exception"/> is thrown.  The purpose of this is to prevent infinite loops.
  /// 
  /// The underlying HashSet can be accessed using the <see cref="Instance"/> property.
  /// </summary>
  public class SafeHashSet<T> : ICollection<T>
  {
    /// <summary>
    /// The maximum number of consecutive duplicates before an exception is thrown.
    /// </summary>
    /// <param name="MaxAddRetries"></param>
    public SafeHashSet(int maxAddRetries)
    {
      MaxAddRetries = maxAddRetries;
      Instance = new HashSet<T>();
    }

    /// <summary>
    /// The underlying <see cref="HashSet"/> instance.
    /// </summary>
    public HashSet<T> Instance { get; private set; }

    /// <summary>
    /// The number of consecutive duplicates before an exception is thrown.
    /// </summary>
    public int MaxAddRetries { get; set; } = 100;

    /// <summary>
    /// The current Add retry count.
    /// </summary>
    public int AddRetryCount { get; private set; } = 0;

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public int Count { get { return Instance.Count; } }

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </summary>
    public bool IsReadOnly { get { return false; } }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <exception cref="Exception">Too many consecutive duplicates added to <see cref="SafeHashSet"/></exception>
    public void Add(T item)
    {
      if (Instance.Add(item) == false)
      {
        if (++AddRetryCount > MaxAddRetries)
          throw new SafeHashSetException(MaxAddRetries);
      }
      else
        AddRetryCount = 0;
    }

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public void Clear()
    {
      Instance.Clear();
    }

    /// <summary>
    /// Determines whether this instance contains the object.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    ///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, <see langword="false" />.
    /// </returns>
    public bool Contains(T item)
    {
      return Instance.Contains(item);
    }

    /// <summary>
    /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
    public void CopyTo(T[] array, int arrayIndex)
    {
      Instance.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    ///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    public bool Remove(T item)
    {
      return Instance.Remove(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<T> GetEnumerator()
    {
      return Instance.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return Instance.GetEnumerator();
    }
  }
}
