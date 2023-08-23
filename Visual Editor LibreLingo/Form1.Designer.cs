namespace Visual_Editor_LibreLingo
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
            treeView1 = new TreeView();
            tabPage1 = new TabPage();
            button1 = new Button();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPage1.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Location = new Point(0, 1);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(165, 580);
            treeView1.TabIndex = 0;
            treeView1.DoubleClick += treeView1_DoubleClick;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(643, 552);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Home Page";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(19, 80);
            button1.Name = "button1";
            button1.Size = new Size(266, 68);
            button1.TabIndex = 1;
            button1.Text = "Open YAML Source Tree";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(13, 18);
            label1.Name = "label1";
            label1.Size = new Size(340, 28);
            label1.TabIndex = 0;
            label1.Text = "Novixx VELL (Visual Editor LibreLingo)";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(165, 1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(651, 580);
            tabControl1.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 581);
            Controls.Add(tabControl1);
            Controls.Add(treeView1);
            Name = "Form1";
            Text = "Form1";
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeView1;
        private TabPage tabPage1;
        private Label label1;
        private TabControl tabControl1;
        private Button button1;
    }
}