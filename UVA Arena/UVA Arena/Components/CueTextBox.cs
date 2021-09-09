using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class CueTextBox : TextBox
    {
        public CueTextBox() { }

        private string _cueText;
        public string CueText
        {
            get { return _cueText; }
            set
            {
                _cueText = value;

                IntPtr lparam = new IntPtr(1);
                IntPtr wparam = Marshal.StringToBSTR(value);
                //<----Native methods---->
                UVA_Arena.NativeMethods.SendMessage(this.Handle,
                    UVA_Arena.NativeMethods.EM_SETCUEBANNER, lparam, wparam);
                Marshal.FreeCoTaskMem(lparam);
                Marshal.FreeBSTR(wparam);
            }
        }
    }

}