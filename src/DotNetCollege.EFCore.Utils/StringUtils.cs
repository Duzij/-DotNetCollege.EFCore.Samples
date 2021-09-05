using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetCollege.EFCore.Utils
{
    public static class StringUtils
    {
        public static string MultiplyStringInput(string input, int multiply)
        {
            using (var stringWriter = new StringWriter())
            {
                for (int i = 0; i < multiply; i++)
                {
                    stringWriter.Write(input + " ");
                }
                return stringWriter.ToString();
            }
        }

        public static string GetFirstWord(string input)
        {
            return input.Split(' ').First(); ;
        }

    }
}
