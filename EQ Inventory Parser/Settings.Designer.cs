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
            this.CB_MoveEmpty = new System.Windows.Forms.CheckBox();
            this.CB_HideEmpty = new System.Windows.Forms.CheckBox();
            this.CB_MoveBags = new System.Windows.Forms.CheckBox();
            this.CB_HideBags = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CB_MoveEmpty
            // 
            this.CB_MoveEmpty.AutoSize = true;
            this.CB_MoveEmpty.Location = new System.Drawing.Point(12, 12);
            this.CB_MoveEmpty.Name = "CB_MoveEmpty";
            this.CB_MoveEmpty.Size = new System.Drawing.Size(85, 17);
            this.CB_MoveEmpty.TabIndex = 0;
            this.CB_MoveEmpty.Text = "Move Empty";
            this.CB_MoveEmpty.UseVisualStyleBackColor = true;
            // 
            // CB_HideEmpty
            // 
            this.CB_HideEmpty.AutoSize = true;
            this.CB_HideEmpty.Location = new System.Drawing.Point(12, 36);
            this.CB_HideEmpty.Name = "CB_HideEmpty";
            this.CB_HideEmpty.Size = new System.Drawing.Size(80, 17);
            this.CB_HideEmpty.TabIndex = 1;
            this.CB_HideEmpty.Text = "Hide Empty";
            this.CB_HideEmpty.UseVisualStyleBackColor = true;
            // 
            // CB_MoveBags
            // 
            this.CB_MoveBags.AutoSize = true;
            this.CB_MoveBags.Location = new System.Drawing.Point(12, 60);
            this.CB_MoveBags.Name = "CB_MoveBags";
            this.CB_MoveBags.Size = new System.Drawing.Size(80, 17);
            this.CB_MoveBags.TabIndex = 2;
            this.CB_MoveBags.Text = "Move Bags";
            this.CB_MoveBags.UseVisualStyleBackColor = true;
            // 
            // CB_HideBags
            // 
            this.CB_HideBags.AutoSize = true;
            this.CB_HideBags.Location = new System.Drawing.Point(12, 84);
            this.CB_HideBags.Name = "CB_HideBags";
            this.CB_HideBags.Size = new System.Drawing.Size(72, 17);
            this.CB_HideBags.TabIndex = 3;
            this.CB_HideBags.Text = "HideBags";
            this.CB_HideBags.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.CB_HideBags);
            this.Controls.Add(this.CB_MoveBags);
            this.Controls.Add(this.CB_HideEmpty);
            this.Controls.Add(this.CB_MoveEmpty);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CB_MoveEmpty;
        private System.Windows.Forms.CheckBox CB_HideEmpty;
        private System.Windows.Forms.CheckBox CB_MoveBags;
        private System.Windows.Forms.CheckBox CB_HideBags;
    }
}