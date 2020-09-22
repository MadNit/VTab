using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTab
{
    class ANSIEscapeSeq
    {
        // Terminal Sequence Codes for terminal VT100
        static byte esc = 27;      // ESC : escape character
        static byte bell = 7;      // BEL : bell character
        static byte opensqb = 91;  // [ : Open Square bracket
        static byte clossqb = 93;  // ] : Close Square bracket

        // Control Sequence Introducer : CSI : ESC [
        static string csi = $"{(char)esc}{(char)opensqb}";
        // String Terminator : ST : ESC \ : ESC BEL
        static string st = $"{(char)esc}{(char)bell}";
        // Operating System Command : OSC : ESC ]
        static string osc = $"{(char)esc}{(char)clossqb}";

        // Terminate character : BEL 
        static string tc = $"{(char)bell}";

        // Check 8 bit Input : CSI ? 1 0 3 4 h
        // https://invisible-island.net/xterm/ctlseqs/ctlseqs.html
        static string eight_bit_input = $"{csi}?1034h";
        static string title_seq = $"{osc}0;";

        // Return the title of string with escap sequence
        public Tuple<string, string> getTitleAndCommandPrompt(string esc_str)
        {
            string title = esc_str.Substring(esc_str.IndexOf(title_seq) + title_seq.Length, esc_str.IndexOf(tc) - title_seq.Length);
            string tmp_str = esc_str.Substring(esc_str.IndexOf(tc) + 1);
            string cmd_prompt = tmp_str;
            // check if csi exists
            if (tmp_str.IndexOf(csi) >= 0)
            {
                // Check for eight bit input sequence
                if (tmp_str.IndexOf(eight_bit_input) >= 0)
                {
                    cmd_prompt = tmp_str.Substring(tmp_str.IndexOf(eight_bit_input) + eight_bit_input.Length);
                }
                // Check for other conditions
            }
            return Tuple.Create(title, cmd_prompt);
        }
    }
}
