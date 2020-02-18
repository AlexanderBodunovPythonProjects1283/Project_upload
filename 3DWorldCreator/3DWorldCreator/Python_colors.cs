using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _3DWorldCreator
{
    public class Python_colors
    {
        public Python_colors()
        {

        }

        public List<int> get_groups_colored(string text)
        {
            text = text.Replace(@"\n", "");
            while (text.Contains("  ")) { text = text.Replace("  ", ""); }
            List<int> result = new List<int>();
            string pattern = @"y";
            Regex newReg = new Regex(pattern);

           
            MatchCollection matches = newReg.Matches(text);
            foreach (Match mat in matches)
            {
                result.Add(mat.Index);
            }

            /*string s0 = "y";
            int n = text.IndexOf(s0);
            int count_ = 1;
            while (n != -1)
            {
                result.Add(n);
                n = text.IndexOf(s0, n+s0.Length);
                count_ += 1;
            }
            */

            return result;
        }

    }
}
