namespace TestingTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label4 = new Label();
            AreaTag_panel = new Panel();
            Scenario_panel = new Panel();
            ScenarioBySearch = new Panel();
            ScenarioFoundTextbox = new TextBox();
            Search = new Button();
            Scenario_label = new Label();
            Scenario_textbox = new TextBox();
            ScenarioByCheckBox = new Panel();
            MethodDropDown = new ComboBox();
            MethodPanel = new Panel();
            Method_ScenarioPanel = new Panel();
            Run = new Button();
            filepath_textbox = new TextBox();
            label1 = new Label();
            label5 = new Label();
            DisplayScenario = new TextBox();
            button1 = new Button();
            FeatureFilesPanel = new Panel();
            FeatureFilesDropdown = new ComboBox();
            label2 = new Label();
            FileLocationPanel = new Panel();
            Scenario_panel.SuspendLayout();
            ScenarioBySearch.SuspendLayout();
            MethodPanel.SuspendLayout();
            Method_ScenarioPanel.SuspendLayout();
            FeatureFilesPanel.SuspendLayout();
            FileLocationPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1, 40);
            label4.Name = "label4";
            label4.Size = new Size(181, 25);
            label4.TabIndex = 20;
            label4.Text = "Choose your method";
            // 
            // AreaTag_panel
            // 
            AreaTag_panel.AutoScroll = true;
            AreaTag_panel.BackColor = Color.Transparent;
            AreaTag_panel.BorderStyle = BorderStyle.FixedSingle;
            AreaTag_panel.Location = new Point(27, 165);
            AreaTag_panel.Name = "AreaTag_panel";
            AreaTag_panel.Size = new Size(706, 679);
            AreaTag_panel.TabIndex = 22;
            AreaTag_panel.Paint += AreaTag_panel_Paint;
            // 
            // Scenario_panel
            // 
            Scenario_panel.BackColor = Color.Transparent;
            Scenario_panel.Controls.Add(ScenarioBySearch);
            Scenario_panel.Controls.Add(ScenarioByCheckBox);
            Scenario_panel.Location = new Point(27, 165);
            Scenario_panel.Name = "Scenario_panel";
            Scenario_panel.Size = new Size(723, 713);
            Scenario_panel.TabIndex = 23;
            Scenario_panel.Visible = false;
            Scenario_panel.Paint += Scenario_panel_Paint;
            // 
            // ScenarioBySearch
            // 
            ScenarioBySearch.Controls.Add(ScenarioFoundTextbox);
            ScenarioBySearch.Controls.Add(Search);
            ScenarioBySearch.Controls.Add(Scenario_label);
            ScenarioBySearch.Controls.Add(Scenario_textbox);
            ScenarioBySearch.Location = new Point(2, 4);
            ScenarioBySearch.Name = "ScenarioBySearch";
            ScenarioBySearch.Size = new Size(718, 440);
            ScenarioBySearch.TabIndex = 22;
            ScenarioBySearch.Paint += ScenarioBySearch_Paint;
            // 
            // ScenarioFoundTextbox
            // 
            ScenarioFoundTextbox.BorderStyle = BorderStyle.FixedSingle;
            ScenarioFoundTextbox.Location = new Point(3, 138);
            ScenarioFoundTextbox.Multiline = true;
            ScenarioFoundTextbox.Name = "ScenarioFoundTextbox";
            ScenarioFoundTextbox.ReadOnly = true;
            ScenarioFoundTextbox.ScrollBars = ScrollBars.Vertical;
            ScenarioFoundTextbox.Size = new Size(718, 239);
            ScenarioFoundTextbox.TabIndex = 21;
            ScenarioFoundTextbox.TextChanged += ScenarioFound_TextChanged;
            // 
            // Search
            // 
            Search.Location = new Point(574, 66);
            Search.Name = "Search";
            Search.Size = new Size(119, 42);
            Search.TabIndex = 20;
            Search.Text = "Search";
            Search.UseVisualStyleBackColor = true;
            Search.Click += SearchScenario_Click;
            // 
            // Scenario_label
            // 
            Scenario_label.AutoSize = true;
            Scenario_label.ImageAlign = ContentAlignment.TopCenter;
            Scenario_label.Location = new Point(-2, 13);
            Scenario_label.Name = "Scenario_label";
            Scenario_label.Size = new Size(124, 25);
            Scenario_label.TabIndex = 17;
            Scenario_label.Text = "Enter Scenario";
            Scenario_label.Click += label6_Click;
            // 
            // Scenario_textbox
            // 
            Scenario_textbox.Location = new Point(-2, 66);
            Scenario_textbox.Multiline = true;
            Scenario_textbox.Name = "Scenario_textbox";
            Scenario_textbox.Size = new Size(489, 42);
            Scenario_textbox.TabIndex = 16;
            Scenario_textbox.TextChanged += SearchScenario_TextChanged;
            // 
            // ScenarioByCheckBox
            // 
            ScenarioByCheckBox.Location = new Point(5, 449);
            ScenarioByCheckBox.Name = "ScenarioByCheckBox";
            ScenarioByCheckBox.Size = new Size(718, 264);
            ScenarioByCheckBox.TabIndex = 21;
            ScenarioByCheckBox.Paint += ScenarioByCheckBox_Paint;
            // 
            // MethodDropDown
            // 
            MethodDropDown.FormattingEnabled = true;
            MethodDropDown.Location = new Point(479, 37);
            MethodDropDown.Name = "MethodDropDown";
            MethodDropDown.Size = new Size(223, 33);
            MethodDropDown.TabIndex = 26;
            MethodDropDown.Text = "Select";
            MethodDropDown.SelectedIndexChanged += MethodDropDown_SelectedIndexChanged;
            // 
            // MethodPanel
            // 
            MethodPanel.Controls.Add(MethodDropDown);
            MethodPanel.Controls.Add(label4);
            MethodPanel.Location = new Point(27, 62);
            MethodPanel.Name = "MethodPanel";
            MethodPanel.Size = new Size(706, 84);
            MethodPanel.TabIndex = 30;
            // 
            // Method_ScenarioPanel
            // 
            Method_ScenarioPanel.Controls.Add(Scenario_panel);
            Method_ScenarioPanel.Controls.Add(MethodPanel);
            Method_ScenarioPanel.Controls.Add(AreaTag_panel);
            Method_ScenarioPanel.Location = new Point(898, 8);
            Method_ScenarioPanel.Name = "Method_ScenarioPanel";
            Method_ScenarioPanel.Size = new Size(769, 908);
            Method_ScenarioPanel.TabIndex = 32;
            // 
            // Run
            // 
            Run.Location = new Point(284, 800);
            Run.Name = "Run";
            Run.Size = new Size(141, 50);
            Run.TabIndex = 0;
            Run.Text = "Run";
            Run.UseVisualStyleBackColor = true;
            Run.Click += RunTest_Click;
            // 
            // filepath_textbox
            // 
            filepath_textbox.Location = new Point(64, 110);
            filepath_textbox.Multiline = true;
            filepath_textbox.Name = "filepath_textbox";
            filepath_textbox.Size = new Size(456, 42);
            filepath_textbox.TabIndex = 1;
            filepath_textbox.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(64, 57);
            label1.Name = "label1";
            label1.Size = new Size(155, 25);
            label1.TabIndex = 2;
            label1.Text = "Enter File Location";
            label1.Click += label1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(58, 526);
            label5.Name = "label5";
            label5.Size = new Size(0, 25);
            label5.TabIndex = 13;
            // 
            // DisplayScenario
            // 
            DisplayScenario.AccessibleRole = AccessibleRole.None;
            DisplayScenario.BorderStyle = BorderStyle.FixedSingle;
            DisplayScenario.Location = new Point(58, 268);
            DisplayScenario.Multiline = true;
            DisplayScenario.Name = "DisplayScenario";
            DisplayScenario.ReadOnly = true;
            DisplayScenario.ScrollBars = ScrollBars.Vertical;
            DisplayScenario.Size = new Size(624, 510);
            DisplayScenario.TabIndex = 27;
            DisplayScenario.TextChanged += textBox1_TextChanged_1;
            // 
            // button1
            // 
            button1.Location = new Point(538, 110);
            button1.Name = "button1";
            button1.Size = new Size(144, 42);
            button1.TabIndex = 28;
            button1.Text = "Search Project";
            button1.UseVisualStyleBackColor = true;
            button1.Click += SearchProject_Click;
            // 
            // FeatureFilesPanel
            // 
            FeatureFilesPanel.Controls.Add(FeatureFilesDropdown);
            FeatureFilesPanel.Controls.Add(label2);
            FeatureFilesPanel.Location = new Point(59, 187);
            FeatureFilesPanel.Name = "FeatureFilesPanel";
            FeatureFilesPanel.Size = new Size(623, 75);
            FeatureFilesPanel.TabIndex = 29;
            // 
            // FeatureFilesDropdown
            // 
            FeatureFilesDropdown.FormattingEnabled = true;
            FeatureFilesDropdown.Location = new Point(241, 16);
            FeatureFilesDropdown.Name = "FeatureFilesDropdown";
            FeatureFilesDropdown.Size = new Size(220, 33);
            FeatureFilesDropdown.TabIndex = 25;
            FeatureFilesDropdown.Text = "Select";
            FeatureFilesDropdown.SelectedIndexChanged += FeatureFilesDropdown_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 19);
            label2.Name = "label2";
            label2.Size = new Size(122, 25);
            label2.TabIndex = 24;
            label2.Text = "Features Files ";
            label2.Click += label2_Click_2;
            // 
            // FileLocationPanel
            // 
            FileLocationPanel.Controls.Add(FeatureFilesPanel);
            FileLocationPanel.Controls.Add(button1);
            FileLocationPanel.Controls.Add(DisplayScenario);
            FileLocationPanel.Controls.Add(label5);
            FileLocationPanel.Controls.Add(label1);
            FileLocationPanel.Controls.Add(filepath_textbox);
            FileLocationPanel.Controls.Add(Run);
            FileLocationPanel.Location = new Point(103, 8);
            FileLocationPanel.Name = "FileLocationPanel";
            FileLocationPanel.Size = new Size(741, 908);
            FileLocationPanel.TabIndex = 31;
            FileLocationPanel.Paint += panel1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1842, 932);
            Controls.Add(Method_ScenarioPanel);
            Controls.Add(FileLocationPanel);
            Name = "Form1";
            Text = "Run Test ";
            Load += Form1_Load;
            Scenario_panel.ResumeLayout(false);
            ScenarioBySearch.ResumeLayout(false);
            ScenarioBySearch.PerformLayout();
            MethodPanel.ResumeLayout(false);
            MethodPanel.PerformLayout();
            Method_ScenarioPanel.ResumeLayout(false);
            FeatureFilesPanel.ResumeLayout(false);
            FeatureFilesPanel.PerformLayout();
            FileLocationPanel.ResumeLayout(false);
            FileLocationPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TextBox featurefile_textbox;
        private TextBox scenario_textbox;
        private Label label4;
        private Panel AreaTag_panel;
        private Panel Scenario_panel;
        private TextBox Scenario_textbox;
        private Label Scenario_label;
        private Label label3;
        private ComboBox MethodDropDown;
        private Button Search;
        private Panel ScenarioByCheckBox;
        private Panel ScenarioBySearch;
        private TextBox ScenarioFoundTextbox;
        private Panel MethodPanel;
        private Panel Method_ScenarioPanel;
        private Button Run;
        private TextBox filepath_textbox;
        private Label label1;
        private Label label5;
        private TextBox DisplayScenario;
        private Button button1;
        private Panel FeatureFilesPanel;
        private ComboBox FeatureFilesDropdown;
        private Label label2;
        private Panel FileLocationPanel;
    }
}