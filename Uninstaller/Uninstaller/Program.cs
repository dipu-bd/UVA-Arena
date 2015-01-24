using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Uninstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"Software\UVA Arena\Problem Database");
            }
            catch { }
        }
    }
}
