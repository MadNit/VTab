using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTab
{
 
    public class VCustomTab : TabPage
    {
        TabPageUserControl tbpgUC;
        public VCustomTab()
        {
            tbpgUC = new TabPageUserControl();
            tbpgUC.Dock = DockStyle.Fill;
            this.Controls.Add(tbpgUC);
        }

        public VCustomTab(String title)
        {
            this.Text = title;
            tbpgUC = new TabPageUserControl();
            tbpgUC.Dock = DockStyle.Fill;
            this.Controls.Add(tbpgUC);
        }

        public void setTitle(String title_str)
        {
            this.Text = title_str;
        }

        public void setPrompt(string cmd_prompt)
        {
            tbpgUC.setPrompt(cmd_prompt);
        }

        public void setProcessTab(TabProcess tabProc)
        {
            tbpgUC.setProcessTab(tabProc);
        }

        public void WriteLine(String data)
        {
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
