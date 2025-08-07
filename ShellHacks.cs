using System.Runtime.InteropServices;

public static class ShellHacks
{
    public static void MinimizeConsole()
    {
        //You could say I IMPORTED this FUNCTION from help.microsoft.com...
        //This is entirely worthless.
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        int SW_HIDE = 0;
        ShowWindow(GetConsoleWindow(), SW_HIDE);
    }
}