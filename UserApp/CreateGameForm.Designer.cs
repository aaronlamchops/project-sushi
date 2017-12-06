namespace UserApp
{
    partial class CreateGameForm
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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.TotalPlayerComboBox = new System.Windows.Forms.ComboBox();
            this.TotalPlayersLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.MinPlayerComboBox = new System.Windows.Forms.ComboBox();
            this.MinPlayerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(84, 12);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(206, 20);
            this.NameTextBox.TabIndex = 0;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 15);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(66, 13);
            this.NameLabel.TabIndex = 1;
            this.NameLabel.Text = "Enter Name:";
            // 
            // TotalPlayerComboBox
            // 
            this.TotalPlayerComboBox.FormattingEnabled = true;
            this.TotalPlayerComboBox.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.TotalPlayerComboBox.Location = new System.Drawing.Point(144, 70);
            this.TotalPlayerComboBox.Name = "TotalPlayerComboBox";
            this.TotalPlayerComboBox.Size = new System.Drawing.Size(146, 21);
            this.TotalPlayerComboBox.TabIndex = 2;
            this.TotalPlayerComboBox.SelectedIndexChanged += new System.EventHandler(this.TotalPlayerComboBox_SelectedIndexChanged);
            // 
            // TotalPlayersLabel
            // 
            this.TotalPlayersLabel.AutoSize = true;
            this.TotalPlayersLabel.Location = new System.Drawing.Point(12, 73);
            this.TotalPlayersLabel.Name = "TotalPlayersLabel";
            this.TotalPlayersLabel.Size = new System.Drawing.Size(126, 13);
            this.TotalPlayersLabel.TabIndex = 3;
            this.TotalPlayersLabel.Text = "Select Max Player Count:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(12, 112);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(215, 112);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 5;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // MinPlayerComboBox
            // 
            this.MinPlayerComboBox.FormattingEnabled = true;
            this.MinPlayerComboBox.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.MinPlayerComboBox.Location = new System.Drawing.Point(144, 42);
            this.MinPlayerComboBox.Name = "MinPlayerComboBox";
            this.MinPlayerComboBox.Size = new System.Drawing.Size(146, 21);
            this.MinPlayerComboBox.TabIndex = 6;
            this.MinPlayerComboBox.SelectedIndexChanged += new System.EventHandler(this.MinPlayerComboBox_SelectedIndexChanged);
            // 
            // MinPlayerLabel
            // 
            this.MinPlayerLabel.AutoSize = true;
            this.MinPlayerLabel.Location = new System.Drawing.Point(12, 45);
            this.MinPlayerLabel.Name = "MinPlayerLabel";
            this.MinPlayerLabel.Size = new System.Drawing.Size(123, 13);
            this.MinPlayerLabel.TabIndex = 7;
            this.MinPlayerLabel.Text = "Select Min Player Count:";
            // 
            // CreateGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 147);
            this.Controls.Add(this.MinPlayerLabel);
            this.Controls.Add(this.MinPlayerComboBox);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.TotalPlayersLabel);
            this.Controls.Add(this.TotalPlayerComboBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameTextBox);
            this.Name = "CreateGameForm";
            this.Text = "CreateGameForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.ComboBox TotalPlayerComboBox;
        private System.Windows.Forms.Label TotalPlayersLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.ComboBox MinPlayerComboBox;
        private System.Windows.Forms.Label MinPlayerLabel;
    }
}