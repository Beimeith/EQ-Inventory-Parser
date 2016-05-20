using System;
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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            CB_HideEmpty.Checked = Options.HideEmpty;
            CB_HideBags.Checked = Options.HideBags;
            CB_HideAllItems.Checked = Options.HideAllItems;
        }

        private void B_Close_Click(object sender, EventArgs e)
        {
            Options.HideEmpty = CB_HideEmpty.Checked;
            Options.HideBags = CB_HideBags.Checked;
            Options.HideAllItems = CB_HideAllItems.Checked;

            Close();
        }
    }
}
