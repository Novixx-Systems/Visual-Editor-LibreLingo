using Microsoft.VisualBasic;
using YamlDotNet.Serialization;
using static System.Collections.Generic.Dictionary<object, object>;

namespace Visual_Editor_LibreLingo
{
    public partial class Editor : UserControl
    {
        public string LessonName { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
        dynamic lessonObj;
        public List<Lesson> lessons = new List<Lesson>();
        public Editor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string w1 = Interaction.InputBox("Word in source language:", "Add word", "", -1, -1);
            string w2 = Interaction.InputBox("Word in target language:", "Add word", "", -1, -1);

            if (w1 != "")
            {
                listBox1.Items.Add(w1 + " -> " + w2);
            }
            if (w2 != "")
            {
                listBox1.Items.Add(w2 + " -> " + w1);
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            // Add all the lessons' words to the list box
            foreach (string lesson in Directory.GetFiles(Form1.projectPath + "\\basics\\skills"))
            {
                if (lesson.EndsWith("\\" + LessonName.ToLower() + ".yaml", StringComparison.CurrentCultureIgnoreCase))
                {
                    Deserializer deserializer = new Deserializer();
                    // Deserialize the lesson
                    string lessonYaml = File.ReadAllText(lesson);
                    lessonObj = deserializer.Deserialize<dynamic>(lessonYaml);

                    foreach (dynamic a in lessonObj["Mini-dictionary"][SourceLanguage])
                    {
                        listBox1.Items.Add(((KeyCollection)a.Keys).ToArray().ElementAt(0) + " -> " + ((ValueCollection)a.Values).ToArray().ElementAt(0));
                    }

                    foreach (dynamic a in lessonObj["Mini-dictionary"][TargetLanguage])
                    {
                        listBox2.Items.Add(((KeyCollection)a.Keys).ToArray().ElementAt(0) + " -> " + ((ValueCollection)a.Values).ToArray().ElementAt(0));
                    }

                    // Get all new words
                    // Example yaml:
                    /*New words:
                    - Word: yio
                    Translation: dog
                    Also accepted:
                    - hound
                    Images:
                    - dog1
                    - dog2
                    - dog3
                    */

                    foreach (dynamic word in lessonObj["New words"])
                    {
                        List<object> alsoAccp = new List<object>();
                        if (word.ContainsKey("Also accepted"))
                        {
                            foreach (dynamic a in word["Also accepted"])
                            {
                                alsoAccp.Add(a);
                            }
                        }
                        List<string> alsoAccepted = new List<string>();
                        foreach (object obj in alsoAccp)
                        {
                            alsoAccepted.Add(obj.ToString());
                        }
                        List<object> imags = new List<object>();
                        if (word.ContainsKey("Images"))
                        {
                            foreach (dynamic a in word["Images"])
                            {
                                imags.Add(a);
                            }
                        }
                        List<string> images = new List<string>();
                        foreach (object obj in imags)
                        {
                            images.Add(obj.ToString());
                        }
                        Lesson lessona = new Lesson(word["Word"].ToString(), 0, word["Translation"].ToString(), alsoAccepted, images);
                        lessons.Add(lessona);
                    }
                }
            }
            // Add all the lessons to the list box 3
            foreach (Lesson lesson in lessons)
            {
                listBox3.Items.Add(lesson.Name);
            }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            // Load lesson into property grid
            propertyGrid1.SelectedObject = lessons[listBox3.SelectedIndex];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Save yaml
            foreach (object obj in lessons)
            {
                Lesson lesson = (Lesson)obj;
                lessonObj["New words"] = new List<object>();
                foreach (Lesson lesson1 in lessons)
                {
                    Dictionary<object, object> word = new Dictionary<object, object>();
                    word.Add("Word", lesson1.Name);
                    word.Add("Translation", lesson1.AcceptedWords);
                    if (lesson1.OtherAcceptedWords.Count > 0)
                    {
                        word.Add("Also accepted", lesson1.OtherAcceptedWords);
                    }
                    if (lesson1.ListOfImageNames.Count > 0)
                    {
                        word.Add("Images", lesson1.ListOfImageNames);
                    }
                    lessonObj["New words"].Add(word);
                }
            }
            foreach (string lesson in Directory.GetFiles(Form1.projectPath + "\\basics\\skills"))
            {
                if (lesson.EndsWith("\\" + LessonName.ToLower() + ".yaml", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Serialize the lesson
                    Serializer serializer = new Serializer();
                    string lessonYaml = serializer.Serialize(lessonObj);
                    File.WriteAllText(lesson, lessonYaml);
                }
            }
        }
    }
}
