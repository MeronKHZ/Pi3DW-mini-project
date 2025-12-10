using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject needKeyText;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door trigger hit by: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player at door. Has key? " + KeyItem.hasKey);

            if (KeyItem.hasKey)
            {
                winScreen.SetActive(true);
                Debug.Log("WinScreen set active: " + winScreen.activeSelf); 

                Time.timeScale = 0f;
            }
            else
            {
                needKeyText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (needKeyText != null)
            needKeyText.SetActive(false);
            Debug.Log("Player left door. NeedKeyText hidden.");
    }
}
