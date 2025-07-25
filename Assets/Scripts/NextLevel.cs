using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    int currentLevel;
    int nextLevel;

    private void Start()
    {
        int.TryParse(SceneManager.GetActiveScene().name, out currentLevel);

        nextLevel = currentLevel + 1;
    }

    public void OnNextLevel()
    {
        SceneManager.LoadScene(nextLevel.ToString());
    }

    public void OnTryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
