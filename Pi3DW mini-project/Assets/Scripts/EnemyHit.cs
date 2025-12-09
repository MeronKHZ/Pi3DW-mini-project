using UnityEngine;

/// <summary>
/// Detects when the enemy collides with the player and triggers the lose screen.
/// </summary>
public class EnemyHit : MonoBehaviour

{
    public GameObject lostText; // UI element shown when the player loses
    public GameObject missionText; // UI element showing mission/objective text
    private bool hasLost = false; // Prevent multiple lose triggers

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy trigger hit by: " + other.name); 
        if (hasLost) return; // If player already lost, don't trigger again

        // Only react if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            missionText.SetActive(false); // Hide mission text
            hasLost = true; // Mark that the player has lost
            lostText.SetActive(true); // Show lose screen
            
            Time.timeScale = 0f; // Pause the game
            Debug.Log("Player caught! Lose screen shown.");
        }
    }
}
