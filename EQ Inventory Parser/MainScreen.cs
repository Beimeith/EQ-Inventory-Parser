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

        List<string> BagList = new List<string>();
        Dictionary<string, string> GuildList = new Dictionary<string, string>();

        public TabPage SelectedCharacter;
        TabPage SelectedType;

        string SelectedGuild;

        #endregion

        public MainScreen()
        {
            InitializeComponent();
            LoadBagList();
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

            //Add the player and the player's guild to the GuildList.
            if (!GuildList.ContainsKey(CharacterName))
                GuildList.Add(CharacterName, CharacterGuild);

            //Set the character's guild as the Selected Guild.
            SelectedGuild = CharacterGuild;

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

            if (!Options.HideAllItems)
                AddTypePage(CharacterName, CharacterServer, CharacterGuild, NewTabControl, "AllItems");

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
            NewListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.GenericColumnSorter);

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
                        NewListView.Columns[0].Tag = "Int";
                        NewListView.Columns.Add("Name");
                        NewListView.Columns.Add("Count");
                        NewListView.Columns[2].Tag = "Int";
                        NewListView.Columns.Add("Slots");
                        NewListView.Columns[3].Tag = "Int";
                        NewListView.Columns.Add("Location");
                        break;
                    }
                case "GuildBank":
                    {
                        NewListView.Columns.Add("ID");
                        NewListView.Columns[0].Tag = "Int";
                        NewListView.Columns.Add("Name");
                        NewListView.Columns.Add("Count");
                        NewListView.Columns[2].Tag = "Int";
                        NewListView.Columns.Add("Permission");
                        NewListView.Columns.Add("Location");
                        break;
                    }
                case "RealEstate":
                case "GuildRealEstate":
                    {
                        NewListView.Columns.Add("ID");
                        NewListView.Columns[0].Tag = "Int";
                        NewListView.Columns.Add("ItemName");
                        NewListView.Columns.Add("Count");
                        NewListView.Columns[2].Tag = "Int";
                        NewListView.Columns.Add("Status");
                        NewListView.Columns.Add("ItemOwner");
                        NewListView.Columns.Add("RealEstateLocation");
                        NewListView.Columns.Add("RealEstateName");
                        break;
                    }
                case "AllItems":
                    {
                        NewListView.Columns.Add("ID");
                        NewListView.Columns[0].Tag = "Int";
                        NewListView.Columns.Add("ItemName");
                        NewListView.Columns.Add("Count");
                        NewListView.Columns[2].Tag = "Int";
                        NewListView.Columns.Add("Location");
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

            if (GuildList.ContainsKey(SelectedCharacter.Text))
                SelectedGuild = GuildList[SelectedCharacter.Text];
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
                        L_File_Loaded.Text = Directory + "\\" + SelectedCharacter.Text + "-Inventory.txt";
                        break;
                case  "RealEstate":
                        L_File_Loaded.Text = Directory + "\\" + SelectedCharacter.Text + "-RealEstate.txt";
                        break;
                default:
                    {
                        if (SelectedType.Text == SelectedGuild + " Guild Bank")
                            L_File_Loaded.Text = Directory + "\\" + SelectedCharacter.Text + "-GuildBank.txt";
                        else if (SelectedType.Text == SelectedGuild + " Real Estate")
                            L_File_Loaded.Text = Directory + "\\" + SelectedGuild + "-RealEstate.txt";
                        break;
                    }
            }

            
        }

        private void GenericColumnSorter(object sender, ColumnClickEventArgs e)
        {
            var lv = sender as ListView;
            var sort = lv.ListViewItemSorter as FlexibleListViewItemSorter;

            // the tag property stores the default sorting mode: String, StringDesc, Int, IntDesc, Time, TimeDesc
            var mode = (lv.Columns[e.Column].Tag ?? "").ToString();

            // if the clicked column is already sorted, then reverse the sort
            if (sort != null && e.Column == sort.Column && mode == sort.Mode)
            {
                sort.Reverse = !sort.Reverse;
            }
            else
            {
                // a new column was clicked
                if (sort == null)
                    sort = new FlexibleListViewItemSorter();
                sort.Column = e.Column;
                sort.Mode = mode;
                sort.Reverse = false;
                lv.ListViewItemSorter = sort;
            }

            lv.Sort();
        }

        private void LoadBagList()
        {
           var lines = Properties.Resources.BagList.Split('\n');
            foreach (var line in lines)
            {
                // treat lines start with a * as comments
                if (line.StartsWith("*"))
                    continue;
                var values = line.Trim();
                if (values.Length < 2)
                    continue;
                BagList.Add(line.PadLeft(7,'0'));
            }
        }

        private void B_Details_Click(object sender, EventArgs e)
        {
            String URL = "";

            if (SelectedType != null)
            {//Check to ensure an item is selected on the Listview of the active Tabpage
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
                //If nothing has been selected, throw a notification the user is dumb
                else if (SelectedType.Controls.OfType<ListView>().First().SelectedItems.Count == 0)
                    MessageBox.Show("You need to select an item to show details for...", "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            //If no file has been loaded at all, throw a notification the user is dumb
            }
            else
                MessageBox.Show("You need to load a file first...", "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }

        #region Parsing

        public void CheckFile(string CharacterName, string GuildName, string Type, string EQDirectory)
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
                        ParseFile(FileName, "Inventory");
                        break;
                case "RealEstate":
                        ParseFile(FileName, "RealEstate");
                        break;
                case "GuildBank":
                        ParseFile(FileName, "GuildBank");
                        break;
                case "GuildRealEstate":
                        ParseFile(FileName, "GuildRealEstate");
                        break;
            }
        }

        private void ParseFile(string FileName, string type)
        {
            //Create the streamreader and read the first two lines because the first line is headings.
            StreamReader sr = new StreamReader(FileName, Encoding.UTF7);
            Line = sr.ReadLine();
            Line = sr.ReadLine();
            
            //Create a list of which item slots will go into the Gear Tab. 
            List<string> Gear = new List<string> { "Charm", "Ear", "Head", "Face", "Neck", "Shoulders", "Arms", "Back", "Wrist", "Range", "Hands",
            "Primary", "Fingers", "Chest", "Legs", "Feet", "Waist", "Power Source", "Secondary", "Ammo", "Held"};
            
            //Create an Empty Listview Variable, must be done outside the while statement as it can be changed.
            var lv = (ListView)null;

            //Create a second empty Listview Variable for the All Items, this one can be set as it doesn't change.
            var lv2 = (ListView)null;

            if (!Options.HideAllItems)
                lv2 = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[7].Controls.OfType<ListView>().First();

            //Create a boolean that controls whether the item in the current Line will be added to the listview. Default to true.
            bool AddLine = true;

            //While there is a line to parse,
            while (Line != null)
            {
                //Create a new Listview Item
                ListViewItem Item = new ListViewItem();
                
                //Create a second Listview Item for the AllItems Listview.
                ListViewItem Item2 = new ListViewItem();

                switch (type)
                {
                    case "Inventory":
                        {
                            //Check the Regex for the line,
                            MyMatch = ParseInventory.Match(Line);

                            //Check the line against the list of Gear item slots
                            int count = Gear.Count(w => MyMatch.Groups[1].Value.Contains(w));

                            //If the line contains one of the items in the list, set the listview variable to the Gear listview,
                            if (Gear.Any(MyMatch.Groups[1].Value.Contains))
                                lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[0].Controls.OfType<ListView>().First();
                            //else if it contains the word General, set the listview variable to the General listview,
                            else if (MyMatch.Groups[1].Value.Contains("General"))
                                lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[1].Controls.OfType<ListView>().First();
                            //else if it contains the word Bank, but not the word Shared, set the listview variable to the Bank listview,
                            else if (MyMatch.Groups[1].Value.Contains("Bank") && !MyMatch.Groups[1].Value.Contains("Shared"))
                                lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[2].Controls.OfType<ListView>().First();
                            //else if it contains the word shared, set the listview bariable to the Shared Bank listview.
                            else if (MyMatch.Groups[1].Value.Contains("Shared"))
                                lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[3].Controls.OfType<ListView>().First();

                            //Build the Listview item
                            Item.Text = MyMatch.Groups[3].Value.PadLeft(6, '0');
                            Item.SubItems.Add(MyMatch.Groups[2].Value);
                            Item.SubItems.Add(MyMatch.Groups[4].Value);
                            Item.SubItems.Add(MyMatch.Groups[5].Value);
                            Item.SubItems.Add(MyMatch.Groups[1].Value);

                            //Build the AllItems Listview item
                            Item2.Text = MyMatch.Groups[3].Value.PadLeft(6, '0');
                            Item2.SubItems.Add(MyMatch.Groups[2].Value);
                            Item2.SubItems.Add(MyMatch.Groups[4].Value);
                            Item2.SubItems.Add(MyMatch.Groups[1].Value);

                            //If the option to hide bags is enabled, check if the BagList contains the ItemID, if it does, set the AddLine variable to false.
                            if (Options.HideBags)
                                if (BagList.Any(str => str.Contains(Item.Text)))
                                    AddLine = false;
                        }
                        break;
                    case "RealEstate":
                        {
                            //Set the listview variable to the RealEstate Listview.
                            lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[4].Controls.OfType<ListView>().First();
                            
                            //Check the Regex for the line,
                            MyMatch = ParseRealEstate.Match(Line);

                            //Build the Listview item
                            Item.Text = MyMatch.Groups[6].Value.PadLeft(6, '0');
                            Item.SubItems.Add(MyMatch.Groups[3].Value);
                            Item.SubItems.Add(MyMatch.Groups[7].Value);
                            Item.SubItems.Add(MyMatch.Groups[5].Value);
                            Item.SubItems.Add(MyMatch.Groups[4].Value);
                            Item.SubItems.Add(MyMatch.Groups[1].Value);
                            Item.SubItems.Add(MyMatch.Groups[2].Value);

                            //Build the AllItems Listview item
                            Item2.Text = MyMatch.Groups[6].Value.PadLeft(6, '0');
                            Item2.SubItems.Add(MyMatch.Groups[3].Value);
                            Item2.SubItems.Add(MyMatch.Groups[7].Value);
                            Item2.SubItems.Add(MyMatch.Groups[2].Value);

                        }
                        break;
                    case "GuildBank":
                        {
                            //Set the listview variable to the GuildBank Listview
                            lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[5].Controls.OfType<ListView>().First();
                            
                            //Check the Regex for the line,
                            MyMatch = ParseInventory.Match(Line);

                            //Build the Listview item
                            Item.Text = MyMatch.Groups[3].Value.PadLeft(6, '0');
                            Item.SubItems.Add(MyMatch.Groups[2].Value);
                            Item.SubItems.Add(MyMatch.Groups[4].Value);
                            Item.SubItems.Add(MyMatch.Groups[5].Value);
                            Item.SubItems.Add(MyMatch.Groups[1].Value);

                            //Build the AllItems Listview item
                            Item2.Text = MyMatch.Groups[3].Value.PadLeft(6, '0');
                            Item2.SubItems.Add(MyMatch.Groups[2].Value);
                            Item2.SubItems.Add(MyMatch.Groups[4].Value);
                            Item2.SubItems.Add("Guild" + MyMatch.Groups[1].Value);

                        }
                        break;
                    case "GuildRealEstate":
                        {
                            //Set the listview variable to the GuildRealEstate Listview
                            lv = SelectedCharacter.Controls.OfType<TabControl>().First().TabPages[6].Controls.OfType<ListView>().First();
                            
                            //Check the Regex for the line,
                            MyMatch = ParseRealEstate.Match(Line);

                            Item.Text = MyMatch.Groups[6].Value.PadLeft(6, '0');
                            Item.SubItems.Add(MyMatch.Groups[3].Value);
                            Item.SubItems.Add(MyMatch.Groups[7].Value);
                            Item.SubItems.Add(MyMatch.Groups[5].Value);
                            Item.SubItems.Add(MyMatch.Groups[4].Value);
                            Item.SubItems.Add(MyMatch.Groups[1].Value);
                            Item.SubItems.Add(MyMatch.Groups[2].Value);

                            //Build the AllItems Listview item
                            Item2.Text = MyMatch.Groups[6].Value.PadLeft(6, '0');
                            Item2.SubItems.Add(MyMatch.Groups[3].Value);
                            Item2.SubItems.Add(MyMatch.Groups[7].Value);
                            Item2.SubItems.Add(MyMatch.Groups[2].Value);
                        }
                        break;
                }

                //If the option to hide empty slots is enabled, check if the ItemID is 000000, if it is, set the AddLine variable to false. 
                if (Options.HideEmpty)
                    if (Item.Text == "000000")
                        AddLine = false;

                //If AddLine is true, add the items to the Listviews.
                if (AddLine)
                {
                    lv.Items.Add(Item);
                    if (!Options.HideAllItems)
                        lv2.Items.Add(Item2);
                }

                //Reset AddLine variable 
                AddLine = true;

                //Read the next line of the file. 
                Line = sr.ReadLine();
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
            CheckFile("Beimeith", null, "Inventory", Directory);
            CheckFile("Beimeith", null, "RealEstate", Directory);
            CheckFile("Beimeith", null, "GuildBank", Directory);
            CheckFile("Beimeith", "Machin Shin", "GuildRealEstate", Directory);

            //   MessageBox.Show("");

            //AddCharacterPage("Zephrina", "Xegony", "Machin Shin", Directory);
            //ParseFile("Zephrina", null, "Inventory", Directory);
            //ParseFile("Zephrina", null, "RealEstate", Directory);
            //ParseFile("Zephrina", null, "GuildBank", Directory);
            //ParseFile("Zephrina", "Machin Shin", "GuildRealEstate", Directory);

            foreach (TabPage tp in SelectedCharacter.Controls.OfType<TabControl>().First().TabPages)
                for (int i = 0; i < tp.Controls.OfType<ListView>().First().Columns.Count; i++)
                    if (tp.Controls.OfType<ListView>().First().Columns[i].Text == "Count" || tp.Controls.OfType<ListView>().First().Columns[i].Text == "Slots")
                        tp.Controls.OfType<ListView>().First().Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    else
                    {
                        tp.Controls.OfType<ListView>().First().Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
            

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings Settings = new Settings();

            Settings.ShowDialog(this);
        }
    }
}
