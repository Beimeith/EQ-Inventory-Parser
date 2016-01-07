namespace EQ_Inventory_Parser
{
    partial class MainScreen
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
            this.TC_Player = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Add_Character = new System.Windows.Forms.ToolStripMenuItem();
            this.L_File_Loaded = new System.Windows.Forms.Label();
            this.B_Details = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TC_Player
            // 
            this.TC_Player.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TC_Player.Location = new System.Drawing.Point(-4, 27);
            this.TC_Player.Name = "TC_Player";
            this.TC_Player.Padding = new System.Drawing.Point(0, 0);
            this.TC_Player.SelectedIndex = 0;
            this.TC_Player.Size = new System.Drawing.Size(692, 602);
            this.TC_Player.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(180)))), ((int)(((byte)(245)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Add_Character});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // TSMI_Add_Character
            // 
            this.TSMI_Add_Character.Name = "TSMI_Add_Character";
            this.TSMI_Add_Character.Size = new System.Drawing.Size(152, 22);
            this.TSMI_Add_Character.Text = "Add Character";
            this.TSMI_Add_Character.Click += new System.EventHandler(this.TSMI_Add_Character_Click);
            // 
            // L_File_Loaded
            // 
            this.L_File_Loaded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.L_File_Loaded.AutoSize = true;
            this.L_File_Loaded.Location = new System.Drawing.Point(12, 640);
            this.L_File_Loaded.Margin = new System.Windows.Forms.Padding(3);
            this.L_File_Loaded.Name = "L_File_Loaded";
            this.L_File_Loaded.Size = new System.Drawing.Size(79, 13);
            this.L_File_Loaded.TabIndex = 2;
            this.L_File_Loaded.Text = "No File Loaded";
            this.L_File_Loaded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // B_Details
            // 
            this.B_Details.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Details.Location = new System.Drawing.Point(597, 635);
            this.B_Details.Name = "B_Details";
            this.B_Details.Size = new System.Drawing.Size(75, 23);
            this.B_Details.TabIndex = 3;
            this.B_Details.Text = "Details";
            this.B_Details.UseVisualStyleBackColor = true;
            this.B_Details.Click += new System.EventHandler(this.B_Details_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(684, 662);
            this.Controls.Add(this.B_Details);
            this.Controls.Add(this.L_File_Loaded);
            this.Controls.Add(this.TC_Player);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EQ Inventory Parser";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl TC_Player;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label L_File_Loaded;
        private System.Windows.Forms.Button B_Details;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Add_Character;
    }
}

