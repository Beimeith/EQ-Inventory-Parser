﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ_Inventory_Parser
{
    class Options
    {
        //Options to hide items you don't want to see on the list.
        public static bool HideEmpty = true;
        public static bool HideBags = true;
        public static bool HideShared = false;
        public static bool HideBank = false;
        public static bool HideInventory = false;
        public static bool HideAllItems = false;

        public static string DefaultPath = "C:\\Users\\Public\\Daybreak Game Company\\Installed Games\\EverQuest";

    }
}
