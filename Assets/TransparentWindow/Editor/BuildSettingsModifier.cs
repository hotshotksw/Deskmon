// filepath: c:\Users\johnb\UnityProjects\EasyTransparentWindow\Assets\TransparentWindow\Editor\BuildSettingsModifier.cs
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildSettingsModifier : IPreprocessBuildWithReport
{
    public int callbackOrder {get {return 0;}}

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("Modifying build settings for transparency...");
        Debug.Log("AllowBuildSettingChanges: " + TransparencyRunner.AllowBuildSettingChanges);
        Debug.Log("Fullscreen: " + TransparencyRunner.Fullscreen);
        Debug.Log("AllowWindowResize: " + TransparencyRunner.AllowWindowResize);
        Debug.Log("WindowWidth: " + TransparencyRunner.WindowWidth);
        Debug.Log("WindowHeight: " + TransparencyRunner.WindowHeight);

        if (TransparencyRunner.AllowBuildSettingChanges)
        {
            if (TransparencyRunner.Fullscreen)
            {
                PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
                //use the default screen width and height for the build
                PlayerSettings.defaultScreenHeight = Screen.currentResolution.height;
                PlayerSettings.defaultScreenWidth = Screen.currentResolution.width;                
                Debug.Log("Fullscreen mode set to FullScreenWindow.");
            }
            else
            {
                PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
                PlayerSettings.defaultScreenHeight = TransparencyRunner.WindowHeight;
                PlayerSettings.defaultScreenWidth = TransparencyRunner.WindowWidth;
                Debug.Log("Fullscreen mode set to Windowed.");
            }
            if (TransparencyRunner.AllowWindowResize || (TransparencyRunner.Fullscreen && 
                TransparencyRunner.Borderless))
            {
                PlayerSettings.resizableWindow = true;
                Debug.Log("Window resize enabled.");
            }
            else
            {
                PlayerSettings.resizableWindow = false;
                Debug.Log("Window resize disabled.");
            }
            if (TransparencyRunner.RunInBackground)
            {
                PlayerSettings.runInBackground = true;
                Debug.Log("Run in background enabled.");
            }
            PlayerSettings.useFlipModelSwapchain = false;
            Debug.Log("DXGI Flip Model Swapchain disabled.");
        }
        else
        {
            Debug.LogWarning("TransparencyRunner instance not found or build settings not allowed.");
        }
    }
}
