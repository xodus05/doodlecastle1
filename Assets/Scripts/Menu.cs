using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu instance; //scene �Ѿ�� �״��

    public GameObject go;
    //public AudioManager theAudio; // ���߿� �Ҹ� �߰�!

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
        Application.Quit(); // �� ����
        System.Diagnostics.Process.GetCurrentProcess().Kill(); //����Ƽ ����
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
