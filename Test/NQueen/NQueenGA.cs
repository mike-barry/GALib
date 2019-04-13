using System;
using System.Collections.Generic;
using System.Drawing;

using GALib;

namespace Test.NQueen
{
  public class NQueenGA : GeneticAlgorithm<int>
  {
    public int NumQueens { get; private set; }
    public List<int> GeneDomain { get; private set; } = new List<int>();
    public int BestFitness { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="numQueens"></param>
    public NQueenGA(NQueenParams p) :
      base(p)
    {
      NumQueens = p.NumQueens;
      BestFitness = 0;

      for (int i = 0; i < NumQueens; i++)
      {
        GeneDomain.Add(i);
        BestFitness += i + 1;
      }
    }

    #region [ Methods ]

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override IGenotype GenerateRandomMember()
    {
      int[] geneSequence;
      double fitness;

      geneSequence = new int[NumQueens];
      GeneDomain.CopyTo(geneSequence);
      Tools.Shuffle(geneSequence);
      fitness = FitnessFunction(geneSequence, out bool junk);

      return new GenotypeGenericList<int>(geneSequence, fitness);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="geneSequence"></param>
    /// <returns></returns>
    public override double FitnessFunction(int[] geneSequence, out bool solutionFound)
    {
      int conflicts = 0;

      for (int i = 0; i < NumQueens; i++)
        for (int j = i + 1; j < NumQueens; j++)
          if (geneSequence[i] + (j - i) == geneSequence[j])
            conflicts++;
          else if (geneSequence[i] - (j - i) == geneSequence[j])
            conflicts++;

      solutionFound = (conflicts == 0);

      return 1.0 / conflicts;
    }

    /// <summary>
    /// Draws the individual.
    /// </summary>
    /// <param name="individual">The individual.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns></returns>
    public override Bitmap DrawIndividual(IGenotype individual, int width, int height)
    {
      return DrawIndividual((Genotype<int>)individual, width, height);
    }

    /// <summary>
    /// Draws the individual.
    /// </summary>
    /// <param name="individual">The individual.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns></returns>
    public Bitmap DrawIndividual(Genotype<int> individual, int widthX, int heightX)
    {
      int newWidth, newHeight;
      Bitmap img, queens;
      Graphics layer0, layer1;
      double xSpacing, ySpacing, xOffset, yOffset;
      bool[] conflict;

      newWidth = Math.Min(widthX, heightX);
      newHeight = Math.Min(widthX, heightX);

      img = new Bitmap(newWidth, newHeight);
      layer0 = Graphics.FromImage(img);
      layer0.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);

      queens = new Bitmap(newWidth, newHeight);
      layer1 = Graphics.FromImage(queens);

      xSpacing = (double)newWidth / NumQueens;
      ySpacing = (double)newHeight / NumQueens;
      xOffset = xSpacing / 2;
      yOffset = ySpacing / 2;

      conflict = new bool[NumQueens];

      // Draw the grid and set all conflicts to false
      for (int i = 1; i < NumQueens; i++)
      {
        layer0.DrawLine(Pens.LightGray, (int)(xSpacing * i), 0, (int)(xSpacing * i), newHeight);
        layer0.DrawLine(Pens.LightGray, 0, (int)(ySpacing * i), newWidth, (int)(ySpacing * i));

        conflict[i] = false;
      }

      // Find the conflicts and draw the conflict lines
      for (int i = 0; i < NumQueens; i++)
      {
        for (int j = i + 1; j < NumQueens; j++)
          if (individual[i] + (j - i) == individual[j])
          {
            layer0.DrawLine(Pens.Red, (int)(i * xSpacing + xOffset), (int)(individual[i] * ySpacing + yOffset), (int)(j * xSpacing + xOffset), (int)(individual[j] * ySpacing + yOffset));
            conflict[i] = true;
            conflict[j] = true;
          }
          else if (individual[i] - (j - i) == individual[j])
          {
            layer0.DrawLine(Pens.Red, (int)(i * xSpacing + xOffset), (int)(individual[i] * ySpacing + yOffset), (int)(j * xSpacing + xOffset), (int)(individual[j] * ySpacing + yOffset));
            conflict[i] = true;
            conflict[j] = true;
          }
      }

      // Draw the queens
      for (int x = 0; x < NumQueens; x++)
      {
        if (conflict[x])
          layer1.FillEllipse(Brushes.Red, (int)(x * xSpacing + xOffset / 2), (int)(individual[x] * ySpacing + yOffset / 2), (int)xOffset, (int)yOffset);
        else
          layer1.FillEllipse(Brushes.Black, (int)(x * xSpacing + xOffset / 2), (int)(individual[x] * ySpacing + yOffset / 2), (int)xOffset, (int)yOffset);

      }

      layer0.DrawImage(queens, 0, 0);

      return img;
    }

    #endregion
  }
}
