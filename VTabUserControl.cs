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
        String temp_str = "";
        String cmd_prompt = "";
        bool shift_pressed = false;

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
            TabPageRTBox.Select(0, TabPageRTBox.Text.Length);
            TabPageRTBox.SelectionProtected = true;
            
            last_pos = TabPageRTBox.Text.Length;
            TabPageRTBox.SelectionStart = last_pos;
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

        public void setPrompt(string cmd_prompt)
        {
            this.cmd_prompt = cmd_prompt;
        }

        public String getCommand()
        {
            return temp_str;
        }

        private void TabPageRTBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                temp_str = TabPageRTBox.Text.Substring(last_pos).Trim();
                Console.WriteLine($"Command is {temp_str}");
                this.AppendText("\n");
              
                TabPageRTBox.DeselectAll();
            }
            /*
            else
            {
                if(e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.Capital)
                {
                    shift_pressed = true;
                }
                else
                {
                    int kval = e.KeyValue;
                    if (!shift_pressed)
                    {
                        kval += 32;
                    }
                    temp_str += (char)kval;
                }
                
            }
            */
        }

        /*
        private void TabPageRTBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.Capital)
                shift_pressed = false;
        }
        */
    }
}
