using Microsoft.VisualBasic;

namespace Visual_Editor_LibreLingo
{
    public partial class Form1 : Form
    {
        public static string projectPath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open YAML source tree
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the folder that contains the YAML source tree.";
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                projectPath = fbd.SelectedPath;
                // Add all YAML files to the tree view
                treeView1.Nodes.Clear();
                while (tabControl1.TabCount > 1) // This is a hack to remove all tabs except the home tab
                {
                    tabControl1.TabPages.RemoveAt(1);
                }

                YamlDotNet.Serialization.Deserializer deserializer = new YamlDotNet.Serialization.Deserializer();

                /*
  Language:
    Name: Qualleish
    IETF BCP 47: ql
  For speakers of:
    Name: English
    IETF BCP 47: en
                */

                // Deserialize course.yaml and get the English name
                string courseYaml = File.ReadAllText(fbd.SelectedPath + "\\course.yaml");
                dynamic course = deserializer.Deserialize<dynamic>(courseYaml)["Course"];
                string courseName = course["For speakers of"]["Name"];

                // Add the course name to the tree view
                treeView1.Nodes.Add(courseName + " -> " + course["Language"]["Name"]);

                // Add all the lessons to the tree view
                foreach (string lesson in Directory.GetFiles(fbd.SelectedPath + "\\basics\\skills"))
                {
                    if (lesson == null) continue;
                    // Deserialize the lesson
                    string lessonYaml = File.ReadAllText(lesson);
                    dynamic lessonObj = deserializer.Deserialize<dynamic>(lessonYaml);

                    // Add the lesson to the tree view
                    treeView1.Nodes[0].Nodes.Add(lessonObj["Skill"]["Name"]);
                }
                // Add context menu to the tree view
                treeView1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;
            // Check if the user double clicked on a lesson
            if (treeView1.SelectedNode.Level == 1)
            {
                // Open the lesson in the editor (tab)
                TabPage newTab = new TabPage(treeView1.SelectedNode.Text);
                tabControl1.TabPages.Add(newTab);
                tabControl1.SelectedTab = newTab;

                Editor editor = new Editor();
                editor.LessonName = treeView1.SelectedNode.Text;
                editor.SourceLanguage = treeView1.Nodes[0].Text.Split(' ')[2];
                editor.TargetLanguage = treeView1.Nodes[0].Text.Split(' ')[0];
                editor.Dock = DockStyle.Fill;
                newTab.Controls.Add(editor);

            }
        }

        private void newYAMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newFileName = "basics\\skills\\" + Interaction.InputBox("Enter the name of the new YAML file.", "New YAML file", "new.yaml");
            if (newFileName == "basics\\skills\\") return; // The user clicked cancel or didn't enter a name
            // Create the new YAML file
            File.Create(projectPath + "\\" + newFileName).Close();
            // Add the content to the new YAML file
            File.WriteAllText(projectPath + "\\" + newFileName, @"Skill:
  Name: " + newFileName.Replace(".yaml", "", StringComparison.CurrentCultureIgnoreCase).Replace("basics\\skills\\", "", StringComparison.CurrentCultureIgnoreCase) + @"
  Id: " + new Random().Next(100, 999999) + @"");
            // Add the new YAML file to the tree view
            treeView1.Nodes[0].Nodes.Add(newFileName.Replace(".yaml", "", StringComparison.CurrentCultureIgnoreCase).Replace("basics\\skills\\", "", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}