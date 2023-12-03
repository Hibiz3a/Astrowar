using UnityEngine;
using UnityEngine.SceneManagement;

public class UimanagerStart : MonoBehaviour
{
    public static UimanagerStart instance;


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


    public void GoToMap1()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void settings()
    {
        SceneManager.LoadScene(1);
    }
}
