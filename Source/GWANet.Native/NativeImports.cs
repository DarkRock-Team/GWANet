using System.Runtime.InteropServices;
using System.Security;

namespace GWANet.Native
{
    public static class NativeImports
    {
        [DllImport("user32.dll", SetLastError = false), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowText([In] IntPtr hWnd, [In] string lpString);
    }
}