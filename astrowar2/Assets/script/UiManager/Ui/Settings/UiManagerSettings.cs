using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManagerSettings : MonoBehaviour
{

    public static UiManagerSettings instance;

    public GameObject MapSelectionPanel;
    public GameObject[] levelSelectionPanels;
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
        SceneManager.LoadScene(0);
    }
}
