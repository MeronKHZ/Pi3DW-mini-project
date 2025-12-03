using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public static bool hasKey = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasKey = true;
            gameObject.SetActive(false);
            Debug.Log("Key collected!");
        }
    }
}
