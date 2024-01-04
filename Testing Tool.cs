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
        //the location of the folder and the file where the test output will be stored
        static string folder_output = string.Format(@"C:\LogTestFile", DateTime.Now);
        static string file_output = string.Format("Testing {0:dd-MM-yyyy}.log", DateTime.Now);
        static string filePath = Path.Combine(folder_output, file_output);

        //display of the tags
        static bool areaTag = false;
        static bool scenarioTag = false;

        //variables to store folder of the test to be run
        static string folderpath;
        static string featureNameSelected;

        //to store the tag 
        Dictionary<string, List<string>> TagDictionary;

        //Lists to store the selected values of the tags or scenarios
        List<CheckBox> TagCheckboxesList;
        static List<String> SelectedScenario;

        //display the scenarios in a checkbox list
        private Panel dropdownPanel;
        private CheckedListBox checkedListBox;
        private System.Windows.Forms.Button buttonShowCheckedList;

        private int itemsToShow = 10; // Number of items to display at a time in the dropdown
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

        // Search Project typed in search bar
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

        //Getting the path to search its features files
        private string GettingPath()
        {
            // Code to fetch the file path in a config file
                //string configpath = @"C:\ConfigFiles\TestInfo.config";
                //ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                //configFileMap.ExeConfigFilename = configpath;

                //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                //string filepath = config.AppSettings.Settings["filename"].Value;

            filepath_textbox.Text = @"C:\NunitTest";
            filepath_textbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            return filepath_textbox.Text;
        }

      
       //Populate the feature files dropdown with its file name 
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
                //hide other panels 
                MethodPanel.Visible = false;
                AreaTag_panel.Visible = false;
                ScenarioBySearch.Visible = false;
                ScenarioByCheckBox.Visible = false;
                DisplayScenario.Visible = false;
                
                FeatureFilesPanel.Visible = false;
                MessageBox.Show("Path/Project does not exist");
            }
        }

        //for each feature file selected -> read and display the tags and their respective scenarios
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
                        //Dictionary used to store the tests' tags and their respective scenarios
                        TagDictionary = new Dictionary<string, List<string>>();

                        DisplayScenario.Text = String.Empty;
                        DisplayScenario.Text = $"Tags and their scenarios\r\n";
                        DisplayScenario.Text += "\r\n";

                        //for the selected feeature file, read all the tags and only input those tags that contain either P1,P2,P3 into the dictionary and their scenario
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

                       int TotalScenariocount = 0;
                        //After adding tags and their scenarios, display them in a textbox
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
                                TotalScenariocount++;
                            }
                            DisplayScenario.Text += "\r\n";
                            DisplayScenario.Text += "\r\n";


                        }
                        DisplayScenario.Text += $"\r\nTotal Test Found: {TotalScenariocount}";

                        //Clear and hide panels that contain any old data
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

        // Two methods are available either by tags or scenarios
        // The methods are loaded in a dropdown
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
            // Method 1 : Run test by Tags 
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

            // Method 2 : Run test by Scenario
            else if (MethodDropDown.SelectedIndex == 1)
            {
                //Display the scenario pabel and hide the tag panel 
                AreaTag_panel.Visible = false;
                Scenario_panel.Visible = true;
                ScenarioBySearch.Visible = true;
                scenarioTag = true;
                DisplayScenario.Visible = true;

                //Populate the scenario dropdown with all scenario available 
                ScenarioByDefault();
            }

        }

        //Populate the scenario dropdown with values found in the dictionary
        private void ScenarioByDefault()
        {
            List<String> scenarioList = new List<string>();

            //Retrieve scenarios in the dictionary and put them in a list 
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
                // load the scenarios in a dropdown checkbox list
                loadScenario(scenarioList);

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //For all the tags selected, search its respective scenarios and return in form of a list
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

        //Search scenarios entered in the scenario search bar and input scenarios found in a list
      
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

        //Display 
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


        //Click Run for the tags or scenarios selected run 
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
        
        private void SearchScenario_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Scenario_textbox.Text))
            {
                ScenarioByDefault();
            }
        }

        //Format the selected tags or scenarios for it to be run as a command in the command prompt
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


            //Validation for scenarios
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

                //Method 1:  Run by TagName
                if (AreaTag_panel.Visible)
                {

                    List<string> checkboxFilters = new List<string>();

                    foreach (CheckBox checkbox in TagCheckboxesList)//for all the checkboxes dynamically created 
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

                                    //Test Command
                                    filtervalue = $"FullyQualifiedName~{featureNameSelected}Feature.{testName}";
                                    checkboxFilters.Add(filtervalue);//put the formated selected scenarios under the tags in a list
                                }

                            }

                        }
                    }

                    //concatenate the values in the array to form a single string of command
                    if (checkboxFilters.Count > 0)
                    {
                        filtertest += $"({string.Join("|", checkboxFilters)})";

                    }


                }
                //Method 2: Run by Scenario
                else if (ScenarioByCheckBox.Visible && ScenarioBySearch.Visible)
                {
                    List<String> scenariosCommand = new List<String>();

                  
                    foreach (string scenario in SelectedScenario)
                    {
                        //Test Command and put the formatted selected scenarios under the tags in a list
                        string newScenarioFeature = $"{featureNameSelected}Feature.{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(scenario).Replace(" ", String.Empty)}";
                        scenariosCommand.Add(newScenarioFeature);
                    }

                    //concatenate the values in the array to form a single string of command
                    if (scenariosCommand.Count > 0)
                    {
                        filtertest += $"({string.Join("|", scenariosCommand)})";
                    }

                }

                command += folderpath;

                //if there have been filters or scenarios selected
                if (!filtertest.Equals(""))
                {
                    command += $" --filter \"{filtertest}\"";
                    var result = MessageBox.Show($"{command}. Do you want to continue?", "Confirm", MessageBoxButtons.OKCancel);


                    if (result == DialogResult.OK)
                    {
                        TestProcess(command);
                    }
                }
                //otherwise the user can run the entire feature files without any filter or scenarios selected
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

        //After the command is ready, start to process it in the command prompt
        //and send the output in a folder called LogFileTest and in a file named after the date the test has been run
        private void TestProcess(string command)
        {
            // Actual testing process starts 
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.RedirectStandardOutput = true;

            if (!Directory.Exists(folder_output))
            {
                Directory.CreateDirectory(folder_output);
            }
            
            if (File.Exists(filePath))
            {
                File.AppendAllText(filePath, $"\n{DateTime.Now}\n");
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);

            }
            else
            {
                File.AppendAllText(filePath, $"\n{DateTime.Now}\n");
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
                    MessageBox.Show($"Test ended with error.{exitCode}");
                }
            }));

        }
        // Read folder_output from command and input into a file
        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                File.AppendAllText(filePath, $"{outLine.Data}\n");

            }
        }

      

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