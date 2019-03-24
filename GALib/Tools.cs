using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib
{
  public static class Tools
  {
    /// <summary>
    /// A static instance of Random
    /// </summary>
    public static Random StaticRandom { get; } = new Random(421784);
    //public static Random StaticRandom { get; private set; } = new Random();

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

    /// <summary>
    /// Returns a random range between the specified minimum and maximum
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="start"></param>
    /// <param name="stop"></param>
    public static void RandomRange(int min, int max, ref int start, ref int stop)
    {
      int hold;

      start = StaticRandom.Next(min, max);
      stop = StaticRandom.Next(min, max);

      if (start > stop)
      {
        hold = start;
        start = stop;
        stop = hold;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="start"></param>
    /// <param name="stop"></param>
    /// <returns></returns>
    public static bool InRange(int value, int start, int stop)
    {
      if (value > start && value < stop)
        return true;
      else
        return false;
    }

    /// <summary>
    /// Clones a list and performs a deep copy on each element
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static IList<T> Clone<T>(this IList<T> list) where T : ICloneable
    {
      return list.Select(i => (T)i.Clone()).ToList();
    }

  }
}
