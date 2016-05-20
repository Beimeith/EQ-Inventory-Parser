namespace EQ_Inventory_Parser
{
    partial class Settings
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
            this.CB_HideEmpty = new System.Windows.Forms.CheckBox();
            this.CB_HideBags = new System.Windows.Forms.CheckBox();
            this.CB_HideAllItems = new System.Windows.Forms.CheckBox();
            this.B_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CB_HideEmpty
            // 
            this.CB_HideEmpty.AutoSize = true;
            this.CB_HideEmpty.Location = new System.Drawing.Point(12, 12);
            this.CB_HideEmpty.Name = "CB_HideEmpty";
            this.CB_HideEmpty.Size = new System.Drawing.Size(80, 17);
            this.CB_HideEmpty.TabIndex = 1;
            this.CB_HideEmpty.Text = "Hide Empty";
            this.CB_HideEmpty.UseVisualStyleBackColor = true;
            // 
            // CB_HideBags
            // 
            this.CB_HideBags.AutoSize = true;
            this.CB_HideBags.Location = new System.Drawing.Point(12, 35);
            this.CB_HideBags.Name = "CB_HideBags";
            this.CB_HideBags.Size = new System.Drawing.Size(72, 17);
            this.CB_HideBags.TabIndex = 3;
            this.CB_HideBags.Text = "HideBags";
            this.CB_HideBags.UseVisualStyleBackColor = true;
            // 
            // CB_HideAllItems
            // 
            this.CB_HideAllItems.AutoSize = true;
            this.CB_HideAllItems.Location = new System.Drawing.Point(12, 58);
            this.CB_HideAllItems.Name = "CB_HideAllItems";
            this.CB_HideAllItems.Size = new System.Drawing.Size(112, 17);
            this.CB_HideAllItems.TabIndex = 4;
            this.CB_HideAllItems.Text = "Hide All Items Tab";
            this.CB_HideAllItems.UseVisualStyleBackColor = true;
            // 
            // B_Close
            // 
            this.B_Close.Location = new System.Drawing.Point(91, 214);
            this.B_Close.Name = "B_Close";
            this.B_Close.Size = new System.Drawing.Size(75, 23);
            this.B_Close.TabIndex = 5;
            this.B_Close.Text = "Close";
            this.B_Close.UseVisualStyleBackColor = true;
            this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.B_Close);
            this.Controls.Add(this.CB_HideAllItems);
            this.Controls.Add(this.CB_HideBags);
            this.Controls.Add(this.CB_HideEmpty);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox CB_HideEmpty;
        private System.Windows.Forms.CheckBox CB_HideBags;
        private System.Windows.Forms.CheckBox CB_HideAllItems;
        private System.Windows.Forms.Button B_Close;
    }
}