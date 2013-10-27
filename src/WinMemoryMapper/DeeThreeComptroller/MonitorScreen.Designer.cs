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
            this.ACDGBTypeTextBox = new System.Windows.Forms.TextBox();
            this.ACDGBTypeLabel = new System.Windows.Forms.Label();
            this.MapPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).BeginInit();
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
            this.RefreshAllButton.Size = new System.Drawing.Size(947, 30);
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
            this.ACDListBox.Location = new System.Drawing.Point(16, 162);
            this.ACDListBox.Name = "ACDListBox";
            this.ACDListBox.Size = new System.Drawing.Size(258, 394);
            this.ACDListBox.TabIndex = 9;
            // 
            // ACDGBTypeTextBox
            // 
            this.ACDGBTypeTextBox.Location = new System.Drawing.Point(116, 124);
            this.ACDGBTypeTextBox.Name = "ACDGBTypeTextBox";
            this.ACDGBTypeTextBox.Size = new System.Drawing.Size(158, 20);
            this.ACDGBTypeTextBox.TabIndex = 10;
            this.ACDGBTypeTextBox.Text = "-1";
            // 
            // ACDGBTypeLabel
            // 
            this.ACDGBTypeLabel.AutoSize = true;
            this.ACDGBTypeLabel.Location = new System.Drawing.Point(13, 127);
            this.ACDGBTypeLabel.Name = "ACDGBTypeLabel";
            this.ACDGBTypeLabel.Size = new System.Drawing.Size(74, 13);
            this.ACDGBTypeLabel.TabIndex = 11;
            this.ACDGBTypeLabel.Text = "ACD GB Type";
            // 
            // MapPictureBox
            // 
            this.MapPictureBox.Location = new System.Drawing.Point(289, 10);
            this.MapPictureBox.Name = "MapPictureBox";
            this.MapPictureBox.Size = new System.Drawing.Size(646, 546);
            this.MapPictureBox.TabIndex = 12;
            this.MapPictureBox.TabStop = false;
            // 
            // MonitorScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 596);
            this.Controls.Add(this.MapPictureBox);
            this.Controls.Add(this.ACDGBTypeLabel);
            this.Controls.Add(this.ACDGBTypeTextBox);
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
            ((System.ComponentModel.ISupportInitialize)(this.MapPictureBox)).EndInit();
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
        private System.Windows.Forms.TextBox ACDGBTypeTextBox;
        private System.Windows.Forms.Label ACDGBTypeLabel;
        private System.Windows.Forms.PictureBox MapPictureBox;
    }
}

