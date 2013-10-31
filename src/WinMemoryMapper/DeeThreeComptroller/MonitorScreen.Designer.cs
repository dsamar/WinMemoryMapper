namespace DeeThreeComptroller
{
    partial class MonitorScreen
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
            this.CharacterNameLabel = new System.Windows.Forms.Label();
            this.CharacterNameTextBox = new System.Windows.Forms.TextBox();
            this.ClassLabel = new System.Windows.Forms.Label();
            this.ClassTextBox = new System.Windows.Forms.TextBox();
            this.LevelAreaTextBox = new System.Windows.Forms.TextBox();
            this.LevelAreaLabel = new System.Windows.Forms.Label();
            this.RefreshAllButton = new System.Windows.Forms.Button();
            this.ACDCountTextBox = new System.Windows.Forms.TextBox();
            this.ACDCountLabel = new System.Windows.Forms.Label();
            this.ACDListBox = new System.Windows.Forms.ListBox();
            this.StartLogicButton = new System.Windows.Forms.Button();
            this.AbortLogicButton = new System.Windows.Forms.Button();
            this.TargetQuadTextBox = new System.Windows.Forms.TextBox();
            this.TargetQuadLabel = new System.Windows.Forms.Label();
            this.TargetAngleTextBox = new System.Windows.Forms.TextBox();
            this.TargetAngleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CharacterNameLabel
            // 
            this.CharacterNameLabel.AutoSize = true;
            this.CharacterNameLabel.Location = new System.Drawing.Point(13, 14);
            this.CharacterNameLabel.Name = "CharacterNameLabel";
            this.CharacterNameLabel.Size = new System.Drawing.Size(84, 13);
            this.CharacterNameLabel.TabIndex = 0;
            this.CharacterNameLabel.Text = "Character Name";
            // 
            // CharacterNameTextBox
            // 
            this.CharacterNameTextBox.Enabled = false;
            this.CharacterNameTextBox.Location = new System.Drawing.Point(116, 10);
            this.CharacterNameTextBox.Name = "CharacterNameTextBox";
            this.CharacterNameTextBox.Size = new System.Drawing.Size(158, 20);
            this.CharacterNameTextBox.TabIndex = 1;
            // 
            // ClassLabel
            // 
            this.ClassLabel.AutoSize = true;
            this.ClassLabel.Location = new System.Drawing.Point(13, 43);
            this.ClassLabel.Name = "ClassLabel";
            this.ClassLabel.Size = new System.Drawing.Size(32, 13);
            this.ClassLabel.TabIndex = 2;
            this.ClassLabel.Text = "Class";
            // 
            // ClassTextBox
            // 
            this.ClassTextBox.Enabled = false;
            this.ClassTextBox.Location = new System.Drawing.Point(116, 39);
            this.ClassTextBox.Name = "ClassTextBox";
            this.ClassTextBox.Size = new System.Drawing.Size(158, 20);
            this.ClassTextBox.TabIndex = 3;
            // 
            // LevelAreaTextBox
            // 
            this.LevelAreaTextBox.Enabled = false;
            this.LevelAreaTextBox.Location = new System.Drawing.Point(116, 68);
            this.LevelAreaTextBox.Name = "LevelAreaTextBox";
            this.LevelAreaTextBox.Size = new System.Drawing.Size(158, 20);
            this.LevelAreaTextBox.TabIndex = 5;
            // 
            // LevelAreaLabel
            // 
            this.LevelAreaLabel.AutoSize = true;
            this.LevelAreaLabel.Location = new System.Drawing.Point(13, 72);
            this.LevelAreaLabel.Name = "LevelAreaLabel";
            this.LevelAreaLabel.Size = new System.Drawing.Size(58, 13);
            this.LevelAreaLabel.TabIndex = 4;
            this.LevelAreaLabel.Text = "Level Area";
            // 
            // RefreshAllButton
            // 
            this.RefreshAllButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.RefreshAllButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RefreshAllButton.Location = new System.Drawing.Point(0, 566);
            this.RefreshAllButton.Name = "RefreshAllButton";
            this.RefreshAllButton.Size = new System.Drawing.Size(297, 30);
            this.RefreshAllButton.TabIndex = 6;
            this.RefreshAllButton.Text = "Refresh All";
            this.RefreshAllButton.UseVisualStyleBackColor = true;
            this.RefreshAllButton.Click += new System.EventHandler(this.RefreshAllButton_Click);
            // 
            // ACDCountTextBox
            // 
            this.ACDCountTextBox.Enabled = false;
            this.ACDCountTextBox.Location = new System.Drawing.Point(116, 97);
            this.ACDCountTextBox.Name = "ACDCountTextBox";
            this.ACDCountTextBox.Size = new System.Drawing.Size(158, 20);
            this.ACDCountTextBox.TabIndex = 8;
            // 
            // ACDCountLabel
            // 
            this.ACDCountLabel.AutoSize = true;
            this.ACDCountLabel.Location = new System.Drawing.Point(13, 101);
            this.ACDCountLabel.Name = "ACDCountLabel";
            this.ACDCountLabel.Size = new System.Drawing.Size(60, 13);
            this.ACDCountLabel.TabIndex = 7;
            this.ACDCountLabel.Text = "ACD Count";
            // 
            // ACDListBox
            // 
            this.ACDListBox.FormattingEnabled = true;
            this.ACDListBox.Location = new System.Drawing.Point(16, 136);
            this.ACDListBox.Name = "ACDListBox";
            this.ACDListBox.Size = new System.Drawing.Size(258, 238);
            this.ACDListBox.TabIndex = 9;
            // 
            // StartLogicButton
            // 
            this.StartLogicButton.Location = new System.Drawing.Point(0, 537);
            this.StartLogicButton.Name = "StartLogicButton";
            this.StartLogicButton.Size = new System.Drawing.Size(146, 23);
            this.StartLogicButton.TabIndex = 13;
            this.StartLogicButton.Text = "Start Logic";
            this.StartLogicButton.UseVisualStyleBackColor = true;
            this.StartLogicButton.Click += new System.EventHandler(this.StartLogicButton_Click);
            // 
            // AbortLogicButton
            // 
            this.AbortLogicButton.Location = new System.Drawing.Point(160, 537);
            this.AbortLogicButton.Name = "AbortLogicButton";
            this.AbortLogicButton.Size = new System.Drawing.Size(137, 23);
            this.AbortLogicButton.TabIndex = 14;
            this.AbortLogicButton.Text = "Abort Logic";
            this.AbortLogicButton.UseVisualStyleBackColor = true;
            this.AbortLogicButton.Click += new System.EventHandler(this.AbortLogicButton_Click);
            // 
            // TargetQuadTextBox
            // 
            this.TargetQuadTextBox.Enabled = false;
            this.TargetQuadTextBox.Location = new System.Drawing.Point(116, 380);
            this.TargetQuadTextBox.Name = "TargetQuadTextBox";
            this.TargetQuadTextBox.Size = new System.Drawing.Size(158, 20);
            this.TargetQuadTextBox.TabIndex = 16;
            // 
            // TargetQuadLabel
            // 
            this.TargetQuadLabel.AutoSize = true;
            this.TargetQuadLabel.Location = new System.Drawing.Point(13, 384);
            this.TargetQuadLabel.Name = "TargetQuadLabel";
            this.TargetQuadLabel.Size = new System.Drawing.Size(85, 13);
            this.TargetQuadLabel.TabIndex = 15;
            this.TargetQuadLabel.Text = "Target Quadrant";
            // 
            // TargetAngleTextBox
            // 
            this.TargetAngleTextBox.Enabled = false;
            this.TargetAngleTextBox.Location = new System.Drawing.Point(116, 406);
            this.TargetAngleTextBox.Name = "TargetAngleTextBox";
            this.TargetAngleTextBox.Size = new System.Drawing.Size(158, 20);
            this.TargetAngleTextBox.TabIndex = 18;
            // 
            // TargetAngleLabel
            // 
            this.TargetAngleLabel.AutoSize = true;
            this.TargetAngleLabel.Location = new System.Drawing.Point(13, 410);
            this.TargetAngleLabel.Name = "TargetAngleLabel";
            this.TargetAngleLabel.Size = new System.Drawing.Size(68, 13);
            this.TargetAngleLabel.TabIndex = 17;
            this.TargetAngleLabel.Text = "Target Angle";
            // 
            // MonitorScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 596);
            this.Controls.Add(this.TargetAngleTextBox);
            this.Controls.Add(this.TargetAngleLabel);
            this.Controls.Add(this.TargetQuadTextBox);
            this.Controls.Add(this.TargetQuadLabel);
            this.Controls.Add(this.AbortLogicButton);
            this.Controls.Add(this.StartLogicButton);
            this.Controls.Add(this.ACDListBox);
            this.Controls.Add(this.ACDCountTextBox);
            this.Controls.Add(this.ACDCountLabel);
            this.Controls.Add(this.RefreshAllButton);
            this.Controls.Add(this.LevelAreaTextBox);
            this.Controls.Add(this.LevelAreaLabel);
            this.Controls.Add(this.ClassTextBox);
            this.Controls.Add(this.ClassLabel);
            this.Controls.Add(this.CharacterNameTextBox);
            this.Controls.Add(this.CharacterNameLabel);
            this.Name = "MonitorScreen";
            this.Text = "Monitor Screen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CharacterNameLabel;
        private System.Windows.Forms.TextBox CharacterNameTextBox;
        private System.Windows.Forms.Label ClassLabel;
        private System.Windows.Forms.TextBox ClassTextBox;
        private System.Windows.Forms.TextBox LevelAreaTextBox;
        private System.Windows.Forms.Label LevelAreaLabel;
        private System.Windows.Forms.Button RefreshAllButton;
        private System.Windows.Forms.TextBox ACDCountTextBox;
        private System.Windows.Forms.Label ACDCountLabel;
        private System.Windows.Forms.ListBox ACDListBox;
        private System.Windows.Forms.Button StartLogicButton;
        private System.Windows.Forms.Button AbortLogicButton;
        private System.Windows.Forms.TextBox TargetQuadTextBox;
        private System.Windows.Forms.Label TargetQuadLabel;
        private System.Windows.Forms.TextBox TargetAngleTextBox;
        private System.Windows.Forms.Label TargetAngleLabel;
    }
}

