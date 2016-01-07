using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EQ_Inventory_Parser
{
    public partial class AddCharacter : Form
    {
     //   private MainScreen MainScreen;
        public AddCharacter()
        {
            InitializeComponent();
        }

        private void B_AddCharacter_OK_Click(object sender, EventArgs e)
        {
            MainScreen MainScreen = (MainScreen)this.Owner;

            string FileName = "";
            string CharacterName = TB_AddCharacter_Character_Name.Text;
            string CharacterServer = TB_AddCharacter_Server_Name.Text;
            string CharacterGuild = TB_AddCharacter_Guild_Name.Text;
            string EQDirectory = TB_AddCharacter_EQ_Directory.Text;

            if (CharacterName == "")
            {
                MessageBox.Show("You must enter a Character Name...");
                TB_AddCharacter_Character_Name.Focus();
                return;
            }
            else
            {
                FileName = EQDirectory + "\\" + CharacterName + "-Inventory.txt";
                if (!File.Exists(FileName))
                {
                    MessageBox.Show("Character Inventory File Not Found.");
                    return;
                }
            }

            if (CharacterServer == "")
            {
                MessageBox.Show("You must enter a Server Name...");
                TB_AddCharacter_Server_Name.Focus();
                return;
            }
            else if (EQDirectory == "")
            {
                MessageBox.Show("You must enter the location of your EQ installion...");
                TB_AddCharacter_EQ_Directory.Focus();
                return;
            }

            MainScreen.Directory = EQDirectory;
            MainScreen.AddCharacterPage(CharacterName, CharacterServer, CharacterGuild, EQDirectory);

            
                MainScreen.ParseFile(CharacterName, null, "Inventory", EQDirectory);

            FileName = EQDirectory + "\\" + CharacterName + "-RealEstate.txt";
            if (File.Exists(FileName))
                MainScreen.ParseFile(CharacterName, null, "RealEstate", EQDirectory);

            FileName = EQDirectory + "\\" + CharacterName + "-GuildBank.txt";
            if (File.Exists(FileName))
                MainScreen.ParseFile(CharacterName, null, "GuildBank", EQDirectory);

            FileName = EQDirectory + "\\" + CharacterGuild + "-RealEstate.txt";
            if (File.Exists(FileName))
                MainScreen.ParseFile(CharacterName, CharacterGuild, "GuildRealEstate", EQDirectory);

            foreach (TabPage tp in MainScreen.SelectedCharacter.Controls.OfType<TabControl>().First().TabPages)
                tp.Controls.OfType<ListView>().First().AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void B_AddCharacter_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
