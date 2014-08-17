using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UVA_Arena
{ 
    internal sealed class NativeMethods
    {
        private NativeMethods() { }
        
        //
        // Messages
        //
        public const int WM_GETTABRECT = 0x130A;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int WM_SETFONT = 0x30;
        public const int WM_FONTCHANGE = 0x1D;
        public const int WM_PAINT = 0xF;
        public const int WS_EX_LAYOUTRTL = 0x400000;
        public const int WS_EX_NOINHERITLAYOUT = 0x100000;
        public const int TCM_HITTEST = 0x130D;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        /// <summary> When horizontal scrolling occurs </summary>
        public const int WM_HSCROLL = 0x114;
        /// <summary> The left mouse button is down </summary>
        public const int MK_LBUTTON = 0x0001;
        /// <summary> Set Cue-text on a Textbox control</summary>
        public const int EM_SETCUEBANNER = 0x1501;

        //
        // Structures
        //
        struct MARGINS
        {
            int left;
            int right;
            int top;
            int bottom;

            public MARGINS(int left, int right, int top, int bottom)
            {
                this.left = left;
                this.right = right;
                this.top = top;
                this.bottom = bottom;
            }
        };

        //
        // Content Alignment
        //
        public static readonly ContentAlignment AnyRightAlign = ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight;
        public static readonly ContentAlignment AnyLeftAlign = ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft;
        public static readonly ContentAlignment AnyTopAlign = ContentAlignment.TopRight | ContentAlignment.TopCenter | ContentAlignment.TopLeft;
        public static readonly ContentAlignment AnyBottomAlign = ContentAlignment.BottomRight | ContentAlignment.BottomCenter | ContentAlignment.BottomLeft;
        public static readonly ContentAlignment AnyMiddleAlign = ContentAlignment.MiddleRight | ContentAlignment.MiddleCenter | ContentAlignment.MiddleLeft;
        public static readonly ContentAlignment AnyCenterAlign = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;
        
        //
        // User32.dll
        //
        [DllImport("user32.dll"), SecurityPermission(SecurityAction.Demand)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll"), SecurityPermission(SecurityAction.Demand)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //
        // DWM Api
        //
        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        //
        // Misc Functions
        //
        public static IntPtr ToIntPtr(object structure)
        {            
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure, lparam, false);
            return lparam;
        }
        public static IntPtr ToIntPtr(string str)
        {            
            return Marshal.StringToBSTR(str);
        }

        public static object FromIntPtr(IntPtr ptr, Type structureType)
        {            
            return Marshal.PtrToStructure(ptr, structureType);
        }

        public static bool ExtendWindowsFrame(Form form, int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            MARGINS mar = new MARGINS(left, right, top, bottom);
            return (DwmExtendFrameIntoClientArea(form.Handle, ref mar) == 0);
        }

        public static void MoveWithMouse(IntPtr Handle)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }
}

