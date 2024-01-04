using Gherkin;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using TechTalk.SpecFlow;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        static string filename_output = string.Format(@"C:\LogTestFile\Testing {0:dd-MM-yyyy}.log", DateTime.Now);

        static bool areaTag = false;
        static bool scenarioTag = false;

        static string folderpath;
        static string featureNameSelected;

        Dictionary<string, List<string>> TagDictionary;

        List<CheckBox> TagCheckboxesList;
        static List<String> SelectedScenario;


        private Panel dropdownPanel;
        private CheckedListBox checkedListBox;
        private System.Windows.Forms.Button buttonShowCheckedList;

        private int itemsToShow = 10; // Number of items to display at a time
        private int currentIndex = 0;
        public Form1()
        {
            InitializeComponent();

            DisplayScenario.Visible = false;
            AreaTag_panel.Visible = false;
            Scenario_panel.Visible = false;
            FeatureFilesPanel.Visible = false;
            MethodPanel.Visible = false;
            Run.Visible = false;
            folderpath = GettingPath();



        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.SizeGripStyle = SizeGripStyle.Auto;
        }

        private string GettingPath()
        {
            string configpath = @"C:\ConfigFiles\TestInfo.config";
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configpath;

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            string filepath = config.AppSettings.Settings["filename"].Value;

            filepath_textbox.Text = filepath;
            filepath_textbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            return filepath_textbox.Text;
        }
        // Press Search Project
        private void SearchProject_Click(object sender, EventArgs e)
        {

            GettingFeaturesAvailable(folderpath);
            loadMethod();
            AreaTag_panel.Visible = false;
            Scenario_panel.Visible = false;

            DisplayScenario.Clear();
            AreaTag_panel.Controls.Clear();

            ScenarioBySearch.Visible = false;

            ScenarioByCheckBox.Controls.Clear();
            Scenario_textbox.Clear();
            ScenarioFoundTextbox.Clear();
        }
        private void GettingFeaturesAvailable(string filepath)
        {
            filepath = filepath_textbox.Text;
            if (Directory.Exists(filepath))
            {

                string[] featurefiles = Directory.GetFiles(Path.Combine(filepath, "Features"), "*feature");


                FeatureFilesPanel.Visible = true;
                MethodPanel.Visible = false;

                if (FeatureFilesDropdown != null)
                {
                    FeatureFilesDropdown.Items.Clear();
                    FeatureFilesDropdown.Text = "Select";
                    foreach (string featurefile in featurefiles)
                    {
                        FeatureFilesDropdown.Items.Add(Path.GetFileName(featurefile));

                    }

                }


            }
            else
            {

                MethodPanel.Visible = false;
                AreaTag_panel.Visible = false;
                ScenarioBySearch.Visible = false;
                ScenarioByCheckBox.Visible = false;
                DisplayScenario.Visible = false;
                
                FeatureFilesPanel.Visible = false;
                MessageBox.Show("Path/Project does not exist");
            }
        }
        private void FeatureFilesDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FeatureFilesDropdown.SelectedIndex != null)
            {

                DisplayScenario.Visible = true;
                ScenarioByCheckBox.Visible = true;
                ScenarioBySearch.Visible = true;
                featureNameSelected = FeatureFilesDropdown.SelectedItem.ToString();

                string directoryPath = Path.Combine(filepath_textbox.Text, "Features");
                string featurepath = Path.Combine(directoryPath, featureNameSelected);

                var parser = new Parser();

                using (var fileStream = new FileStream(featurepath, FileMode.Open))
                {
                    var gherkinDocument = parser.Parse(new StreamReader(fileStream));
                    var feature = gherkinDocument.Feature;


                    try
                    {
                        TagDictionary = new Dictionary<string, List<string>>();

                        DisplayScenario.Text = String.Empty;
                        DisplayScenario.Text = $"Tags and their scenarios\r\n";
                        DisplayScenario.Text += "\r\n";

                        foreach (var featureChild in gherkinDocument.Feature.Children)
                        {
                            if (featureChild is Gherkin.Ast.Scenario scenario)
                            {
                                if (scenario.Tags.Any())
                                {
                                    foreach (var tag in scenario.Tags)
                                    {
                                        if (tag.Name.Contains("P1") || tag.Name.Contains("P2") || tag.Name.Contains("P3"))
                                        {
                                            if (!TagDictionary.ContainsKey(tag.Name))
                                            {
                                                TagDictionary[tag.Name] = new List<string>();
                                            }
                                            TagDictionary[tag.Name].Add(scenario.Name.ToString());
                                            //MessageBox.Show(scenario.Name.ToString());
                                        }
                                    }
                                }
                            }
                        }

                       

                        foreach (var kvp in TagDictionary)
                        {

                            string tagName = kvp.Key;
                            List<string> scenarioNames = kvp.Value;

                            var testCount = kvp.Value.Count;
                            if (testCount > 1)
                            {
                                DisplayScenario.Text += $"Tag: {tagName} ( {testCount} Tests Found )\r\n";

                            }
                            else if (testCount == 1)
                            {
                                DisplayScenario.Text += $"Tag: {tagName} ( {testCount} Test Found )\r\n";
                            }
                            else { DisplayScenario.Text += $"Tag: {tagName} ( No Test Found )\r\n"; }


                            DisplayScenario.Text += "\r\n";
                            foreach (var scenarioName in scenarioNames)
                            {
                                DisplayScenario.Text += $"  Scenario: {scenarioName}\r\n";

                            }
                            DisplayScenario.Text += "\r\n";
                            DisplayScenario.Text += "\r\n";


                        }


                        AreaTag_panel.Controls.Clear();
                        AreaTag_panel.Visible = false;
                        ScenarioBySearch.Visible = false;
                        Scenario_textbox.Clear();
                        ScenarioByCheckBox.Controls.Clear();
                        ScenarioFoundTextbox.Clear(); 
                        Run.Visible = true;




                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }

                }

                MethodPanel.Visible = true;


            }


        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loadMethod()
        {
            string[] methodname = { "By Tag Name", "By Scenario" };
            if (MethodDropDown != null)
            {
                MethodDropDown.Items.Clear();
                MethodDropDown.Text = "Select";
            }
            MethodDropDown.Items.AddRange(methodname);
        }
        private void MethodDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MethodDropDown.SelectedIndex == 0)
            {
                AreaTag_panel.Visible = true;
                Scenario_panel.Visible = false;
               
                DisplayScenario.Visible = true;
                areaTag = true;
                featureNameSelected = FeatureFilesDropdown.SelectedItem.ToString();
                TagCheckboxesList = new List<CheckBox>();


                if (AreaTag_panel.Visible)
                {

                    try
                    {
                        int startLocation = 50;

                        AreaTag_panel.Controls.Clear();
                        Label filter = new Label();
                        filter.Text = "Filter";
                        AreaTag_panel.Controls.Add(filter);
                        foreach (var kvp in TagDictionary)
                        {
                            string tagName = kvp.Key;
                            List<string> scenarioNames = kvp.Value;

                            CheckBox checkbox = new CheckBox();
                            checkbox.Height = 50;
                            checkbox.Width = 200;
                            checkbox.Text = tagName;
                            checkbox.TextAlign = ContentAlignment.MiddleLeft;

                            checkbox.Location = new Point(50, startLocation);
                            TagCheckboxesList.Add(checkbox);
                            AreaTag_panel.Controls.Add(checkbox);
                            startLocation += 50;

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else if (MethodDropDown.SelectedIndex == 1)
            {

                AreaTag_panel.Visible = false;
                Scenario_panel.Visible = true;
                ScenarioBySearch.Visible = true;
                scenarioTag = true;
                DisplayScenario.Visible = true;

                //MessageBox.Show(featureNameSelected);

                ScenarioByDefault();
            }

        }

        private void ScenarioByDefault()
        {
            List<String> scenarioList = new List<string>();
            foreach (var kvp in TagDictionary)
            {
                string key_tagName = kvp.Key;
                List<string> tagValue = kvp.Value;
                foreach (string tagData in tagValue)
                {
                    scenarioList.Add(tagData);
                }
            }

            try
            {

                loadScenario(scenarioList);

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private List<String> SearchScenarios()
        {
            List<string> ScenarioFound = new List<string>();
            string searchScenario = Scenario_textbox.Text;

            if (!string.IsNullOrEmpty(searchScenario))
            {
                bool notFoundFlag = true;

                foreach (var kvp in TagDictionary)
                {
                    string value = kvp.Key;
                    List<String> scenarioNames = kvp.Value;

                    string[] searchScenario_split = searchScenario.Split(" ");


                    foreach (var scenarioName in scenarioNames)
                    {
                        if (searchScenario_split.Any() && searchScenario_split.All(word => scenarioName.IndexOf(word, StringComparison.OrdinalIgnoreCase) != -1))
                        {
                            ScenarioFound.Add(scenarioName);
                            notFoundFlag = false;

                        }
                    }
                }
                if (notFoundFlag)
                {
                    MessageBox.Show("Scenario Not Found");
                }
            }
            return ScenarioFound;
        }

        private void SearchScenario_Click(object sender, EventArgs e)
        {

            List<String> displayScenarioFound = SearchScenarios();

            ScenarioFoundTextbox.Text = $"Scenario Found ({displayScenarioFound.Count})\r\n";
            ScenarioFoundTextbox.Text += $"\r\n";
            for (int i = 0; i < displayScenarioFound.Count; i++)
            {
                ScenarioFoundTextbox.Text += $"\nScenario {i + 1} : {displayScenarioFound[i]}\r\n";

            }


            if (displayScenarioFound.Count > 0)
            {
                var result = MessageBox.Show($"Do you want to run those tests?", "Confirm", MessageBoxButtons.OKCancel);
                //Button selecttest = new Button();
                //selecttest.Width = 200;
                //selecttest.Height = 30;

                //selecttest.Text = "Run Results";
                //selecttest.Location = new Point(400,100);
                //ScenarioBySearch.Controls.Add(selecttest);

                if (result == DialogResult.OK)
                {
                    loadScenario(displayScenarioFound);
                }

            }
            else
            {
                ScenarioByDefault();
            }


        }

        //load Scenario and create checkbox dynamically accordingly
        private void loadScenario(List<String> listScenarios)
        {
            List<String> items_ = new List<String>(listScenarios);

            dropdownPanel = new Panel();
            dropdownPanel.Visible = false;
            dropdownPanel.BorderStyle = BorderStyle.None;

            checkedListBox = new CheckedListBox();
            checkedListBox.Items.AddRange(items_.ToArray());
            checkedListBox.Width = 200;
            checkedListBox.Height = 200;

            dropdownPanel.Controls.Add(checkedListBox);

            buttonShowCheckedList = new System.Windows.Forms.Button();
            buttonShowCheckedList.Text = "Select";
            buttonShowCheckedList.Click += ButtonShowCheckedList_Click;
            buttonShowCheckedList.Width = 200;
            buttonShowCheckedList.Height = 30;
            buttonShowCheckedList.Location = new Point(500, 10);

            Label scenario = new Label();
            scenario.Text = "Scenario";
            ScenarioByCheckBox.Controls.Add(scenario);


            ScenarioByCheckBox.Controls.Add(dropdownPanel);
            ScenarioByCheckBox.Controls.Add(buttonShowCheckedList);

            SelectedScenario = new List<string>();//use to store all scenarios checked


            checkedListBox.ItemCheck += (s, args) =>
            {
                string checkedValue = checkedListBox.Items[args.Index].ToString();

                if (args.NewValue == CheckState.Checked)
                {
                    // Item is checked, add it to the list
                    SelectedScenario.Add(checkedValue);
                }
                else if (args.NewValue == CheckState.Unchecked)
                {
                    // Item is unchecked, remove it from the list
                    SelectedScenario.Remove(checkedValue);
                }
            };



        }
        private void ButtonShowCheckedList_Click(object sender, EventArgs e)
        {
            ToggleDropdownPanel();
        }

        private void ToggleDropdownPanel()
        {
            dropdownPanel.Visible = !dropdownPanel.Visible;

            if (dropdownPanel.Visible)
            {
                dropdownPanel.Size = new Size(200, 200);
                // Limit the height or use a default value

                // Set the specific location for the dropdown panel on the screen
                dropdownPanel.Location = new Point(buttonShowCheckedList.Left, buttonShowCheckedList.Bottom);
            }
        }

        private void RunTest_Click(object sender, EventArgs e)
        {
            try
            {
                // Run the actual test 
                RunTest(folderpath);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in reading" + ex.Message);
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Scenario_textbox.Text))
            {
                ScenarioByDefault();
            }
        }
        public void RunTest(string folderpath)
        {
            //Validation for feature files 
            if (featureNameSelected == null)
            {
                MessageBox.Show("Select a Feature File");
            }

            //Validation for method
            if (MethodDropDown.SelectedIndex == -1)
            {

                MessageBox.Show("Select a Method");
            }



            if (SelectedScenario == null && ScenarioByCheckBox.Visible)
            {
                MessageBox.Show("Select a Scenario");
            }
            else
            {
                featureNameSelected = featureNameSelected.TrimEnd(".feature".ToCharArray());
                string command = @"/C dotnet test ";

                string filtertest = "";

                string filtervalue;

                // Run by TagName
                if (AreaTag_panel.Visible)
                {

                    List<string> checkboxFilters = new List<string>();

                    foreach (CheckBox checkbox in TagCheckboxesList)
                    {
                        if (checkbox.Checked)
                        {
                            string searchkeyvalue = checkbox.Text;

                            List<string> searched_result = FindKeyValue(searchkeyvalue);
                            if (searched_result == null)
                            {
                                MessageBox.Show("Key not found");
                            }
                            else
                            {
                                foreach (string value in searched_result)
                                {
                                    string testName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value).Replace(" ", String.Empty);
                                    filtervalue = $"FullyQualifiedName~{featureNameSelected}Feature.{testName}";
                                    checkboxFilters.Add(filtervalue);
                                }

                            }

                        }
                    }

                    if (checkboxFilters.Count > 0)
                    {
                        filtertest += $"({string.Join("|", checkboxFilters)})";

                    }


                }
                //Run by Scenario
                else if (ScenarioByCheckBox.Visible && ScenarioBySearch.Visible)
                {
                    List<String> scenariosCommand = new List<String>();

                    //if (string.IsNullOrEmpty(Scenario_textbox.Text))
                    //{
                    foreach (string scenario in SelectedScenario)
                    {
                        string newScenarioFeature = $"{featureNameSelected}Feature.{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scenario).Replace(" ", String.Empty)}";
                        scenariosCommand.Add(newScenarioFeature);
                    }

                    //}
                    //else
                    //{
                    //    List<string> Searched_scenario = SearchScenarios();
                    //    foreach (string item in Searched_scenario)
                    //    {
                    //        string newScenarioFeature = $"{featureNameSelected}Feature.{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item).Replace(" ", String.Empty)}";
                    //        scenariosCommand.Add(newScenarioFeature);
                    //    }
                    //}
                    if (scenariosCommand.Count > 0)
                    {
                        filtertest += $"({string.Join("|", scenariosCommand)})";
                    }

                }

                command += folderpath;


                if (!filtertest.Equals(""))
                {
                    command += $" --filter \"{filtertest}\"";
                    var result = MessageBox.Show($"{command}. Do you want to continue?", "Confirm", MessageBoxButtons.OKCancel);


                    if (result == DialogResult.OK)
                    {
                        TestProcess(command);
                    }
                }
                else
                {
                    filtertest = $"{featureNameSelected}Feature";
                    command += $" --filter \"{filtertest}\"";
                    var result = MessageBox.Show($"No tags or scenarios selected . Do you want to run all tests in the feature files?{command}", "Confirm", MessageBoxButtons.OKCancel);


                    if (result == DialogResult.OK)
                    {
                        TestProcess(command);
                    }
                }
            }

        }

        private void TestProcess(string command)
        {
            // Actual testing process starts 
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.RedirectStandardOutput = true;


            if (File.Exists(filename_output))
            {
                File.AppendAllText(filename_output, $"\n{DateTime.Now}\n");
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

            }
            else
            {
                File.AppendAllText(filename_output, $"\n{DateTime.Now}\n");
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            }

            process.Start();
            process.BeginOutputReadLine();


            process.WaitForExit();
            int exitCode = process.ExitCode;

            // Use Invoke to update UI elements from a different thread
            Invoke(new Action(() =>
            {
                if (exitCode == 1)
                {
                    MessageBox.Show("Test has run successfully");
                }
                else
                {
                    MessageBox.Show($"Test ended with error.");
                }
            }));

        }
        // Read filename_output from command and input into a file
        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                File.AppendAllText(filename_output, $"{outLine.Data}\n");

            }
        }


        private List<String> FindKeyValue(string searchkeyvalue)
        {
            List<string> scenarios = new List<string>();

            foreach (var kdata in TagDictionary)
            {
                if (kdata.Key == searchkeyvalue)
                {
                    foreach (var s in kdata.Value)
                    {
                        scenarios.Add(s);
                    }
                }

            }

            return scenarios;
        }

        //Searching for scenarios

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void AreaTag_panel_Paint(object sender, PaintEventArgs e)
        {

        }



        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }


        private void Filter_label_Click(object sender, EventArgs e)
        {
        }

        private void Scenario_panel_Paint(object sender, PaintEventArgs e)
        {

        }



        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void ScenarioByCheckBox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ScenarioBySearch_Paint(object sender, PaintEventArgs e)
        {

        }



        private void ScenarioFound_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}