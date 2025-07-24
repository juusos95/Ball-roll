using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("menu");
    }
}
