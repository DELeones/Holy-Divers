using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para trabajar con el componente Slider

public class SimplePauseMenu : MonoBehaviour
{
    // Paneles de menú - Asigna estos en el Inspector
    public GameObject pauseMenuPanel;
    public GameObject optionsMenuPanel;

    // Referencia al Slider de volumen - ASIGNA ESTO EN EL INSPECTOR
    public Slider volumeSlider;

    // Configuración de la tecla de pausa
    public KeyCode pauseKey = KeyCode.P; // La tecla que activará/desactivará la pausa

    // Variable para rastrear si el juego está pausado
    private bool isPaused = false;

    void Start()
    {
        // Asegúrate de que ambos menús estén ocultos al inicio del juego
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        if (optionsMenuPanel != null)
        {
            optionsMenuPanel.SetActive(false);
        }

        // Bloquea y oculta el cursor al inicio del juego
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // --- NUEVO CÓDIGO PARA EL SLIDER ---
        // Cargar el volumen guardado o establecer el máximo si no hay ninguno
        if (volumeSlider != null)
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                float savedVolume = PlayerPrefs.GetFloat("MasterVolume");
                AudioListener.volume = savedVolume;
                volumeSlider.value = savedVolume; // Actualiza el valor del slider
                Debug.Log("Volumen cargado: " + savedVolume);
            }
            else
            {
                // Si no hay volumen guardado, establece el máximo (1f)
                AudioListener.volume = 1f;
                volumeSlider.value = 1f; // Actualiza el valor del slider
                PlayerPrefs.SetFloat("MasterVolume", 1f); // Guarda este valor por defecto
                PlayerPrefs.Save();
                Debug.Log("Volumen por defecto (máximo) establecido.");
            }
        }
        else
        {
            Debug.LogWarning("¡Slider de volumen no asignado en el Inspector!");
            // Asegurarse de que el volumen del juego esté al menos a 1f si el slider no está asignado
            AudioListener.volume = 1f;
        }
        // --- FIN DEL NUEVO CÓDIGO ---
    }

    void Update()
    {
        // Detecta cuando se presiona la tecla de pausa configurada
        if (Input.GetKeyDown(pauseKey))
        {
            Debug.Log("Tecla '" + pauseKey.ToString() + "' presionada");
            TogglePauseMenu(); // Llama al método para alternar la pausa
        }
    }

    // Método principal para alternar entre el estado de pausa y juego
    void TogglePauseMenu()
    {
        // Si no hay un panel de pausa asignado, no podemos hacer nada
        if (pauseMenuPanel == null)
        {
            Debug.LogError("¡Panel de menú de pausa no asignado en el Inspector!");
            return;
        }

        // Cambia el estado de la variable isPaused
        isPaused = !isPaused;

        // Activa/desactiva el panel de pausa
        pauseMenuPanel.SetActive(isPaused);

        // Asegúrate de que el panel de opciones siempre se cierre al alternar la pausa
        if (optionsMenuPanel != null)
        {
            optionsMenuPanel.SetActive(false);
        }

        if (isPaused) // Si el juego se acaba de pausar
        {
            Time.timeScale = 0f; // Pausa el tiempo del juego (movimiento, físicas, etc.)
            Cursor.visible = true; // Muestra el cursor
            Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor para interactuar con la UI
            Debug.Log("Juego pausado. Cursor visible y desbloqueado.");
        }
        else // Si el juego se acaba de reanudar
        {
            Time.timeScale = 1f; // Reanuda el tiempo del juego a la velocidad normal
            Cursor.visible = false; // Oculta el cursor
            Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor para el control de la cámara
            Debug.Log("Juego reanudado. Cursor oculto y bloqueado.");
        }
    }

    // --- Métodos para los botones de la UI ---

    // Llamado por el botón "Continue"
    public void ContinueGame()
    {
        // Reanuda el juego
        TogglePauseMenu(); // Llama a TogglePauseMenu para reanudar
        Debug.Log("Juego continuado desde botón 'Continue'.");
    }

    // Llamado por el botón "Options" en el menú de pausa
    public void OpenOptions()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false); // Oculta el menú de pausa
        if (optionsMenuPanel != null) optionsMenuPanel.SetActive(true); // Muestra el menú de opciones
        Debug.Log("Abriendo menú de opciones.");
    }

    // Llamado por el botón "Back" en el menú de opciones
    public void BackToPauseMenu()
    {
        if (optionsMenuPanel != null) optionsMenuPanel.SetActive(false); // Oculta el menú de opciones
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true); // Muestra el menú de pausa
        Debug.Log("Volviendo al menú de pausa desde opciones.");
    }

    // Llamado por el slider de volumen
    public void SetVolume(float volume)
    {
        // AudioListener.volume controla el volumen maestro de todos los sonidos
        AudioListener.volume = volume;
        Debug.Log("Volumen ajustado a: " + volume);

        // Guarda el valor del volumen para que se recuerde la próxima vez
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save(); // Guarda los PlayerPrefs inmediatamente
    }

    // Llamado por el botón "Quit"
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        // Esto funciona tanto en el editor como en un build del juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el modo Play en el editor
#else
        Application.Quit(); // Cierra la aplicación en un build
#endif
    }

    // Métodos adicionales (puedes añadir más si los necesitas)
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
