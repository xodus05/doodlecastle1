using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{

    public void SceneChange()
    {
        SceneManager.LoadScene("quote"); //quote 稽 scene 戚疑
    }

    public void QuoteChange()
    {
        SceneManager.LoadScene("house");
    }

    public void OnClickExit()
    {
        Application.Quit(); // 詔 蟹亜奄
        System.Diagnostics.Process.GetCurrentProcess().Kill(); // omg 戚暗 刊牽檎 政艦銅 焼森 襖獄顕..せせせせせせせせせせ
    }
}
