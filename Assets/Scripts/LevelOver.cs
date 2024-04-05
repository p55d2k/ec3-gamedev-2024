using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOver : MonoBehaviour
{
    public string nextLevel;

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}