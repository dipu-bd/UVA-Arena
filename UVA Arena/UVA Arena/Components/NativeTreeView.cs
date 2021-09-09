using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public class NativeTreeView : TreeView
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            IntPtr lparam = Marshal.StringToBSTR("explorer");
            UVA_Arena.NativeMethods.SetWindowTheme(this.Handle, lparam, IntPtr.Zero);
        }
    }
}
