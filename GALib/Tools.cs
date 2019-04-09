using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GALib
{
  public static class Tools
  {
    /// <summary>
    /// A static instance of Random
    /// </summary>
    //public static Random StaticRandom { get; } = new Random(421784);
    public static Random StaticRandom { get; private set; } = new Random();

    /// <summary>
    /// Returns a list of distinct random values.
    /// </summary>
    /// <param name="num">The number of distinct random values.</param>
    /// <param name="min">The minimum random value (inclusive).</param>
    /// <param name="max">The maximum random value (exclusive).</param>
    /// <returns></returns>
    public static List<int> GetDistinctRandomValues(int num, int min, int max)
    {
      HashSet<int> values;

      values = new HashSet<int>();

      while (values.Count < num)
        values.Add(StaticRandom.Next(min, max));

      return values.ToList();
    }

    /// <summary>
    /// Creates a random range between min (inclusive) and max (exclusive).
    /// </summary>
    /// <param name="min">The minimum value (inclusive).</param>
    /// <param name="max">The maximum value (exclusive).</param>
    /// <returns></returns>
    public static IEnumerable<int> GetRandomRange(int min, int max)
    {
      int a, b;

      a = b = StaticRandom.Next(min, max);

      while (b == a)
        b = StaticRandom.Next(min, max);

      if (a < b)
        Swap(ref a, ref b);

      return Enumerable.Range(a, b - a);
    }


    /// <summary>
    /// Swaps the specified items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a">Item a</param>
    /// <param name="b">Item b</param>
    public static void Swap<T>(ref T a, ref T b)
    {
      T hold = a;
      a = b;
      b = hold;
    }

    /// <summary>
    /// Performs the Fisher-Yates shuffle on a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
      int n, k;
      T value;

      n= list.Count;

      while (n > 1)
      {
        n--;
        k = StaticRandom.Next(n + 1);
        value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }

    public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
    {
      return
        assembly.GetTypes()
                .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                .ToArray();
    }

    public static Type[] GetDerivedTypes(Type type)
    {
      return type.Assembly.GetTypes()
                .Where(t => t.Namespace == type.Namespace && t.BaseType.Name == type.Name)
                .ToArray();
    }

    ///// <summary>
    ///// Returns a random range between the specified minimum and maximum
    ///// </summary>
    ///// <param name="min"></param>
    ///// <param name="max"></param>
    ///// <param name="start"></param>
    ///// <param name="stop"></param>
    //public static void RandomRange(int min, int max, ref int start, ref int stop)
    //{
    //  int hold;

    //  start = StaticRandom.Next(min, max);
    //  stop = StaticRandom.Next(min, max);

    //  if (start > stop)
    //  {
    //    hold = start;
    //    start = stop;
    //    stop = hold;
    //  }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="value"></param>
    ///// <param name="start"></param>
    ///// <param name="stop"></param>
    ///// <returns></returns>
    //public static bool InRange(int value, int start, int stop)
    //{
    //  if (value > start && value < stop)
    //    return true;
    //  else
    //    return false;
    //}

    ///// <summary>
    ///// Clones a list and performs a deep copy on each element
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="list"></param>
    ///// <returns></returns>
    //public static IList<T> Clone<T>(this IList<T> list) where T : ICloneable
    //{
    //  return list.Select(i => (T)i.Clone()).ToList();
    //}

  }
}
