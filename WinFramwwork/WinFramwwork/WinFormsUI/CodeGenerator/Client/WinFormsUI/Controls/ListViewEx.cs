namespace CodeGenerator.Client.WinFormsUI.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class ListViewEx : ListView
    {
        private Dictionary<ListViewItem, ItemColor> dicItemColor = new Dictionary<ListViewItem, ItemColor>();

        public ListViewEx()
        {
            base.GotFocus += new EventHandler(this.listView1_GotFocus);
            base.LostFocus += new EventHandler(this.listView1_LostFocus);
            base.HideSelection = true;
            base.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.listView1_LostFocus(null, null);
            this.listView1_GotFocus(null, null);
        }

        private void listView_Validated(object sender, EventArgs e)
        {
            if (base.FocusedItem != null)
            {
                base.FocusedItem.BackColor = SystemColors.Highlight;
                base.FocusedItem.ForeColor = Color.White;
            }
        }

        private void listView1_GotFocus(object sender, EventArgs e)
        {
            foreach (KeyValuePair<ListViewItem, ItemColor> pair in this.dicItemColor)
            {
                ListViewItem key = pair.Key;
                ItemColor color = pair.Value;
                key.ForeColor = color.ForeColor;
                key.BackColor = color.BackColor;
            }
            foreach (ListViewItem item in base.SelectedItems)
            {
                item.ForeColor = Color.White;
                item.BackColor = SystemColors.Highlight;
            }
        }

        private void listView1_LostFocus(object sender, EventArgs e)
        {
            base.HideSelection = true;
            foreach (ListViewItem item in base.SelectedItems)
            {
                ItemColor color = null;
                if (!this.dicItemColor.ContainsKey(item))
                {
                    color = new ItemColor {
                        ForeColor = item.ForeColor,
                        BackColor = item.BackColor
                    };
                    this.dicItemColor.Add(item, color);
                }
                item.ForeColor = Color.White;
                item.BackColor = SystemColors.Highlight;
            }
        }

        private class ItemColor
        {
            public Color BackColor;
            public Color ForeColor;
        }
    }
}

