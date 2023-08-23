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
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
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
    }
}