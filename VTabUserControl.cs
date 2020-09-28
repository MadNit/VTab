using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VTab
{
    public partial class TabPageUserControl : UserControl
    {
        int last_pos;
        String temp_str = "";
        String cmd_prompt = "";
        //bool shift_pressed = false;
        TabProcess tabProc;

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
            TabPageRTBox.Focus();
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
            string ent_cmd = TabPageRTBox.Text.Substring(last_pos);
        }

        public void setPrompt(string cmd_prompt)
        {
            this.cmd_prompt = cmd_prompt;
        }

        public String getCommand()
        {
            return temp_str;
        }

        public void setProcessTab(TabProcess mstr)
        {
            this.tabProc = mstr;
        }

        private async void TabPageRTBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                temp_str = TabPageRTBox.Text.Substring(last_pos).Trim();
                Console.WriteLine($"Command TabPageRTBox_KeyDown is {temp_str}");
                // write to the Stream from begining every time.
                // Write the length first as int (4 bytes)
                //mstream.Position = 0;
                byte[] cmd_bytes = Encoding.Unicode.GetBytes(temp_str);
                
                byte[] cmd_len = BitConverter.GetBytes(cmd_bytes.Length);
                int cmd_len_int = cmd_bytes.Length;
                int len_of_int = cmd_len.Length;  // usually four bytes on 64 bit systems, sizeof int

                /*
                mstream.Write(cmd_len, 0, len_of_int);
                int count = 0;
                while (count < cmd_len_int)
                {
                    mstream.WriteByte(cmd_bytes[count++]);
                }
                */
                //mstream.WriteAsync(temp_str);
                await tabProc.WriteToProcessAsync(temp_str + "\n");
                Console.WriteLine($"Written to stream........{temp_str}");

                Console.WriteLine("Readming from stream........");
                string outstr = await tabProc.ReadFromProcessAsync();
                Console.WriteLine($"Line........:{outstr}");
                outstr = await tabProc.ReadFromProcessAsync();
                Console.WriteLine($"Line........:{outstr}");

                this.AppendText("\n");
              
                TabPageRTBox.DeselectAll();
            }
            else
            {

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
