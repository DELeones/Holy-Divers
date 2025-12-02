using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;

    void Start()
    {
        // Mostrar cursor y liberar control
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Mostrar solo el menú principal
        mainMenuPanel.SetActive(true);
        optionsMenuPanel.SetActive(false);

        // Asegurar que el tiempo está pausado en menú
        Time.timeScale = 0f;
    }

    // --- BOTONES ---

    public void PlayGame()
    {
        Time.timeScale = 1f; // reiniciar tiempo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Carga tu escena de jugabilidad
        SceneManager.LoadScene("HubWorld");
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
