using UnityEngine;

public class Endpoint : MonoBehaviour
{
    public GameObject levelEndUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelEndUI.SetActive(true);
            GameManager.instance.gameOver = true;
        }
    }
}
