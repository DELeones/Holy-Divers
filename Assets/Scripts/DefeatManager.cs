using UnityEngine;
using UnityEngine.SceneManagement; 

public class DefeatMenuManager : MonoBehaviour
{
    void OnEnable() 
    {
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f; 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si el juego está compilado (una versión final), esto cierra la aplicación
        Application.Quit();
#endif
    }
}
