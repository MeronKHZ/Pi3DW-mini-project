using UnityEngine;

/// <summary>
/// Handles the key pickup. When the player touches the key,
/// it sets the key flag to true and hides the key object.
/// </summary>
public class KeyItem : MonoBehaviour
{
    public static bool hasKey = false; // Static variable shared across the game to track if the player has the key

    private void OnTriggerEnter(Collider other)
    {
        // Only the player can pick up the key
        if (other.CompareTag("Player"))
        {
            hasKey = true; // Player now has the key
            gameObject.SetActive(false); // Hide the key in the scene
            Debug.Log("Key collected!");
        }
    }
}
