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
            this.AddressTextBox.Size = new System.Drawing.Size(284, 20);
            this.AddressTextBox.TabIndex = 1;
            this.AddressTextBox.Text = "127.0.0.1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(394, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "30";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(359, 9);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 3;
            this.PortLabel.Text = "Port:";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(500, 4);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 23);
            this.SendButton.TabIndex = 4;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ReceivingListView
            // 
            this.ReceivingListView.Location = new System.Drawing.Point(12, 100);
            this.ReceivingListView.Name = "ReceivingListView";
            this.ReceivingListView.Size = new System.Drawing.Size(563, 136);
            this.ReceivingListView.TabIndex = 5;
            this.ReceivingListView.UseCompatibleStateImageBehavior = false;
            // 
            // ReceivingLabel
            // 
            this.ReceivingLabel.AutoSize = true;
            this.ReceivingLabel.Location = new System.Drawing.Point(15, 81);
            this.ReceivingLabel.Name = "ReceivingLabel";
            this.ReceivingLabel.Size = new System.Drawing.Size(58, 13);
            this.ReceivingLabel.TabIndex = 6;
            this.ReceivingLabel.Text = "Receiving:";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 248);
            this.Controls.Add(this.ReceivingLabel);
            this.Controls.Add(this.ReceivingListView);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.AddressTextBox);
            this.Controls.Add(this.AddressLabel);
            this.Name = "ClientForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LobbyForm_FormClosed);
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

