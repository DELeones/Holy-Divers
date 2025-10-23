using StarterAssets;

using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if it's the player
        if (other.CompareTag("Player"))
        {
            // Get the player controller
            ThirdPersonController playerController = other.GetComponent<ThirdPersonController>();

            // If using the starter assets, you might need to respawn the player
            // This depends on your game's respawn system
            if (playerController != null)
            {
                // Option 1: Reset player to a spawn point
                Transform spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
                other.transform.position = spawnPoint.position;

                // Option 2: Or simply kill the player
                // Destroy(other.gameObject);
            }
        }
    }
}
