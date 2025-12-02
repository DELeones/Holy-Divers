using UnityEngine;
using UnityEngine.UI; 


public class SimpleTimerManager : MonoBehaviour
{
    [Header("Configuración del Temporizador")]
    public Text timerText;
    public float totalGameTime = 300f;
    private float currentTime;

    [Header("Pantalla de Derrota")]
    public GameObject defeatScreenPanel; 

    void Start()
    {
        currentTime = totalGameTime; 
        UpdateTimerUI(); 
        Time.timeScale = 1f;

        if (defeatScreenPanel != null)
        {
            defeatScreenPanel.SetActive(false);
        }
    }

    void Update()
    {

        if (Time.timeScale != 0)
        {
            currentTime -= Time.deltaTime; 
            UpdateTimerUI();

            if (currentTime <= 0)
            {
                currentTime = 0; 
                UpdateTimerUI(); 
                EndGame();
            }
        }
    }

   
    void UpdateTimerUI()
    {
        if (timerText != null)
        {

            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogWarning("¡Alerta! El 'Timer Text' no está asignado en el Inspector de SimpleTimerManager.");
        }
    }

    void EndGame()
    {
        Time.timeScale = 0f; 

        if (defeatScreenPanel != null)
        {
            defeatScreenPanel.SetActive(true); 
        }
        else
        {
            Debug.LogError("¡ERROR! El 'Defeat Screen Panel' no está asignado en el Inspector de SimpleTimerManager.");
        }
    }
}
