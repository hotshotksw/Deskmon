//only run if not in unity editor
using UnityEngine;
using UnityTransparentWindow;

using System.Runtime.InteropServices;
using System;

public class TransparencyRunner : MonoBehaviour
{
    //Windows DLLs for window management
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out Rect rect);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    private const int SW_MAXIMIZE = 3; // Command to maximize the window
    //Transparency Settings
    [Header("Transparency Settings")]
    [Tooltip("Select the color to be used as the transparency key.")]
    public transparencyColors transparencyColorSelected; // Default to black

    public enum transparencyColors
    {
        Black = 0x000000,
        White = 0xFFFFFF,
        Red = 0xFF0000,
        Green = 0x00FF00,
        Blue = 0x0000FF,
        Yellow = 0xFFFF00,
        Cyan = 0x00FFFF,
        Magenta = 0xFF00FF
    }
    [SerializeField]
    public Camera mainCamera; // Reference to the main camera
    [SerializeField]
    [Tooltip("Set to true to pin the window to the top of the screen.")]
    public bool pinToTop = false;
    [SerializeField]
    [Tooltip("*disables window toolbar* Set to true to make the window borderless.")]
    public bool borderless = false; // Set to true to make the window borderless
    public static bool Borderless = false; // Set to true to make the window borderless
    [SerializeField]
    [Tooltip("Allow Build Setting changes.")]
    public bool allowBuildSettingChanges = false; // Set to true to allow build settings changes
    public static bool AllowBuildSettingChanges = false; // Set to true to allow build settings changes
    [SerializeField]
    [Tooltip("Allow Program To Run In Background")]
    public bool runInBackground = true; // Set to true to allow the program to run in the background
    public static bool RunInBackground = true; // Set to true to allow the program to run in the background
    [SerializeField]
    [Tooltip("Build Fullscreen mode.")]
    public bool fullscreen = false; // Set to true to make the window borderless
    public static bool Fullscreen = false; // Set to true to make the window borderless
    [SerializeField]
    [Tooltip("Allow Window Resize.")]
    public bool allowWindowResize = false; // Set to true to allow window resizing
    public static bool AllowWindowResize = false; // Set to true to allow window resizing
    [SerializeField]
    [Tooltip("Set Window Width.")]
    public int windowWidth = 800; // Set the width of the window
    public static int WindowWidth = 800; // Set the width of the window
    [SerializeField]
    [Tooltip("Set Window Height.")]
    public int windowHeight = 600; // Set the height of the window
    public static int WindowHeight = 600; // Set the height of the window
    WindowTransparency transparentWindow; // Reference to the TransparentWindow script

    //Ensure a single instance
    private static TransparencyRunner instance = null;
    public static TransparencyRunner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TransparencyRunner>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("TransparencyRunner");
                    instance = obj.AddComponent<TransparencyRunner>();
                }
            }
            return instance;
        }
    }
    void Start()
    {
        //only run if not in unity editor
        if (Application.isEditor)
        {
            return;
        }
        if(fullscreen){
            IntPtr hwnd = GetActiveWindow();  
            Debug.Log("Fullscreen mode set to FullScreenWindow.");
            // Maximize the window
            ShowWindow(hwnd, SW_MAXIMIZE);
    
            // Debug.Log("Fullscreen mode set to FullScreenWindow.");
            // //Get screen dimension from Windows API
            // IntPtr hwnd = GetActiveWindow();
            // Rect rect;
            // GetWindowRect(hwnd, out rect);

            // int screenWidth = (int)rect.width;
            // int screenHeight = (int)rect.height;
            // // int screenWidth = Screen.currentResolution.width;
            // // int screenHeight = Screen.currentResolution.height;
            // Screen.SetResolution(screenWidth, screenHeight, false);
        }
        Color transparencyColor = colorFromEnum(); // Get the selected transparency color
        // Set the camera background color to match the transparency color
        transparencyColor.a = 0; // Set alpha to 0 for transparency
        mainCamera.backgroundColor = transparencyColor;
        mainCamera.clearFlags = CameraClearFlags.SolidColor;
        //create a new TransparentWindow instance
        transparentWindow = new WindowTransparency(pinToTop, borderless);
        transparentWindow.SetWindowAttributes(ColorToUint(transparencyColor), 0); // Set the transparency color
        //if set to fullscreen, set the window to fullscreen

    }

    void Update()
    {
        //only run if not in unity editor
        if (Application.isEditor)
        {
            return;
        }
        // if(Application.isFocused)
        // {
            transparentWindow.UpdateWindowTransparency();
        // }
    }

        private uint ColorToUint(Color color)
    {
        byte r = (byte)(color.r * 255);
        byte g = (byte)(color.g * 255);
        byte b = (byte)(color.b * 255);

        return (uint)((b << 16) | (g << 8) | r);
    }

    private Color colorFromEnum(){
        switch (transparencyColorSelected)
        {
            case transparencyColors.Black:
                return Color.black;
            case transparencyColors.White:
                return Color.white;
            case transparencyColors.Red:
                return Color.red;
            case transparencyColors.Green:
                return Color.green;
            case transparencyColors.Blue:
                return Color.blue;
            case transparencyColors.Yellow:
                return Color.yellow;
            case transparencyColors.Cyan:
                return Color.cyan;
            case transparencyColors.Magenta:
                return Color.magenta;
            default:
                return Color.clear; // Default to clear if no match found
        }
    }
}