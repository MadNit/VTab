using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTab
{
    public partial class TabPageUserControl : UserControl
    {
        int last_pos;
        public TabPageUserControl()
        {
            InitializeComponent();
        }

        void CutAction(object sender, EventArgs e)
        {
            TabPageRTBox.Cut();
        }

        void CopyAction(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Rtf, TabPageRTBox.SelectedRtf);
            Clipboard.Clear();
        }

        void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                TabPageRTBox.SelectedRtf
                    = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }

        public void AppendText(String data)
        {
            TabPageRTBox.AppendText(data);
            TabPageRTBox.Select(0, TabPageRTBox.Text.Length - 1);
            TabPageRTBox.SelectionProtected = true;
            last_pos = TabPageRTBox.Text.Length;
            Console.WriteLine($"Last position in Append Text : {last_pos}");
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   //click event
                //MessageBox.Show("you got it!");
                ContextMenu contextMenu = new ContextMenu();
                MenuItem menuItem = new MenuItem("Cut");
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste");
                menuItem.Click += new EventHandler(PasteAction);
                contextMenu.MenuItems.Add(menuItem);

                TabPageRTBox.ContextMenu = contextMenu;
            }

        }

        private void TabPageRTBox_Enter(object sender, EventArgs e)
        {
            Console.WriteLine($"Last position in TabPageRTBox_Enter : {last_pos}");
            Console.WriteLine($"Text is : {TabPageRTBox.Text}");
            string ent_cmd = TabPageRTBox.Text.Substring(last_pos);
            Console.WriteLine($"Entered command is : {ent_cmd}");

        }
    }
}
