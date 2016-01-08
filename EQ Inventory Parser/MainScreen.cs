using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EQ_Inventory_Parser
{
    public partial class MainScreen : Form
    {
        #region Variables

        string ProgramVersion = "1.0.0.0";
        string Title = "EQ Inventory Parser";

        public string Directory = "";
        string Line = "";

        Regex ParseInventory = new Regex("(?<1>.+)\t(?<2>.+)\t(?<3>.+)\t(?<4>.+)\t(?<5>.+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        Regex ParseRealEstate = new Regex("(?<1>.+)\t(?<2>.+)\t(?<3>.+)\t(?<4>.+)\t(?<5>.+)\t(?<6>.+)\t(?<7>.+)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        Match MyMatch;

        public TabPage SelectedCharacter;
        TabPage SelectedType;

        #endregion

        public MainScreen()
        {
            InitializeComponent();

            //Set Program Title
            Text = Title + " v" + ProgramVersion;

            //Add event handler for changing between characters.
            TC_Player.Selecting += new TabControlCancelEventHandler(TC_Player_Selecting);
        }

        #region Add Pages and Controls

        public void AddCharacterPage(string CharacterName, string CharacterServer, string CharacterGuild, string EQDirectory)
        {
            //Create a string with the new TabPage name based on the character name and server.
            string CharacterPageName = "TP_" + CharacterName + "_" + CharacterServer;

            //Create the new TabPage with the PageName we set.
            TabPage NewCharacterPage = new TabPage(CharacterPageName);

            //Set the properties for the new TabPage.
            NewCharacterPage.Text = CharacterName;
            NewCharacterPage.BackColor = System.Drawing.Color.Silver;

            //Add the new TabPage to the main TabControl and set it as the currently selected tab and SelectedCharacter.
            TC_Player.TabPages.Add(NewCharacterPage);
            TC_Player.SelectedTab = NewCharacterPage;
            SelectedCharacter = TC_Player.SelectedTab;

            //Add in the Secondary TabControl for the new Character.
            AddSecondaryTabControl(CharacterName, CharacterServer, CharacterGuild, SelectedCharacter);
        }

        private void AddSecondaryTabControl(string CharacterName, string CharacterServer, string CharacterGuild, TabPage SelectedCharacter)
        {
            //Create a string with the new TabControl name based on the character name and server.
            string TabControlName = "TC_" + CharacterName + "_" + CharacterServer;

            //Create the new TabControl.
            TabControl NewTabControl = new TabControl();

            //Set the properties for the NewTabControl.            
            NewTabControl.Name = TabControlName;
            NewTabControl.BackColor = System.Drawing.Color.Silver;
            NewTabControl.Location = new System.Drawing.Point(-4, 6);
            NewTabControl.Size = new System.Drawing.Size(692, 590);
            NewTabControl.Dock = DockStyle.Fill;

            //Add to the Selected TabPage of the Main TabControl.
            SelectedCharacter.Controls.Add(NewTabControl);

            //Create the new Type Pages for the Secondary TabControl.
            AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "Gear");
            AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "Inventory");
            AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "Bank");
            AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "SharedBank");
            AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "RealEstate");
            AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "GuildBank");
            AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "GuildRealEstate");

            //Set the first Type Page as the currently selected tab and SelectedType.
            NewTabControl.SelectedTab = NewTabControl.TabPages[0];
            TC_Type_Selecting(null, null);
        }

        private void AddTypePage(string CharacterName, string CharacterServer, string CharacterGuild, TabControl control, string type)
        {
            string PageName = "TP_" + CharacterName + "_" + CharacterServer + "_" + type;
            TabPage NewPage = new TabPage(PageName);

            if (type == "GuildBank")
                NewPage.Text = CharacterGuild + " Guild Bank";
            else if (type == "GuildRealEstate")
                NewPage.Text = CharacterGuild + " Real Estate";
            else
                NewPage.Text = type;

            NewPage.BackColor = System.Drawing.Color.Silver;

            control.TabPages.Add(NewPage);

            //Set the properties for the NewListView
            ListView NewListView = new ListView();
            NewListView.Name = "LV_" + CharacterName + "_" + CharacterServer + "_" + type;
            NewListView.Location = new System.Drawing.Point(0, 6);
            NewListView.Size = new System.Drawing.Size(672, 553);
            NewListView.View = View.Details;
            NewListView.Scrollable = true;
            NewListView.FullRowSelect = true;
            NewListView.GridLines = true;
            NewListView.Dock = DockStyle.Fill;

            //Add columns to the NewListView based on what type of page it is.
            switch (type)
            {
                case "Gear":
                case "Inventory":
                case "General":
                case "Bank":
                case "SharedBank":
                    {
                        NewListView.Columns.Add("ID");
                        NewListView.Columns.Add("Name");
                        NewListView.Columns.Add("Count");
                        NewListView.Columns.Add("Slots");
                        NewListView.Columns.Add("Location");
                        break;
                    }
                case "GuildBank":
                    {
                        NewListView.Columns.Add("ID");
                        NewListView.Columns.Add("Name");
                        NewListView.Columns.Add("Count");
                        NewListView.Columns.Add("Permission");
                        NewListView.Columns.Add("Location");

                        break;
                    }
                case "RealEstate":
                case "GuildRealEstate":
                    {
                        NewListView.Columns.Add("ID");
                        NewListView.Columns.Add("ItemName");
                        NewListView.Columns.Add("Count");
                        NewListView.Columns.Add("Status");
                        NewListView.Columns.Add("ItemOwner");
                        NewListView.Columns.Add("RealEstateLocation");
                        NewListView.Columns.Add("RealEstateName");
                        break;
                    }
            }

            control.Selecting += new TabControlCancelEventHandler(TC_Type_Selecting);

            NewPage.Controls.Add(NewListView);

        }

        #endregion

        void TC_Player_Selecting(object sender, TabControlCancelEventArgs e)
        {
            SelectedCharacter = (sender as TabControl).SelectedTab;
        }

        void TC_Type_Selecting(object sender, TabControlCancelEventArgs e)
        {

            SelectedType = SelectedCharacter.Controls
                .OfType<TabControl>()
                .First()
                .SelectedTab;

            switch (SelectedType.Text)
            {
                case "Inventory":
                case "Bank":
                case "SharedBank":
                case "Gear":
                    {
                        L_File_Loaded.Text = Directory + "\\" + SelectedCharacter.Text + "-Inventory.txt";
                        break;
                    }
                case "GuildBank":
                    {
                        L_File_Loaded.Text = Directory + "\\" + SelectedCharacter.Text + "-GuildBank.txt";
                        break;
                    }
                case "RealEstate":
                    {
                        L_File_Loaded.Text = Directory + "\\" + SelectedCharacter.Text + "-RealEstate.txt";
                        break;
                    }
                case "GuildRealEstate":
                    {
                        //L_File_Loaded.Text = EQDirectory + "\\" + SelectedCharacter. + "-RealEstate.txt";
                        break;
                    }
            }

            
        }

        private void B_Details_Click(object sender, EventArgs e)
        {
            String URL = "";
            
            //Check to ensure an item is selected on the Listview of the active Tabpage
            if (SelectedType.Controls.OfType<ListView>().First().SelectedItems.Count > 0)
            {
                //Check to ensure the selected item is not an empty slot, if it is throw a notification the user is dumb
                if (SelectedType.Controls.OfType<ListView>().First().SelectedItems[0].Text == "000000")
                    MessageBox.Show("There is no item in this slot to show details for...", "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                //If not, put the ItemID into a string and open the url for that item id on EQitems with the default browser.
                else
                {
                    String ItemID = SelectedType.Controls
                        .OfType<ListView>()
                        .First()
                        .SelectedItems[0].Text;

                    String ItemName = SelectedType.Controls
                        .OfType<ListView>()
                        .First()
                        .SelectedItems[0].SubItems[1].Text;

                    if (DD_Item_Search.SelectedItem.ToString() == "EQ Items")
                        URL = "http://items.sodeq.org/item.php?id=" + ItemID;
                    else if (DD_Item_Search.SelectedItem.ToString() == "EQ Resource")
                        URL = "http://items.eqresource.com/items.php?id=" + ItemID;
                    else if (DD_Item_Search.SelectedItem.ToString() == "Lucy")
                        URL = "http://lucy.allakhazam.com/item.html?id=" + ItemID;
                    else if (DD_Item_Search.SelectedItem.ToString() == "Allakhazam")
                        URL = "http://everquest.allakhazam.com/search.html?q=" + ItemName;

                    System.Diagnostics.Process.Start(URL);
                }
            }

            
        }

        #region Parsing

        public void ParseFile(string CharacterName, string GuildName, string Type, string EQDirectory)
        {
            string FileName = "";

            if (!String.IsNullOrEmpty(GuildName) && Type == "GuildRealEstate")
                FileName = EQDirectory + "\\" + GuildName + "-" + Type.Substring(5, 10)  + ".txt";
            else
                FileName = EQDirectory + "\\" + CharacterName + "-" + Type + ".txt";

            if (!File.Exists(FileName))
            {
                MessageBox.Show("File Not Found: " + Type);
                return;
            }

            switch (Type)
            {
                case "Inventory":
                    {
                        ParseInventoryFile(FileName);
                        break;
                    }
                case "RealEstate":
                    {
                        ParseRealEstateFile(FileName);
                        break;
                    }
                case "GuildBank":
                    {
                        ParseGuildBankFile(FileName);
                        break;
                    }
                case "GuildRealEstate":
                    {
                        ParseGuildRealEstateFile(FileName);
                        break;
                    }
            }
        }

        private void ParseInventoryFile(string FileName)
        {
            StreamReader sr = new StreamReader(FileName, Encoding.UTF7);

            Line = sr.ReadLine();
            Line = sr.ReadLine();

            //Create a list of which item slots will go into the Gear Tab. 
            List<string> Gear = new List<string> { "Charm", "Ear", "Head", "Face", "Neck", "Shoulders", "Arms", "Back", "Wrist", "Range", "Hands",
            "Primary", "Fingers", "Chest", "Legs", "Feet", "Waist", "Power Source", "Secondary", "Ammo", "Held"};

            //Create an Empty Listview Variable, must be done outside the while statement.
            var lv = (ListView)null;
            bool AddLine = true;
            //While there is a line to parse,
            while (Line != null)
            {
                //Check the Regex for the line,
                MyMatch = ParseInventory.Match(Line);
                //Check the line against the list of Gear item slots
                int count = Gear.Count(w => MyMatch.Groups[1].Value.Contains(w));

                //If the line contains one of the items in the list, set the listview variable to the Gear listview.
                if (Gear.Any(MyMatch.Groups[1].Value.Contains))
                    lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[0].Controls.OfType<ListView>().First();
                else if (MyMatch.Groups[1].Value.Contains("General"))
                    lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[1].Controls.OfType<ListView>().First();
                else if (MyMatch.Groups[1].Value.Contains("Bank") && !MyMatch.Groups[1].Value.Contains("Shared"))
                    lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[2].Controls.OfType<ListView>().First();
                else if (MyMatch.Groups[1].Value.Contains("Shared"))
                    lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[3].Controls.OfType<ListView>().First();

                

                ListViewItem Item = new ListViewItem();
                Item.Text = MyMatch.Groups[3].Value.PadLeft(6, '0');
                Item.SubItems.Add(MyMatch.Groups[2].Value);
                Item.SubItems.Add(MyMatch.Groups[4].Value);
                Item.SubItems.Add(MyMatch.Groups[5].Value);
                Item.SubItems.Add(MyMatch.Groups[1].Value);

                Line = sr.ReadLine();

                //If the option to hide empty slots is enabled, check if the ItemID is 000000, if it is, set the AddLine variable to false. 
                if (Options.HideEmpty)
                    if (Item.Text == "000000")
                        AddLine = false;

                if (AddLine)
                    lv.Items.Add(Item);

                //Reset AddLine variable 
                AddLine = true;
            }
        }

        private void ParseRealEstateFile(string FileName)
        {
            StreamReader sr = new StreamReader(FileName, Encoding.UTF7);

            Line = sr.ReadLine();
            Line = sr.ReadLine();

            while (Line != null)
            {
                MyMatch = ParseRealEstate.Match(Line);

                var lv = (ListView)null;
                bool AddLine = true;
                lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[4].Controls.OfType<ListView>().First();


                ListViewItem Item = new ListViewItem();
                Item.Text = MyMatch.Groups[6].Value.PadLeft(6, '0');
                Item.SubItems.Add(MyMatch.Groups[3].Value);
                Item.SubItems.Add(MyMatch.Groups[7].Value);
                Item.SubItems.Add(MyMatch.Groups[5].Value);
                Item.SubItems.Add(MyMatch.Groups[4].Value);
                Item.SubItems.Add(MyMatch.Groups[1].Value);
                Item.SubItems.Add(MyMatch.Groups[2].Value);

                Line = sr.ReadLine();

                //If the option to hide empty slots is enabled, check if the ItemID is 000000, if it is, set the AddLine variable to false. 
                if (Options.HideEmpty)
                    if (Item.Text == "000000")
                        AddLine = false;

                if (AddLine)
                    lv.Items.Add(Item);

                //Reset AddLine variable 
                AddLine = true;
            }
        }

        private void ParseGuildBankFile(string FileName)
        {
            StreamReader sr = new StreamReader(FileName, Encoding.UTF7);

            Line = sr.ReadLine();
            Line = sr.ReadLine();

            while (Line != null)
            {
                MyMatch = ParseInventory.Match(Line);

                var lv = (ListView)null;
                bool AddLine = true;
                lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[5].Controls.OfType<ListView>().First();


                ListViewItem Item = new ListViewItem();
                Item.Text = MyMatch.Groups[3].Value.PadLeft(6, '0');
                Item.SubItems.Add(MyMatch.Groups[2].Value);
                Item.SubItems.Add(MyMatch.Groups[4].Value);
                Item.SubItems.Add(MyMatch.Groups[5].Value);
                Item.SubItems.Add(MyMatch.Groups[1].Value);

                Line = sr.ReadLine();

                //If the option to hide empty slots is enabled, check if the ItemID is 000000, if it is, set the AddLine variable to false. 
                if (Options.HideEmpty)
                    if (Item.Text == "000000")
                        AddLine = false;

                if (AddLine)
                    lv.Items.Add(Item);

                //Reset AddLine variable 
                AddLine = true;
            }
        }

        private void ParseGuildRealEstateFile(string FileName)
        {
            StreamReader sr = new StreamReader(FileName, Encoding.UTF7);

            Line = sr.ReadLine();
            Line = sr.ReadLine();

            while (Line != null)
            {
                MyMatch = ParseRealEstate.Match(Line);

                var lv = (ListView)null;
                bool AddLine = true;
                lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[6].Controls.OfType<ListView>().First();


                ListViewItem Item = new ListViewItem();
                Item.Text = MyMatch.Groups[6].Value.PadLeft(6, '0');
                Item.SubItems.Add(MyMatch.Groups[3].Value);
                Item.SubItems.Add(MyMatch.Groups[7].Value);
                Item.SubItems.Add(MyMatch.Groups[5].Value);
                Item.SubItems.Add(MyMatch.Groups[4].Value);
                Item.SubItems.Add(MyMatch.Groups[1].Value);
                Item.SubItems.Add(MyMatch.Groups[2].Value);

                Line = sr.ReadLine();

                //If the option to hide empty slots is enabled, check if the ItemID is 000000, if it is, set the AddLine variable to false. 
                if (Options.HideEmpty)
                    if (Item.Text == "000000")
                        AddLine = false;

                if (AddLine)
                    lv.Items.Add(Item);

                //Reset AddLine variable 
                AddLine = true;
            }
        }

        #endregion

        private void TSMI_Add_Character_Click(object sender, EventArgs e)
        {
            AddCharacter AddCharacter = new AddCharacter();

            AddCharacter.ShowDialog(this);
        }

        private void B_Test_Click(object sender, EventArgs e)
        {
            Directory = "C:\\EverQuest";

            AddCharacterPage("Beimeith", "Xegony", "Machin Shin", Directory);
            ParseFile("Beimeith", null, "Inventory", Directory);
            ParseFile("Beimeith", null, "RealEstate", Directory);
            ParseFile("Beimeith", null, "GuildBank", Directory);
            ParseFile("Beimeith", "Machin Shin", "GuildRealEstate", Directory);

            //   MessageBox.Show("");

            //AddCharacterPage("Zephrina", "Xegony", "Machin Shin", Directory);
            //ParseFile("Zephrina", null, "Inventory", Directory);
            //ParseFile("Zephrina", null, "RealEstate", Directory);
            //ParseFile("Zephrina", null, "GuildBank", Directory);
            //ParseFile("Zephrina", "Machin Shin", "GuildRealEstate", Directory);

            foreach (TabPage tp in SelectedCharacter.Controls.OfType<TabControl>().First().TabPages)
                tp.Controls.OfType<ListView>().First().AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings Settings = new Settings();

            Settings.ShowDialog(this);
        }
    }
}
