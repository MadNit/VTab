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
            // Create a tab
            String title = "vijai123";
            VCustomTab myTabPage = new VCustomTab(title);
            tabControl1.TabPages.Add(myTabPage);
            //myTabPage.WriteLine(title);
            string process_exec = "C:\\Users\\medav2\\source\\repos\\VTab\\exe\\plink.exe";
            await RunProcessAsync(process_exec, myTabPage);

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

            public async Task RunProcessAsync(string cmd, VCustomTab myTabPage)
        {
            // Execute plink.exe
            // Exit on error and close the tab
            // Else stay on the tab, wait for the user input 
            // and return output.
            try
            {
                using (Process myProcess = new Process())
                {
                    String macname = macTxtBox.Text;
                    String usrname = usrTxtBox.Text;
                    String pwd = pwdTxtBox.Text;
                    String cmd_prompt_recv;
                    String args = "-l " + usrname + " -pw " + pwd + " " + macname;
                    ProcessStartInfo startInfo = new ProcessStartInfo(cmd, args);
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

                    outstr = await ReadFromProcessAsync(myProcess.StandardOutput);
                    myTabPage.WriteLine(outstr);
                    outstr = await ReadFromProcessAsync(myProcess.StandardOutput);
                    myTabPage.WriteLine(outstr);

                    string nextcmd = "\n";
                    await WriteToProcessAsync(myProcess.StandardInput, nextcmd);
                    myTabPage.WriteLine(nextcmd);

                    while (!outstr.Trim().EndsWith("$"))
                    {
                        outstr = await ReadFromProcessAsync(myProcess.StandardOutput);
                        Console.WriteLine("outstr in loop:" + outstr);
                        myTabPage.WriteLine(outstr + "\n");
                    }
                    Console.WriteLine("CommandLine Received is:" + outstr);
                    //myTabPage.WriteLine(outstr);

                    // TODO: The command prompt can be any character '$', '<', '>', '#', etc.
                    /*
                    if (outstr.Trim().EndsWith("$"))
                    { 
                        Console.WriteLine("First:" + outstr);
                        
                        Console.WriteLine(outstr.Substring(0, 4));
                        byte esc_char = 27;
                        char[] chr = { (char)27, (char)93, (char)48, (char)59 };
                        byte[] escTitleBytesAct = Encoding.GetEncoding("UTF-8").GetBytes(chr);
                        byte[] escTitleBytesRec = Encoding.ASCII.GetBytes(outstr.Substring(0, 4));
                        byte firstChar = escTitleBytesRec[0];
                        if (esc_char.Equals(firstChar))
                        {
                            String cmd_prompt_recv = outstr.Substring(4);
                            String cmd_prompt_disp = outstr.Substring(outstr.IndexOf('~') + 10);
                            Console.WriteLine("Command prompt received is:" + cmd_prompt_recv);
                            Console.WriteLine("Command prompt to display is:" + cmd_prompt_disp);
                        }
                        else
                        {
                            Console.WriteLine("Not a command prompt:" + outstr);
                        }
                    }
                    else
                        Console.WriteLine("Last:" + outstr);

                    */


                    try
                    {
                        nextcmd = "logout";
                        await WriteToProcessAsync(myProcess.StandardInput, nextcmd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred");  // The $ is the fstrings in python.
                    }
                    

                    // WaitForExit will block the entire UI when used in Forms.
                    // myProcess.WaitForExit();
                    //Thread.Sleep(10000);

                    /*
                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    myTabPage.WriteLine(outstr);
                    myProcess.StandardInput.WriteLine("\n");

                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    myTabPage.WriteLine(outstr);
                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    myTabPage.WriteLine(outstr);


                    myProcess.StandardInput.WriteLine("ls -ltr");
                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    myTabPage.WriteLine(outstr);
                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    myTabPage.WriteLine(outstr);
                    outstr = "" + myProcess.StandardOutput.ReadLine();
                    myTabPage.WriteLine(outstr);
                    */






                    //Thread.Sleep(10);

                    /*
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
