using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}