using UnityEngine;

/// <summary>
/// Handles the exit door logic: checks if the player has the key,
/// shows the correct UI, and triggers the win condition.
/// </summary>
public class ExitDoor : MonoBehaviour
{
    public GameObject winText; // UI shown when the player wins/escapes 
    public GameObject needKeyText; // UI shown when the player collides with the exit door without having the key
    public GameObject missionText; // Mission text that should disappear when exiting

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door trigger hit by: " + other.name);
         
         // Only react if the player enters the trigger area
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player at door. Has key? " + KeyItem.hasKey);

            // Player has the key --> WIN the game
            if (KeyItem.hasKey)
            {
                missionText.SetActive(false); // Hide mission text
                winText.SetActive(true); // Show win screen
                Debug.Log("WinScreen set active: " + winText.activeSelf); 

                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                // Player does NOT have the key --> show "need key" message
                needKeyText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the "need key" message when the player walks away
        if (needKeyText != null)
            needKeyText.SetActive(false);
            Debug.Log("Player left door. NeedKeyText hidden.");
    }
}
