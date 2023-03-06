using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
/*    public GameObject playButton;
    public GameObject playAgainButton;
    public GameObject menuButton;*/
    public shovelEvent shovelEvent;

    void Start()
    {
        shovelEvent = FindObjectOfType<shovelEvent>();    
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
        if(shovelEvent != null)
        {       
            SceneManager.LoadScene("map6");
        }
    }

    public void OnClickExit()
    {
        Application.Quit(); // 앱 나가기
        System.Diagnostics.Process.GetCurrentProcess().Kill(); // omg 이거 누르면 유니티 아예 꺼버림..ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ
    }
}
