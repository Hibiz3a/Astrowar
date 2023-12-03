using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public LineHelp player;
    public BotShoot ennemie;
    public bullet bullet;
    public move move;
    public playerControle controle;
    public bool IsColliding = false;

    public GameObject pauseMenuUI;
    private GameObject HUD;
    [SerializeField] public Vector2 wind;
    public float Timer = 1f;

    private void Start()
    {
        HUD = GameObject.Find("HUD");
        RandomizeWind();
        RandomStart();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }

        Turn();

    }
    void Paused()
    {
        BotShoot.instance.enabled = false;
        move.instance.enabled = false;
        playerControle.instance.enabled = false;
        LineHelp.instance.enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        BotShoot.instance.enabled = true;
        move.instance.enabled = true;
        playerControle.instance.enabled = true;
        LineHelp.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void start()
    {
        SceneManager.LoadScene(0);
    }

    private void RandomizeWind()
    {
        float r = UnityEngine.Random.Range(-7, 7);
        wind = new Vector2(r, 0);
        if (wind.x > 0)
        {
            HUD.transform.Find("WindArrow").transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            HUD.transform.Find("WindArrow").transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        HUD.transform.Find("WindText").GetComponent<TextMeshProUGUI>().text = Mathf.Abs(wind.x).ToString() + " m/s";
    }

    public void EndTurnPlayer()
    {
        player.fire = false;
        player.haveShoot = false;

        playerControle.instance.enabled = false;
        move.instance.enabled = false;
    }

    public void StartEnnemiTurn()
    {
        bullet.Collision = false;
        ennemie.fire = true;
        RandomizeWind();
    }
    
    public void EndEnnemiTurn()
    {
        ennemie.fire = false;
        ennemie.haveShoot = false;
    }

    public void StartPlayerTurn()
    {
        bullet.Collision = false;
        player.fire = true;
        RandomizeWind();
        playerControle.instance.enabled = true;
        move.instance.enabled = true;
    }
    IEnumerator StartFunction(Action nextfunction, int delay)
    {
        yield  return new WaitForSeconds(delay);
        nextfunction();
    }


    public void PlayerTurn()
    {
        if(!bullet.Collision && !ennemie.fire)
        {
            if (!player.haveShoot)
            {
                player.TrajectoryDraw();
            }

            if(player.haveShoot && IsColliding)
            {
                IsColliding = false;
                EndTurnPlayer();
                StartCoroutine(StartFunction(StartEnnemiTurn, 3));
            }
        }
    }

    public void EnnemiTurn()
    {
        if (!bullet.Collision && !player.fire)
        {
            if (!ennemie.haveShoot)
            {
                ennemie.LaunchProjectile();
                ennemie.haveShoot=true;
            }

            if (ennemie.haveShoot && IsColliding)
            {
                IsColliding = false;
                EndEnnemiTurn();
                StartCoroutine(StartFunction(StartPlayerTurn, 3));
            }
        }
    }

    public void RandomStart()
    {
        int WhoStart = UnityEngine.Random.Range(1, 2);
        if(WhoStart == 1 )
        {
            StartPlayerTurn();
        }
        if(WhoStart == 2 )
        {
            StartEnnemiTurn();
        }
    }

    public void Turn()
    {
        if (player.fire && !ennemie.fire)
        {
            PlayerTurn();
        }
        if (ennemie.fire && !player.fire)
        {
            EnnemiTurn();
        }
    }
}
