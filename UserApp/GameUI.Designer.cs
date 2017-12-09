namespace UserApp
{
    partial class GameUI
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
            this.CardHandFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ChatListView = new System.Windows.Forms.ListView();
            this.ChatMessageTextBox = new System.Windows.Forms.TextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.PlayersFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // CardHandFlowLayoutPanel
            // 
            this.CardHandFlowLayoutPanel.Location = new System.Drawing.Point(12, 449);
            this.CardHandFlowLayoutPanel.Name = "CardHandFlowLayoutPanel";
            this.CardHandFlowLayoutPanel.Size = new System.Drawing.Size(710, 209);
            this.CardHandFlowLayoutPanel.TabIndex = 0;
            // 
            // ChatListView
            // 
            this.ChatListView.Location = new System.Drawing.Point(728, 12);
            this.ChatListView.Name = "ChatListView";
            this.ChatListView.Size = new System.Drawing.Size(227, 617);
            this.ChatListView.TabIndex = 1;
            this.ChatListView.UseCompatibleStateImageBehavior = false;
            // 
            // ChatMessageTextBox
            // 
            this.ChatMessageTextBox.Location = new System.Drawing.Point(728, 637);
            this.ChatMessageTextBox.Name = "ChatMessageTextBox";
            this.ChatMessageTextBox.Size = new System.Drawing.Size(166, 20);
            this.ChatMessageTextBox.TabIndex = 2;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(900, 635);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(55, 23);
            this.SendMessageButton.TabIndex = 3;
            this.SendMessageButton.Text = "Send";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // PlayersFlowLayoutPanel
            // 
            this.PlayersFlowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.PlayersFlowLayoutPanel.Name = "PlayersFlowLayoutPanel";
            this.PlayersFlowLayoutPanel.Size = new System.Drawing.Size(710, 431);
            this.PlayersFlowLayoutPanel.TabIndex = 0;
            // 
            // GameUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 670);
            this.Controls.Add(this.PlayersFlowLayoutPanel);
            this.Controls.Add(this.SendMessageButton);
            this.Controls.Add(this.ChatMessageTextBox);
            this.Controls.Add(this.ChatListView);
            this.Controls.Add(this.CardHandFlowLayoutPanel);
            this.Name = "GameUI";
            this.Text = "GameApp";
            this.Load += new System.EventHandler(this.GameUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel CardHandFlowLayoutPanel;
        private System.Windows.Forms.ListView ChatListView;
        private System.Windows.Forms.TextBox ChatMessageTextBox;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.FlowLayoutPanel PlayersFlowLayoutPanel;
    }
}