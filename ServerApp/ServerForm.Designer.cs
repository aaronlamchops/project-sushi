namespace ServerApp
{
    partial class ServerForm
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.SetAddressPortButton = new System.Windows.Forms.Button();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.ReceivingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 76);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(414, 228);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // SetAddressPortButton
            // 
            this.SetAddressPortButton.Location = new System.Drawing.Point(351, 8);
            this.SetAddressPortButton.Name = "SetAddressPortButton";
            this.SetAddressPortButton.Size = new System.Drawing.Size(75, 23);
            this.SetAddressPortButton.TabIndex = 1;
            this.SetAddressPortButton.Text = "Set";
            this.SetAddressPortButton.UseVisualStyleBackColor = true;
            this.SetAddressPortButton.Click += new System.EventHandler(this.SetAddressPortButton_Click);
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(13, 13);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(48, 13);
            this.AddressLabel.TabIndex = 2;
            this.AddressLabel.Text = "Address:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(131, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(239, 10);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(106, 20);
            this.textBox2.TabIndex = 4;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(204, 13);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 5;
            this.PortLabel.Text = "Port:";
            // 
            // ReceivingLabel
            // 
            this.ReceivingLabel.AutoSize = true;
            this.ReceivingLabel.Location = new System.Drawing.Point(12, 57);
            this.ReceivingLabel.Name = "ReceivingLabel";
            this.ReceivingLabel.Size = new System.Drawing.Size(58, 13);
            this.ReceivingLabel.TabIndex = 6;
            this.ReceivingLabel.Text = "Receiving:";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 316);
            this.Controls.Add(this.ReceivingLabel);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.SetAddressPortButton);
            this.Controls.Add(this.listView1);
            this.Name = "ServerForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button SetAddressPortButton;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label ReceivingLabel;
    }
}

