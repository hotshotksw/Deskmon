using UnityEditor;
using UnityEngine;

public class ForceRecompile : EditorWindow
{
    [MenuItem("Tools/Force Recompile")]
    public static void Recompile()
    {
        // Save all assets to ensure changes are recognized
        AssetDatabase.SaveAssets();

        // Force script compilation
        AssetDatabase.Refresh();
        Debug.Log("Scripts recompiled successfully!");
    }
}
