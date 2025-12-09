using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject loseScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
