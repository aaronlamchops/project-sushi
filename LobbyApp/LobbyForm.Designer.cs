using System;

namespace LobbyApp
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

        private void ClientForm_FormClosed()
        {
            _SendingInvoker.Stop();
            _keepReceiving = false;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddressLabel = new System.Windows.Forms.Label();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.ReceivingListView = new System.Windows.Forms.ListView();
            this.ReceivingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(16, 11);
            this.AddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(68, 17);
            this.AddressLabel.TabIndex = 0;
            this.AddressLabel.Text = "Address: ";
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Location = new System.Drawing.Point(92, 7);
            this.AddressTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(377, 22);
            this.AddressTextBox.TabIndex = 1;
            this.AddressTextBox.Text = "127.0.0.1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(525, 7);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 22);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "30";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(479, 11);
            this.PortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(38, 17);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port:";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(667, 5);
            this.SendButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(100, 28);
            this.SendButton.TabIndex = 4;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ReceivingListView
            // 
            this.ReceivingListView.Location = new System.Drawing.Point(16, 123);
            this.ReceivingListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReceivingListView.Name = "ReceivingListView";
            this.ReceivingListView.Size = new System.Drawing.Size(749, 166);
            this.ReceivingListView.TabIndex = 5;
            this.ReceivingListView.UseCompatibleStateImageBehavior = false;
            // 
            // ReceivingLabel
            // 
            this.ReceivingLabel.AutoSize = true;
            this.ReceivingLabel.Location = new System.Drawing.Point(20, 100);
            this.ReceivingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ReceivingLabel.Name = "ReceivingLabel";
            this.ReceivingLabel.Size = new System.Drawing.Size(74, 17);
            this.ReceivingLabel.TabIndex = 6;
            this.ReceivingLabel.Text = "Receiving:";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 305);
            this.Controls.Add(this.ReceivingLabel);
            this.Controls.Add(this.ReceivingListView);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.AddressLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ClientForm";
            this.Text = "Form1";
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
        private System.Windows.Forms.Label ReceivingLabel;
    }
}

