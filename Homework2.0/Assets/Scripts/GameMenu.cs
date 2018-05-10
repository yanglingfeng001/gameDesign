using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {

    public void OnStartGame() {
        Application.LoadLevel("MainScenes");
    }

    public void OnExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
