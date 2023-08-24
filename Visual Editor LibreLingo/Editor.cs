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
                listBox2.Items.Add(w1 + " -> " + w2);
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

                    if (!lessonObj.ContainsKey("Mini-dictionary"))
                    {
                        // Add the mini-dictionary to the lesson (incluing the source and target languages)
                        lessonObj.Add("Mini-dictionary", new Dictionary<object, object>());
                        lessonObj["Mini-dictionary"].Add(SourceLanguage, new List<object>());
                        lessonObj["Mini-dictionary"].Add(TargetLanguage, new List<object>());
                    }

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

                    if (!lessonObj.ContainsKey("New words"))
                    {
                        lessonObj.Add("New words", new List<object>());
                        lessonObj["New words"].Add(new Dictionary<object, object>()
                        {
{ "Word", "Test" }, { "Translation", "Tset" }, { "Also accepted", new List<object>() { "test2" } }, { "Images", new List<object>() { "dog1", "dog2", "dog3" } } });
                    }

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
                        Lesson lessona = new Lesson(word["Word"].ToString(), 1, word["Translation"].ToString(), alsoAccepted, images);
                        lessons.Add(lessona);
                    }
                    if (!lessonObj.ContainsKey("Phrases"))
                    {
                        lessonObj.Add("Phrases", new List<object>());
                        lessonObj["Phrases"].Add(new Dictionary<object, object>()
                        {
                              { "Phrase", "Test" }, { "Translation", "Tset" }, { "Alternative versions", new List<object>() { "test2" } }
                        });
                    }
                    foreach (dynamic sentence in lessonObj["Phrases"])
                    {
                        List<object> altTrans = new List<object>();
                        if (sentence.ContainsKey("Alternative versions"))
                        {
                            foreach (dynamic a in sentence["Alternative versions"])
                            {
                                altTrans.Add(a);
                            }
                        }
                        List<string> alternateTranslation = new List<string>();
                        foreach (object obj in altTrans)
                        {
                            alternateTranslation.Add(obj.ToString());
                        }
                        Lesson lessona = new Lesson(sentence["Phrase"].ToString(), 2, sentence["Translation"].ToString(), alternateTranslation, alternateTranslation);
                        lessona.Phrase = sentence["Phrase"].ToString();
                        lessona.Translation = sentence["Translation"].ToString();
                        lessona.AlternateTranslation = alternateTranslation;
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
            if (listBox3.SelectedIndex == -1)
            {
                return;
            }
            propertyGrid1.SelectedObject = lessons[listBox3.SelectedIndex];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Save yaml
            foreach (object obj in lessons)
            {
                Lesson lesson = (Lesson)obj;
                lessonObj["New words"] = new List<object>();
                lessonObj["Phrases"] = new List<object>();
                foreach (Lesson lesson1 in lessons)
                {
                    if (lesson1.Type == 1)
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
                        else if (lesson1.ListOfImageNames.Count == 0)
                        {
                            // Add default images
                            word.Add("Images", new List<string>()
                            {
                                "and1",
                                "and2",
                                "and3"
                            });
                        }
                        lessonObj["New words"].Add(word);
                    }
                    else if (lesson1.Type == 2)
                    {
                        Dictionary<object, object> sentence = new Dictionary<object, object>();
                        sentence.Add("Phrase", lesson1.Phrase);
                        sentence.Add("Translation", lesson1.Translation);
                        if (lesson1.AlternateTranslation.Count > 0)
                        {
                            sentence.Add("Alternative versions", lesson1.AlternateTranslation);
                        }
                        lessonObj["Phrases"].Add(sentence);
                    }
                }
            }
            if (lessonObj == null)
            {
                return;
            }
            // Update words
            if (lessonObj.ContainsKey("Mini-dictionary"))
            {
                lessonObj["Mini-dictionary"] = new Dictionary<object, object>();
                lessonObj["Mini-dictionary"].Add(SourceLanguage, new List<object>());
                lessonObj["Mini-dictionary"].Add(TargetLanguage, new List<object>());
            }
            else
            {
                lessonObj.Add("Mini-dictionary", new Dictionary<object, object>());
                lessonObj["Mini-dictionary"].Add(SourceLanguage, new List<object>());
                lessonObj["Mini-dictionary"].Add(TargetLanguage, new List<object>());
            }
            foreach (string item in listBox1.Items)
            {
                Dictionary<object, object> word = new Dictionary<object, object>();
                word.Add(item.Split(new string[] { " -> " }, StringSplitOptions.None)[0], item.Split(new string[] { " -> " }, StringSplitOptions.None)[1]);
                lessonObj["Mini-dictionary"][SourceLanguage].Add(word);
            }
            lessonObj["Mini-dictionary"][TargetLanguage] = new List<object>();
            foreach (string item in listBox2.Items)
            {
                Dictionary<object, object> word = new Dictionary<object, object>();
                word.Add(item.Split(new string[] { " -> " }, StringSplitOptions.None)[0], item.Split(new string[] { " -> " }, StringSplitOptions.None)[1]);
                lessonObj["Mini-dictionary"][TargetLanguage].Add(word);
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Add new lesson
            string name = Interaction.InputBox("Name of the lesson:", "Add lesson", "", -1, -1);
            string type = Interaction.InputBox("Type of the lesson (1 = words, 2 = sentences):", "Add lesson", "", -1, -1);
            while (type != "1" && type != "2")
            {
                type = Interaction.InputBox("Type of the lesson (1 = words, 2 = sentences):", "Add lesson", "", -1, -1);
            }
            if (name != "")
            {
                Lesson lesson = new Lesson(name, Convert.ToInt32(type), "", new List<string>(), new List<string>());
                if (type == "1")
                {
                    lesson.OtherAcceptedWords = new List<string>();
                    lesson.ListOfImageNames = new List<string>();
                }
                else if (type == "2")
                {
                    lesson.AlternateTranslation = new List<string>();
                }
                lessons.Add(lesson);
                listBox3.Items.Add(name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Remove lesson
            if (listBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a lesson to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lessons.RemoveAt(listBox3.SelectedIndex);
            listBox3.Items.RemoveAt(listBox3.SelectedIndex);
        }
    }
}
