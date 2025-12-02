using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    public Text collectibleCounterText; 

    public int totalCollectiblesInScene = 7; 
    private int currentCollectibles = 0; 

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentCollectibles = 0; 
        UpdateCollectibleUI(); 
    }

    public void CollectiblePickedUp()
    {
        currentCollectibles++;
        UpdateCollectibleUI(); 

        if (currentCollectibles >= totalCollectiblesInScene)
        {
            Debug.Log("¡Todos los coleccionables recogidos en esta escena!");
        }
    }

    void UpdateCollectibleUI()
    {
        if (collectibleCounterText != null)
        {
            collectibleCounterText.text = currentCollectibles + " / " + totalCollectiblesInScene + " Almas";
        }
        else
        {
            Debug.LogWarning("Collectible Counter Text no asignado en GameManager.");
        }
    }
}
