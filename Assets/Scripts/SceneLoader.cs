using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // El nombre de la escena a cargar (ej. "CastleInterior")

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró en el trigger es el jugador
        // Asegúrate de que tu jugador tenga el Tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha entrado en el trigger de la puerta del castillo.");
            LoadNewScene();
        }
    }

    void LoadNewScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Cargando escena: " + sceneToLoad);
            // Asegúrate de que el tiempo esté en 1f antes de cargar la escena
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("¡No hay nombre de escena asignado en el SceneLoader!");
        }
    }
}