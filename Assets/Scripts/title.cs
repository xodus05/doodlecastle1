using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    /*    public GameObject playButton;
        public GameObject playAgainButton;
        public GameObject menuButton;*/
    public Vector2 startPosition;

    private Inventory inventory;

    private PlayerMove thePlayer;

    private Enemy theEnemy;
    private EnemyAI theEnemyAI;
    private CameraManager theCamera;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerMove>();
        theEnemy = FindObjectOfType<Enemy>();
        theEnemyAI = FindObjectOfType<EnemyAI>();
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("quote"); //quote 로 scene 이동
    }

    public void QuoteChange()
    {
            SceneManager.LoadScene("house"); //quote에서 house로 scene 이동

    }

    public void GameOverChange()
    {
        SceneManager.LoadScene("title"); // title 로 scene 이동
    }

    public void ReRoad()
    {
        SceneManager.LoadScene("map5");
        thePlayer.transform.position = new Vector2(3770, 297);
        thePlayer.currentMapName = "map5"; 
        thePlayer.startPointNumber = 3;
        theEnemy.transform.position = new Vector2(2896, 447);
        theEnemyAI.transform.position = new Vector2(2896, 447);
        // theCamera.transform.position = new Vector2(40, 370);
    }

    public void ReRoad2()
    {
        SceneManager.LoadScene("buttonroom");
        thePlayer.transform.position = new Vector2(-11048, 3455);
        //theCamera.transform.position = new Vector2(-11048, 3455);
    }

    public void ReRoad3()
    {
        SceneManager.LoadScene("hall");
        thePlayer.transform.position = new Vector2(-10749, 3428);
        //theCamera.transform.position = new Vector2(-11048, 3455);
    }

    public void OnClickExit()
    {
        Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    public void OnChapter2()
    {
        SceneManager.LoadScene("start");
        thePlayer.transform.position = new Vector2(-10728, 1832);
    }
}
