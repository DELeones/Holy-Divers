using UnityEngine;

public class KillZone : MonoBehaviour
{
    public Transform spawnPoint; // Arrastra aqu� tu punto de spawn

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entr� es el jugador
        if (other.CompareTag("Player"))
        {
            // Teletransporta al jugador al punto de spawn
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        if (spawnPoint == null)
        {
            Debug.LogError("�No hay punto de spawn asignado!");
            return;
        }

        // Teletransporta al jugador al punto de spawn
        player.transform.position = spawnPoint.position;

        // Si el jugador tiene un CharacterController, desact�valo brevemente
        // para evitar problemas de colisi�n durante el teletransporte
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            controller.enabled = true;
        }

        // Si el jugador tiene un Rigidbody, resetea su velocidad
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}