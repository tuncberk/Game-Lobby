namespace Kesin_Step_Server1._1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.listen = new System.Windows.Forms.Button();
            this.msgList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port No:";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(67, 20);
            this.portBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(104, 20);
            this.portBox.TabIndex = 1;
            this.portBox.Text = "81";
            // 
            // listen
            // 
            this.listen.Location = new System.Drawing.Point(197, 17);
            this.listen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listen.Name = "listen";
            this.listen.Size = new System.Drawing.Size(86, 23);
            this.listen.TabIndex = 2;
            this.listen.Text = "LISTEN";
            this.listen.UseVisualStyleBackColor = true;
            this.listen.Click += new System.EventHandler(this.listen_Click);
            // 
            // msgList
            // 
            this.msgList.FormattingEnabled = true;
            this.msgList.Location = new System.Drawing.Point(10, 58);
            this.msgList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.msgList.Name = "msgList";
            this.msgList.Size = new System.Drawing.Size(309, 212);
            this.msgList.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 288);
            this.Controls.Add(this.msgList);
            this.Controls.Add(this.listen);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Button listen;
        private System.Windows.Forms.ListBox msgList;
    }
}

