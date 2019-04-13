using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Mutation
{
  public class NoMutation : MutationMethod
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NoMutation"/> class.
    /// </summary>
    public NoMutation() : base(true) { }

    /// <summary>
    /// Performs no mutation.
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene</typeparam>
    /// <param name="chromosome">The chromosome</param>
    /// <returns>False</returns>
    public override bool DoMutation<Gene>(ref Gene[] chromosome)
    {
      return false;
    }

    /// <summary>
    /// Handles the mutation.
    /// </summary>
    /// <typeparam name="Gene">The type of the Gene.</typeparam>
    /// <param name="chromosome">The chromosome.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override void HandleMutation<Gene>(ref Gene[] chromosome)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="System.String" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
      return "No Mutation";
    }

  }
}
