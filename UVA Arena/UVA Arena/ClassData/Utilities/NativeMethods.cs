using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace UVA_Arena
{
    public static class NativeMethods
    {
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
        /// <summary> When Mouse Wheel scrolling occurs </summary
        public const int WM_MOUSEWHEEL = 0x20A;
        /// <summary> When horizontal scrolling occurs </summary>
        public const int WM_HSCROLL = 0x114;
        /// <summary> The left mouse button is down </summary>
        public const int MK_LBUTTON = 0x0001;
        /// <summary> Set Cue-text on a Textbox control</summary>
        public const int EM_SETCUEBANNER = 0x1501;
        /// Animates the window from left to right. 
        /// This flag can be used with roll or slide animation.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_HOR_POSITIVE = 0X1;
        /// <span class="code-SummaryComment"><summary></span>
        /// Animates the window from right to left. 
        /// This flag can be used with roll or slide animation.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_HOR_NEGATIVE = 0X2;
        /// <span class="code-SummaryComment"><summary></span>
        /// Animates the window from top to bottom. 
        /// This flag can be used with roll or slide animation.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_VER_POSITIVE = 0X4;
        /// <span class="code-SummaryComment"><summary></span>
        /// Animates the window from bottom to top. 
        /// This flag can be used with roll or slide animation.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_VER_NEGATIVE = 0X8;
        /// <span class="code-SummaryComment"><summary></span>
        /// Makes the window appear to collapse inward 
        /// if AW_HIDE is used or expand outward if the AW_HIDE is not used.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_CENTER = 0X10;
        /// <span class="code-SummaryComment"><summary></span>
        /// Hides the window. By default, the window is shown.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_HIDE = 0X10000;
        /// <span class="code-SummaryComment"><summary></span>
        /// Activates the window.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_ACTIVATE = 0X20000;
        /// <span class="code-SummaryComment"><summary></span>
        /// Uses slide animation. By default, roll animation is used.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_SLIDE = 0X40000;
        /// <span class="code-SummaryComment"><summary></span>
        /// Uses a fade effect. 
        /// This flag can be used only if hwnd is a top-level window.
        /// <span class="code-SummaryComment"></summary></span>
        public const int AW_BLEND = 0X80000;

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

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        // P/Invoke declarations
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point pt);

        //
        // UXTheme
        //
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, IntPtr pszSubAppName, IntPtr pszSubIdList);

        //
        // DWM Api
        //
        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        //
        // WinINet
        [DllImport("wininet.dll", SetLastError = true)]
        public static extern int InternetAttemptConnect(uint res);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetConnectedState(long flags, long reserved);


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
            IntPtr lparam = new IntPtr(HT_CAPTION);
            IntPtr wparam = new IntPtr(0);
            SendMessage(Handle, WM_NCLBUTTONDOWN, lparam, wparam);
        }
    }
}

