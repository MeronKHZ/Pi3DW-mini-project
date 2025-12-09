using UnityEngine;

public class EnemyHit : MonoBehaviour

{
    public GameObject lostText;
    public GameObject missionText;
    private bool hasLost = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy trigger hit by: " + other.name); 
        if (hasLost) return;

        if (other.CompareTag("Player"))
        {
            missionText.SetActive(false);
            hasLost = true;
            lostText.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Player caught! Lose screen shown.");
        }
    }
}
