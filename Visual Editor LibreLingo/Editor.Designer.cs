namespace Visual_Editor_LibreLingo
{
    partial class Editor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            label1 = new Label();
            button1 = new Button();
            label2 = new Label();
            propertyGrid1 = new PropertyGrid();
            listBox3 = new ListBox();
            timer1 = new System.Windows.Forms.Timer(components);
            button2 = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 422);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(321, 124);
            listBox1.TabIndex = 0;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(440, 422);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(322, 124);
            listBox2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 21F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(3, 316);
            label1.Name = "label1";
            label1.Size = new Size(916, 38);
            label1.TabIndex = 2;
            label1.Text = "WORDS __________________________________________________________________";
            // 
            // button1
            // 
            button1.Location = new Point(14, 357);
            button1.Name = "button1";
            button1.Size = new Size(748, 59);
            button1.TabIndex = 3;
            button1.Text = "Add Manually";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 21F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(0, 14);
            label2.Name = "label2";
            label2.Size = new Size(871, 38);
            label2.TabIndex = 5;
            label2.Text = "LESSONS _____________________________________________________________";
            // 
            // propertyGrid1
            // 
            propertyGrid1.Location = new Point(474, 55);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(288, 244);
            propertyGrid1.TabIndex = 6;
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 15;
            listBox3.Location = new Point(3, 55);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(465, 244);
            listBox3.TabIndex = 7;
            listBox3.DoubleClick += listBox3_DoubleClick;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1500;
            timer1.Tick += timer1_Tick;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(431, 269);
            button2.Name = "button2";
            button2.Size = new Size(37, 30);
            button2.TabIndex = 8;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button2);
            Controls.Add(listBox3);
            Controls.Add(propertyGrid1);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Name = "Editor";
            Size = new Size(778, 587);
            Load += Editor_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private ListBox listBox2;
        private Label label1;
        private Button button1;
        private Label label2;
        private PropertyGrid propertyGrid1;
        private ListBox listBox3;
        private System.Windows.Forms.Timer timer1;
        private Button button2;
    }
}
