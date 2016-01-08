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

            CB_MoveEmpty.Checked = Options.MoveEmpty;
            CB_HideEmpty.Checked = Options.HideEmpty;

            CB_MoveBags.Checked = Options.MoveBags;
            CB_HideBags.Checked = Options.HideBags;
        }

    }
}
