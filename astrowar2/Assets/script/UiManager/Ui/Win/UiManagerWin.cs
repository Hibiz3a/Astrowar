using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManagerWin : MonoBehaviour
{
    public static UiManagerWin instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    public void GoToStart()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
