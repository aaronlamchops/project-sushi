namespace UserApp
{
    partial class WaitingRoom
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
            this.GameNameLabel = new System.Windows.Forms.Label();
            this.PlayersInRoomLabel = new System.Windows.Forms.Label();
            this.PlayerCountLabel = new System.Windows.Forms.Label();
            this.StartGameButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.MinPlayerLabel = new System.Windows.Forms.Label();
            this.MaxPlayerLabel = new System.Windows.Forms.Label();
            this.MinPlayerCountLabel = new System.Windows.Forms.Label();
            this.MaxPlayerCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GameNameLabel
            // 
            this.GameNameLabel.AutoSize = true;
            this.GameNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameNameLabel.Location = new System.Drawing.Point(62, 9);
            this.GameNameLabel.Name = "GameNameLabel";
            this.GameNameLabel.Size = new System.Drawing.Size(177, 29);
            this.GameNameLabel.TabIndex = 0;
            this.GameNameLabel.Text = "<Game Name>";
            // 
            // PlayersInRoomLabel
            // 
            this.PlayersInRoomLabel.AutoSize = true;
            this.PlayersInRoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayersInRoomLabel.Location = new System.Drawing.Point(12, 101);
            this.PlayersInRoomLabel.Name = "PlayersInRoomLabel";
            this.PlayersInRoomLabel.Size = new System.Drawing.Size(142, 20);
            this.PlayersInRoomLabel.TabIndex = 1;
            this.PlayersInRoomLabel.Text = "Players in Room: ";
            // 
            // PlayerCountLabel
            // 
            this.PlayerCountLabel.AutoSize = true;
            this.PlayerCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerCountLabel.Location = new System.Drawing.Point(160, 101);
            this.PlayerCountLabel.Name = "PlayerCountLabel";
            this.PlayerCountLabel.Size = new System.Drawing.Size(18, 20);
            this.PlayerCountLabel.TabIndex = 2;
            this.PlayerCountLabel.Text = "#";
            // 
            // StartGameButton
            // 
            this.StartGameButton.Location = new System.Drawing.Point(116, 54);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(75, 23);
            this.StartGameButton.TabIndex = 3;
            this.StartGameButton.Text = "Start";
            this.StartGameButton.UseVisualStyleBackColor = true;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(12, 204);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // MinPlayerLabel
            // 
            this.MinPlayerLabel.AutoSize = true;
            this.MinPlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinPlayerLabel.Location = new System.Drawing.Point(13, 137);
            this.MinPlayerLabel.Name = "MinPlayerLabel";
            this.MinPlayerLabel.Size = new System.Drawing.Size(89, 17);
            this.MinPlayerLabel.TabIndex = 5;
            this.MinPlayerLabel.Text = "Min Players: ";
            // 
            // MaxPlayerLabel
            // 
            this.MaxPlayerLabel.AutoSize = true;
            this.MaxPlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxPlayerLabel.Location = new System.Drawing.Point(13, 163);
            this.MaxPlayerLabel.Name = "MaxPlayerLabel";
            this.MaxPlayerLabel.Size = new System.Drawing.Size(92, 17);
            this.MaxPlayerLabel.TabIndex = 6;
            this.MaxPlayerLabel.Text = "Max Players: ";
            // 
            // MinPlayerCountLabel
            // 
            this.MinPlayerCountLabel.AutoSize = true;
            this.MinPlayerCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinPlayerCountLabel.Location = new System.Drawing.Point(113, 137);
            this.MinPlayerCountLabel.Name = "MinPlayerCountLabel";
            this.MinPlayerCountLabel.Size = new System.Drawing.Size(16, 17);
            this.MinPlayerCountLabel.TabIndex = 7;
            this.MinPlayerCountLabel.Text = "#";

            // 
            // MaxPlayerCountLabel
            // 
            this.MaxPlayerCountLabel.AutoSize = true;
            this.MaxPlayerCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxPlayerCountLabel.Location = new System.Drawing.Point(113, 163);
            this.MaxPlayerCountLabel.Name = "MaxPlayerCountLabel";
            this.MaxPlayerCountLabel.Size = new System.Drawing.Size(16, 17);
            this.MaxPlayerCountLabel.TabIndex = 8;
            this.MaxPlayerCountLabel.Text = "#";
            // 
            // WaitingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 239);
            this.Controls.Add(this.MaxPlayerCountLabel);
            this.Controls.Add(this.MinPlayerCountLabel);
            this.Controls.Add(this.MaxPlayerLabel);
            this.Controls.Add(this.MinPlayerLabel);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.StartGameButton);
            this.Controls.Add(this.PlayerCountLabel);
            this.Controls.Add(this.PlayersInRoomLabel);
            this.Controls.Add(this.GameNameLabel);
            this.Name = "WaitingRoom";
            this.Text = "WaitingRoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameNameLabel;
        private System.Windows.Forms.Label PlayersInRoomLabel;
        private System.Windows.Forms.Label PlayerCountLabel;
        private System.Windows.Forms.Button StartGameButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label MinPlayerLabel;
        private System.Windows.Forms.Label MaxPlayerLabel;
        private System.Windows.Forms.Label MinPlayerCountLabel;
        private System.Windows.Forms.Label MaxPlayerCountLabel;
    }
}