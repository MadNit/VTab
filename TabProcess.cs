using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTab
{
    public class TabProcess
    {
        TextWriter tabProcWriter;
        TextReader tabProcReader;

        public TabProcess(TextWriter procWriter, TextReader procReader)
        {
            this.tabProcWriter = procWriter;
            this.tabProcReader = procReader;
        }

        public async Task WriteToProcessAsync(string text)
        {
            await tabProcWriter.WriteAsync(text);
        }

        public async Task<string> ReadFromProcessAsync()
        {
            string text;
            text = await tabProcReader.ReadLineAsync();
            //text = await txtRdr.ReadToEndAsync();
            return text;
        }

    }
}
