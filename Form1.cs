using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GroupBox3_Enter(object sender, EventArgs e)
        {

        }


        private async void SshButton_Click_1(object sender, EventArgs e)
        {
            //myTabPage.WriteLine(title);
            string ansicon_proc = "C:\\Users\\medav2\\source\\repos\\VTab\\exe\\ansicon.exe";
            string plnkexe_proc = "C:\\Users\\medav2\\source\\repos\\VTab\\exe\\plink.exe";
            await RunProcessAsync(ansicon_proc, plnkexe_proc);

        }

        public async Task<string> ReadFromProcessAsync(System.IO.TextReader txtRdr)
        {
            string text;
            text = await txtRdr.ReadLineAsync();
            //text = await txtRdr.ReadToEndAsync();
            return text;
        }

        public async Task WriteToProcessAsync(System.IO.TextWriter txtWr, string text)
        {
            await txtWr.WriteAsync(text);
        }

            public async Task RunProcessAsync(string ansicon_cmd, string plink_cmd)
        {
            // Execute plink.exe
            // Exit on error and close the tab
            // Else stay on the tab, wait for the user input 
            // and return output.
            // Create a tab
            VCustomTab myTabPage = new VCustomTab("");
            
            try
            {
                using (Process myProcess = new Process())
                {
                    String macname = macTxtBox.Text;
                    String usrname = usrTxtBox.Text;
                    String pwd = pwdTxtBox.Text;
                    //String args = plink_cmd + " -l " + usrname + " -pw " + pwd + " " + macname;
                    String args = $"{plink_cmd} -l {usrname} -pw {pwd} {macname}";
                    ProcessStartInfo startInfo = new ProcessStartInfo(ansicon_cmd, args);
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.RedirectStandardInput = true;

                    // plink.exe is the cmd tool for putty.
                    myProcess.StartInfo = startInfo;
                    //myProcess.OutputDataReceived += CaptureOutput;
                    //myProcess.ErrorDataReceived += CaptureError;
                    // This code assumes the process you are starting will terminate itself. 
                    // Given that is is started without a window so you cannot terminate it 
                    // on the desktop, it must terminate itself or you can do it programmatically
                    // from this application using the Kill method.

                    String outstr = "vj";
                    myProcess.Start();
                    String tempStr = "";
                    outstr = await ReadFromProcessAsync(myProcess.StandardOutput);
                    tempStr += outstr;
                    outstr = await ReadFromProcessAsync(myProcess.StandardOutput);
                    tempStr += outstr;

                    string nextcmd = "\n"; // Enter after success
                    await WriteToProcessAsync(myProcess.StandardInput, nextcmd);
                    
                    ANSIEscapeSeq ansi_seq = new ANSIEscapeSeq();
                    // TODO: The end with character may differ : Taken Care, ANSIEscapeSeq class
                    while (outstr != null && !outstr.Trim().EndsWith("$"))
                    {
                        tempStr += outstr + "\n";
                        outstr = await ReadFromProcessAsync(myProcess.StandardOutput);
                        /*
                        byte[] escTitleBytesRec = Encoding.ASCII.GetBytes(outstr);
                        foreach (byte b in escTitleBytesRec)
                            Console.Write((char)b + "( " + b + ") ");
                        */
                        //myTabPage.WriteLine(outstr + "\n");
                    }
                    // Get the command prompt and title now.
                    var out_tup = ansi_seq.getTitleAndCommandPrompt(outstr);
                    myTabPage.setTitle(out_tup.Item1);
                    tabControl1.TabPages.Add(myTabPage);

                    myTabPage.WriteLine(tempStr);
                    myTabPage.WriteLine(out_tup.Item2);

                    // Loop until the command is logout or exit or on error.

                    outstr = await ReadFromProcessAsync(myProcess.StandardOutput);
                    /*
                    try
                    {
                        Thread.Sleep(10000);
                        //nextcmd = "logout";
                        nextcmd = "";
                        await WriteToProcessAsync(myProcess.StandardInput, nextcmd);
                    }
                    catch (Exception eex)
                    {
                        Console.WriteLine($"An error occurred {eex.Message}");  // The $ is the fstrings in python.
                    }
                   
                    if (myProcess.HasExited)
                    {
                        myProcess.Close();
                        myProcess.Dispose();
                    }
                    else
                    {
                        myProcess.Kill();
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        
    }

}
