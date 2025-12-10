using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject winText;
    public GameObject needKeyText;
    public GameObject missionText;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door trigger hit by: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player at door. Has key? " + KeyItem.hasKey);

            if (KeyItem.hasKey)
            {
                missionText.SetActive(false);
                winText.SetActive(true);
                Debug.Log("WinScreen set active: " + winText.activeSelf); 

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
