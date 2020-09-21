using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTab
{
 
    public class VCustomTab : TabPage
    {
        public RichTextBox textbox;
        TabPageUserControl tbpgUC;
        public VCustomTab()
        {
            /*
            textbox = new RichTextBox();
            this.Controls.Add(textbox);
            textbox.Dock = DockStyle.Fill;
            */

            tbpgUC = new TabPageUserControl();
            tbpgUC.Dock = DockStyle.Fill;
            this.Controls.Add(tbpgUC);
        }

        public VCustomTab(String title)
        {
            /*
            textbox = new RichTextBox();
            this.Controls.Add(textbox);
            textbox.Dock = DockStyle.Fill;
            */

            this.Text = title;
            tbpgUC = new TabPageUserControl();
            tbpgUC.Dock = DockStyle.Fill;
            this.Controls.Add(tbpgUC);


        }

        public void WriteLine(String data)
        {
            /*
            textbox.AppendText(data);
            textbox.AppendText("\n");
            */
            if (data != null)
            {
                tbpgUC.AppendText(data);
            }
            else
            {
                tbpgUC.AppendText("");
            }
        }

    }
}
