﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALib.Mutation
{
  public abstract class MutationMethod
  {
    public abstract bool DoMutation<Gene>(ref Gene[] chromosome);
  }
}