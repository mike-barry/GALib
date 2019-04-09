using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using GALib;
using GALib.Crossover;
using GALib.Selection;
using GALib.Mutation;
using Test.TravelingSalesman;
using Test.NQueen;

namespace Test
{
  public partial class MainForm : Form
  {

    #region [ Members ]

    private TravellingSalesmanGA ga;
    private bool stopRequested = false;

    #endregion

    #region [ Constructor ]

    public MainForm()
    {
      InitializeComponent();
    }

    #endregion

    #region [ Event Handling ]

    /// <summary>
    /// Handles the Load event of the TravelingSalesmanForm control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void TravelingSalesmanForm_Load(object sender, EventArgs e)
    {
      paramsComboBox.Items.Clear();
      selectionComboBox.Items.Clear();
      crossoverComboBox.Items.Clear();
      mutationComboBox.Items.Clear();

      paramsComboBox.Items.Add(new TravellingSalesmanParams());
      paramsComboBox.Items.Add(new NQueenParams());

      foreach (Type t in Tools.GetDerivedTypes(typeof(SelectionMethod)))
        selectionComboBox.Items.Add((SelectionMethod)Activator.CreateInstance(t));

      foreach (Type t in Tools.GetDerivedTypes(typeof(CrossoverMethod)))
        crossoverComboBox.Items.Add((CrossoverMethod)Activator.CreateInstance(t));

      foreach (Type t in Tools.GetDerivedTypes(typeof(MutationMethod)))
        mutationComboBox.Items.Add((MutationMethod)Activator.CreateInstance(t));

      //TODO TerminationMethod

      ComboBoxSelectByType(paramsComboBox, typeof(TravellingSalesmanParams));
      ComboBoxSelectByType(selectionComboBox, typeof(FitnessProportionateSelection));
      ComboBoxSelectByType(crossoverComboBox, typeof(PartiallyMappedCrossover));
      ComboBoxSelectByType(mutationComboBox, typeof(SwapMutation));
    }

    /// <summary>
    /// Handles the Click event of the startStopButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void startStopButton_Click(object sender, EventArgs e)
    {
      GeneticAlgorithmParameters parameters;

      if (startStopButton.Text == "Start")
      {
        startStopButton.Text = "Stop";
        EnableForm(false);

        parameters = (GeneticAlgorithmParameters)paramsComboBox.SelectedItem;

        if (parameters.GetType() == typeof(TravellingSalesmanParams))
          ga = new TravellingSalesmanGA((TravellingSalesmanParams)parameters);
        else
          throw new NotImplementedException();

        ga.SelectionMethod = (SelectionMethod)selectionComboBox.SelectedItem;
        ga.CrossoverMethod = (CrossoverMethod)crossoverComboBox.SelectedItem;
        ga.MutationMethod = (MutationMethod)mutationComboBox.SelectedItem;

        ga.TerminationMethods.Clear();
        //TODO: ga.TerminationMethods.Add(terminationListBox.Items);
        ga.TerminationMethods.Add(new GALib.Termination.SolutionFound()); //TEMP
        ga.TerminationMethods.Add(new GALib.Termination.GenerationLimit((int)terminationNumericUpDown.Value)); //TEMP

        backgroundWorker.RunWorkerAsync();
      }
      else
        stopRequested = true;
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the gaComboBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void gaComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      paramsPropertyGrid.SelectedObject = paramsComboBox.SelectedItem;
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the selectionComboBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void selectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      selectionPropertyGrid.SelectedObject = selectionComboBox.SelectedItem;
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the crossoverComboBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void crossoverComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      crossoverPropertyGrid.SelectedObject = crossoverComboBox.SelectedItem;
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the mutationComboBox control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private void mutationComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      mutationPropertyGrid.SelectedObject = mutationComboBox.SelectedItem;
    }

    /// <summary>
    /// Handles the DoWork event of the backgroundWorker control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      while (true)
      {
        ga.Run();

        backgroundWorker.ReportProgress(0);

        if (ga.SolutionFound || ga.Converged || ga.Terminated || stopRequested)
          break;

        Thread.Sleep(10);
      }
    }

    /// <summary>
    /// Handles the ProgressChanged event of the backgroundWorker control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs" /> instance containing the event data.</param>
    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      bestTextBox.Text = ga.BestCurrent.ToString();
      NewGeneration();
    }

    /// <summary>
    /// Handles the RunWorkerCompleted event of the backgroundWorker control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      //if (ga.SolutionFound)
      //  MessageBox.Show("Found solution after " + ga.GenerationNumber + " generations");
      //else if (ga.Converged)
      //  MessageBox.Show("Prematurely converged after " + ga.GenerationNumber + " generations");
      //else if (ga.Terminated)
      //  MessageBox.Show("Terminated after " + ga.GenerationNumber + " generations");
      //else if (stopRequested)
      //  MessageBox.Show("Stopped by user after " + ga.GenerationNumber + " generations");

      stopRequested = false;

      startStopButton.Text = "Start";
      EnableForm(true);

      paramsPropertyGrid.Refresh();
    }

    #endregion

    #region [ Methods ]

    /// <summary>
    /// Handles a new generation
    /// </summary>
    private void NewGeneration()
    {
      Bitmap img;

      img = ga.DrawIndividual((Genotype<int>)ga.BestCurrent);

      pictureBox.Image = img;
    }

    /// <summary>
    /// Enables or disables controls on the form.
    /// </summary>
    /// <param name="enable">if set to <c>true</c> enables; otherwise, disables.</param>
    private void EnableForm(bool enable)
    {
      paramsComboBox.Enabled = enable;
      paramsPropertyGrid.Enabled = enable;
      selectionComboBox.Enabled = enable;
      selectionPropertyGrid.Enabled = enable;
      crossoverComboBox.Enabled = enable;
      crossoverPropertyGrid.Enabled = enable;
      mutationComboBox.Enabled = enable;
      mutationPropertyGrid.Enabled = enable;
      terminationNumericUpDown.Enabled = enable;
    }

    /// <summary>
    /// Selects an item in a combobox by it's type.  If none found, selects first item.
    /// </summary>
    /// <param name="comboBox">The combo box.</param>
    /// <param name="type">The type.</param>
    private static void ComboBoxSelectByType(ComboBox comboBox, Type type)
    {
      foreach (object o in comboBox.Items)
        if (o.GetType() == type)
        {
          comboBox.SelectedItem = o;
          return;
        }

      comboBox.SelectedIndex = 0;
    }

    #endregion

  }
}
