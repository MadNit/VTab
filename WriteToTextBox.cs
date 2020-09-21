using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace VTab
{
    public class WriteToTextBox
    {
        String std_output;
        VCustomTab WTabRT;
        Process WProcessObj;

        public WriteToTextBox(VCustomTab TabRT, Process ProcessObj)
        {
            WTabRT = TabRT;
            WProcessObj = ProcessObj;
        }
        public void WriteThread()
        {
            try
            {
 
                while ((std_output = WProcessObj.StandardOutput.ReadLine()) != null)
                {
                    WTabRT.WriteLine(std_output);
                }
            }
            catch(Exception ed)
            {
                Console.WriteLine($"Error Occured in writing to Tab RT." + ed.Message);
            }           

        }


    }
}
