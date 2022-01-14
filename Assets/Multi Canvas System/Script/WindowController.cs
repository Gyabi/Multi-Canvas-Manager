using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
public static class WindowController 
{
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);

    // Sets window attributes
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    // Gets window attributes
    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    // assorted constants needed
    public static int GWL_STYLE = -16;
    public static int WS_CHILD = 0x40000000; //child window
    public static int WS_BORDER = 0x00800000; //window with border
    public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
    public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar

    public static int WS_SYSMENU = 0x00080000;
    const int HWND_TOP = 0x0; // 最前面に表示
    const uint SWP_NOSIZE = 0x1; // 現在のサイズを維持(cxとcyパラメータを無視)
    const uint SWP_NOMOVE = 0x2; // 現在の位置を維持(xとyパラメータを無視)
    const int SWP_FRAMECHANGED = 0x0020; // SetWindowLongの内容を適用
    const int SWP_SHOWWINDOW = 0x0040; // ウィンドウを表示

    public static void windowReplace(string name, int x, int y, int width, int height, bool hideTitleBar)
    {
        var window = FindWindow(null, name);

        if (hideTitleBar)
        {
            int style = GetWindowLong(window, GWL_STYLE);
            SetWindowLong(window, GWL_STYLE, (style & ~WS_CAPTION));
        }
        else
        {
            int style = GetWindowLong(window, GWL_STYLE);
            style |= WS_CAPTION;
            // style |= WS_SYSMENU;
            // SetWindowLong(window, GWL_STYLE, style ^ WS_SYSMENU);
            SetWindowLong(window, GWL_STYLE, style);
        }

        // SetWindowPos(window, 0, x, y, width, height,SWP_NOSIZE | SWP_NOMOVE | SWP_FRAMECHANGED | SWP_SHOWWINDOW);
        SetWindowPos(window, 0, x, y, width, height, width * height == 0 ? 1 : 0);
    }
}