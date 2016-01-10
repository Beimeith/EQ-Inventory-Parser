using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;



class FlexibleListViewItemSorter : IComparer
{
    public int Column;
    public string Mode;
    public int SecondaryColumn;
    public string SecondaryMode;
    public bool Reverse;

    public FlexibleListViewItemSorter()
    {
        Column = 0;
        Mode = null;
        SecondaryColumn = 0;
        SecondaryMode = null;
        Reverse = false;
    }

    public int Compare(object x, object y)
    {
        int result = Compare(x, y, Column, Mode);

        // if the result is equal then sort by a secondary column
        if (result == 0 && Column != SecondaryColumn)
            return Compare(x, y, SecondaryColumn, SecondaryMode);
        else if (Reverse)
            return -result;

        return result;
    }

    public static int Compare(object x, object y, int col, string mode)
    {
        var xt = ((ListViewItem)x).SubItems[col].Text;
        var yt = ((ListViewItem)y).SubItems[col].Text;

        long result = 0;
        int send = 0;

        if (mode == "Int")
            result = Convert.ToInt64(xt) - Convert.ToInt64(yt);
        else if (mode == "IntDesc")
            result = Convert.ToInt64(yt) - Convert.ToInt64(xt);
        else if (mode == "Time")
            result = (int)(DateTime.Parse(xt) - DateTime.Parse(yt)).TotalSeconds;
        else if (mode == "TimeDesc")
            result = (int)(DateTime.Parse(yt) - DateTime.Parse(xt)).TotalSeconds;
        else if (mode == "StringDesc")
            result = String.Compare(yt, xt, true);
        else
            result = String.Compare(xt, yt, true);

        if (result > Int32.MaxValue)
            send = Int32.MaxValue;
        else if (result < Int32.MinValue)
            send = Int32.MinValue;
        else
            send = Convert.ToInt32(result);


        return send;
    }
}


class ListViewItemComparerNumberDescending : IComparer
{
    private int col;
    public ListViewItemComparerNumberDescending()
    {
        col = 0;
    }
    public ListViewItemComparerNumberDescending(int column)
    {
        col = column;
    }
    public int Compare(object x, object y)
    {
        return Convert.ToInt32(((ListViewItem)y).SubItems[col].Text) - Convert.ToInt32(((ListViewItem)x).SubItems[col].Text);
    }
}

class ListViewItemComparerNumberAscending : IComparer
{
    private int col;
    public ListViewItemComparerNumberAscending()
    {
        col = 0;
    }
    public ListViewItemComparerNumberAscending(int column)
    {
        col = column;
    }
    public int Compare(object x, object y)
    {
        return Convert.ToInt32(((ListViewItem)x).SubItems[col].Text) - Convert.ToInt32(((ListViewItem)y).SubItems[col].Text);
    }
}

class ListViewItemComparerText : IComparer
{
    private int col;
    public ListViewItemComparerText()
    {
        col = 0;
    }
    public ListViewItemComparerText(int column)
    {
        col = column;
    }
    public int Compare(object x, object y)
    {
        return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
    }
}
class ListViewItemComparerTime : IComparer
{
    private int col;
    public ListViewItemComparerTime()
    {
        col = 0;
    }
    public ListViewItemComparerTime(int column)
    {
        col = column;
    }
    public int Compare(object x, object y)
    {
        return (int)(DateTime.Parse(((ListViewItem)x).SubItems[col].Text) - DateTime.Parse(((ListViewItem)y).SubItems[col].Text)).TotalSeconds;
    }
}
