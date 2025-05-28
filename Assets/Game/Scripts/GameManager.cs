using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3[] toyPositions;

    public void CloseGame()
    {
        // Close the game application
        Application.Quit();

        // If running in the editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
