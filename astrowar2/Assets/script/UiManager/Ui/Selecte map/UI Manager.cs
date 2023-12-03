using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public static UIManager instance;

    public GameObject MapSelectionPanel;
    public GameObject[] levelSelectionPanels;
    private void Awake()
    {
        if(instance == null) 
        {  
            instance = this; 
        }
        else
        {
            if(instance != this) 
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Mapbuttonispressed(int _mapIndex)
    {
            levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
            MapSelectionPanel.gameObject.SetActive(false);
    }

    public void GoToMap1()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToMap3()
    {
        SceneManager.LoadScene(4);
    }
}
