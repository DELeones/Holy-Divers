using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [Header("Panel de Victoria")]
    public GameObject victoryScreenPanel; 

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Jugador ha entrado en la zona de victoria!");
            // Activamos el panel de victoria
            if (victoryScreenPanel != null)
            {
                victoryScreenPanel.SetActive(true);
            }
            else
            {
                Debug.LogError("¡ERROR! El 'Victory Screen Panel' no está asignado en el Inspector del VictoryTrigger.");
            }

        }
    }
}
