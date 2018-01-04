namespace ByamlEditor
{
    partial class BymlEditor
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
            this.browserYml = new System.Windows.Forms.Button();
            this.textBoxBrowseByml = new System.Windows.Forms.TextBox();
            this.treeViewByml = new System.Windows.Forms.TreeView();
            this.buttonLoadByml = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // browserYml
            // 
            this.browserYml.Location = new System.Drawing.Point(13, 13);
            this.browserYml.Name = "browserYml";
            this.browserYml.Size = new System.Drawing.Size(75, 23);
            this.browserYml.TabIndex = 0;
            this.browserYml.Text = "Browse...";
            this.browserYml.UseVisualStyleBackColor = true;
            this.browserYml.Click += new System.EventHandler(this.BrowserYml_Click);
            // 
            // textBoxBrowseByml
            // 
            this.textBoxBrowseByml.Location = new System.Drawing.Point(103, 13);
            this.textBoxBrowseByml.Name = "textBoxBrowseByml";
            this.textBoxBrowseByml.Size = new System.Drawing.Size(307, 20);
            this.textBoxBrowseByml.TabIndex = 1;
            // 
            // treeViewByml
            // 
            this.treeViewByml.Location = new System.Drawing.Point(12, 69);
            this.treeViewByml.Name = "treeViewByml";
            this.treeViewByml.Size = new System.Drawing.Size(479, 362);
            this.treeViewByml.TabIndex = 2;
            this.treeViewByml.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewByml_NodeMouseClick);
            // 
            // buttonLoadByml
            // 
            this.buttonLoadByml.AllowDrop = true;
            this.buttonLoadByml.Location = new System.Drawing.Point(416, 12);
            this.buttonLoadByml.Name = "buttonLoadByml";
            this.buttonLoadByml.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadByml.TabIndex = 3;
            this.buttonLoadByml.Text = "Load";
            this.buttonLoadByml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonLoadByml.UseVisualStyleBackColor = true;
            this.buttonLoadByml.Click += new System.EventHandler(this.ButtonLoadByml_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(325, 40);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(307, 20);
            this.textBox1.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(416, 39);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 441);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonLoadByml);
            this.Controls.Add(this.treeViewByml);
            this.Controls.Add(this.textBoxBrowseByml);
            this.Controls.Add(this.browserYml);
            this.Name = "Form1";
            this.Text = "BymlEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browserYml;
        private System.Windows.Forms.TextBox textBoxBrowseByml;
        private System.Windows.Forms.TreeView treeViewByml;
        private System.Windows.Forms.Button buttonLoadByml;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonSave;
    }
}

