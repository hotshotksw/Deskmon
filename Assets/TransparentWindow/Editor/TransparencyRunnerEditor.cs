using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TransparencyRunner))]
public class TransparencyRunnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get a reference to the target script
        TransparencyRunner runner = (TransparencyRunner)target;

        // Draw the default Inspector for the script
        EditorGUILayout.LabelField("Transparency Settings", EditorStyles.boldLabel);
        //fields that are always enabled
        runner.mainCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Main Camera", "Select the main camera to be used."), runner.mainCamera, typeof(Camera), true);
        runner.borderless = EditorGUILayout.Toggle(new GUIContent("Borderless", "Enable borderless window."), runner.borderless);
        runner.pinToTop = EditorGUILayout.Toggle(new GUIContent("Pin to Top", "Pin the window to the top of the screen."), runner.pinToTop);
        runner.transparencyColorSelected = (TransparencyRunner.transparencyColors)EditorGUILayout.EnumPopup(new GUIContent("Transparency Color", "Select the color to be used as the transparency"
            + "key."), runner.transparencyColorSelected);

        // Draw the allowBuildSettingChanges field
        runner.allowBuildSettingChanges = EditorGUILayout.Toggle(new GUIContent("Allow Build Setting Changes", "Enable or disable build setting changes."), runner.allowBuildSettingChanges);

        // Disable other fields if allowBuildSettingChanges is false
        EditorGUI.BeginDisabledGroup(!runner.allowBuildSettingChanges);
        runner.runInBackground = EditorGUILayout.Toggle(new GUIContent("Run In Background", "Allow the program to run in the background."), runner.runInBackground);
        runner.fullscreen = EditorGUILayout.Toggle(new GUIContent("Fullscreen", "Enable fullscreen mode."), runner.fullscreen);
        EditorGUI.BeginDisabledGroup(runner.fullscreen);
        runner.allowWindowResize = EditorGUILayout.Toggle(new GUIContent("Allow Window Resize", "Allow resizing of the window."), runner.allowWindowResize);
        EditorGUI.BeginDisabledGroup(!runner.allowWindowResize);
        runner.windowWidth = EditorGUILayout.IntField(new GUIContent("Window Width", "Set the width of the window."), runner.windowWidth);
        runner.windowHeight = EditorGUILayout.IntField(new GUIContent("Window Height", "Set the height of the window."), runner.windowHeight);
        EditorGUI.EndDisabledGroup();
        EditorGUI.EndDisabledGroup();
        EditorGUI.EndDisabledGroup();
        // Apply changes to the serialized object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(runner);
        }
        //apply the changes to the transparency runner instance
        if (runner != null)
        {
            //if any changes are made to the transparency runner instance, apply them to the static fields
            if(TransparencyRunner.AllowBuildSettingChanges != runner.allowBuildSettingChanges || 
                TransparencyRunner.Fullscreen != runner.fullscreen ||
                TransparencyRunner.AllowWindowResize != runner.allowWindowResize ||
                TransparencyRunner.WindowWidth != runner.windowWidth ||
                TransparencyRunner.WindowHeight != runner.windowHeight ||
                TransparencyRunner.RunInBackground != runner.runInBackground ||
                TransparencyRunner.Borderless != runner.borderless)
            {
                Debug.Log("Applying changes to TransparencyRunner instance...");
                TransparencyRunner.AllowBuildSettingChanges = runner.allowBuildSettingChanges;
                TransparencyRunner.Fullscreen = runner.fullscreen;
                TransparencyRunner.AllowWindowResize = runner.allowWindowResize;
                TransparencyRunner.WindowWidth = runner.windowWidth;
                TransparencyRunner.WindowHeight = runner.windowHeight;
                TransparencyRunner.RunInBackground = runner.runInBackground;
                TransparencyRunner.Borderless = runner.borderless;
            }
        }
    }
}