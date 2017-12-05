using System;

namespace UserApp
{
    partial class ClientForm
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

        //private void ClientForm_FormClosed()
        //{
        //    _SendingInvoker.Stop();
        //    _keepReceiving = false;
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.ReceivingListView = new System.Windows.Forms.ListView();
            this.Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GameId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AvailablePlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TotalPlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GameLabel = new System.Windows.Forms.Label();
            this.CreateGameButton = new System.Windows.Forms.Button();
            this.JoinButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(12, 9);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(51, 13);
            this.AddressLabel.TabIndex = 0;
            this.AddressLabel.Text = "Address: ";
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(69, 6);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(186, 20);
            this.AddressTextBox.TabIndex = 1;
            this.AddressTextBox.Text = "127.0.0.1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(296, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "5";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(261, 9);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port:";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(402, 4);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 4;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ReceivingListView
            // 
            this.ReceivingListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name,
            this.GameId,
            this.AvailablePlayers,
            this.TotalPlayers});
            this.ReceivingListView.Location = new System.Drawing.Point(12, 107);
            this.ReceivingListView.Name = "ReceivingListView";
            this.ReceivingListView.Size = new System.Drawing.Size(465, 342);
            this.ReceivingListView.TabIndex = 5;
            this.ReceivingListView.FullRowSelect = true;
            this.ReceivingListView.UseCompatibleStateImageBehavior = false;
            this.ReceivingListView.View = System.Windows.Forms.View.Details;
            this.ReceivingListView.SelectedIndexChanged += new System.EventHandler(this.ReceivingListView_SelectedIndexChanged);
            // 
            // Name
            // 
            this.Name.Tag = "";
            this.Name.Text = "Name";
            //
            // RefreshTimer
            //
            this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // Address
            // 
            this.GameId.Text = "Game ID";
            this.GameId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GameId.Width = 100;
            // 
            // AvailablePlayers
            // 
            this.AvailablePlayers.Text = "Available Players";
            this.AvailablePlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AvailablePlayers.Width = 150;
            // 
            // TotalPlayers
            // 
            this.TotalPlayers.Text = "Total Players";
            this.TotalPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TotalPlayers.Width = 151;
            // 
            // GameLabel
            // 
            this.GameLabel.AutoSize = true;
            this.GameLabel.Location = new System.Drawing.Point(15, 81);
            this.GameLabel.Name = "GameLabel";
            this.GameLabel.Size = new System.Drawing.Size(43, 13);
            this.GameLabel.TabIndex = 6;
            this.GameLabel.Text = "Games:";
            // 
            // CreateGameButton
            // 
            this.CreateGameButton.Location = new System.Drawing.Point(301, 78);
            this.CreateGameButton.Name = "CreateGameButton";
            this.CreateGameButton.Size = new System.Drawing.Size(95, 23);
            this.CreateGameButton.TabIndex = 7;
            this.CreateGameButton.Text = "Create Game";
            this.CreateGameButton.UseVisualStyleBackColor = true;
            this.CreateGameButton.Click += new System.EventHandler(this.CreateGameButton_Click);
            // 
            // JoinButton
            // 
            this.JoinButton.Location = new System.Drawing.Point(402, 78);
            this.JoinButton.Name = "JoinButton";
            this.JoinButton.Size = new System.Drawing.Size(75, 23);
            this.JoinButton.TabIndex = 8;
            this.JoinButton.Text = "Join";
            this.JoinButton.UseVisualStyleBackColor = true;
            this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(69, 76);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 9;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 461);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.JoinButton);
            this.Controls.Add(this.CreateGameButton);
            this.Controls.Add(this.GameLabel);
            this.Controls.Add(this.ReceivingListView);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.AddressLabel);
            //this.Name = "ClientForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientForm_FormClosed);
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox AddressTextBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.ListView ReceivingListView;
        private System.Windows.Forms.Label GameLabel;
        private System.Windows.Forms.Button CreateGameButton;
        private System.Windows.Forms.ColumnHeader Name;
        private System.Windows.Forms.ColumnHeader GameId;
        private System.Windows.Forms.ColumnHeader AvailablePlayers;
        private System.Windows.Forms.ColumnHeader TotalPlayers;
        private System.Windows.Forms.Button JoinButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Timer RefreshTimer;
    }
}

