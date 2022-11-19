using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu instance; //scene 넘어가도 그대로

    public GameObject go;
    //public AudioManager theAudio; // 나중에 소리 추가!

    public OrderManager theOrder;

    private bool activated;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Exit()
    {
        Application.Quit(); // 앱 종료
        System.Diagnostics.Process.GetCurrentProcess().Kill(); //유니티 종료
    }

    public void restart()
    {
        SceneManager.LoadScene("title");
    }

    public void Continue()
    {
        activated = false;
        go.SetActive(false);
        theOrder.Move();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;
            if (activated)
            {
                theOrder.NotMove();
                go.SetActive(true);
            }
            else
            {
                theOrder.Move();
                go.SetActive(false);
            }
        }
    }
}
