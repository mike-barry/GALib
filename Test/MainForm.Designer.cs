namespace Test
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
      this.startStopButton = new System.Windows.Forms.Button();
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.terminationNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.problemTabPage = new System.Windows.Forms.TabPage();
      this.paramsComboBox = new System.Windows.Forms.ComboBox();
      this.paramsPropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.selectionTabPage = new System.Windows.Forms.TabPage();
      this.selectionComboBox = new System.Windows.Forms.ComboBox();
      this.selectionPropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.crossoverTabPage = new System.Windows.Forms.TabPage();
      this.crossoverPropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.crossoverComboBox = new System.Windows.Forms.ComboBox();
      this.mutationTabPage = new System.Windows.Forms.TabPage();
      this.mutationPropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.mutationComboBox = new System.Windows.Forms.ComboBox();
      this.terminationTabPage = new System.Windows.Forms.TabPage();
      this.terminationPropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.terminationRemoveButton = new System.Windows.Forms.Button();
      this.terminationListBox = new System.Windows.Forms.ListBox();
      this.terminationAddButton = new System.Windows.Forms.Button();
      this.terminationComboBox = new System.Windows.Forms.ComboBox();
      this.bestTextBox = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.terminationNumericUpDown)).BeginInit();
      this.tabControl.SuspendLayout();
      this.problemTabPage.SuspendLayout();
      this.selectionTabPage.SuspendLayout();
      this.crossoverTabPage.SuspendLayout();
      this.mutationTabPage.SuspendLayout();
      this.terminationTabPage.SuspendLayout();
      this.SuspendLayout();
      // 
      // backgroundWorker
      // 
      this.backgroundWorker.WorkerReportsProgress = true;
      this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
      this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
      this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
      // 
      // startStopButton
      // 
      this.startStopButton.Location = new System.Drawing.Point(12, 628);
      this.startStopButton.Name = "startStopButton";
      this.startStopButton.Size = new System.Drawing.Size(75, 23);
      this.startStopButton.TabIndex = 0;
      this.startStopButton.Text = "Start";
      this.startStopButton.UseVisualStyleBackColor = true;
      this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(373, 12);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(640, 640);
      this.pictureBox.TabIndex = 1;
      this.pictureBox.TabStop = false;
      // 
      // terminationNumericUpDown
      // 
      this.terminationNumericUpDown.Location = new System.Drawing.Point(258, 560);
      this.terminationNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.terminationNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.terminationNumericUpDown.Name = "terminationNumericUpDown";
      this.terminationNumericUpDown.Size = new System.Drawing.Size(85, 20);
      this.terminationNumericUpDown.TabIndex = 3;
      this.terminationNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(188, 562);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(64, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Generations";
      // 
      // tabControl
      // 
      this.tabControl.Controls.Add(this.problemTabPage);
      this.tabControl.Controls.Add(this.selectionTabPage);
      this.tabControl.Controls.Add(this.crossoverTabPage);
      this.tabControl.Controls.Add(this.mutationTabPage);
      this.tabControl.Controls.Add(this.terminationTabPage);
      this.tabControl.Location = new System.Drawing.Point(13, 13);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(354, 609);
      this.tabControl.TabIndex = 5;
      // 
      // problemTabPage
      // 
      this.problemTabPage.Controls.Add(this.paramsComboBox);
      this.problemTabPage.Controls.Add(this.paramsPropertyGrid);
      this.problemTabPage.Location = new System.Drawing.Point(4, 22);
      this.problemTabPage.Name = "problemTabPage";
      this.problemTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.problemTabPage.Size = new System.Drawing.Size(346, 583);
      this.problemTabPage.TabIndex = 0;
      this.problemTabPage.Text = "Problem";
      this.problemTabPage.UseVisualStyleBackColor = true;
      // 
      // paramsComboBox
      // 
      this.paramsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.paramsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.paramsComboBox.FormattingEnabled = true;
      this.paramsComboBox.Location = new System.Drawing.Point(3, 3);
      this.paramsComboBox.Name = "paramsComboBox";
      this.paramsComboBox.Size = new System.Drawing.Size(340, 21);
      this.paramsComboBox.TabIndex = 3;
      this.paramsComboBox.SelectedIndexChanged += new System.EventHandler(this.gaComboBox_SelectedIndexChanged);
      // 
      // paramsPropertyGrid
      // 
      this.paramsPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.paramsPropertyGrid.Location = new System.Drawing.Point(3, 30);
      this.paramsPropertyGrid.Name = "paramsPropertyGrid";
      this.paramsPropertyGrid.Size = new System.Drawing.Size(340, 550);
      this.paramsPropertyGrid.TabIndex = 0;
      // 
      // selectionTabPage
      // 
      this.selectionTabPage.Controls.Add(this.selectionComboBox);
      this.selectionTabPage.Controls.Add(this.selectionPropertyGrid);
      this.selectionTabPage.Location = new System.Drawing.Point(4, 22);
      this.selectionTabPage.Name = "selectionTabPage";
      this.selectionTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.selectionTabPage.Size = new System.Drawing.Size(346, 583);
      this.selectionTabPage.TabIndex = 1;
      this.selectionTabPage.Text = "Selection";
      this.selectionTabPage.UseVisualStyleBackColor = true;
      // 
      // selectionComboBox
      // 
      this.selectionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.selectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.selectionComboBox.FormattingEnabled = true;
      this.selectionComboBox.Location = new System.Drawing.Point(3, 3);
      this.selectionComboBox.Name = "selectionComboBox";
      this.selectionComboBox.Size = new System.Drawing.Size(340, 21);
      this.selectionComboBox.TabIndex = 2;
      this.selectionComboBox.SelectedIndexChanged += new System.EventHandler(this.selectionComboBox_SelectedIndexChanged);
      // 
      // selectionPropertyGrid
      // 
      this.selectionPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.selectionPropertyGrid.Location = new System.Drawing.Point(3, 30);
      this.selectionPropertyGrid.Name = "selectionPropertyGrid";
      this.selectionPropertyGrid.Size = new System.Drawing.Size(340, 550);
      this.selectionPropertyGrid.TabIndex = 1;
      // 
      // crossoverTabPage
      // 
      this.crossoverTabPage.Controls.Add(this.crossoverPropertyGrid);
      this.crossoverTabPage.Controls.Add(this.crossoverComboBox);
      this.crossoverTabPage.Location = new System.Drawing.Point(4, 22);
      this.crossoverTabPage.Name = "crossoverTabPage";
      this.crossoverTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.crossoverTabPage.Size = new System.Drawing.Size(346, 583);
      this.crossoverTabPage.TabIndex = 2;
      this.crossoverTabPage.Text = "Crossover";
      this.crossoverTabPage.UseVisualStyleBackColor = true;
      // 
      // crossoverPropertyGrid
      // 
      this.crossoverPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.crossoverPropertyGrid.Location = new System.Drawing.Point(3, 30);
      this.crossoverPropertyGrid.Name = "crossoverPropertyGrid";
      this.crossoverPropertyGrid.Size = new System.Drawing.Size(340, 550);
      this.crossoverPropertyGrid.TabIndex = 4;
      // 
      // crossoverComboBox
      // 
      this.crossoverComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.crossoverComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.crossoverComboBox.FormattingEnabled = true;
      this.crossoverComboBox.Location = new System.Drawing.Point(3, 3);
      this.crossoverComboBox.Name = "crossoverComboBox";
      this.crossoverComboBox.Size = new System.Drawing.Size(340, 21);
      this.crossoverComboBox.TabIndex = 3;
      this.crossoverComboBox.SelectedIndexChanged += new System.EventHandler(this.crossoverComboBox_SelectedIndexChanged);
      // 
      // mutationTabPage
      // 
      this.mutationTabPage.Controls.Add(this.mutationPropertyGrid);
      this.mutationTabPage.Controls.Add(this.mutationComboBox);
      this.mutationTabPage.Location = new System.Drawing.Point(4, 22);
      this.mutationTabPage.Name = "mutationTabPage";
      this.mutationTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.mutationTabPage.Size = new System.Drawing.Size(346, 583);
      this.mutationTabPage.TabIndex = 3;
      this.mutationTabPage.Text = "Mutation";
      this.mutationTabPage.UseVisualStyleBackColor = true;
      // 
      // mutationPropertyGrid
      // 
      this.mutationPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.mutationPropertyGrid.Location = new System.Drawing.Point(3, 30);
      this.mutationPropertyGrid.Name = "mutationPropertyGrid";
      this.mutationPropertyGrid.Size = new System.Drawing.Size(340, 550);
      this.mutationPropertyGrid.TabIndex = 6;
      // 
      // mutationComboBox
      // 
      this.mutationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.mutationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.mutationComboBox.FormattingEnabled = true;
      this.mutationComboBox.Location = new System.Drawing.Point(3, 3);
      this.mutationComboBox.Name = "mutationComboBox";
      this.mutationComboBox.Size = new System.Drawing.Size(340, 21);
      this.mutationComboBox.TabIndex = 5;
      this.mutationComboBox.SelectedIndexChanged += new System.EventHandler(this.mutationComboBox_SelectedIndexChanged);
      // 
      // terminationTabPage
      // 
      this.terminationTabPage.Controls.Add(this.terminationPropertyGrid);
      this.terminationTabPage.Controls.Add(this.terminationRemoveButton);
      this.terminationTabPage.Controls.Add(this.terminationListBox);
      this.terminationTabPage.Controls.Add(this.terminationAddButton);
      this.terminationTabPage.Controls.Add(this.terminationComboBox);
      this.terminationTabPage.Controls.Add(this.terminationNumericUpDown);
      this.terminationTabPage.Controls.Add(this.label1);
      this.terminationTabPage.Location = new System.Drawing.Point(4, 22);
      this.terminationTabPage.Name = "terminationTabPage";
      this.terminationTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.terminationTabPage.Size = new System.Drawing.Size(346, 583);
      this.terminationTabPage.TabIndex = 4;
      this.terminationTabPage.Text = "Termination";
      this.terminationTabPage.UseVisualStyleBackColor = true;
      // 
      // terminationPropertyGrid
      // 
      this.terminationPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.terminationPropertyGrid.Enabled = false;
      this.terminationPropertyGrid.Location = new System.Drawing.Point(3, 196);
      this.terminationPropertyGrid.Name = "terminationPropertyGrid";
      this.terminationPropertyGrid.Size = new System.Drawing.Size(340, 358);
      this.terminationPropertyGrid.TabIndex = 10;
      this.terminationPropertyGrid.Visible = false;
      // 
      // terminationRemoveButton
      // 
      this.terminationRemoveButton.Enabled = false;
      this.terminationRemoveButton.Location = new System.Drawing.Point(268, 168);
      this.terminationRemoveButton.Name = "terminationRemoveButton";
      this.terminationRemoveButton.Size = new System.Drawing.Size(75, 23);
      this.terminationRemoveButton.TabIndex = 9;
      this.terminationRemoveButton.Text = "Remove";
      this.terminationRemoveButton.UseVisualStyleBackColor = true;
      this.terminationRemoveButton.Visible = false;
      // 
      // terminationListBox
      // 
      this.terminationListBox.Enabled = false;
      this.terminationListBox.FormattingEnabled = true;
      this.terminationListBox.Location = new System.Drawing.Point(3, 30);
      this.terminationListBox.Name = "terminationListBox";
      this.terminationListBox.Size = new System.Drawing.Size(259, 160);
      this.terminationListBox.TabIndex = 8;
      this.terminationListBox.Visible = false;
      // 
      // terminationAddButton
      // 
      this.terminationAddButton.Enabled = false;
      this.terminationAddButton.Location = new System.Drawing.Point(268, 2);
      this.terminationAddButton.Name = "terminationAddButton";
      this.terminationAddButton.Size = new System.Drawing.Size(75, 23);
      this.terminationAddButton.TabIndex = 7;
      this.terminationAddButton.Text = "Add";
      this.terminationAddButton.UseVisualStyleBackColor = true;
      this.terminationAddButton.Visible = false;
      // 
      // terminationComboBox
      // 
      this.terminationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.terminationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.terminationComboBox.Enabled = false;
      this.terminationComboBox.FormattingEnabled = true;
      this.terminationComboBox.Location = new System.Drawing.Point(3, 3);
      this.terminationComboBox.Name = "terminationComboBox";
      this.terminationComboBox.Size = new System.Drawing.Size(259, 21);
      this.terminationComboBox.TabIndex = 6;
      this.terminationComboBox.Visible = false;
      // 
      // bestTextBox
      // 
      this.bestTextBox.Location = new System.Drawing.Point(373, 658);
      this.bestTextBox.Name = "bestTextBox";
      this.bestTextBox.ReadOnly = true;
      this.bestTextBox.Size = new System.Drawing.Size(640, 20);
      this.bestTextBox.TabIndex = 6;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1024, 688);
      this.Controls.Add(this.bestTextBox);
      this.Controls.Add(this.tabControl);
      this.Controls.Add(this.pictureBox);
      this.Controls.Add(this.startStopButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.Text = "Genetic Algorithm Testbed";
      this.Load += new System.EventHandler(this.TravelingSalesmanForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.terminationNumericUpDown)).EndInit();
      this.tabControl.ResumeLayout(false);
      this.problemTabPage.ResumeLayout(false);
      this.selectionTabPage.ResumeLayout(false);
      this.crossoverTabPage.ResumeLayout(false);
      this.mutationTabPage.ResumeLayout(false);
      this.terminationTabPage.ResumeLayout(false);
      this.terminationTabPage.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.ComponentModel.BackgroundWorker backgroundWorker;
    private System.Windows.Forms.Button startStopButton;
    private System.Windows.Forms.PictureBox pictureBox;
    private System.Windows.Forms.NumericUpDown terminationNumericUpDown;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage problemTabPage;
    private System.Windows.Forms.TabPage selectionTabPage;
    private System.Windows.Forms.PropertyGrid paramsPropertyGrid;
    private System.Windows.Forms.TabPage crossoverTabPage;
    private System.Windows.Forms.TabPage mutationTabPage;
    private System.Windows.Forms.PropertyGrid selectionPropertyGrid;
    private System.Windows.Forms.TabPage terminationTabPage;
    private System.Windows.Forms.ComboBox selectionComboBox;
    private System.Windows.Forms.ComboBox crossoverComboBox;
    private System.Windows.Forms.PropertyGrid crossoverPropertyGrid;
    private System.Windows.Forms.PropertyGrid mutationPropertyGrid;
    private System.Windows.Forms.ComboBox mutationComboBox;
    private System.Windows.Forms.ComboBox paramsComboBox;
    private System.Windows.Forms.Button terminationAddButton;
    private System.Windows.Forms.ComboBox terminationComboBox;
    private System.Windows.Forms.ListBox terminationListBox;
    private System.Windows.Forms.PropertyGrid terminationPropertyGrid;
    private System.Windows.Forms.Button terminationRemoveButton;
    private System.Windows.Forms.TextBox bestTextBox;
  }
}