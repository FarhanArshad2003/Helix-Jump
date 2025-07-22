using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Nxt_Lvl()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        // Check if we are running in the Unity Editor
#if UNITY_EDITOR
        // If we are in the Editor, stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // If we are running as a standalone build, quit the application
            Application.Quit();
#endif
    }
}
