using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<Vector3> toyPositions = new List<Vector3>();
    [SerializeField] RectTransform toy;
    [SerializeField] int currentPositionIndex = 0;

    public void MoveToyPosition()
    {
        currentPositionIndex = (currentPositionIndex + 1 >= toyPositions.Count) ? 0 : currentPositionIndex + 1;
        toy.anchoredPosition = toyPositions[currentPositionIndex];
    }

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
